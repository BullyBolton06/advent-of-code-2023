using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySix : MonoBehaviour
{
    private List<Race> races;

    private void Start()
    {
        races = new List<Race>();
        races.Add(new Race(62649190, 553101014731074));

        long counter = 1;
      

        Debug.Log(races[0].CalculateWinningSpeed());
    }
}

public class Race
{
    public long maxTime;
    public long recordDistance;
    public long winningCombinationCounter { get; private set; }

    //constructor that sets the time and distance
    public Race(long t, long d)
    {
        maxTime = t;
        recordDistance = d;
    }


    public long CalculateWinningSpeed()
    {
        winningCombinationCounter = 0;
        for (long speed = 0; speed < maxTime; speed++)
        {
            long time = maxTime - speed;
            long distance = time * speed;

            if (distance > recordDistance)
            {
                winningCombinationCounter++;
            }
        }

        return winningCombinationCounter;
    }
}