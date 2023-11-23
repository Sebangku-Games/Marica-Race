using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentRound;

    public bool isGameRunning = false;
    public bool isBoosting = false;
    public bool isPenalty = false;

    [SerializeField] private Player player;
    private Boost boost;
    private Penalty penalty;
    private ScrollbarPointer scrollbarPointer;
    private RoundManager roundManager;
    private UIManager uIManager;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
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
    }

    private void RoundOver(){
        Debug.Log("Round END");
        uIManager.ShowRoundOverPanel();
        player.ResetDistance();

        Time.timeScale = 0f;

        isGameRunning = false;
    }

    public void StartNextRound(){
        roundManager.AddRound();

        Time.timeScale = 1f;
        
        uIManager.HideRoundOverPanel();

        isGameRunning = true;
    }
}
