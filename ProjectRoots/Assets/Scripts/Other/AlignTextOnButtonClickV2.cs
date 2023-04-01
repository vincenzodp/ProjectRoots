using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlignTextOnButtonClickV2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    float MarginTopOffset;
    [SerializeField]
    float MarginBottomOffset;

    TextMeshProUGUI childTextMesh;
    Vector4 originalMargins;
    Vector4 newMargins;

    void Awake()
    {
        childTextMesh = GetComponentInChildren<TextMeshProUGUI>();
        originalMargins = childTextMesh.margin;
        newMargins = new Vector4(
            originalMargins.x,
            originalMargins.y + MarginTopOffset,
            originalMargins.z,
            originalMargins.w + MarginBottomOffset
        );
    }

    public void OnPointerDown(PointerEventData data)
    {
        childTextMesh.margin = newMargins;
    }

    public void OnPointerUp(PointerEventData data)
    {
        childTextMesh.margin = originalMargins;
    }
}
