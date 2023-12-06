using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollbarPointer : MonoBehaviour
{
    public UIManager uiManager;

    [Header("Yellow Area")]
    [SerializeField] private Transform yellowArea;
    private float[] yellowPossiblePosition = { -0.35f, -0.25f, -0.15f, -0.05f, 0.05f, 0, 0.15f, 0.25f, 0.35f };

    [Header("Green Area")]
    [SerializeField] private Transform greenArea;
    private float[] greenPossiblePosition = { -0.25f, -0.125f, 0, 0.125f, 0.25f };


    [Header("Pointer")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float radius = 0.1f;
    private bool greenAreaDetected = false;
    private bool yellowAreaDetected = false;

    public float duration;
    private float tempDuration;
    private bool isLerping = true; // Flag to control lerping

    private float initialOffset = -1f;
    private float targetOffset = 1f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private string currentArea;

    private bool hasIncreasedSpeed = false;


    
    private void Start()
    {
        initialPosition = new Vector3(initialOffset, transform.localPosition.y, transform.localPosition.z);
        targetPosition = new Vector3(targetOffset, transform.localPosition.y, transform.localPosition.z);

        
        tempDuration = duration;
    }


    private void Update()
    {
        if (!GameManager.instance.isGameRunning){
            return;
        }
        
        CheckScrollBarPointer();

        // If isLerping is true, lerp the position of the GameObject
        if (isLerping)
        {
            float t = Mathf.PingPong(Time.time / duration, 1.0f); // PingPong between 0 and 1
            transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, t);
        }

        UpdatePlayerInput();
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
                //uiManager.ShowAreaText("Red Area");
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

    public void UpdatePlayerInput(){
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(PlayerInputCoroutine());
        }
    }

    IEnumerator PlayerInputCoroutine(){
        ToggleLerping();
        Debug.Log("Player input in area : " + currentArea);
        uiManager.ShowAreaText(currentArea);
        CheckPlayerInput(currentArea);
        yield return new WaitForSeconds(1);
        ChangeYellowAreaTransform();
        ChangeGreenAreaTransform();
        ToggleLerping();
        
    }

    private void CheckPlayerInput(string currArea){
        if (currArea == "Green Area"){
            GameManager.instance.DoSomethingInGreenArea();
        }
        else if (currArea == "Yellow Area"){
            GameManager.instance.DoSomethingInYellowArea();
        }
        else if (currArea == "Red Area"){
            GameManager.instance.DoSomethingInRedArea();
        }
    }

    private void ChangeYellowAreaTransform(){
        // change yellow area x position to random from random possible position
        float randomX = yellowPossiblePosition[UnityEngine.Random.Range(0, yellowPossiblePosition.Length)];
        yellowArea.localPosition = new Vector3(randomX, yellowArea.localPosition.y, yellowArea.localPosition.z);
    }

    private void ChangeGreenAreaTransform(){
        // change green area x position to random from random possible position
        float randomX = greenPossiblePosition[UnityEngine.Random.Range(0, greenPossiblePosition.Length)];
        greenArea.localPosition = new Vector3(randomX, greenArea.localPosition.y, greenArea.localPosition.z);
    }

    private void GetArea(string area){
        currentArea = area;
    }

    public void IncreaseScrollbarPointerSpeed(){
        if (hasIncreasedSpeed){
            return;
        }

        // Debug.Log("INcrease scrollbar pointer speed");
        duration = duration / 2;
        hasIncreasedSpeed = true;
    }

    public void ResetScrollbarPointerSpeed(){
        // Debug.Log("Reset Scrollbar Pointer Speed");
        duration = tempDuration;
        hasIncreasedSpeed = false;
    }
}
