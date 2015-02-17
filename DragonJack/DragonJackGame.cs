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
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            string[] deckLines = new string[DragonJackGame.cardHeight];
            deckLines[0] = "┌───────┐";
            deckLines[1] = "│░░░░░░░│";
            deckLines[2] = "│░░░░░░░│";
            deckLines[3] = "│░░░░░░░│";
            deckLines[4] = "│░░░░░░░│";
            deckLines[5] = "│░░░░░░░│";
            deckLines[6] = "│░░░░░░░│";
            deckLines[7] = "└───────┘";

            for (int i = 0; i < DragonJackGame.cardHeight; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(deckLines[i]);
            }
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        // Print a card
        public void PrintCard(int x, int y)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            string[] cardStrengths = { "A ", "2 ", "3 ", "4 ", "5 ", "6 ", "7 ", "8 ", "9 ", "10", "J ", "Q ", "K " };
            string[] cardLines = new string[DragonJackGame.cardHeight];
            string[] cardSuitLines = new string[4];
            string cardStrength = cardStrengths[this.cardStrength];
            cardLines[0] = "┌───────┐";
            cardLines[1] = "│" + cardStrength + "     │";
            cardLines[2] = "│       │";
            cardLines[3] = "│       │";
            cardLines[4] = "│       │";
            cardLines[5] = "│       │";
            cardLines[6] = "│     " + cardStrength + "│";
            cardLines[7] = "└───────┘";
            
            for (int i = 0; i < DragonJackGame.cardHeight; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(cardLines[i], DragonJackGame.cardWidth);
            }
            if (this.cardSuit == 0)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                cardSuitLines[0] = "  _  ";
                cardSuitLines[1] = " ( ) ";
                cardSuitLines[2] = "(_X_)";
                cardSuitLines[3] = "  I  ";
            }
            else if (this.cardSuit == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                cardSuitLines[0] = "  ^  ";
                cardSuitLines[1] = " / \\ ";
                cardSuitLines[2] = " \\ / ";
                cardSuitLines[3] = "  v  ";
            }
            else if (this.cardSuit == 2)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                cardSuitLines[0] = "  ^  ";
                cardSuitLines[1] = " / \\ ";
                cardSuitLines[2] = "(_^_)";
                cardSuitLines[3] = "  I  ";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                cardSuitLines[0] = " _ _ ";
                cardSuitLines[1] = "( V )";
                cardSuitLines[2] = " \\ / ";
                cardSuitLines[3] = "  V  ";
            }
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(x + 2, y + 2 + i);
                Console.WriteLine(cardSuitLines[i]);
            }
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }

    class Hand
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
            int acesSum = 0;
            foreach (var card in cards)
            {
                sum += card.CardValue;
                if (card.CardValue == 11)
                {
                    acesSum = sum - 10;
                }
            }
            if (sum > 21)
            {
                int acesCount = AcesCount();
                while (acesCount > 0 && sum > 21)
                {
                    sum -= 10;
                    acesCount--;
                }
            }
            return sum;
        }

        public void PrintSum()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Yellow;
            string sums;
            int fullSum = 0;
            foreach (var card in cards)
            {
                fullSum += card.CardValue;
            }
            sums = GetSum().ToString() + "   ";
            int acesCount = AcesCount();
            if (fullSum - (acesCount - 1) * 10 < 21 && acesCount > 0)
            {
                sums = (fullSum - acesCount * 10).ToString() + "/" + GetSum().ToString() + " ";
            }
            
            Console.WriteLine(sums);
        }

        public bool AreEqualCards()
        {
            if (cards[0].CardStrength == cards[1].CardStrength)
            {
                return true;
            }
            return false;
        }

        public bool IsDragonJack()
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
        // 95 = 19 cards max
        public static int dealerPosX = (winWidth - 95) / 2;
        public static int dealerPosY = winHeight / 10;
        public static int cardHeight = 8;
        public static int cardWidth = 10;
        public static int playerPosX = (winWidth - 95) / 2;
        public static int playerPosY = winHeight - winHeight / 10 - cardHeight;
        public static int legendPosX = 5;
        public static int legendPosY = 17;
        public static int decksCount = 6;
        public static int suitsCount = 4;
        public static int cardStrengthsCount = 13;

        static void Main()
        {
            Console.CursorVisible = false;
            // Set console width and height
            InitializeConsoleScreen();

            // Get new deck made of 6 decks
            int[,] deck = NewDeck();

            Hand playerCards = new Hand();
            Hand dealerCards = new Hand();
            // Initial dealing
            playerCards.FillHand(new Card(deck));
            Thread.Sleep(500);
            playerCards.GetCardsInHand()[0].PrintCard(playerPosX, playerPosY);
            Console.SetCursorPosition(playerPosX - 7, playerPosY);
            playerCards.PrintSum();

            dealerCards.FillHand(new Card(deck));
            Thread.Sleep(500);
            dealerCards.GetCardsInHand()[0].PrintCard(dealerPosX, dealerPosY);
            Console.SetCursorPosition(dealerPosX - 7, dealerPosY);
            dealerCards.PrintSum();

            playerCards.FillHand(new Card(deck));
            Thread.Sleep(500);
            playerCards.GetCardsInHand()[1].PrintCard(playerPosX + 5, playerPosY);
            Console.SetCursorPosition(playerPosX - 7, playerPosY);
            playerCards.PrintSum();

            Thread.Sleep(500);
            dealerCards.GetCardsInHand()[0].PrintBack(dealerPosX + 5, dealerPosY);
            
            //for (int i = 0; i < 11; i++)
            //{
            //    printCard(i, (winWidth - 55) / 2 + i * 5, winHeight - 7 - winHeight / 10);
            //}

            // Print options acording to the cards we have
            PrintLegend(playerCards.AreEqualCards());

            ConsoleKeyInfo key = Console.ReadKey(true);
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
                    case ConsoleKey.Spacebar: 
                        if (playerCards.AreEqualCards()) { /*TODO: Implement Split;Print Cards(each Part on new line)*/  } break;
                    case ConsoleKey.Z: 
                        Hitting(playerCards, deck);
                        if (playerCards.GetSum() >= 21)
                        {
                            DealerPlay(dealerCards, playerCards, deck);
                            break;
                        }
                        break;
                    case ConsoleKey.X: 
                        DealerPlay(dealerCards, playerCards, deck); break;
                    case ConsoleKey.C: /*TODO: Implement Double*/ break;
                    default: Console.SetCursorPosition(20, 20);
                             Console.WriteLine("Invalid command!"); break;
                    // TODO: Bets
                    //Another TODO: Check win, push or bust...
                }

                key = Console.ReadKey(true);
            }

        }

        static void InitializeConsoleScreen()
        {
            Console.BufferHeight = winHeight;
            Console.BufferWidth = winWidth;
            Console.SetWindowSize(winWidth, winHeight);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            PrintDeck(winWidth - winWidth / cardWidth, (winHeight - 5) / 2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        static void Hitting(Hand playerCards, int[,] deck)
        {
            playerCards.FillHand(new Card(deck));
            int playerCardIndex = playerCards.GetCardsInHand().Count - 1;

            playerCards.GetCardsInHand()[playerCardIndex]
                       .PrintCard(playerPosX + playerCardIndex * 5, playerPosY);

            Console.SetCursorPosition(playerPosX - 7, playerPosY);
            playerCards.PrintSum();
        }

        static void DealerPlay(Hand dealerCards, Hand playerCards, int[,] deck)
        {
            dealerCards.FillHand(new Card(deck));
            dealerCards.GetCardsInHand()[1].PrintCard(dealerPosX + 5, dealerPosY);
            Console.SetCursorPosition(dealerPosX - 7, dealerPosY);
            dealerCards.PrintSum();
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

            if (playerCards.GetSum() > 21)
            {
                //Console.SetCursorPosition(dealerPositionX - 7, dealerPositionY);
                //Console.WriteLine(dealerCards.GetSum() + "   ");
                return;
            }
            while (dealerCards.GetSum() < 17)
            {
                
                Thread.Sleep(800);
                dealerCards.FillHand(new Card(deck));
                int dealerCardIndex = dealerCards.GetCardsInHand().Count - 1;

                dealerCards.GetCardsInHand()[dealerCardIndex]
                           .PrintCard(dealerPosX + dealerCardIndex * 5, dealerPosY);
                Console.SetCursorPosition(dealerPosX - 7, dealerPosY);
                dealerCards.PrintSum();
                //Thread.Sleep(1000);
                //for (int i = playerCards.GetCardsInHand().Count - 1; i < playerCards.GetCardsInHand().Count; i++)
                //{

                //}
            }
            //Console.SetCursorPosition(dealerPositionX - 7, dealerPositionY);
            //Console.WriteLine(dealerCards.GetSum() + "   ");
        }

        private static void PrintLegend(bool printSplit)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Yellow;
            List<string> options = new List<string>();
            options.Add("Z -> Hit");
            options.Add("X -> Stand");
            options.Add("C -> Double");

            if (printSplit)
            {
                options.Add("Space -> Split");
            }

            
            for (int i = 0; i < options.Count; i++)
            {
                Console.SetCursorPosition(legendPosX, legendPosY + (i * 2));
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
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            string[] deckLines = new string[DragonJackGame.cardHeight];
            deckLines[0] = "┌───────┐";
            deckLines[1] = "║░░░░░░░│";
            deckLines[2] = "║░░░░░░░│";
            deckLines[3] = "║░░░░░░░│";
            deckLines[4] = "║░░░░░░░│";
            deckLines[5] = "║░░░░░░░│";
            deckLines[6] = "║░░░░░░░│";
            deckLines[7] = "╚═══════┘";

            for (int i = 0; i < cardHeight; i++)
            {
                Console.SetCursorPosition(x, y - 1 + i);
                Console.WriteLine(deckLines[i]);
            }
        }
        
    }

}
