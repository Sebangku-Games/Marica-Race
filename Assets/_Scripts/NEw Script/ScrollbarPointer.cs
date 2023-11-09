using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollbarPointer : MonoBehaviour
{
    [Header("Yellow Area")]
    [SerializeField] private Transform yellowArea;
    private float[] yellowPossiblePosition = { -0.35f, -0.25f, -0.15f, -0.05f, 0.05f, 0, 0.15f, 0.25f, 0.35f };

    [Header("Green Area")]
    [SerializeField] private Transform greenArea;
    private float[] greenPossiblePosition = { -0.25f, -0.125f, 0, 0.125f, 0.25f };


    [Header("Pointer")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float radius = 0.01f;
    private bool greenAreaDetected = false;
    private bool yellowAreaDetected = false;

    [SerializeField] private float duration = 2f;
    private bool isLerping = true; // Flag to control lerping

    private float initialOffset = -1f;
    private float targetOffset = 1f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private string currentArea;

    
    private void Start()
    {
        initialPosition = new Vector3(initialOffset, transform.position.y, transform.position.z);
        targetPosition = new Vector3(targetOffset, transform.position.y, transform.position.z);
    }


    private void Update()
    {
        CheckScrollBarPointer();

        // If isLerping is true, lerp the position of the GameObject
        if (isLerping)
        {
            float t = Mathf.PingPong(Time.time / duration, 1.0f); // PingPong between 0 and 1
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
        }

        CheckPlayerInput();
    }

    public void CheckScrollBarPointer()
    {
        // Calculate the position to cast the raycast from
        Vector2 raycastPosition = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(raycastPosition, radius, layerMask);

        if (colliders != null)
        {

            foreach (Collider2D col in colliders)
            {
                if (col.gameObject.CompareTag("GreenArea"))
                {
                    //Debug.Log("Green Area");
                    GetArea("Green Area");
                    greenAreaDetected = true;
                } 
                else if (col.gameObject.CompareTag("YellowArea"))
                {
                    if (!greenAreaDetected)
                    {
                        //Debug.Log("Yellow Area");
                        GetArea("Yellow Area");
                        yellowAreaDetected = true;
                    }
                }
            }

            // If neither green nor yellow area is detected, you can consider it as the "Red Area."
            if (!greenAreaDetected && !yellowAreaDetected)
            {
                //Debug.Log("Red Area");
                GetArea("Red Area");
            }
        }

        // Reset the flags
        greenAreaDetected = false;
        yellowAreaDetected = false;
    }

    public void ToggleLerping()
    {
        // Toggle the isLerping flag to start/stop lerping
        isLerping = !isLerping;
    }

    public void CheckPlayerInput(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PlayerInputCoroutine());
        }
    }

    IEnumerator PlayerInputCoroutine(){
        ToggleLerping();
        Debug.Log("Player input in area : " + currentArea);
        yield return new WaitForSeconds(1f);
        ChangeYellowAreaTransform();
        ChangeGreenAreaTransform();
        ToggleLerping();
    }

    private void ChangeYellowAreaTransform(){
        // change yellow area x position to random from random possible position
        float randomX = yellowPossiblePosition[UnityEngine.Random.Range(0, yellowPossiblePosition.Length)];
        yellowArea.localPosition = new Vector3(randomX, yellowArea.position.y, yellowArea.position.z);
    }

    private void ChangeGreenAreaTransform(){
        // change green area x position to random from random possible position
        float randomX = greenPossiblePosition[UnityEngine.Random.Range(0, greenPossiblePosition.Length)];
        greenArea.localPosition = new Vector3(randomX, greenArea.position.y, greenArea.position.z);
    }

    private void GetArea(string area){
        currentArea = area;
    }
}
