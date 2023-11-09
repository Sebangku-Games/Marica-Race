using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarHandler1 : MonoBehaviour
{
    public Scrollbar scrollBar;
    public RectTransform redArea;
    public RectTransform yellowArea;
    public RectTransform greenArea;
    public RectTransform pointer;

    private void Start()
    {
        // Randomly position the yellow and green areas within the red area
        RandomizeAreaPosition(yellowArea, redArea);
        RandomizeAreaPosition(greenArea, yellowArea);
    }

    private void Update()
    {
        // Move the pointer along the scrollbar
        MovePointer();

        // Check which area the pointer is over
        CheckPointerArea();
    }

    private void RandomizeAreaPosition(RectTransform area, RectTransform parent)
    {
        // Randomize the position of the area within the parent area
        float minX = parent.rect.xMin;
        float maxX = parent.rect.xMax;
        float minY = parent.rect.yMin;
        float maxY = parent.rect.yMax;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        area.anchoredPosition = new Vector2(randomX, randomY);
    }

    private void MovePointer()
    {
        // Implement code to move the pointer based on player input or some other mechanism
        // For example, you can use Input.GetAxis("Vertical") to control the scroll bar value and move the pointer accordingly.
        float scrollValue = Input.GetAxis("Vertical");
        scrollBar.value = scrollValue;
    }

    private void CheckPointerArea()
    {
        // Check if the pointer is within the green, yellow, or red area
        if (RectTransformUtility.RectangleContainsScreenPoint(greenArea, pointer.position))
        {
            Debug.Log("Pointer is over Green Area");
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(yellowArea, pointer.position))
        {
            Debug.Log("Pointer is over Yellow Area");
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(redArea, pointer.position))
        {
            Debug.Log("Pointer is over Red Area");
        }
    }
}
