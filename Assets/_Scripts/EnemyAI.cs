using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Player
{
    public int enemyIndex;
    [SerializeField] private float interval = 1f;

    [SerializeField] private float chanceToMove0 = 0.4f;
    [SerializeField] private float chanceToMoveHalf = 0.45f;

    private float distanceToMove;

    private Animator animator;

    private void Awake()
    {
        // get animator component on children
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        

        interval = GameManager.instance.currentRoundData.enemyMoveInterval;
        chanceToMove0 = GameManager.instance.currentRoundData.chanceToMove0;
        chanceToMoveHalf = GameManager.instance.currentRoundData.chanceToMoveHalf;
    }


    public float SelectDistance(){
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        float selectedDistance;

        if (randomValue < chanceToMove0) // 40% chance
        {
            selectedDistance = 0f;
        }
        else if (randomValue < (chanceToMove0 + chanceToMoveHalf)) // 45% chance
        {
            selectedDistance = 0.5f;
        }
        else // 15% chance
        {
            selectedDistance = 1f;
        }

        return selectedDistance;
    }

    private bool IsEnemyBehindPlayer()
    {
        return transform.position.x < GameManager.instance.player.transform.position.x;
    }

    public float GetEnemyMoveDistance(){
        return distanceToMove;
    }

    public void TriggerMoveAnimation()
    {
        animator.SetTrigger("EnemyMove");
    }
}
