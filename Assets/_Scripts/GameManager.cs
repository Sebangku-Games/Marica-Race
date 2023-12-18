using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isPlayerMoving = false;
    public bool isGameRunning = false;
    public bool isBoosting = false;
    public bool isPenalty = false;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject finishLinePrefab;
    private List<GameObject> finishLines = new List<GameObject>();
    [SerializeField] private List<GameObject> enemyAI;

    public Player player;
    private Boost boost;
    private Penalty penalty;
    private ScrollbarPointer scrollbarPointer;
    public RoundManager roundManager;
    private UIManager uIManager;
    private PathChecker pathChecker;

    private Leaderboards leaderboards;
    private Achievements achievements;

    public RoundData[] roundDatas;
    public RoundData currentRoundData;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        // player = FindObjectOfType<Player>();
        scrollbarPointer = FindObjectOfType<ScrollbarPointer>();
        boost = GetComponent<Boost>();
        penalty = GetComponent<Penalty>();
        roundManager = GetComponent<RoundManager>();
        uIManager = GetComponent<UIManager>();
        pathChecker = FindObjectOfType<PathChecker>();

        leaderboards = FindObjectOfType<Leaderboards>();
        achievements = FindObjectOfType<Achievements>();

        
        UpdateCurrentRoundData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        if (isGameRunning)
        {
            return;
        }
        SpawnEnemies();
        SpawnFinishLine();

        isGameRunning = true;
    }

    public void DoSomethingInGreenArea(){
        penalty.ResetAmountRedClickInARow();
        scrollbarPointer.ResetScrollbarPointerSpeed();

        boost.UpdateBoost();

        player.PlayMoveAnimation();
        AudioManager.instance.PlayGreenSfx();

        if (isBoosting)
        {
            // Debug.Log("Boost");
            player.MoveToRightScreen(2f);
            pathChecker.UpdatePathCheckerPlayer(2f);
            player.PlayBoostParticle();
            AudioManager.instance.PlayBoostSfx();

            achievements.UnlockAchievement(7);
        }
        else
        {
            player.MoveToRightScreen(1f);
            pathChecker.UpdatePathCheckerPlayer(1f);
        }
    }

    public void DoSomethingInYellowArea(){
        boost.ResetAmountGreenClickInARow();
        penalty.ResetAmountRedClickInARow();
        scrollbarPointer.ResetScrollbarPointerSpeed();

        
        player.PlayMoveAnimation();
        AudioManager.instance.PlayYellowSfx();

        player.MoveToRightScreen(0.5f);
        pathChecker.UpdatePathCheckerPlayer(0.5f);
    }

    public void DoSomethingInRedArea(){
        boost.ResetAmountGreenClickInARow();
        
        penalty.UpdatePenalty();

        AudioManager.instance.PlayRedSfx();

        if (isPenalty)
        {
            // Debug.Log("Penalty");
            scrollbarPointer.IncreaseScrollbarPointerSpeed();

            achievements.UnlockAchievement(6);
        }
        else
        {
            scrollbarPointer.ResetScrollbarPointerSpeed();
        }
    }

    public void CheckRoundOver(){
        if (player.GetTotalDistance() >= roundManager.distanceToFinish)
        {
            // Round Over
            RoundOver();
        }

        foreach (GameObject enemyAI in enemyAI)
        {
            // get component player
            Player enemy = enemyAI.GetComponent<Player>();
            if (enemy.GetTotalDistance() >= roundManager.distanceToFinish)
            {
                // Game Over
                GameOver();
            }
        }
    }

    private void GameOver(){
        if (!isGameRunning)
        {
            return;
        }
        Debug.Log("Game END");
        AudioManager.instance.PlayGameOverSfx();
        uIManager.ShowGameOverPanel();
        SetLeaderboardScore();

        //Time.timeScale = 0f;

        isGameRunning = false;
    }

    private void RoundOver(){
        Debug.Log("Round END");
        AudioManager.instance.PlayRoundOverSfx();
        uIManager.ShowRoundOverPanel();
        player.ResetDistance();
        DestroyEnemies();

        if (roundManager.currentRound == 1){
            achievements.UnlockAchievement(1);
        } else if (roundManager.currentRound == 5){
            achievements.UnlockAchievement(2);
        } else if (roundManager.currentRound == 10){
            achievements.UnlockAchievement(3);
        } else if (roundManager.currentRound == 15){
            achievements.UnlockAchievement(4);
        } else if (roundManager.currentRound == 20){
            achievements.UnlockAchievement(5);
        }

        StopAllCoroutines();

        //Time.timeScale = 0f;

        isGameRunning = false;
        
    }

    public void StartNextRound(){
        roundManager.AddRound();
        UpdateCurrentRoundData();
        pathChecker.ResetPosition();
        uIManager.StartCountdownCoroutine();

        Time.timeScale = 1f;
        
        uIManager.HideRoundOverPanel();
    }

    public void RestartGame(){
        // Reload Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 1f;
    }

    public void BackToMainMenu(){
        // Load Main Menu Scene
        SceneManager.LoadScene(0);

        Time.timeScale = 1f;
    }

    private void UpdateCurrentRoundData(){
        if (roundManager.currentRound < roundDatas.Length)
        {
            currentRoundData = roundDatas[roundManager.currentRound - 1];
        } else {
            currentRoundData = roundDatas[roundDatas.Length - 1];
        }

        UpdateAllRoundData();
    }

    private void SpawnEnemies(){
        // spawn enemies based on Player position + give y.offset 2 & -2
        Vector2 enemy1SpawnPosition = new Vector2(player.transform.position.x, player.transform.position.y + 1.5f);
        Vector2 enemy2SpawnPosition = new Vector2(player.transform.position.x, player.transform.position.y - 1.5f);

        GameObject enemy1 = Instantiate(enemyPrefab, enemy1SpawnPosition, Quaternion.identity);
        enemy1.GetComponent<EnemyAI>().enemyIndex = 1;  // Set the enemyIndex for enemy1
        enemyAI.Add(enemy1);

        GameObject enemy2 = Instantiate(enemyPrefab, enemy2SpawnPosition, Quaternion.identity);
        enemy2.GetComponent<EnemyAI>().enemyIndex = 2;  // Set the enemyIndex for enemy2
        enemyAI.Add(enemy2);

        StartCoroutine(MoveEnemy(enemy1));
        StartCoroutine(MoveEnemy(enemy2));

        //
    }

    IEnumerator MoveEnemy(GameObject enemyAI){
        EnemyAI enemyAIs = enemyAI.GetComponent<EnemyAI>();
        // call Player.MoveToRight every interval seconds
        while (true)
        {
            float selectedDistance = enemyAIs.SelectDistance();

            enemyAIs.MoveToRightScreen(selectedDistance);

            if (selectedDistance != 0)
            {
                enemyAIs.TriggerMoveAnimation();
                AudioManager.instance.PlayGreenSfx();
            }

            // Call the UpdatePathChecker method with the enemy index and distance
            pathChecker.UpdatePathChecker(enemyAIs.enemyIndex, selectedDistance);
            
            // Debug.Log("Enemy move : " + selectedDistance);
            yield return new WaitForSeconds(currentRoundData.enemyMoveInterval);

            // Debug.Log("Enemy is : " + IsEnemyBehindPlayer(enemyAI));
        }
    }


    private void DestroyEnemies(){
        foreach (GameObject enemyAI in enemyAI)
        {
            Destroy(enemyAI);
        }

        enemyAI.Clear();

        // destroy first finish line
        if (finishLines.Count > 1)
        {
            Destroy(finishLines[0]);
            finishLines.RemoveAt(0);
        }
    }

    private void UpdateAllRoundData(){
        // reset boost/penalty flag
        isBoosting = false;
        isPenalty = false;
        boost.amountGreenClickInARow = 0;
        penalty.amountRedClickInARow = 0;

        // boost & penalty
        boost.amountToGetBoost = currentRoundData.amountToGetBoost;
        penalty.amountToGetPenalty = currentRoundData.amountToGetPenalty;

        // scrollbar
        scrollbarPointer.duration = currentRoundData.scrollbarPointerSpeed;
    }

    private void SpawnFinishLine(){
        // spawn finish line based on Player position + distanceToFinish
        Vector2 finishLineSpawnPosition = new Vector2(player.transform.position.x + roundManager.distanceToFinish, player.transform.position.y - 0.4f);

        GameObject finishLine = Instantiate(finishLinePrefab, finishLineSpawnPosition, Quaternion.identity);
        finishLines.Add(finishLine);
    }

    private void SetLeaderboardScore(){
        // set leaderboard score
        leaderboards.AddScoreToLeaderboard(roundManager.currentRound);
    }

    private void DestroyGameObject(GameObject gameObject){
        Destroy(gameObject);
    }
}
