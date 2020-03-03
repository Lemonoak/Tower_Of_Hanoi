using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [Header("Properties")]
    public List<Ring> ringsOnPin;
    [Tooltip("Edit the prefab to edit all pins at once!")]
    public AnimationCurve slideCurve;

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

    public bool CheckRingOnTop(Ring newRing)
    {
        //check if ring is on top of pin
        if (ringsOnPin.Count > 0 && newRing == ringsOnPin[ringsOnPin.Count - 1])
        {
            //this makes you able to pickup a ring thats "still sliding down"
            StopAllCoroutines();
            RemoveRing(newRing);
            return true;
        }
        else
            return false;
    }

    void PositionRing(Ring ringToPosition)
    {
        //Put the ring in the correct place on the pin
        int stepsUp = 0;
        for (int i = 0; i < ringsOnPin.Count; i++)
        {
            stepsUp++;
        }

        //position the ring at the top of the pin to then look like its sliding down properly
        ringToPosition.transform.position = new Vector3(gameObject.transform.position.x, -3.6f + (1.2f * 5));
        //start the slide down the pin to correct position
        StartCoroutine(SlideRingDown(ringToPosition, stepsUp));
    }

    IEnumerator SlideRingDown(Ring ringToSlide, int steps)
    {
        float time = 0;
        float maxTime = 1;
        Transform slideStartPos = ringToSlide.transform;
        while (time < maxTime)
        {
            ringToSlide.transform.position = Vector3.Lerp(slideStartPos.position, new Vector3(gameObject.transform.position.x, -3.6f + (1.2f * steps), slideStartPos.position.z), slideCurve.Evaluate(time / maxTime));
            time += Time.deltaTime;
            yield return null;
        }
    }

    void AddRing(Ring ringToAdd)
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
