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

            Card newCard = newDeck.getTopCard();
            Console.WriteLine("Random card is " + newCard.getType() + " of " + newCard.getSuit());

            Console.WriteLine();

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
        private List<Card> deck = new List<Card>();

        public Deck()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 2; j < 15; j++)
                {
                    this.deck.Add(new Card(getCardValueFromValue(j), getTypeFromValue(j), getSuitFromValue(i)));
                }
            }
            this.deck.Shuffle();
        }

        public List<Card> getDeck()
        {
            return this.deck;
        }

        public Card getTopCard()
        {
            Card card;
            card = this.deck[0];
            this.deck.RemoveAt(0);
            return card;
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

    //Player and dealer classes
    class Person
    {
        private List<Card> hand = new List<Card>();
        private string name;
        private int score;

        public Person(string name)
        {
            this.name = name;
        }

        public void addCardToHand(Card card)
        {
            this.hand.Add(card);
        }

        public void getScore()
        {
            foreach (Card card in hand)
            {
                this.score = this.score + card.getValue();
            }

        }

        public List<Card> getHand()
        {
            return this.hand;
        }
    }

    class Player : Person
    {

    }

    class Dealer : Person
    {

    }

    public static class IListExtensions
    {
        /// <summary>
        /// Shuffles the element order of the specified list.
        /// </summary>
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

}
