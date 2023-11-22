using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private int currentRound = 1;
    public float initialDistanceToFinish = 8f;
    public float distanceToFinish;

    private void Start()
    {
        SetDistanceToFinish();
    }

    private void SetDistanceToFinish(){ // Add distance +2 for each round increase
        distanceToFinish = initialDistanceToFinish + (currentRound * 2);
    }

    public void AddRound(){
        currentRound++;
        SetDistanceToFinish();
    }
}
