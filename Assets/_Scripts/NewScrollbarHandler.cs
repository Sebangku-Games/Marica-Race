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
        // check if scrollBarPointer is in the yellow area
        if (scrollBarPointer.value >= scrollBarYellowArea.value - 0.1f && scrollBarPointer.value <= scrollBarYellowArea.value + 0.1f)
        {
            // check if scrollBarPointer is in the green area
            if (scrollBarPointer.value >= scrollBarGreenArea.value - 0.1f && scrollBarPointer.value <= scrollBarGreenArea.value + 0.1f)
            {
                Debug.Log("You are in the green area");
            }
            else
            {
                Debug.Log("You are in the yellow area");
            }
        }
        else
        {
            Debug.Log("You are in the red area");
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
