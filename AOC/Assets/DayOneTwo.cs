using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Serialization;

public class DayOneTwo : MonoBehaviour
{
    public TextAsset puzzleInput;

    Dictionary<string, string> replacements = new()
    {
        { "one", "o1e" },
        { "two", "t2o" },
        { "three", "t3e" },
        { "four", "f4r" },
        { "five", "f5e" },
        { "six", "s6x" },
        { "seven", "s7n" },
        { "eight", "e8t" },
        { "nine", "n9e" }
    };

    int total = 0;
    string numbers = "1234567890";

    private void Start()
    {
        foreach (var line in puzzleInput.text.Split("\r\n"))
        {
            string fixedString = line;
            foreach (var replacement in replacements)
            {
                fixedString = fixedString.Replace(replacement.Key, replacement.Value);
            }

            char first = fixedString.FirstOrDefault(c => numbers.Contains(c));
            char last = fixedString.LastOrDefault(c => numbers.Contains(c));
            string number = new(new[] { first, last });
            if (int.TryParse(number, out int val))
            {
                total += val;
            }
        }

        Debug.Log(total);


    }
}