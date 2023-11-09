using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewScrollbarHandler : MonoBehaviour
{
    [Header("Scroll Bar Yellow Area")]
    public Scrollbar scrollBarYellowArea;

    [Header("Scroll Bar Green Area")]
    public Scrollbar scrollBarGreenArea;

    [Header("Scroll Bar Pointer")]
    public Scrollbar scrollBarPointer;
    [SerializeField] private float duration = 2f;

    [SerializeField] private LayerMask layerMask;

    private void Start()
    {
        // start the initial lerping
        StartCoroutine(LerpScrollBarPointerCoroutine());
    }

    private void Update()
    {
        CheckScrollBarPointer();

        
    }



    // make function that check if scrollBarPointer overlap with other scrollbararea
    public void CheckScrollBarPointer()
    {
        // Calculate the position to cast the raycast from
        Vector3 raycastPosition = scrollBarPointer.transform.position;
        Collider[] collider = Physics.OverlapSphere(raycastPosition, 0.1f, layerMask);

        if (collider != null)
        {
            foreach (Collider col in collider)
            {
                Debug.Log("There is a collider");
                if (col.gameObject.CompareTag("GreenArea"))
                {
                    Debug.Log("Green Area");
                }
                else if (col.gameObject.CompareTag("YellowArea"))
                {
                    Debug.Log("Yellow Area");
                }
                // else if (col.gameObject.CompareTag("RedArea"))
                // {
                //     Debug.Log("Red Area");
                // }
            }

            //Debug.Log("Red Area");
        }
    }
    


    // make function / coroutine that lerp scrollbar pointer left and right
    public IEnumerator LerpScrollBarPointerCoroutine()
    {
        // set the initial value of the scrollbar pointer
        float startValue = 0f;
        float endValue = 1f;

        // set the initial time
        float time = 0f;

        // set the initial position of the scrollbar pointer
        scrollBarPointer.value = startValue;

        // set the initial lerping flag
        bool isLerping = true;

        // while the lerping flag is true
        while (isLerping)
        {
            // increase the time
            time += Time.deltaTime;

            // calculate the value of the scrollbar pointer
            float value = Mathf.Lerp(startValue, endValue, time / duration);

            // set the value of the scrollbar pointer
            scrollBarPointer.value = value;

            // if the value of the scrollbar pointer is equal to the end value
            if (scrollBarPointer.value == endValue)
            {
                // reset the time
                time = 0f;

                // set the end value to the start value
                endValue = startValue;

                // set the start value to the value of the scrollbar pointer
                startValue = scrollBarPointer.value;
            }

            // if the value of the scrollbar pointer is equal to the start value
            if (scrollBarPointer.value == startValue)
            {
                // reset the time
                time = 0f;

                // set the start value to the end value
                startValue = endValue;

                // set the end value to the value of the scrollbar pointer
                endValue = scrollBarPointer.value;
            }

            // wait for a frame
            yield return null;
        }
    }

    
}
