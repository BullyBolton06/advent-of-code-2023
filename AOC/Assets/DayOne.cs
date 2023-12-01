using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DayOne : MonoBehaviour
{
    public TextAsset puzzleInput;

    // Start is called before the first frame update
    void Start()
    {
        int puzzleOneAnswer = 0;
        puzzleInput.text.Split('\n').ToList().ForEach(line =>
        {
            if (!string.IsNullOrEmpty(line))
            {
                string calibrationNumberText = GetNumbers(line);
                if (calibrationNumberText.Length >= 2)
                {
                    calibrationNumberText = calibrationNumberText[0] + "" + calibrationNumberText[^1];
                }

                if (calibrationNumberText.Length == 1)
                {
                    calibrationNumberText = calibrationNumberText[0] + "" + calibrationNumberText[0];
                }
                
                int calibrationNumber = (int.Parse(calibrationNumberText[0].ToString()) * 10) +
                                        int.Parse(calibrationNumberText[1].ToString());
                puzzleOneAnswer += calibrationNumber;
            }
        });

        Debug.Log(puzzleOneAnswer);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private string GetNumbers(string input)
    {
        return new string(input.Where(c => char.IsDigit(c)).ToArray());
    }
}