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
            if (cards[0].CardValue == cards[1].CardValue)
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
        public static int winWidth = 160;
        public static int winHeight = 40;
        public static int cardHeight = 8;
        public static int cardWidth = 10;
        public static int suitsCount = 4;
        public static int cardStrengthsCount = 13;
        public static int decksCount = 6;
        public static int maxCardsWidth = (8 + (decksCount - 1)) * 5;
        public static int dealerPosX = (winWidth - maxCardsWidth) / 2;
        public static int dealerPosY = winHeight / 10;
        public static int playerPosX = (winWidth - maxCardsWidth) / 2;
        public static int playerPosY = winHeight - winHeight / 10 - cardHeight;
        public static int doublePosX1 = (winWidth - (maxCardsWidth) * 2) / 4;
        public static int doublePosY1 = playerPosY;
        public static int doublePosX2 = doublePosX1 + maxCardsWidth + doublePosX1;
        public static int doublePosY2 = playerPosY;
        public static int legendPosX = 5;
        public static int legendPosY = 17;
        
        
        

        static void Main()
        {
            //TODO: Catch exception
            //Unhandled Exception: System.ArgumentOutOfRangeException: The value must be less than the console's current maximum window size of 113 in that dimensio
            //n. Note that this value depends on screen resolution and the console font.
            //Parameter name: width
            //Actual value was 150.
            //   at System.Console.SetWindowSize(Int32 width, Int32 height)
            //   at DragonJack.DragonJackGame.IntroScreen() in c:\Users\ilia\Downloads\Dragon-Jack-master\Dragon-Jack-master\DragonJack\DragonJackGame.cs:line 670
            //   at DragonJack.DragonJackGame.Main() in c:\Users\ilia\Downloads\Dragon-Jack-master\Dragon-Jack-master\DragonJack\DragonJackGame.cs:line 278
            //Press any key to continue . . .

            Console.CursorVisible = false;
            IntroScreen();
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

            dealerCards.FillHand(new Card(deck));
            Thread.Sleep(500);
            dealerCards.GetCardsInHand()[1].PrintBack(dealerPosX + 5, dealerPosY);

            if (playerCards.IsDragonJack() ^ dealerCards.IsDragonJack())
            {
                DragonJacking(dealerCards);
                Console.ReadKey(true);
                return;
            }
            //for (int i = 0; i < 11; i++)
            //{
            //    printCard(i, (winWidth - 55) / 2 + i * 5, winHeight - 7 - winHeight / 10);
            //}

            // Print options acording to the cards we have
            PrintLegend(playerCards.AreEqualCards(), playerCards.GetCardsInHand().Count);
            

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

            while (key.Key != ConsoleKey.Escape )
            {
                switch (key.Key)
                {

                    case ConsoleKey.Spacebar:
                        DeleteLegend();
                        PrintLegend(false, playerCards.GetCardsInHand().Count + 1);
                        if (playerCards.AreEqualCards())
                        {
                            int splitResult = Splitting(playerCards, deck);
                            DealerPlay(dealerCards, splitResult, deck);

                        }
                        else
                        {
                            InvalidInput();
                        }
                        break;
                    case ConsoleKey.Z: 
                        Hitting(playerCards, deck, playerPosX, playerPosY);
                        DeleteLegend();
                        PrintLegend(false, playerCards.GetCardsInHand().Count);
                        if (playerCards.GetSum() >= 21)
                        {
                            DeleteLegend();
                            DealerPlay(dealerCards, playerCards.GetSum(), deck);
                            break;
                        }
                        break;
                    case ConsoleKey.X:
                        DeleteLegend();
                        DealerPlay(dealerCards, playerCards.GetSum(), deck); break;
                    case ConsoleKey.C: /*TODO: Implement Double*/ break;
                    default: InvalidInput(); 
                             break;
                    // TODO: Bets
                    //Another TODO: Check win, push or bust...
                }

                key = Console.ReadKey(true);
            }

        }

        static void InitializeConsoleScreen()
        {
            //Console.WindowHeight = winHeight;
            //Console.WindowWidth = winWidth;
            Console.BufferHeight = winHeight;
            Console.BufferWidth = winWidth;
            Console.SetWindowSize(winWidth, winHeight);
            Console.Title = "DRAGONJACK";
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            PrintDeck(winWidth - winWidth / cardWidth, (winHeight - 5) / 2);
        }


        static void Hitting(Hand playerCards, int[,] deck, int x, int y)
        {
            playerCards.FillHand(new Card(deck));
            int playerCardIndex = playerCards.GetCardsInHand().Count - 1;

            playerCards.GetCardsInHand()[playerCardIndex]
                       .PrintCard(x + playerCardIndex * 5, y);

            Console.SetCursorPosition(x - 7, y);
            playerCards.PrintSum();
        }

        static void DealerPlay(Hand dealerCards, int playerCardsSum, int[,] deck)
        {
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

            if (playerCardsSum > 21)
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

        static int Splitting( Hand playerCards, int[,] deck)
        {
            DeleteCardAndSum(playerPosX, playerPosY);
            DeleteCardAndSum(playerPosX + 5, playerPosY);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Hand playerCards1 = new Hand();
            Hand playerCards2 = new Hand();

            playerCards1.FillHand(playerCards.GetCardsInHand()[0]);
            playerCards1.GetCardsInHand()[0].PrintCard(doublePosX1, doublePosY1);
            Console.SetCursorPosition(doublePosX1 - 7, doublePosY1);
            playerCards1.PrintSum();

            playerCards2.FillHand(playerCards.GetCardsInHand()[1]);
            playerCards2.GetCardsInHand()[0].PrintCard(doublePosX2, doublePosY2);
            Console.SetCursorPosition(doublePosX2 - 7, doublePosY2);
            playerCards2.PrintSum();

            playerCards1.FillHand(new Card(deck));
            Thread.Sleep(500);
            playerCards1.GetCardsInHand()[1].PrintCard(doublePosX1 + 5, doublePosY1);
            Console.SetCursorPosition(doublePosX1 - 7, doublePosY1);
            playerCards1.PrintSum();

            playerCards2.FillHand(new Card(deck));
            Thread.Sleep(500);
            playerCards2.GetCardsInHand()[1].PrintCard(doublePosX2 + 5, doublePosY2);
            Console.SetCursorPosition(doublePosX2 - 7, doublePosY2);
            playerCards2.PrintSum();

            if (playerCards1.GetSum() < 21 || playerCards2.GetSum() < 21 )
            {
                bool playDouble = true;
                while (playDouble)
                {
                    if (playerCards1.GetSum() < 21)
                    {
                        PrintArrow(doublePosX1, doublePosY1);
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        switch (key.Key)
                        {
                            case ConsoleKey.Z:
                                Hitting(playerCards1, deck, doublePosX1, doublePosY1);
                                if (playerCards1.GetSum() >= 21)
                                {
                                    DeleteArrow(doublePosX1, doublePosY1);
                                    if (playerCards2.GetSum() < 21)
                                    {
                                        PrintArrow(doublePosX2, doublePosY2);
                                        key = Console.ReadKey(true);
                                        while (key.Key != ConsoleKey.Escape)
                                        {
                                            
                                            switch (key.Key)
                                            {
                                                case ConsoleKey.Z:
                                                    Hitting(playerCards2, deck, doublePosX2, doublePosY2);
                                                    break;
                                                case ConsoleKey.X: break;
                                                case ConsoleKey.Escape: break;
                                                default: InvalidInput();
                                                    break;
                                            }
                                            if (key.Key == ConsoleKey.X || playerCards2.GetSum() >= 21)
                                            {
                                                DeleteArrow(doublePosX2, doublePosY2);
                                                playDouble = false;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        playDouble = false;
                                        break;
                                    }
                                }
                                break;
                            case ConsoleKey.X:
                                DeleteArrow(doublePosX1, doublePosY1);
                                if (playerCards2.GetSum() < 21)
                                {
                                    PrintArrow(doublePosX2, doublePosY2);
                                    key = Console.ReadKey(true);
                                    while (key.Key != ConsoleKey.Escape)
                                    {
                                        switch (key.Key)
                                        {
                                            case ConsoleKey.Z:
                                                Hitting(playerCards2, deck, doublePosX2, doublePosY2);
                                                if (playerCards2.GetSum() >= 21)
                                                {
                                                    break;
                                                }
                                                break;
                                            case ConsoleKey.X: break;
                                            default: InvalidInput(); break;
                                        }
                                        if (key.Key == ConsoleKey.X || playerCards2.GetSum() >= 21)
                                        {
                                            DeleteArrow(doublePosX2, doublePosY2);
                                            playDouble = false;
                                            break;
                                        }
                                    }
                                }
                                break;
                            default: InvalidInput(); break;
                        }
                    }
                    else
                    {
                        DeleteArrow(doublePosX1, doublePosY1);
                        PrintArrow(doublePosX2, doublePosY2);
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        while (key.Key != ConsoleKey.Escape)
                        {
                            switch (key.Key)
                            {
                                case ConsoleKey.Z:
                                    Hitting(playerCards2, deck, doublePosX2, doublePosY2);
                                    if (playerCards2.GetSum() >= 21)
                                    {
                                        break;
                                    }
                                    break;
                                case ConsoleKey.X: break;
                                default: InvalidInput(); break;
                            }
                            if (key.Key == ConsoleKey.X || playerCards2.GetSum() >= 21)
                            {
                                DeleteArrow(doublePosX2, doublePosY2);
                                playDouble = false;
                                break;
                            }
                        }
                        
                    }

                    if (!playDouble)
                    {
                        break;
                    }
                }
            }
            int playerSum1 = playerCards1.GetSum();
            int playerSum2 = playerCards2.GetSum();
            if (playerSum1 < 21  || playerSum2 < 21)
            {
                if (playerSum2 < 21)
                {
                    return playerSum2;
                }
                return playerSum1;
            }
            else
            {
                return playerSum1;
            }
        }

        //method Doubling

        private static void DragonJacking(Hand dealerCards)
        {
            
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            DeleteLegend();
            if (dealerCards.GetSum() == 21)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                string[] dragonjackingDealer = new string[6];
                dragonjackingDealer[0] = @"  ____                               _            _    ";
                dragonjackingDealer[1] = @" |  _ \ _ __ __ _  __ _  ___  _ __  (_) __ _  ___| | _ ";
                dragonjackingDealer[2] = @" | | | | '__/ _` |/ _` |/ _ \| '_ \ | |/ _` |/ __| |/ |";
                dragonjackingDealer[3] = @" | |_| | | | (_| | (_| | (_) | | | || | (_| | (__|   < ";
                dragonjackingDealer[4] = @" |____/|_|  \__,_|\__, |\___/|_| |__/ |\__,_|\___|_|\_|";
                dragonjackingDealer[5] = @"                  |___/           |__/                 ";
                for (int i = 0; i < dragonjackingDealer.Length; i++)
                {
                    Console.SetCursorPosition((winWidth - dragonjackingDealer[i].Length) / 2 - 10, winHeight / 2 - 3 + i);
                    Console.WriteLine(dragonjackingDealer[i]);
                }
            
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                string[] dragonjackongPlayer = new string[6];                                                          
                dragonjackongPlayer[0] =  @"    ____                                 _            __   "; 
                dragonjackongPlayer[1] =  @"   / __ \_________ _____ _____  ____    (_____ ______/ /__ ";
                dragonjackongPlayer[2] =  @"  / / / / ___/ __ `/ __ `/ __ \/ __ \  / / __ `/ ___/ //_/ ";
                dragonjackongPlayer[3] =  @" / /_/ / /  / /_/ / /_/ / /_/ / / / / / / /_/ / /__/ ,<    ";
                dragonjackongPlayer[4] =  @"/_____/_/   \__,_/\__, /\____/_/ /___/ /\__,_/\___/_/|_/   ";
                dragonjackongPlayer[5] =  @"                 /____/           /___/                    ";                                                         
                for (int i = 0; i < dragonjackongPlayer.Length; i++)                                                                                                                         
                {
                    Console.SetCursorPosition((winWidth - dragonjackongPlayer.Length) / 2 - 60, winHeight / 2 - dragonjackongPlayer.Length / 2 + i);
                    Console.WriteLine(dragonjackongPlayer[i]);
                }
            }
            Thread.Sleep(500);
            dealerCards.GetCardsInHand()[1].PrintCard(dealerPosX + 1 * 5, dealerPosY);
            Console.SetCursorPosition(dealerPosX - 7, dealerPosY);
            dealerCards.PrintSum();
        }

        private static void IntroScreen()
        {
            Console.BufferHeight = winHeight;
            Console.BufferWidth = winWidth;
            Console.SetWindowSize(winWidth, winHeight);
            Console.Title = "DRAGONJACK";
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            string[] titleDragon = new string[9];
            string[,] letterDragon = new string[9, 10];
            letterDragon[0, 0] = "████████▄ ";
            letterDragon[1, 0] = "███   ▀███";
            letterDragon[2, 0] = "███    ███";
            letterDragon[3, 0] = "███    ███";
            letterDragon[4, 0] = "███    ███";
            letterDragon[5, 0] = "███    ███";
            letterDragon[6, 0] = "███   ▄███";
            letterDragon[7, 0] = "████████▀ ";
            letterDragon[8, 0] = "          ";
            letterDragon[0, 1] = "    ▄████████";
            letterDragon[1, 1] = "   ███    ███";
            letterDragon[2, 1] = "   ███    ███";
            letterDragon[3, 1] = "  ▄███▄▄▄▄██▀";
            letterDragon[4, 1] = " ▀▀███▀▀▀▀▀  ";
            letterDragon[5, 1] = " ▀███████████";
            letterDragon[6, 1] = "   ███    ███";
            letterDragon[7, 1] = "   ███    ███";
            letterDragon[8, 1] = "   ███    ███";
            letterDragon[0, 2] = "    ▄████████";
            letterDragon[1, 2] = "   ███    ███";
            letterDragon[2, 2] = "   ███    ███";
            letterDragon[3, 2] = "   ███    ███";
            letterDragon[4, 2] = " ▀███████████";
            letterDragon[5, 2] = "   ███    ███";
            letterDragon[6, 2] = "   ███    ███";
            letterDragon[7, 2] = "   ███    █▀ ";
            letterDragon[8, 2] = "             ";
            letterDragon[0, 3] = "    ▄██████▄ ";
            letterDragon[1, 3] = "   ███    ███";
            letterDragon[2, 3] = "   ███    █▀ ";
            letterDragon[3, 3] = "  ▄███       ";
            letterDragon[4, 3] = " ▀▀███ ████▄ ";
            letterDragon[5, 3] = "   ███    ███";
            letterDragon[6, 3] = "   ███    ███";
            letterDragon[7, 3] = "   ████████▀ ";
            letterDragon[8, 3] = "             ";
            letterDragon[0, 4] = "  ▄██████▄ ";
            letterDragon[1, 4] = " ███    ███";
            letterDragon[2, 4] = " ███    ███";
            letterDragon[3, 4] = " ███    ███";
            letterDragon[4, 4] = " ███    ███";
            letterDragon[5, 4] = " ███    ███";
            letterDragon[6, 4] = " ███    ███";
            letterDragon[7, 4] = "  ▀██████▀ ";
            letterDragon[8, 4] = "           ";
            letterDragon[0, 5] = " ███▄▄▄▄  ";
            letterDragon[1, 5] = " ███▀▀▀██▄";
            letterDragon[2, 5] = " ███   ███";
            letterDragon[3, 5] = " ███   ███";
            letterDragon[4, 5] = " ███   ███";
            letterDragon[5, 5] = " ███   ███";
            letterDragon[6, 5] = " ███   ███";
            letterDragon[7, 5] = "  ▀█   █▀ ";
            letterDragon[8, 5] = "          ";
            letterDragon[0, 6] = "      ▄█";
            letterDragon[1, 6] = "     ███";
            letterDragon[2, 6] = "     ███";
            letterDragon[3, 6] = "     ███";
            letterDragon[4, 6] = "     ███";
            letterDragon[5, 6] = "     ███";
            letterDragon[6, 6] = "     ███";
            letterDragon[7, 6] = " █▄ ▄███";
            letterDragon[8, 6] = " ▀▀▀▀▀▀ ";
            letterDragon[0, 7] = "    ▄████████";
            letterDragon[1, 7] = "   ███    ███";
            letterDragon[2, 7] = "   ███    ███";
            letterDragon[3, 7] = "   ███    ███";
            letterDragon[4, 7] = " ▀███████████";
            letterDragon[5, 7] = "   ███    ███";
            letterDragon[6, 7] = "   ███    ███";
            letterDragon[7, 7] = "   ███    █▀ ";
            letterDragon[8, 7] = "             ";
            letterDragon[0, 8] = "  ▄████████";
            letterDragon[1, 8] = " ███    ███";
            letterDragon[2, 8] = " ███    █▀ ";
            letterDragon[3, 8] = " ███       ";
            letterDragon[4, 8] = " ███       ";
            letterDragon[5, 8] = " ███    █▄ ";
            letterDragon[6, 8] = " ███    ███";
            letterDragon[7, 8] = " ████████▀ ";
            letterDragon[8, 8] = "           ";
            letterDragon[0, 9] = "    ▄█   ▄█▄";
            letterDragon[1, 9] = "   ███ ▄███▀";
            letterDragon[2, 9] = "   ███▐██▀  ";
            letterDragon[3, 9] = "  ▄█████▀   ";
            letterDragon[4, 9] = " ▀▀█████▄   ";
            letterDragon[5, 9] = "   ███▐██▄  ";
            letterDragon[6, 9] = "   ███ ▀███▄";
            letterDragon[7, 9] = "   ███   ▀█▀";
            letterDragon[8, 9] = "   ▀        ";
            titleDragon[0] = "████████▄     ▄████████    ▄████████    ▄██████▄   ▄██████▄  ███▄▄▄▄        ▄█    ▄████████  ▄████████    ▄█   ▄█▄";
            titleDragon[1] = "███   ▀███   ███    ███   ███    ███   ███    ███ ███    ███ ███▀▀▀██▄     ███   ███    ███ ███    ███   ███ ▄███▀";
            titleDragon[2] = "███    ███   ███    ███   ███    ███   ███    █▀  ███    ███ ███   ███     ███   ███    ███ ███    █▀    ███▐██▀  ";
            titleDragon[3] = "███    ███  ▄███▄▄▄▄██▀   ███    ███  ▄███        ███    ███ ███   ███     ███   ███    ███ ███         ▄█████▀   ";
            titleDragon[4] = "███    ███ ▀▀███▀▀▀▀▀   ▀███████████ ▀▀███ ████▄  ███    ███ ███   ███     ███ ▀███████████ ███        ▀▀█████▄   ";
            titleDragon[5] = "███    ███ ▀███████████   ███    ███   ███    ███ ███    ███ ███   ███     ███   ███    ███ ███    █▄    ███▐██▄  ";
            titleDragon[6] = "███   ▄███   ███    ███   ███    ███   ███    ███ ███    ███ ███   ███     ███   ███    ███ ███    ███   ███ ▀███▄";
            titleDragon[7] = "████████▀    ███    ███   ███    █▀    ████████▀   ▀██████▀   ▀█   █▀  █▄ ▄███   ███    █▀  ████████▀    ███   ▀█▀";
            titleDragon[8] = "             ███    ███                                                ▀▀▀▀▀▀                            ▀        ";

            string[] dragon1 = new string[11];
            dragon1[0] = @"               \||/       ";
            dragon1[1] = @"                |  @___oo ";
            dragon1[2] = @"      /\  /\   / (__,,,,| ";
            dragon1[3] = @"     ) /^\) ^\/ _)        ";
            dragon1[4] = @"     )   /^\/   _)        ";
            dragon1[5] = @"     )   _ /  / _)        ";
            dragon1[6] = @" /\  )/\/ ||  | )_)       ";
            dragon1[7] = @"<  >      |(,,) )__)      ";
            dragon1[8] = @" ||      /    \)___)\     ";
            dragon1[9] = @" | \____(      )___) )___ ";
            dragon1[10] = @"  \______(_______;;; __;;;";

            string[] dragon2 = new string[15];
            dragon2[0] =  @"  <>=======()";
            dragon2[1] =  @" (/\___   /|\\          ()==========<>_ ";
            dragon2[2] =  @"(      \_/ | \\        //|\   ______/ \)";
            dragon2[3] =  @"         \_|  \\      // | \_/";
            dragon2[4] =  @"           \|\/|\_   //  /\/";
            dragon2[5] =  @"           (66)\ \_//  /";
            dragon2[6] =  @"           //_/\_\/ /  |";
            dragon2[7] =  @"          @@/  |=\  \  |";
            dragon2[8] =  @"               \_=\_ \ |";
            dragon2[9] =  @"                 \==\ \|\_";
            dragon2[10] = @"              __(\===\(  )\";
            dragon2[11] = @"             (((~) __(_/   |";
            dragon2[12] = @"                  (((~) \  /";
            dragon2[13] = @"                  ______/ /";
            dragon2[14] = @"                  '------'";

            int titlePos = (winWidth - titleDragon[0].Length) / 2;
            for (int i = 0; i < 10; i++)
            {
                if (i == 0 || i == 1 || i == 6 || i == 7)
                {
                    Thread.Sleep(700);
                }
                else
                {
                    Thread.Sleep(150);
                }
                for (int j = 0; j < 9; j++)
                {
                    Console.SetCursorPosition(titlePos, (winHeight - titleDragon.Length) / 2 + j);
                    Console.WriteLine(letterDragon[j , i]);
                    
                }
                
                
                titlePos += letterDragon[0, i].Length;
            }
            titlePos = (winWidth - titleDragon[0].Length) / 2;
            Thread.Sleep(500);
            Console.Clear();
            Thread.Sleep(400);
            for (int i = 0; i < titleDragon.Length; i++)
            {
                Console.SetCursorPosition(titlePos, (winHeight - titleDragon.Length) / 2 + i);
                    Console.WriteLine(titleDragon[i]);
            }
            Console.ReadKey(true);
            
        }
        private static void PrintArrow(int x, int y) 
        {

            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(x - 4, y + 3);
            Console.WriteLine("►►►");

        }
        private static void DeleteArrow(int x, int y)
        {

            Console.SetCursorPosition(x - 4, y + 3);
            Console.WriteLine("   ");

        }
        private static void PrintLegend(bool isSplit, int cardCount)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Yellow;
            List<string> options = new List<string>();
            options.Add("Z ► Hit");
            options.Add("X ► Stand");
            options.Add("C ► Double");

            if (isSplit)
            {
                options.Add("Space ► Split");
            }

            else if (cardCount > 2)
            {
                    options.RemoveAt(options.Count - 1);
                    DeleteLegend();
            }

            
            for (int i = 0; i < options.Count; i++)
            {
                Console.SetCursorPosition(legendPosX, legendPosY + (i * 2) + options.Count / 2);
                Console.WriteLine(options[i]);
                Console.WriteLine();
            }

        }
        private static void DeleteLegend()
        {
            string deleteLegend = "               ";
            for (int i = 0; i < playerPosY - (dealerPosY + cardHeight) - 2; i++)
            {
                Console.SetCursorPosition(legendPosX, dealerPosY + cardHeight + i);
                Console.WriteLine(deleteLegend);
            }
        }

        static void InvalidInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(20, 20);
            Console.WriteLine("Invalid command!");
            Thread.Sleep(1500);
            Console.SetCursorPosition(20, 20);
            Console.WriteLine("                ");
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
        static void DeleteCardAndSum(int x, int y) 
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            string delCardLines = "         ";
            string delSum = "     ";

            Console.SetCursorPosition(x - 7, y);
            Console.WriteLine(delSum);

            for (int i = 0; i < DragonJackGame.cardHeight; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(delCardLines);
            }
        }
        
    }

}
