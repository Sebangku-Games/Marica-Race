using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float durationLerp = 1f;
    [SerializeField] private float totalDistance;
    

    public void MoveToRightScreen(float distance)
    {
        Vector3 targetPosition = transform.position + transform.right * distance;
        totalDistance += distance;

        StartCoroutine(LerpToPosition(targetPosition, durationLerp));
    }

    private IEnumerator LerpToPosition(Vector3 targetPosition, float duration)
    {
        float time = 0f;
        Vector3 initialPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Ensure that the final position is exactly the target position
        transform.position = targetPosition;

        yield return new WaitForSeconds(duration);
        GameManager.instance.CheckRoundOver();
    }



    public float GetTotalDistance()
    {
        return totalDistance;
    }

    public void ResetDistance()
    {
        totalDistance = 0f;
    }

    internal Vector3 GetPosition()
    {
        return transform.position;
    }
}
