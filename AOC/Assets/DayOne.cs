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

    void addNumbers(string line, string checkingNumber, List<NumberWithIndex> stringNumbers, int residualIndex = 0)
    {
        if (line.Contains(checkingNumber))
        {
            stringNumbers.Add(new NumberWithIndex()
            {
                number = checkingNumber,
                index = line.IndexOf(checkingNumber) + residualIndex
            });
            addNumbers(line.Substring(line.IndexOf(checkingNumber) + 1), checkingNumber, stringNumbers, residualIndex + line.IndexOf
                (checkingNumber) + checkingNumber.Length);
        }
    }

    void Start()
    {
       
    }

    void RealDealSolution()
    {
        //get numbers from each line of text
        List<NumberWithIndex> stringNumbers = new List<NumberWithIndex>();
        
        List<string> lines = puzzleInput.text.Split('\n').ToList();

        foreach (var line in lines)
        {
            GetWrittenNumbers(stringNumbers);
            GetNumericNumbers(stringNumbers);
        }
    }
    
    
    //extract written numbers from text

    void GetWrittenNumbers(List<NumberWithIndex> n)
    {
       
        
        
    }
    
    void GetNumericNumbers(List<NumberWithIndex> n)
    {
        
    }
    
    
    
    
    void BinnedSolution()
    {
         int puzzleTwoAnswer = 0;

        List<NumberWithIndex> stringNumbers = new List<NumberWithIndex>();
        puzzleInput.text.Split('\n').ToList().ForEach(line =>
        {
            int calibrationNumber = 0;
            if (!string.IsNullOrEmpty(line))
            {
                // extract text numbers and store index
                foreach (var checkingNumber in numberTable.Keys)
                {
                    addNumbers(line, checkingNumber, stringNumbers);
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

        Debug.Log(puzzleTwoAnswer);
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

    private string GetNumbers(string input)
    {
        return new string(input.Where(c => char.IsDigit(c)).ToArray());
    }


    struct NumberWithIndex
    {
        public string number;
        public int index;
    }
}