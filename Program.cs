using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck newDeck = new Deck();
            newDeck.buildCards();

            List<Card> cardList = new List<Card>(newDeck.getDeck());
            Console.WriteLine(cardList.Count);
            foreach (Card card in newDeck.getDeck())
            {
                Console.WriteLine("Value: " + card.getValue() + ";  Type: " + card.getType() + ";  Suit: " + card.getSuit()) ;
            }
            
        }
    }

    //card class

    class Card
    {
        private int value;
        private string type;
        private string suit;

        public Card(int value, string type, string suit)
        {
            this.value = value;
            this.type = type;
            this.suit = suit;
        }

        public int getValue()
        {
            return this.value;
        }

        public string getType()
        {
            return this.type;
        }

        public string getSuit()
        {
            return this.suit;
        }
    }

    class Deck
    {
        private List<Card> sortedDeck = new List<Card>();
        public void buildCards()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 2; j < 15; j++)
                {
                    sortedDeck.Add(new Card(getCardValueFromValue(j), getTypeFromValue(j), getSuitFromValue(i)));
                }
            }
        }

        public List<Card> getDeck()
        {
            return this.sortedDeck;
        }

        private int getCardValueFromValue(int value)
        {
            if (value == 14)
            {
                return 11;
            }
            else if (value > 10)
            {
                return 10;
            } else
            {
                return value;
            }
        }

        private string getTypeFromValue(int value)
        {
            switch (value)
            {
                case 2:
                    return "two";
                case 3:
                    return "three";
                case 4:
                    return "four";
                case 5:
                    return "five";
                case 6:
                    return "six";
                case 7:
                    return "seven";
                case 8:
                    return "eight";
                case 9:
                    return "nine";
                case 10:
                    return "ten";
                case 11:
                    return "jack";
                case 12:
                    return "queen";
                case 13:
                    return "king";
                case 14:
                    return "ace";
            }

            throw new System.ArgumentException("Cannot find this type from value = " + value);
        }

        private string getSuitFromValue(int value)
        {
            switch (value)
            {
                case 0:
                    return "hearts";
                case 1:
                    return "diamonds";
                case 2:
                    return "spades";
                case 3:
                    return "clubs";
            }
            throw new System.ArgumentException("Cannot find this suit from value = " + value);
        }

    }


    //deck and hand classes


    //Player and dealer classes
    class Person
    {

    }

    class Player : Person
    {

    }

    class Dealer : Person
    {

    }
}
