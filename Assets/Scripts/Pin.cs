using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    void CheckRingSize()
    {
        //Compare Ring size here
    }

    void SlideRingDown()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ring"))
        {
            Debug.Log("Ring Entered Collision");
        }
    }
}
