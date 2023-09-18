using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarHandler : MonoBehaviour
{
    [Header("Scroll Bar Yellow Area")]
    public Scrollbar scrollBarArea;
    [Header("Scroll Bar Green Area")]
    public Scrollbar scrollBarAreaInside;
    [Header("Scroll Bar Pointer")]
    public Scrollbar scrollBarPointer;
    [SerializeField] private float duration = 2f;
    private bool isLerping = true; // Flag to control lerping

    // Start is called before the first frame update
    void Start()
    {
        // Set the scroll bar value to random
        RandomScrollBarArea();
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
        int randomNumber = Random.Range(0, 101);
        int randomNumber2 = Random.Range(0, 11);
        // Adjust the randomNumber to 0 ~ 1 for the slider value
        scrollBarArea.value = randomNumber / 100f;
        scrollBarAreaInside.value = randomNumber2 / 10f;
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

        // If the value is inside the yellow area range, then it's in the yellow area
        if (scrollBarPointer.value >= yellowAreaStart && scrollBarPointer.value <= yellowAreaEnd)
        {
            Debug.Log("In Yellow Area");
            // Debug value
            Debug.Log("scrollBarArea.value: " + scrollBarArea.value.ToString("F2"));
            Debug.Log("scrollBarPointer.value: " + scrollBarPointer.value.ToString("F2"));
        }
        else
        {
            Debug.Log("Not in Yellow Area");
            // Debug value
            Debug.Log("scrollBarArea.value: " + scrollBarArea.value.ToString("F2"));
            Debug.Log("scrollBarPointer.value: " + scrollBarPointer.value.ToString("F2"));
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
