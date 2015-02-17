﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace DragonJack
{
    class Card
    {
        static Random random = new Random();
        private int cardStrength;
        private int cardValue;
        private int cardSuit;

        // Get methods for card fields
        public int CardStrength
        {
            get
            {
                return this.cardStrength;
            }
        }
        public int CardValue
        {
            get
            {
                return this.cardValue;
            }
        }
        public int CardSuit
        {
            get
            {
                return this.cardSuit;
            }
        }

        // Get a random card from the deck
        public Card(int[,] deck)
        {
            int[] result = new int[3];
            int row;
            int col;
            do
            {
                row = random.Next(0, 4);
                col = random.Next(0, 13);
                this.cardSuit = row;
                this.cardStrength = col;
                switch (col)
                {
                    case 0: this.cardValue = 11; break;
                    case 1: this.cardValue = 2; break;
                    case 2: this.cardValue = 3; break;
                    case 3: this.cardValue = 4; break;
                    case 4: this.cardValue = 5; break;
                    case 5: this.cardValue = 6; break;
                    case 6: this.cardValue = 7; break;
                    case 7: this.cardValue = 8; break;
                    case 8: this.cardValue = 9; break;
                    case 9: this.cardValue = 10; break;
                    case 10: this.cardValue = 10; break;
                    case 11: this.cardValue = 10; break;
                    case 12: this.cardValue = 10; break;
                }

            } while (deck[row, col] == 0);
            deck[row, col]--;

        }

        public void PrintBack(int x, int y)
        {
            string[] deckLines = new string[7];
            deckLines[0] = "┌───────┐";
            deckLines[1] = "│░░░░░░░│";
            deckLines[2] = "│░░░░░░░│";
            deckLines[3] = "│░░░░░░░│";
            deckLines[4] = "│░░░░░░░│";
            deckLines[5] = "│░░░░░░░│";
            deckLines[6] = "└───────┘";

            for (int i = 0; i < DragonJackGame.cardHeight; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(deckLines[i]);
            }
        }

        // Print a card
        public void PrintCard(int x, int y)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            string[] cardStrengths = { "A ", "2 ", "3 ", "4 ", "5 ", "6 ", "7 ", "8 ", "9 ", "10", "J ", "Q ", "K " };
            string[] cardLines = new string[DragonJackGame.cardHeight];
            string cardStrength = cardStrengths[this.cardStrength];
            if (this.cardSuit == 0)
            {
                cardLines[0] = "┌───────┐";
                cardLines[1] = "│" + cardStrength + " _   │";
                cardLines[2] = "│  ( )  │";
                cardLines[3] = "│ (_X_) │";
                cardLines[4] = "│   I   │";
                cardLines[5] = "│     " + cardStrength + "│";
                cardLines[6] = "└───────┘";
            }
            else if (this.cardSuit == 1)
            {
                cardLines[0] = "┌───────┐";
                cardLines[1] = "│" + cardStrength + " ^   │";
                cardLines[2] = "│  / \\  │";
                cardLines[3] = "│  \\ /  │";
                cardLines[4] = "│   v   │";
                cardLines[5] = "│     " + cardStrength + "│";
                cardLines[6] = "└───────┘";
            }
            else if (this.cardSuit == 2)
            {
                cardLines[0] = "┌───────┐";
                cardLines[1] = "│" + cardStrength + " ^   │";
                cardLines[2] = "│  / \\  │";
                cardLines[3] = "│ (_^_) │";
                cardLines[4] = "│   I   │";
                cardLines[5] = "│     " + cardStrength + "│";
                cardLines[6] = "└───────┘";
            }
            else
            {
                cardLines[0] = "┌───────┐";
                cardLines[1] = "│" + cardStrength + "_ _  │";
                cardLines[2] = "│ ( V ) │";
                cardLines[3] = "│  \\ /  │";
                cardLines[4] = "│   V   │";
                cardLines[5] = "│     " + cardStrength + "│";
                cardLines[6] = "└───────┘";
            }

            for (int i = 0; i < DragonJackGame.cardHeight; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(cardLines[i], DragonJackGame.cardWidth);
            }
        }
    }

    class CardHand
    {
        List<Card> cards = new List<Card>();

        public void FillHand(Card card)
        {
            this.cards.Add(card);
        }

        public List<Card> GetCardsInHand()
        {
            return this.cards;
        }

        private int AcesCount()
        {
            int acesCount = 0;

            foreach (var card in cards)
            {
                // There is an Ace
                if (card.CardStrength == 0)
                {
                    acesCount++;
                }
            }
            return acesCount;
        }

        public int GetSum()
        {
            int sum = 0;

            foreach (var card in cards)
            {
                sum += card.CardValue;
            }

            int acesCount = 0;
            if (sum > 21)
            {
                acesCount = AcesCount();
                if (acesCount != 0)
                {
                    sum -= 10;
                    acesCount--;
                    //do
                    //{
                    //    sum -= 10;
                    //    acesCount--;
                    //} while (sum > 21);
                }
            }
            return sum;
        }

        public bool AreThereEqualCards()
        {
            if (cards[0].CardStrength == cards[1].CardStrength)
            {
                return true;
            }
            return false;
        }

        public bool IsThereDragonJack()
        {
            if (cards[0].CardValue + cards[1].CardValue == 21)
	        {
                return true;
	        }
            return false;
        }
    }

    public class DragonJackGame
    {
        public static Random random = new Random();
        public static int winWidth = 120;
        public static int winHeight = 40;
        // 105 = 21 cards max
        public static int dealerPositionX = (winWidth - 105) / 2;
        public static int dealerPositionY = winHeight / 10;
        public static int cardHeight = 7;
        public static int cardWidth = 10;
        public static int playerPositionX = (winWidth - 105) / 2;
        public static int playerPositionY = winHeight - winHeight / 10 - cardHeight;
        public static int decksCount = 6;
        public static int suitsCount = 4;
        public static int cardStrengthsCount = 13;

        static void Main()
        {
            // Set console width and height
            InitializeConsoleScreen();

            // Get new deck made of 6 decks
            int[,] deck = NewDeck();

            CardHand playerCards = new CardHand();
            CardHand dealerCards = new CardHand();
            // Initial dealing
            playerCards.FillHand(new Card(deck));
            dealerCards.FillHand(new Card(deck));
            playerCards.FillHand(new Card(deck));
            dealerCards.FillHand(new Card(deck));

            playerCards.GetCardsInHand()[0].PrintCard(playerPositionX, playerPositionY);
            playerCards.GetCardsInHand()[1].PrintCard(playerPositionX + 5, playerPositionY);
            dealerCards.GetCardsInHand()[0].PrintCard(dealerPositionX, dealerPositionY);
            dealerCards.GetCardsInHand()[1].PrintBack(dealerPositionX + 5, dealerPositionY);

            Console.SetCursorPosition(playerPositionX - 3, playerPositionY);
            Console.WriteLine(playerCards.GetSum());

            Console.SetCursorPosition(dealerPositionX - 3, dealerPositionY);
            Console.WriteLine(dealerCards.GetSum());
            //for (int i = 0; i < 11; i++)
            //{
            //    printCard(i, (winWidth - 55) / 2 + i * 5, winHeight - 7 - winHeight / 10);
            //}

            // Print options acording to the cards we have
            PrintLegend(playerCards.AreThereEqualCards());

            ConsoleKeyInfo key = Console.ReadKey();
            //do
            //{
            //    key = Console.ReadKey();
            //    switch (key.Key)
            //    {
            //        case ConsoleKey.Spacebar: if (playerCards.AreThereEqualCards()) { /*TODO: Implement Split;Print Cards(each Part on new line)*/  } break;
            //        case ConsoleKey.Z: HittingMethod(playerCards, deck); break;
            //        case ConsoleKey.X: StandingMethod(dealerCards, deck); break;
            //        case ConsoleKey.C: /*TODO: Implement Double*/ break;
            //        default: Console.WriteLine("Invalid command!"); break;
            //        // TODO: Bets
            //        //Another TODO: Check win, push or bust...
            //    }
            //} while (key.Key != ConsoleKey.Escape);
            while (key.Key != ConsoleKey.Escape)
            {
                switch (key.Key)
                {
                    case ConsoleKey.Spacebar: if (playerCards.AreThereEqualCards()) { /*TODO: Implement Split;Print Cards(each Part on new line)*/  } break;
                    case ConsoleKey.Z: HittingMethod(playerCards, deck); break;
                    case ConsoleKey.X: StandingMethod(dealerCards, deck); break;
                    case ConsoleKey.C: /*TODO: Implement Double*/ break;
                    default: Console.WriteLine("Invalid command!"); break;
                    // TODO: Bets
                    //Another TODO: Check win, push or bust...
                }

                key = Console.ReadKey();
            }

            //TODO: Make "Press any key to continue" disappear!
            //Console.ReadKey();
            //Console.CursorVisible = false;
        }

        static void InitializeConsoleScreen()
        {
            Console.BufferHeight = winHeight;
            Console.BufferWidth = winWidth;
            Console.SetWindowSize(winWidth, winHeight);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            PrintDeck(winWidth - winWidth / cardWidth, (winHeight - 3) / 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        static void HittingMethod(CardHand playerCards, int[,] deck)
        {
            playerCards.FillHand(new Card(deck));
            int playerCardIndex = playerCards.GetCardsInHand().Count - 1;

            playerCards.GetCardsInHand()[playerCardIndex]
                       .PrintCard(playerPositionX + playerCardIndex * 5, playerPositionY);

            Console.SetCursorPosition(playerPositionX - 3, playerPositionY);
            Console.WriteLine(playerCards.GetSum());
        }

        static void StandingMethod(CardHand dealerCards, int[,] deck)
        {
            dealerCards.GetCardsInHand()[1].PrintCard(dealerPositionX + 5, dealerPositionY);
            // With this two conditions,the while loop is infinite, so removed <= 21
            //while (dealerCards.GetSum() <= 21)
            //{
            //    if (dealerCards.GetSum() < 17)
            //    {
            //        Thread.Sleep(1000);
            //        dealerCards.FillHand(new Card(deck));

            //        dealerCards.GetCardsInHand()[dealerCards.GetCardsInHand().Count - 1]
            //          .PrintCard(dealerPositionX + (dealerCards.GetCardsInHand().Count - 1) * 5, dealerPositionY);
            //        Thread.Sleep(1000);
            //    }
            //}

            
            
            while (dealerCards.GetSum() < 17)
            {
                Thread.Sleep(1000);
                dealerCards.FillHand(new Card(deck));
                int dealerCardIndex = dealerCards.GetCardsInHand().Count - 1;

                dealerCards.GetCardsInHand()[dealerCardIndex]
                           .PrintCard(dealerPositionX + dealerCardIndex * 5, dealerPositionY);
                Thread.Sleep(1000);
                //for (int i = playerCards.GetCardsInHand().Count - 1; i < playerCards.GetCardsInHand().Count; i++)
                //{

                //}
            }
            Console.SetCursorPosition(dealerPositionX - 3, dealerPositionY);
            Console.WriteLine(dealerCards.GetSum());
        }

        private static void PrintLegend(bool printSplit)
        {
            List<string> options = new List<string>();
            options.Add("Z -> Hit");
            options.Add("X -> Stand");
            options.Add("C -> Double");

            if (printSplit)
            {
                options.Add("Space -> Split");
            }

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < options.Count; i++)
            {
                Console.SetCursorPosition(5, 17 + (i * 2));
                Console.WriteLine(options[i]);
                Console.WriteLine();
            }

        }

        // TODO: New method - Print available options
        // Parameters - Player's cards, Output - void
        static int[,] NewDeck()
        {
            int[,] deck = new int[suitsCount, cardStrengthsCount];
            for (int i = 0; i < deck.GetLength(0); i++)
            {
                for (int j = 0; j < deck.GetLength(1); j++)
                {
                    deck[i, j] = decksCount;
                }
            }
            return deck;
        }

        static void PrintDeck (int x, int y)
        {
            string[] deckLines = new string[7];
            deckLines[0] = "┌───────┐";
            deckLines[1] = "║░░░░░░░│";
            deckLines[2] = "║░░░░░░░│";
            deckLines[3] = "║░░░░░░░│";
            deckLines[4] = "║░░░░░░░│";
            deckLines[5] = "║░░░░░░░│";
            deckLines[6] = "╚═══════┘";

            for (int i = 0; i < cardHeight; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(deckLines[i]);
            }
        }
        
    }

}
