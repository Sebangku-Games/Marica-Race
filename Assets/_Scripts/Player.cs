using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float durationLerp = 1f;
    

    public void MoveToRightScreen(float distance)
    {
        Vector3 targetPosition = transform.position + transform.right * distance;
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
            yield return null;
        }

        // Ensure that the final position is exactly the target position
        transform.position = targetPosition;
    }
}
