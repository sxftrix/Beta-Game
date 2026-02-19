using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCheck : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Button Highlight Triggered");
    }
}