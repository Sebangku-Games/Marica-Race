using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Player
{
    [SerializeField] private float interval = 1f;

    [SerializeField] private float chanceToMove0 = 0.4f;
    [SerializeField] private float chanceToMoveHalf = 0.45f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyMove());

        interval = GameManager.instance.currentRoundData.enemyMoveInterval;
        chanceToMove0 = GameManager.instance.currentRoundData.chanceToMove0;
        chanceToMoveHalf = GameManager.instance.currentRoundData.chanceToMoveHalf;
    }

    IEnumerator EnemyMove()
    {
        // call Player.MoveToRight every interval seconds
        while (true)
        {
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

            MoveToRightScreen(selectedDistance);
            
            Debug.Log("Enemy move : " + selectedDistance);
            yield return new WaitForSeconds(interval);
        }
    }
}
