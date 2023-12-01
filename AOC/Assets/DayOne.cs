using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Serialization;

public class DayOne : MonoBehaviour
{
    public TextAsset puzzleInput;

    [FormerlySerializedAs("numbersInString")]
    private Dictionary<string, int> numberTable =
        new Dictionary<string, int>
        {
            { "zero", 0 }, { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 },
            { "five", 5 }, { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 }
        };

    // Start is called before the first frame update


    void Start()
    {
        RealDealSolution();
    }

    void RealDealSolution()
    {
        int puzzleTwoAnswer = 0;
        //get numbers from each line of text
        List<NumberWithIndex> stringNumbers = new List<NumberWithIndex>();

        List<string> lines = puzzleInput.text.Split('\n').ToList();

        foreach (var line in lines)
        {
            if (line != "")
            {
                int calibrationNumber;
                GetWrittenNumbers(stringNumbers, line);
                GetNumericNumbers(stringNumbers, line);

                //sort stringNumbers by index
                stringNumbers = stringNumbers.OrderBy(n => n.index).ToList();
                //get lowest index number and add it to the calibration number times 10
                calibrationNumber = stringNumbers[0].number * 10;
                //get highest index number and add it to the calibration number
                calibrationNumber += stringNumbers[^1].number;

                puzzleTwoAnswer += calibrationNumber;
                stringNumbers.Clear();
            }
        }

        Debug.Log(puzzleTwoAnswer);
    }


    //extract written numbers from text

    void GetWrittenNumbers(List<NumberWithIndex> n, string line)
    {
        foreach (var comparisonNumber in numberTable)
        {
            if (line.Contains(comparisonNumber.Key))
            {
                //get the index of the first character of the number
                int index = line.IndexOf(comparisonNumber.Key);
                //add the number to the list
                n.Add(new NumberWithIndex()
                {
                    number = comparisonNumber.Value,
                    index = index
                });
                //check to see if the number appears again in the string and add it to the list with its index
                if (line.LastIndexOf(comparisonNumber.Key) != -1)
                {
                    n.Add(new NumberWithIndex()
                    {
                        number = comparisonNumber.Value,
                        index = line.LastIndexOf(comparisonNumber.Key)
                    });
                }
            }
        }
    }

    void GetNumericNumbers(List<NumberWithIndex> n, string line)
    {
        //for each number in the string, add it to the list with its index
        foreach (var number in GetNumbers(line))
        {
            n.Add(new NumberWithIndex()
            {
                number = number,
                index = line.IndexOf(number.ToString())
            });

            if (line.LastIndexOf(number.ToString()) != -1)
            {
                n.Add(new NumberWithIndex()
                {
                    number = number,
                    index = line.LastIndexOf(number.ToString())
                });
            }
        }
    }


    void BinnedSolution()
    {
        /*      int puzzleTwoAnswer = 0;

             List<NumberWithIndex> stringNumbers = new List<NumberWithIndex>();
             puzzleInput.text.Split('\n').ToList().ForEach(line =>
             {
                 int calibrationNumber = 0;
                 if (!string.IsNullOrEmpty(line))
                 {
                     // extract text numbers and store index
                     foreach (var checkingNumber in numberTable.Keys)
                     {
                       //  addNumbers(line, checkingNumber, stringNumbers);
                     }

                     string calibrationIntegersText = GetNumbers(line);

                     if (calibrationIntegersText.Length >= 2)
                     {
                         //add first and last number to stringNumbers
                         stringNumbers.Add(new NumberWithIndex()
                         {
                             number = calibrationIntegersText[0].ToString(),
                             index = line.IndexOf(calibrationIntegersText[0])
                         });

                         stringNumbers.Add(new NumberWithIndex()
                         {
                             number = calibrationIntegersText[^1].ToString(),
                             index = line.LastIndexOf(calibrationIntegersText[^1])
                         });
                     }
                     else if (calibrationIntegersText.Length == 1)
                     {
                         //add first and last number to stringNumbers
                         stringNumbers.Add(new NumberWithIndex()
                         {
                             number = calibrationIntegersText[0].ToString(),
                             index = line.IndexOf(calibrationIntegersText[0])
                         });
                     }

                     //sort stringNumbers by index
                     stringNumbers = stringNumbers.OrderBy(n => n.index).ToList();
                     if (stringNumbers[0].number.Length == 1)
                     {
                         calibrationNumber = (int.Parse(stringNumbers[0].number) * 10);
                     }
                     else
                     {
                         calibrationNumber = numberTable[stringNumbers[0].number] * 10;
                     }

                     if (stringNumbers[^1].number.Length == 1)
                     {
                         calibrationNumber += int.Parse(stringNumbers[^1].number);
                     }
                     else
                     {
                         calibrationNumber += numberTable[stringNumbers[^1].number];
                     }


                     puzzleTwoAnswer += calibrationNumber;
                     //write calibration number to text file
                     File.AppendAllText("Assets/DayOneCalibrationNumbers.txt", calibrationNumber + "\n");
                 }


                 stringNumbers.Clear();
             });

             Debug.Log(puzzleTwoAnswer);*/
    }

    // Update is called once per frame
    void Update()
    {
        /*string calibrationNumberText = GetNumbers(line);
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
                     puzzleOneAnswer += calibrationNumber;*/
    }

    private List<int> GetNumbers(string input)
    {
        List<int> singleDigitIntegers = new List<int>();

        // Use regular expression to find all numeric substrings
        MatchCollection matches = Regex.Matches(input, @"\d");

        // Parse each match and add to the list if it's a single-digit number
        foreach (Match match in matches)
        {
            if (int.TryParse(match.Value, out int num) && num >= 0 && num <= 9)
            {
                singleDigitIntegers.Add(num);
            }
        }

        return singleDigitIntegers;

        return null;
    }


    struct NumberWithIndex
    {
        public int number;
        public int index;
    }
}