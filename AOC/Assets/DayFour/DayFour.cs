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
        List<Card> cards = new List<Card>();
        foreach (var card in scratchCards)
        {
            Card newCard = new Card();
            //split the line ysing '|' as the delimiter
            string[] splitCard = card.Split('|');
            string winningNumbers = splitCard[0].Remove(0, 9);
            Debug.Log(winningNumbers);
            string[] winningNumbersArray = winningNumbers.Split(' ');
            List<int> winningNumbersList = new List<int>();
            foreach (var number in winningNumbersArray)
            {
                if (number != "")
                {
                    winningNumbersList.Add(int.Parse(number));
                }
            }
            //add the winning numbers to the cards


            string[] myNumbers = splitCard[1].Split(' ');
            List<int> myNumbersList = new List<int>();
            foreach (var number in myNumbers)
            {
                if (number != "")
                {
                    myNumbersList.Add(int.Parse(number));
                }
            }

            newCard.winningNumbers = winningNumbersList;
            newCard.myNumbers = myNumbersList;

            cards.Add(newCard);
        }

        for (var i = 0; i < cards.Count; i++)
        {
            int matchingNumbers = 0;
            var card = cards[i];
            foreach (var number in card.myNumbers)
            {
                if (card.winningNumbers.Contains(number))
                {
                    matchingNumbers++;
                }
            }

            if (matchingNumbers == 0)
            {
                Debug.Log("no matches :O");
            }
            
            for (int j = 1; j <= matchingNumbers; j++)
            {
                cards[i+j].copies += cards[i].copies ;
            }
        }

        int copiesCount = 0;
        foreach (var card in cards)
        {
            copiesCount += card.copies;
        }
        
        Debug.Log(copiesCount);
    }

    public class Card
    {
        public List<int> winningNumbers;
        public List<int> myNumbers;
        public int copies = 1;
    }
}