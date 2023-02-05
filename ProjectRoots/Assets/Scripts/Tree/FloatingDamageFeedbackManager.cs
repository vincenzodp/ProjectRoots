using TMPro;
using UnityEngine;

public class FloatingDamageFeedbackManager : MonoBehaviour
{
    public TextMeshPro textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void DisplayDamage(float damage)
    {
        // Note: use underscore to display a minus
        textMesh.text = $"_{Mathf.Floor(damage).ToString("N0")}";
    }
}
