using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [Header("Properties")]
    public List<Ring> ringsOnPin;
    [Tooltip("Edit the prefab to edit all pins at once!")]
    public AnimationCurve slideCurve;
    public AnimationCurve jumpCurve;

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
        //find the correct position on the pin to put the ring at
        int stepsUp = 0;
        for (int i = 0; i < ringsOnPin.Count; i++)
        {
            stepsUp++;
        }

        //position the ring at the top of the pin to then look like its sliding down properly
        StartCoroutine(JumpRingToTopOfPin(ringToPosition, stepsUp));
    }

    IEnumerator JumpRingToTopOfPin(Ring ringToSlide, int stepsUp)
    {
        float startTime = Time.time;
        float maxTime = 1;
        Transform slideStartPos = ringToSlide.transform;

        bool isMoving = true;

        while (isMoving)
        {
            float timeSinceStarted = Time.time - startTime;
            float percent = timeSinceStarted / maxTime;
            //move the ring 1 "ring" above the pin to then smoothly fall down
            ringToSlide.transform.position = Vector3.Slerp(slideStartPos.position, new Vector3(gameObject.transform.position.x, -3.6f + (1.2f * 5), slideStartPos.position.z), jumpCurve.Evaluate(percent));

            if (percent >= maxTime)
                isMoving = false;

            //fixed a problem where the ring would pause while ontop of the ring because x position was not completely 0
            if (ringToSlide.transform.position.x <= gameObject.transform.position.x + 0.01f && percent >= maxTime / 2 ||
                ringToSlide.transform.position.x >= gameObject.transform.position.x - 0.01f && percent >= maxTime / 2)
            {
                ringToSlide.transform.position = new Vector2(gameObject.transform.position.x, ringToSlide.transform.position.y);
                isMoving = false;
            }

            yield return null;
        }

        //change to display back part of ring behind the pin
        ringToSlide.ChangeSpriteRenderingOrder();
        //start the slide down the pin to correct position
        StartCoroutine(SlideRingDown(ringToSlide, stepsUp));
    }

    IEnumerator SlideRingDown(Ring ringToSlide, int steps)
    {
        float startTime = Time.time;
        float maxTime = 1;
        Transform slideStartPos = ringToSlide.transform;

        bool isMoving = true;

        while (isMoving)
        {
            //-3.6f is the bottom of the pin (where the ring will go if the pin is empty) then it moves 1.2f up per ring on the pin
            float timeSinceStarted = Time.time - startTime;
            float percent = timeSinceStarted / maxTime;
            ringToSlide.transform.position = Vector3.Lerp(slideStartPos.position, new Vector3(gameObject.transform.position.x, -3.6f + (1.2f * steps), slideStartPos.position.z), slideCurve.Evaluate(percent));

            if(percent >= maxTime)
                isMoving = false;

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
