using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRing : MonoBehaviour
{
    public static PickupRing instance;

    [Header("Properties")]
    public float ringMoveSpeed = 1.0f;

    [Header("Debugging")]
    [SerializeField] Ring ringToHold;
    LayerMask ringMask;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //make raycast only find rings
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
                ringToHold = hit.collider.GetComponent<Ring>();
                if (!ringToHold.TryPickup())
                    ringToHold = null;
            }
        }
        //hold ring
        if(Input.GetMouseButton(0) && ringToHold != null)
        {
            Vector2 oldRingPos = ringToHold.transform.position;
            ringToHold.transform.position = Vector3.Lerp(oldRingPos, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, ringToHold.transform.position.z), ringMoveSpeed * Time.deltaTime);
        }
        //release ring
        if(Input.GetMouseButtonUp(0) && ringToHold != null)
        {
            if(ringToHold.TryRelease())
                ringToHold = null;
        }
    }

    //only one pickup instance should ever be in the scene (but always one)
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
