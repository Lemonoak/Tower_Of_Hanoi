using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [Header("Properties")]
    public int ringSize = 0;
    public SpriteRenderer backSprite;
    public SpriteRenderer frontSprite;

    [Header("Debugging")]
    Pin pinToEnter;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    //Change sprites order in layer so they will display ontop of other rings and pins
    public void ChangeSpriteRenderingOrder()
    {
        if(backSprite.sortingOrder > 100)
        {
            backSprite.sortingOrder -= 100;
            frontSprite.sortingOrder -= 100;
        }
        else
        {
            backSprite.sortingOrder += 100;
            frontSprite.sortingOrder += 100;
        }
    }

    //check if ring can be released
    public bool TryRelease()
    {
        if (pinToEnter != null)
        {
            if(pinToEnter.CheckRingSize(this))
            {
                anim.SetBool("Wiggle", false);
                return pinToEnter.CheckRingSize(this);
            }
            else
                anim.SetTrigger("Shake");
        }

        return false;
    }

    //check if ring can be picked up
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
            {
                anim.SetTrigger("Shake");
                return false;
            }
        }

        anim.SetTrigger("Shake");
        return false;
    }

    //ring gets picked up
    public void GetPickedUp()
    {
        ChangeSpriteRenderingOrder();
        pinToEnter.RemoveRing(this);
        pinToEnter = null;
        anim.SetBool("Wiggle", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pin"))
            pinToEnter = collision.GetComponent<Pin>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Pin") && pinToEnter == null)
            pinToEnter = collision.GetComponent<Pin>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pin") && pinToEnter != null)
            pinToEnter = null;
    }
}
