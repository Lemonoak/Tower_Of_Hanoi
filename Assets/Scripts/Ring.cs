using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public int ringSize = 0;
    [SerializeField] bool isBeingHeld = false;

    public void SetIsBeingHeld(bool isHeld)
    {
        isBeingHeld = isHeld;
    }

}
