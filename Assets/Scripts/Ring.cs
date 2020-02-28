using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public int ringSize = 0;
    [SerializeField] bool isBeingHeld = false;
    Pin pinToEnter;

    public void SetIsBeingHeld(bool isHeld)
    {
        isBeingHeld = isHeld;
    }

    public bool TryRelease()
    {
        if (pinToEnter != null)
            return pinToEnter.CheckRingSize(this);
        else
            return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pin"))
        {
            pinToEnter = collision.GetComponent<Pin>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pin") && isBeingHeld)
        {
            pinToEnter.RemoveRing(this);
            pinToEnter = null;
        }
    }

}
