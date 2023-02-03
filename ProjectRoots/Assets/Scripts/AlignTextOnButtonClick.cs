using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AlignTextOnButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    TextMeshProUGUI childTextMesh;

    void Awake()
    {
        childTextMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        childTextMesh.verticalAlignment = VerticalAlignmentOptions.Middle;
    }

    public void OnPointerUp(PointerEventData data)
    {
        childTextMesh.verticalAlignment = VerticalAlignmentOptions.Top;

    }
}
