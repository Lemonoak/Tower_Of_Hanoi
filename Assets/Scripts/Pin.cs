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

    public void PositionRing()
    {
        //Put the ring in the correct place on the pin
        int stepsUp = -1;
        for (int i = 0; i < ringsOnPin.Count; i++)
        {
            stepsUp++;
        }

        ringsOnPin[ringsOnPin.Count - 1].transform.position = new Vector3(gameObject.transform.position.x, -3.6f + (1.2f * stepsUp));
    }

    public void AddRing(Ring ringToAdd)
    {
        ringsOnPin.Add(ringToAdd);
        PositionRing();
    }

    public void RemoveRing(Ring ringToRemove)
    {
        ringsOnPin.Remove(ringToRemove);
    }
}
