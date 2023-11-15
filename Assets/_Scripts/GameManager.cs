using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Player player;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoSomethingInGreenArea(){
        player.MoveToRightScreen(1f);
    }

    public void DoSomethingInYellowArea(){
        player.MoveToRightScreen(0.5f);
    }

    public void DoSomethingInRedArea(){
        //player.MoveToRightScreen();
    }
}
