using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameRunning = false;
    public bool isBoosting = false;
    public bool isPenalty = false;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<GameObject> enemyAI;

    public Player player;
    private Boost boost;
    private Penalty penalty;
    private ScrollbarPointer scrollbarPointer;
    private RoundManager roundManager;
    private UIManager uIManager;

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

        isGameRunning = true;
    }

    public void DoSomethingInGreenArea(){
        penalty.ResetAmountRedClickInARow();
        scrollbarPointer.ResetScrollbarPointerSpeed();

        boost.UpdateBoost();

        if (isBoosting)
        {
            // Debug.Log("Boost");
            player.MoveToRightScreen(2f);
        }
        else
        {
            player.MoveToRightScreen(1f);
        }
    }

    public void DoSomethingInYellowArea(){
        boost.ResetAmountGreenClickInARow();
        penalty.ResetAmountRedClickInARow();
        scrollbarPointer.ResetScrollbarPointerSpeed();

        player.MoveToRightScreen(0.5f);
    }

    public void DoSomethingInRedArea(){
        boost.ResetAmountGreenClickInARow();
        
        penalty.UpdatePenalty();

        if (isPenalty)
        {
            // Debug.Log("Penalty");
            scrollbarPointer.IncreaseScrollbarPointerSpeed();
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
        Debug.Log("Game END");
        uIManager.ShowGameOverPanel();

        Time.timeScale = 0f;

        isGameRunning = false;
    }

    private void RoundOver(){
        Debug.Log("Round END");
        uIManager.ShowRoundOverPanel();
        player.ResetDistance();
        DestroyEnemies();

        StopAllCoroutines();

        Time.timeScale = 0f;

        isGameRunning = false;

    }

    public void StartNextRound(){
        roundManager.AddRound();
        UpdateCurrentRoundData();
        uIManager.StartCountdownCoroutine();

        Time.timeScale = 1f;
        
        uIManager.HideRoundOverPanel();
    }

    public void RestartGame(){
        // Reload Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

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
        Vector2 enemy1SpawnPosition = new Vector2(player.transform.position.x, player.transform.position.y + 2f);
        Vector2 enemy2SpawnPosition = new Vector2(player.transform.position.x, player.transform.position.y - 2f);

        GameObject enemy1 = Instantiate(enemyPrefab, enemy1SpawnPosition, Quaternion.identity);
        enemyAI.Add(enemy1);
        GameObject enemy2 = Instantiate(enemyPrefab, enemy2SpawnPosition, Quaternion.identity);
        enemyAI.Add(enemy2);

        //
    }

    private void DestroyEnemies(){
        foreach (GameObject enemyAI in enemyAI)
        {
            Destroy(enemyAI);
        }

        enemyAI.Clear();
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
}
