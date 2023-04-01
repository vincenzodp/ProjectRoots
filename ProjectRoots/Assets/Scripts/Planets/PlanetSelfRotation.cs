using UnityEngine;

public class PlanetSelfRotation : MonoBehaviour
{
    [SerializeField]
    float Speed;

    [SerializeField]
    Vector3 RotationAxis = Vector3.up;




    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotationAxis * Speed * Time.deltaTime);
    }
}
