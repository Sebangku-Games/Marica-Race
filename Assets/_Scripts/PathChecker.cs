using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathChecker : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform enemy1;
    [SerializeField] private Transform enemy2;
    private void Start()
    {
        
    }

    
    public void UpdatePathCheckerPlayer(float distance){
        float distanceToMove = distance/GameManager.instance.roundManager.distanceToFinish * 2f;
        StartCoroutine(LerpToAddPositionPlayer(distanceToMove, 0.5f));
    }

    private IEnumerator LerpToAddPositionPlayer(float AddedPosition, float duration)
    {
        float time = 0f;
        Vector3 initialPosition = player.transform.localPosition;
        Vector3 targetPosition = player.transform.localPosition + new Vector3(AddedPosition, 0, 0);

        if (targetPosition.x > 1){
            yield return null;
        }

        while (time < duration)
        {
            player.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        player.transform.localPosition = targetPosition;
    }

    public void UpdatePathCheckerEnemy1(float distance){
        float distanceToMove = distance/GameManager.instance.roundManager.distanceToFinish * 2f;
        StartCoroutine(LerpToAddPositionEnemy1(distanceToMove, 0.5f));
    }

    private IEnumerator LerpToAddPositionEnemy1(float AddedPosition, float duration)
    {
        float time = 0f;
        Vector3 initialPosition = enemy1.transform.localPosition;
        Vector3 targetPosition = enemy1.transform.localPosition + new Vector3(AddedPosition, 0, 0);

        if (targetPosition.x > 1){
            yield return null;
        }

        while (time < duration)
        {
            enemy1.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        enemy1.transform.localPosition = targetPosition;
    }

    public void UpdatePathCheckerEnemy2(float distance){
        float distanceToMove = distance/GameManager.instance.roundManager.distanceToFinish * 2f;
        StartCoroutine(LerpToAddPositionEnemy2(distanceToMove, 0.5f));
    }

    private IEnumerator LerpToAddPositionEnemy2(float AddedPosition, float duration)
    {
        float time = 0f;
        Vector3 initialPosition = enemy2.transform.localPosition;
        Vector3 targetPosition = enemy2.transform.localPosition + new Vector3(AddedPosition, 0, 0);

        if (targetPosition.x > 1){
            yield return null;
        }

        while (time < duration)
        {
            enemy2.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        enemy2.transform.localPosition = targetPosition;
    }
}
