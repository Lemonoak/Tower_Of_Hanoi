using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public List<Ring> ringsOnPin;
    Ring ringToAdd;

    public bool CheckRingSize(Ring newRing)
    {
        //compare ring size
        if (ringsOnPin.Count > 0)
        {
            if (newRing.ringSize > ringsOnPin[ringsOnPin.Count - 1].ringSize)
                return false;
            else
            {
                AddRing(newRing);
                return true;
            }
        }
        else
        {
            AddRing(newRing);
            return true;
        }
    }

    public void PositionRing(Ring ringToPosition)
    {
        //Put the ring in the correct place on the pin
        int stepsUp = 0;
        for (int i = 0; i < ringsOnPin.Count; i++)
        {
            stepsUp++;
        }

        ringToPosition.transform.position = new Vector3(gameObject.transform.position.x, -3.6f + (1.2f * stepsUp));
    }

    public void AddRing(Ring ringToAdd)
    {
        if(!ringsOnPin.Contains(ringToAdd))
        {
            PositionRing(ringToAdd);
            ringsOnPin.Add(ringToAdd);
        }
    }

    public void RemoveRing(Ring ringToRemove)
    {
        if (ringsOnPin.Contains(ringToRemove))
            ringsOnPin.Remove(ringToRemove);
    }
}
