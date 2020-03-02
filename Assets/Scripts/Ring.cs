using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [Header("Properties")]
    public int ringSize = 0;

    [Header("Debugging")]
    [SerializeField] bool isBeingHeld = false;
    Pin pinToEnter;

    public void SetIsBeingHeld(bool isHeld)
    {
        isBeingHeld = isHeld;
    }

    public bool GetIsBeingHeld()
    {
        return isBeingHeld;
    }

    public bool TryRelease()
    {
        if (pinToEnter != null)
            return pinToEnter.CheckRingSize(this);
        else
            //TODO: SHAKE RING NONO
            return false;
    }

    public bool TryPickup()
    {
        if (pinToEnter != null)
        {
            if (pinToEnter.CheckRingOnTop(this))
            {
                GetPickedUp();
                return true;
            }
            else
                //TODO: SHAKE RING NONO
                return false;
        }
        //TODO: SHAKE RING NONO
        return false;
    }

    public void GetPickedUp()
    {
        SetIsBeingHeld(true);
        pinToEnter.RemoveRing(this);
        pinToEnter = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pin"))
        {
            pinToEnter = collision.GetComponent<Pin>();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Pin") && pinToEnter == null)
        {
            pinToEnter = collision.GetComponent<Pin>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pin") && pinToEnter != null)
        {
            pinToEnter = null;
        }
    }
}
