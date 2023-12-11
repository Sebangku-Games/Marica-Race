using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;

    [SerializeField] private Transform background1;
    [SerializeField] private Transform background2;
    [SerializeField] private Player player;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = background1.Find("EndPosition").position;
        SpawnBackgroundPart2();
        SpawnBackgroundPart1();
        SpawnBackgroundPart2();
        SpawnBackgroundPart1();
        SpawnBackgroundPart2();
        SpawnBackgroundPart1();
        SpawnBackgroundPart2();
        SpawnBackgroundPart1();
        SpawnBackgroundPart2();

    }

    private void Update()
    {
        if (Vector3.Distance(player.GetPosition(), lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART){
            SpawnBackgroundPart1();
            DestroyBackground();
            SpawnBackgroundPart2();
            DestroyBackground();
        }
    }

    private void SpawnBackgroundPart1(){
        Transform lastBackgroundTransform = SpawnBackground(background1, lastEndPosition);
        lastEndPosition = lastBackgroundTransform.Find("EndPosition").position;
    }

    private void SpawnBackgroundPart2(){
        Transform lastBackgroundTransform = SpawnBackground(background2, lastEndPosition);
        lastEndPosition = lastBackgroundTransform.Find("EndPosition").position;
    }

    private Transform SpawnBackground(Transform background, Vector3 spawnPosition)
    {
        Transform backgroundTransform = Instantiate(background, spawnPosition, Quaternion.identity, transform);
        return backgroundTransform;
    }

    private void DestroyBackground()
    {
        // destroy the first child gameobject of this transform
        Destroy(transform.GetChild(0).gameObject);
    }
}
