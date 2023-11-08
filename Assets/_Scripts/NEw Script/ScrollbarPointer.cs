using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollbarPointer : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float radius = 0.01f;
    private bool greenAreaDetected = false;
    private bool yellowAreaDetected = false;

    

    private void Update()
    {
        CheckScrollBarPointer();
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
                    Debug.Log("Green Area");
                    greenAreaDetected = true;
                } 
                else if (col.gameObject.CompareTag("YellowArea"))
                {
                    if (!greenAreaDetected)
                    {
                        Debug.Log("Yellow Area");
                        yellowAreaDetected = true;
                    }
                }
            }

            // If neither green nor yellow area is detected, you can consider it as the "Red Area."
            if (!greenAreaDetected && !yellowAreaDetected)
            {
                Debug.Log("Red Area");
            }
        }

        // Reset the flags
        greenAreaDetected = false;
        yellowAreaDetected = false;
    }


    
}
