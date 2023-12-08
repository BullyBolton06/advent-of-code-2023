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
        races.Add(new Race(62, 553));
        races.Add(new Race(64, 1010));
        races.Add(new Race(91, 1473));
        races.Add(new Race(90, 1074));

        int counter = 1;
        foreach (var race in races)
        {
            counter *= race.CalculateWinningSpeed();
        }

        Debug.Log(counter);
    }
}

public class Race
{
    public int maxTime;
    public int recordDistance;
    public int winningCombinationCounter { get; private set; }

    //constructor that sets the time and distance
    public Race(int t, int d)
    {
        maxTime = t;
        recordDistance = d;
    }


    public int CalculateWinningSpeed()
    {
        for (int speed = 0; speed < maxTime; speed++)
        {
            int time = maxTime - speed;
            int distance = time * speed;

            if (distance > recordDistance)
            {
                winningCombinationCounter++;
            }
        }

        return winningCombinationCounter;
    }
}