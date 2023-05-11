using UnityEngine;

public class PlanetSatelliteRotation : MonoBehaviour
{
    [SerializeField]
    Transform PlanetPivot;

    [SerializeField]
    float AnglePerSecond;

    [SerializeField]
    Vector3 RotationAxis = Vector3.up;


    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(PlanetPivot.position, RotationAxis, AnglePerSecond * Time.deltaTime);
    }
}
