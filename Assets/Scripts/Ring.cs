using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    [Header("Properties")]
    public int ringSize = 0;

    [Header("Debugging")]
    Pin pinToEnter;
    Animator anim;

    public SpriteRenderer backSprite;
    public SpriteRenderer frontSprite;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

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

    public bool TryRelease()
    {
        if (pinToEnter != null)
        {
            if(pinToEnter.CheckRingSize(this))
            {
                ChangeSpriteRenderingOrder();
                return pinToEnter.CheckRingSize(this);
            }
        }
        else
        {
            anim.SetTrigger("Shake");
            return false;
        }

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
            {
                anim.SetTrigger("Shake");
                return false;
            }
        }
        anim.SetTrigger("Shake");
        return false;
    }

    public void GetPickedUp()
    {
        ChangeSpriteRenderingOrder();
        pinToEnter.RemoveRing(this);
        pinToEnter = null;
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
