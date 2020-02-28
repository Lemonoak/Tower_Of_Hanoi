using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRing : MonoBehaviour
{
    public static PickupRing instance;

    public Ring ringToHold;
    LayerMask ringMask;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ringMask = LayerMask.GetMask("Rings");
    }

    void Update()
    {
        HoldRing();
    }

    void HoldRing()
    {
        //find ring
        if (Input.GetMouseButtonDown(0) && ringToHold == null)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1, ringMask);
            if(hit.collider != null)
            {
                ringToHold = hit.transform.GetComponent<Ring>();
                ringToHold.SetIsBeingHeld(true);
            }
        }
        //hold ring
        if(Input.GetMouseButton(0) && ringToHold != null)
        {
            //TODO: Smooth the position from ring to mouse!
            ringToHold.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, ringToHold.transform.position.z);
        }
        //release ring
        if(Input.GetMouseButtonUp(0) && ringToHold != null)
        {
            if(ringToHold.TryRelease())
                ringToHold = null;
        }
    }

    public static PickupRing GetInstance()
    {
        if (instance == null)
            instance = new GameObject("PickupRing").AddComponent<PickupRing>();

        return instance;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
