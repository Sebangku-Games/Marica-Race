using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float durationLerp = 1f;
    [SerializeField] private float totalDistance;

    private Animator animator;

    private void Awake()
    {
        // get animator component on children
        animator = GetComponentInChildren<Animator>();
    }
    

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
        yield return new WaitForEndOfFrame();
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

    public void PlayBoostParticle(){
        // get all particle system in child and play it all together
        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }
    }

    public void PlayMoveAnimation(){
        animator.SetTrigger("PlayerMove");
    }

    public void SetPenaltyAnimation(bool isPenalty){
        animator.SetBool("PlayerPenalty", isPenalty);
    }
}
