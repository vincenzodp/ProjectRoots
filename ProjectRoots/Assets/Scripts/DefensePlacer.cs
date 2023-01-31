using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePlacer : MonoBehaviour
{
    ResourceBar resourceBar;

    [SerializeField] GameObject defenseTurret;
    [SerializeField] float turretLifetime = 10;
    [SerializeField] int defenseTurretCost = 30;

    
    

    // Start is called before the first frame update
    void Start()
    {
        resourceBar = FindObjectOfType<ResourceBar>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if(hit.collider.tag == "DefensePosition")
                {
                    if(resourceBar.GetResource() >= defenseTurretCost)
                        {
                            resourceBar.DecreaseResource(defenseTurretCost);    
                            GameObject turret = Instantiate(defenseTurret, new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y - 1.47f, -3), Quaternion.Euler(0, 125, 0));   
                            Destroy(turret, turretLifetime);
                        }
                    
                }
            }           
        } 

     
    }

    
}
