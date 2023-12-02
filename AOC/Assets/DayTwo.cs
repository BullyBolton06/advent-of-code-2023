using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DayTwo : MonoBehaviour
{
    public TextAsset puzzleInput;

    void Start()
    {
        int powerTotal = 0;
        List<string> lines = puzzleInput.text.Split('\n').ToList();
        //remove empty lines
        lines.RemoveAll(string.IsNullOrEmpty);
        foreach (var gameLine in lines)
        {
            //This is what a game line looks like Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            //Remove Whitespace, commas and semi-colons
            Game game = new Game();
            string gameLineNoCommas = gameLine.Replace(",", "");
            string gameLineNoSemiColons = gameLineNoCommas.Replace(";", "");

            string gameIDString = gameLineNoSemiColons.Substring(0, gameLineNoSemiColons.IndexOf(':'));
            game.ID = int.Parse(gameIDString.Replace("Game ", ""));
           
            string cubesAndAmounts = gameLineNoSemiColons.Replace(gameIDString + ':', "");
            Debug.Log(cubesAndAmounts);

            // string will look like this  3 blue 4 red 1 red 2 green 6 blue 2 green
            cubesAndAmounts = cubesAndAmounts.Substring(1);
            string[] splitCubesAndAmounts = cubesAndAmounts.Split(' ');
            for (int i = 0; i < splitCubesAndAmounts.Length; i += 2)
            {
                int amount = int.Parse(splitCubesAndAmounts[i]);
                string colour = splitCubesAndAmounts[i + 1];
                switch (colour)
                {
                    case "red":
                        if(amount > game.MinNeededRedCubes)
                           game.MinNeededRedCubes = amount;
                        break;
                    case "blue":
                        if (amount > game.MinNeededBlueCubes)
                            game.MinNeededBlueCubes = amount;
                        break;
                    case "green":
                        if (amount > game.MinNeededGreenCubes)
                            game.MinNeededGreenCubes = amount;
                        break;
                }
            }

            powerTotal += (game.MinNeededRedCubes * game.MinNeededBlueCubes * game.MinNeededGreenCubes);
        }
        Debug.Log(powerTotal);
    }
}

public class Game
{
    public Game()
    {
        RedCubes = new Cubes();
        RedCubes.colour = cubeColour.red;
        BlueCubes = new Cubes();
        BlueCubes.colour = cubeColour.blue;
        GreenCubes = new Cubes();
        GreenCubes.colour = cubeColour.green;
        
    }

    public Cubes RedCubes;
    public Cubes BlueCubes;
    public Cubes GreenCubes;
    public int ID;
    public int MinNeededRedCubes = 0;
    public int MinNeededBlueCubes = 0;
    public int MinNeededGreenCubes = 0;
    
}

public class Cubes
{
    public cubeColour colour;
    public int count;
}

public enum cubeColour
{
    red,
    blue,
    green,
}