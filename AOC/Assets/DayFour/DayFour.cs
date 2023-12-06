using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DayFour : MonoBehaviour
{
    public TextAsset puzzleInput;

    private void Start()
    {
        //get each line of text
        List<string> scratchCards = puzzleInput.text.Split('\n').ToList();
        //remove any empty lines
        scratchCards.RemoveAll(l => l == "");
        int totalPoints = 0;
        foreach (var card in scratchCards)
        {
            //split the line ysing '|' as the delimiter
            string[] splitCard = card.Split('|');
            string winningNumbers = splitCard[0].Remove(0, 9);
            Debug.Log(winningNumbers);
            string[] winningNumbersArray = winningNumbers.Split(' ');
            List<int> winningNumbersList = new List<int>();
            foreach (var number in winningNumbersArray)
            {
                if (number !="")
                {
                    winningNumbersList.Add(int.Parse(number));
                }
                
            }
            
            string[] myNumbers = splitCard[1].Split(' ');
            List<int> myNumbersList = new List<int>();
            foreach (var number in myNumbers)
            {
                if (number !="")
                {
                    myNumbersList.Add(int.Parse(number));
                }
                
            }
            
            //check for matching numbers in the lists
            int matchingNumbers = 0;
            foreach (var number in myNumbersList)
            {
                if (winningNumbersList.Contains(number))
                {
                    matchingNumbers++;
                }
            }

            if (matchingNumbers != 0)
            {
                totalPoints += (int)Mathf.Pow(2.0f, (float)(matchingNumbers - 1));
            }
        }
        Debug.Log(totalPoints);
        
    }
}