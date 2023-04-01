using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    [SerializeField]
    float RotateSpeed = 1f;

    bool canRotate = true;
    public void StopRotation()
    {
        canRotate = false;
    }


    void Update()
    {
        if (canRotate)
        {
            RenderSettings.skybox.SetFloat("_Rotation", RotateSpeed * Time.time);
        }
    }
}
