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
        ResetPosition();
    }

    public void ResetPosition(){
        Debug.Log("RESET POSITION CHECKER");
        // only set x position to 0
        player.transform.localPosition = new Vector3(-1.01f, player.transform.localPosition.y, player.transform.localPosition.z);
        enemy1.transform.localPosition = new Vector3(-1.03f, enemy1.transform.localPosition.y, enemy1.transform.localPosition.z);
        enemy2.transform.localPosition = new Vector3(-1.02f, enemy2.transform.localPosition.y, enemy2.transform.localPosition.z);
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
        }

        player.transform.localPosition = targetPosition;

        yield return new WaitForSeconds(duration);
    }

    public void UpdatePathChecker(int enemyIndex, float distance)
    {
        float distanceToMove = distance;

        if (enemyIndex == 1)
        {
            UpdatePathCheckerEnemy1(distanceToMove);
        }
        else if (enemyIndex == 2)
        {
            UpdatePathCheckerEnemy2(distanceToMove);
        }
        // Add more conditions if you have additional enemies

        // You can also handle errors or log a message if the enemy index is not recognized
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
        }

        enemy2.transform.localPosition = targetPosition;
    }
}
