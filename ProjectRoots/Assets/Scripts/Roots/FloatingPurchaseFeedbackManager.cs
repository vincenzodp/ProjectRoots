using TMPro;
using UnityEngine;

public class FloatingPurchaseFeedbackManager : MonoBehaviour
{
    public TextMeshPro textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void DisplayCost(int buyCost)
    {
        // Note: use underscore to display a minus
        textMesh.text = $"_{buyCost.ToString("N0")}";
    }
}
