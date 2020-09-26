using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blackjack
{
    class Program
    {

        static void Main(string[] args)
        {
            Game g = new Game();
        }

        
    }

    class Game
    {

        private Dealer dealer = new Dealer();
        private List<Player> players = new List<Player>();
        private bool gameOver = false;

        public Game()
        {
            bool noNewPlayers = false;
            string choice;
            while (!noNewPlayers)
            {
                if (players.Count < 5)
                {
                    Console.Write("Would you like to add another player? y/n : ");
                    choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "y":
                            createPlayer();
                            break;
                        case "n":
                            if (players.Count = 0) 
                            {
                                Console.WriteLine("invalid player count, must be at least 1 player");
                            }
                            else 
                            {
                                noNewPlayers = true;
                            }
                            break;
                        default:
                            Console.WriteLine("invalid choice please try again");
                            break;
                    }
                }
                else
                {
                    noNewPlayers = true;
                }
                
            }

            while (!gameOver)
            {
                Console.WriteLine("Welcome to Blackjack");
                Console.WriteLine();
                Console.WriteLine("Would you like to play a new round? y/n  : " );
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "y":
                        Round r = new Round(players, dealer);
                        break;
                    case "n":
                        gameOver = true;
                        break;
                    default:
                        Console.WriteLine("invalid option please try again");
                        break;
                }
            }
        }

        private void createPlayer()
        {
            bool valid = false;
            string name;
            while (!valid)
            {
                Console.Write("Enter Name of new Player : ");
                name = Console.ReadLine();

                bool unique = true;

                foreach (Player player in players)
                {
                    if (name == player.getName())
                    {
                        unique = false;
                    }
                }
                if (unique)
                {
                    if (name.Length > 1 && name.Length < 11)
                    {
                        valid = true;
                        players.Add(new Player(name));
                    }
                    else
                    {
                        Console.WriteLine("name cannot be less than 1 character or greater than 10 characters");
                    }
                }
                else
                {
                    Console.WriteLine("name cannot be the same as another players");
                }

            }
        }

        private void removePlayers()
        {
            foreach (Player player in players)
            {
                if (player.getChips() == 0)
                {
                    players.Remove(player);
                }
            }
        }

    }

    class Round
    {
        private List<Player> players = new List<Player>();
        private Dealer dealer;
        private Deck deck = new Deck();
        private Card card;

        public Round (List<Player> playerList, Dealer d)
        {
            this.players = playerList;
            this.dealer = d;

            foreach (Player player in players)
            {
                player.placeBet();
            }
            writeSpace();

            this.dealCards();

            this.showTable(false);

            bool turnOver = false;

            foreach (Player player in players)
            { 

                turnOver = false;
                while(!turnOver)
                {
                    Console.WriteLine(turnOver);
                    turnOver = player.takeTurn(deck);
                    writeSpace();
                    
                }
            }

            turnOver = false;
            while (!turnOver)
            {
                turnOver = dealer.takeTurn(deck);
                writeSpace();
            }

            showTable(true);
            winners();
        }


        private void showTable(bool endOfRound)
        {
            if (endOfRound)
            {
                string cards = "";
                foreach (Card card in dealer.getHand())
                {
                    cards = cards + " " + card.getType() + " " + card.getSuit() +  "(" + card.getValue() + ")";
                }

                Console.WriteLine("Dealers's Cards :  {0}  ({1})",cards, dealer.getScore());
            }
            else
            {
                Console.WriteLine("Dealer's Cards : {0} {1}, X", dealer.getHand()[0].getType(), dealer.getHand()[0].getSuit());
            }
            
            Console.WriteLine();
            foreach (Player player in players)
            {
                string cards = "";
                foreach (Card card in player.getHand())
                {
                    cards = cards + " " + card.getType() + " " + card.getSuit() +  "(" + card.getValue() + ")";
                }

                Console.WriteLine("{0}'s Cards :  {1}  ({2})", player.getName(), cards, player.getScore());
            }
        }

        private void writeSpace()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }

        private void dealCards()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (Player player in players)
                {
                    card = deck.getTopCard();
                    player.addCardToHand(card);
                }
                card = deck.getTopCard();
                dealer.addCardToHand(card);
            }
        }

        private void winners()
        {
            foreach (Player player in players)
            {
                string outcome = player.getOutcome();
                int payFactor = 0;
                if (outcome == "bust") //if player is bust they lose
                {
                    payFactor = -2;
                }
                else if (outcome == "blackjack" && dealer.getOutcome() == "blackjack") //if player has blackjack and dealer has blackjack then pot is pushed
                {
                    payFactor = 0;
                }
                else if (outcome == "blackjack") //if player has blackjack payouyt is 3/2
                {
                    payFactor = 3;
                }
                else if (dealer.getOutcome() == "bust") //if dealer is bust player wins
                {
                    payFactor = 2;
                }
                else
                {
                    if (player.getScore() > dealer.getScore())
                    {
                        payFactor = 2;
                    }
                    else if (player.getScore() == dealer.getScore())
                    {
                        payFactor = 0;
                    }
                    else
                    {
                        payFactor = -2;
                    }
                }
                player.getWinnings(payFactor);
                player.clearHand();
            }
            dealer.clearHand();
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

        public void setValue(int i){
            this.value = i;
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
        private int score;

        public void addCardToHand(Card card)
        {
            this.score = getScore();
            Console.WriteLine(score + card.getValue());
            if (this.score + card.getValue() > 21 && card.getType() == "ace") //if top card from deck is ace and makes score > 21 then change cards value to 1
            {
                card.setValue(1);
            }
            else if (this.score + card.getValue() > 21) //if top card makes score > 21 and is not an ace then chack hand for an ace to swap
            {
                bool aceSwapped = false;
                int i = 0;
                while (!aceSwapped && i < hand.Count)
                {
                    if (hand[i].getValue() == 11)
                    {
                        hand[i].setValue(1);
                        aceSwapped = true;
                    }
                    i++;
                }
            }
            this.hand.Add(card);
            this.score = this.getScore();
        }

        public int getScore()
        {
            int s = 0;
            foreach (Card card in hand)
            {
                s = s + card.getValue();
            }
            return s;

        }

        public List<Card> getHand()
        {
            return this.hand;
        }

        public void showHand()
        {
            foreach (Card c in hand)
            {
                Console.Write("Persons Cards : " + c.getValue() + " " + c.getType() + " " + c.getSuit());
                Console.WriteLine();
            }
        }

        public void hitHand(Deck deck)
        {
            this.addCardToHand(deck.getTopCard());
        }
        
        public string getOutcome()
        {
            this.score = this.getScore();
            string outcome;
            if (this.score > 21)
            {
                outcome = "bust";
            }
            else if (this.score == 21 && this.hand.Count == 2)
            {
                outcome = "blackjack";
            }
            else if (this.score == 21 && this.hand.Count > 2)
            {
                outcome = "numerical-21";
            }
            else
            {
                outcome = Convert.ToString(this.score);
            }

            return outcome;
        }

        public void clearHand()
        {
            this.hand = new List<Card>();
        }

       
    }

    class Player : Person
    {
        private string name;
        private int chips = 500;
        private int bet;

        public Player(string n)
        {
            this.name = n;
        }

        public string getName()
        {
            return this.name;
        }

        public void placeBet()
        {

            bool validBet = false;

            while (!validBet)
            {
                Console.Write("{0}, How much would you like to bet? : ", this.name);
                try
                {
                    this.bet = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("invalid input to bet :   Error:" + e.Message);
                }

                if (this.bet > chips)
                {
                    Console.WriteLine("You do not have enough chips!");
                }
                else if (this.bet = 0)
                {
                    Console.WriteLine("You cannot bet 0");
                }
                else
                {
                    this.chips = this.chips - this.bet;
                    validBet = true;
                }
            }
            Console.WriteLine("{0}'s bet is {1} chips", this.name, this.bet);
            
        }

        public bool takeTurn(Deck deck)
        {

            bool turnOver = false;
            bool validOption = false;
            turnOver = getTurnOver();
            this.showHand();
            if (!turnOver)
            {
                while (!validOption)
                {
                    Console.WriteLine("You Have " + this.getScore());
                    Console.WriteLine("You Have 2 options: ");
                    Console.WriteLine("1: Hit");
                    Console.WriteLine("2: Stick");
                    Console.Write("Enter the number of the option you want to choose: ");
                    string option = Console.ReadLine();
                    switch (option)
                    {
                        case "1":
                            this.hitHand(deck);
                            validOption = true;
                            turnOver = this.getTurnOver();
                            break;
                        case "2":
                            validOption = true;
                            turnOver = true;
                            break;
                        default:
                            Console.WriteLine("invalid option selection please try again");
                            break;
                    }
                }
            }
            
            return turnOver;

        }

        public bool getTurnOver()
        {
            bool done = false;
            Console.WriteLine("{0}'s Turn", this.name);
            Console.WriteLine("Outcome :  {0}", this.getOutcome());
            if (this.getScore() >= 21)
            {
                done = true;
            }
            return done;
        }

        public void getWinnings(int payFactor)
        {
            int payout = bet;

            payout += ((payout * payFactor) / 2);
            this.chips += payout;
            Console.WriteLine("{3} has {0} and has {1} Players chips is now : {2}", this.getScore(), this.getOutcome(), this.chips, this.name);
        }
    }

    class Dealer : Person
    {
        public bool takeTurn(Deck deck)
        {
            this.showHand();
            Console.WriteLine("Dealer Has " + this.getScore());
            int s = this.getScore();
            if (s >= 17)
            {
                return true;
            }
            else 
            {
                this.hitHand(deck);
                return getTurnOver();
            }     
            
        }

        public bool getTurnOver()
        {
            bool done = false;
            Console.WriteLine("Dealer's Turn");
            Console.WriteLine("Outcome :  {0}", this.getOutcome());

            Console.WriteLine("Dealer's Score : {0}", this.getScore());
            this.showHand();

            if (this.getScore() >= 17)
            {
                done = true;
            }
            return done;
        }

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
