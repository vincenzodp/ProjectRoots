using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefensePlacer : MonoBehaviour
{
    ResourceBar resourceBar;

    [SerializeField] GameObject defenseTurret;

    Turret turretScript;


    GameObject currentlySelected = null;






    // Start is called before the first frame update
    void Start()
    {
        resourceBar = FindObjectOfType<ResourceBar>();

        turretScript = defenseTurret.GetComponent<Turret>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "DefensePosition")
                {
                    if (currentlySelected == null)
                    {
                        return;
                    }

                    if (resourceBar.GetResource() >= turretScript.GetCost())
                    {
                        resourceBar.DecreaseResource(turretScript.GetCost());
                        GameObject turret = Instantiate(defenseTurret.gameObject, new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y - 1.47f, -3), Quaternion.Euler(0, 125, 0));
                        Destroy(turret, turretScript.GetLifetime());
                        DeselectTurret();
                    }

                }
                else { DeselectTurret(); }
            }

            else { DeselectTurret(); }

        }


    }





    public void SelectTurret1()
    {
        currentlySelected = defenseTurret;
    }

    public void DeselectTurret()
    {
        currentlySelected = null;
    }




}
