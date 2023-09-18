using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarHandler : MonoBehaviour
{
    [SerializeField] private float forgiveOffset = 0.01f;

    [Header("Scroll Bar Yellow Area")]
    public Scrollbar scrollBarArea;
    public float[] scrollBarAreaValues = {0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 
                                          0.6f, 0.7f, 0.8f, 0.9f, 1f};

    [Header("Scroll Bar Green Area")]
    public Scrollbar scrollBarAreaInside;
    public float[] scrollBarAreaInsideValues = {0.25f, 0.5f, 0.75f, 1f};
    
    [Header("Scroll Bar Pointer")]
    public Scrollbar scrollBarPointer;
    [SerializeField] private float duration = 2f;
    private bool isLerping = true; // Flag to control lerping

    // Start is called before the first frame update
    void Start()
    {
        // Set the scroll bar value to random
        //RandomScrollBarArea();

        // Set the scroll bar value to test
        SetTestArea();

        // Start the initial lerping
        StartCoroutine(LerpScrollBarPointerCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the scrollBarPointer is in the yellow area
        if(Input.GetKeyDown(KeyCode.A)){
            StartCoroutine(PlayerInput());
        }
    }

    // Set scrollBarArea's value to random number and save the number
    public void RandomScrollBarArea()
    {
        int randomNumber = Random.Range(0, scrollBarAreaValues.Length);
        scrollBarArea.value = scrollBarAreaValues[randomNumber];

        int randomNumber2 = Random.Range(0, scrollBarAreaInsideValues.Length);
        scrollBarAreaInside.value = scrollBarAreaInsideValues[randomNumber2];
    }

    public void SetTestArea(){
        scrollBarArea.value = 0.5f;
        scrollBarAreaInside.value = 0.5f;
    }

    // Make function to Lerp scrollBarPointer from 0 to 100 and assign it to scrollBarPointer's value
    public void LerpScrollBarPointer()
    {
        // Toggle the flag to start/stop lerping
        isLerping = !isLerping;

        // If the flag is true, start lerping
        if (isLerping)
        {
            StartCoroutine(LerpScrollBarPointerCoroutine());
        }
    }

    IEnumerator LerpScrollBarPointerCoroutine()
    {
        float time = 0f;
        float startValue = 0f;
        float endValue = 1f;

        while (time < duration && isLerping) // Check the flag before continuing lerping
        {
            time += Time.deltaTime;
            float value = Mathf.Lerp(startValue, endValue, time / duration);
            scrollBarPointer.value = value;
            yield return null;
        }

        // If the flag is still true, start another lerping cycle (back to 0)
        if (isLerping)
        {
            StartCoroutine(ReverseLerpScrollBarPointerCoroutine());
        }
    }

    // Coroutine to reverse the lerping back to 0
    IEnumerator ReverseLerpScrollBarPointerCoroutine()
    {
        float time = 0f;
        float startValue = 1f;
        float endValue = 0f;

        while (time < duration && isLerping) // Check the flag before continuing lerping
        {
            time += Time.deltaTime;
            float value = Mathf.Lerp(startValue, endValue, time / duration);
            scrollBarPointer.value = value;
            yield return null;
        }

        // If the flag is still true, start another lerping cycle (back to 1)
        if (isLerping)
        {
            StartCoroutine(LerpScrollBarPointerCoroutine());
        }
    }

    // Check ScrollBar Pointer if in yellow area (scrollbar area)
    public void CheckYellowArea()
    {
        // Calculate the range for the yellow area
        float yellowAreaStart = scrollBarArea.value - scrollBarArea.size / 2;
        float yellowAreaEnd = scrollBarArea.value + scrollBarArea.size / 2;

        // Ensure that the scrollbar values are clamped to the valid range [0, 1]
        float clampedScrollBarValue = Mathf.Clamp(scrollBarPointer.value, 0f, 1f);

        if (scrollBarArea.value == 0f || scrollBarArea.value == 1f)
        {
            // Special handling for minimum and maximum values
            if (clampedScrollBarValue >= yellowAreaStart || clampedScrollBarValue <= yellowAreaEnd)
            {
                Debug.Log("In Yellow Area");
                // Debug values
                Debug.Log("scrollBarArea.value: " + scrollBarArea.value);
                Debug.Log("scrollBarPointer.value: " + clampedScrollBarValue);
            }
            else
            {
                Debug.Log("Not in Yellow Area");
                // Debug values
                Debug.Log("scrollBarArea.value: " + scrollBarArea.value);
                Debug.Log("scrollBarPointer.value: " + clampedScrollBarValue);
            }
        }
        else
        {
            if (clampedScrollBarValue >= yellowAreaStart && clampedScrollBarValue <= yellowAreaEnd)
            {
                Debug.Log("In Yellow Area");
                // Debug values
                Debug.Log("scrollBarArea.value: " + scrollBarArea.value);
                Debug.Log("scrollBarPointer.value: " + clampedScrollBarValue);
            }
            else
            {
                Debug.Log("Not in Yellow Area");
                // Debug values
                Debug.Log("scrollBarArea.value: " + scrollBarArea.value);
                Debug.Log("scrollBarPointer.value: " + clampedScrollBarValue);
            }
        }
    }




    IEnumerator PlayerInput()
    {
        // stop the scroll bar pointer lerping
        isLerping = false;
        
        // check if the scroll bar pointer is in the yellow area
        CheckYellowArea();

        // wait for 1 second
        yield return new WaitForSeconds(1f);

        // start the scroll bar pointer lerping again
        isLerping = true;

        // start the initial lerping
        StartCoroutine(LerpScrollBarPointerCoroutine());

        

    }

}
