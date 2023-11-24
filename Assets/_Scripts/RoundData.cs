using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Round", menuName = "Round")]
public class RoundData : ScriptableObject
{
    [Header("Enemy AI")]
    public float enemyMoveInterval;
    public float chanceToMove0;
    public float chanceToMoveHalf;
    public float chanceToMove1;

    [Header("Scrollbar")]
    public float scrollbarPointerSpeed;

    [Header("Boost")]
    public int amountToGetBoost;

    [Header("Penalty")]
    public int amountToGetPenalty;

}
