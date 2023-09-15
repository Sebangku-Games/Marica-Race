using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarHandler : MonoBehaviour
{
    public Scrollbar scrollBarArea;
    public Scrollbar scrollBarPointer;

    [SerializeField] private float duration = 2f;
    private bool isLerping = true; // Flag to control lerping

    // Start is called before the first frame update
    void Start()
    {
        // Set the scroll bar value to random
        RandomNumber();
        // Start the initial lerping
        StartCoroutine(LerpScrollBarPointerCoroutine());
    }

    // Set scrollBarArea's value to random number and save the number
    public void RandomNumber()
    {
        int randomNumber = Random.Range(0, 100);
        // Adjust the randomNumber to 0 ~ 1 for the slider value
        scrollBarArea.value = randomNumber / 100f;
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


}
