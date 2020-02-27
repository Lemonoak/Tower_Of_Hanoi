using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRing : MonoBehaviour
{
    public GameObject ringToHold;

    void Update()
    {
        FindRing();
    }

    void FindRing()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null)
            {
                ringToHold = hit.transform.gameObject;
            }
        }
        if(Input.GetMouseButton(0) && ringToHold != null)
        {
            ringToHold.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, ringToHold.transform.position.z);
        }
        if(Input.GetMouseButtonUp(0) && ringToHold != null)
        {
            ringToHold = null;
        }
    }

}
