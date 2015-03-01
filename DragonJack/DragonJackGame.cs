﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Linq;

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
                col = random.Next(0, 13 * DragonJackGame.decksCount);
                this.cardSuit = row;
                this.cardStrength = col % 13;
                switch (col % 13)
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
            deckLines[0] = "┌" + "".PadRight(DragonJackGame.cardWidth - 2, '─') + "┐";
            deckLines[1] = "│" + "".PadRight(DragonJackGame.cardWidth - 2, '░') + "│";
            deckLines[2] = "└" + "".PadRight(DragonJackGame.cardWidth - 2, '─') + "┘";

            Console.SetCursorPosition(x, y);
            Console.WriteLine(deckLines[0]);
            for (int i = 1; i < DragonJackGame.cardHeight - 1; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(deckLines[1]);
            }
            Console.SetCursorPosition(x, y + DragonJackGame.cardHeight - 1);
            Console.WriteLine(deckLines[2]);
        }

        // Print a card
        public void PrintCard(int x, int y)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            string[] cardStrengths = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            string[] cardLines = new string[3];
            string[] cardSuitLines = new string[6];
            string cardStrength = cardStrengths[this.cardStrength];
            cardLines[0] = "┌" + "".PadRight(DragonJackGame.cardWidth - 2, '─') + "┐";
            cardLines[1] = "│" + "".PadRight(DragonJackGame.cardWidth - 2, ' ') + "│";
            cardLines[2] = "└" + "".PadRight(DragonJackGame.cardWidth - 2, '─') + "┘";

            Console.SetCursorPosition(x, y);
            Console.WriteLine(cardLines[0]);
            for (int i = 1; i < DragonJackGame.cardHeight - 1; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(cardLines[1]);
            }
            Console.SetCursorPosition(x, y + DragonJackGame.cardHeight - 1);
            Console.WriteLine(cardLines[2]);

            if (this.cardSuit == 0)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                cardSuitLines[0] = (cardStrength + "♣").ToString().PadRight(3, ' ') + "    ";
                cardSuitLines[1] = "   _   ";
                cardSuitLines[2] = "  ( )  ";
                cardSuitLines[3] = " (_X_) ";
                cardSuitLines[4] = "   I   ";
                cardSuitLines[5] = "    " + (cardStrength + "♣").ToString().PadLeft(3, ' ');
            }
            else if (this.cardSuit == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                cardSuitLines[0] = (cardStrength + "♦").ToString().PadRight(3, ' ') + "    ";
                cardSuitLines[1] = "   ^   ";
                cardSuitLines[2] = "  / \\  ";
                cardSuitLines[3] = "  \\ /  ";
                cardSuitLines[4] = "   V   ";
                cardSuitLines[5] = "    " + (cardStrength + "♦").ToString().PadLeft(3, ' ');
            }
            else if (this.cardSuit == 2)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                cardSuitLines[0] = (cardStrength + "♠").ToString().PadRight(3, ' ') + "    ";
                cardSuitLines[1] = "   ^   ";
                cardSuitLines[2] = "  / \\  ";
                cardSuitLines[3] = " (_^_) ";
                cardSuitLines[4] = "   I   ";
                cardSuitLines[5] = "    " + (cardStrength + "♠").ToString().PadLeft(3, ' ');
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                cardSuitLines[0] = (cardStrength + "♥").ToString().PadRight(3, ' ') + "    ";
                cardSuitLines[1] = "  _ _  ";
                cardSuitLines[2] = " ( V ) ";
                cardSuitLines[3] = "  \\ /  ";
                cardSuitLines[4] = "   V   ";
                cardSuitLines[5] = "    " + (cardStrength + "♥").ToString().PadLeft(3, ' ');
            }
            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(x + (DragonJackGame.cardWidth - 7) / 2, y + (DragonJackGame.cardHeight - 6) / 2 + i);
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
            sums = GetSum().ToString();
            int acesCount = AcesCount();
            if (fullSum - (acesCount - 1) * 10 < 21 && acesCount > 0)
            {
                sums = (fullSum - acesCount * 10).ToString().PadLeft(2, ' ') + "/" + GetSum().ToString() + " ";
            }

            Console.WriteLine(sums.PadLeft(5, ' '));
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
        public const int cardHeight = 8;
        public const int cardWidth = 9;
        public const int suitsCount = 4;
        public const int cardStrengthsCount = 13;
        public const int decksCount = 6;
        public const int cardOverlap = 4;
        public const int maxCardsWidth = (8 + (decksCount - 1)) * cardOverlap;
        public const int winWidth = 2 * maxCardsWidth + 30;
        public const int winHeight = 40;
        public const int dealerPosX = (winWidth - maxCardsWidth) / 2;
        public const int dealerPosY = winHeight / 10;
        public const int playerPosX = (winWidth - maxCardsWidth) / 2;
        public const int playerPosY = winHeight - winHeight / 10 - cardHeight;
        public const int doublePosX1 = (winWidth - (maxCardsWidth) * 2) / 4;
        public const int doublePosY1 = playerPosY;
        public const int doublePosX2 = doublePosX1 + maxCardsWidth + doublePosX1;
        public const int doublePosY2 = playerPosY;
        public const int legendPosX = 5;
        public const int legendPosY = 17;
        public const int dealingSpeed = 500;


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
            Console.WindowHeight = winHeight;
            Console.WindowWidth = winWidth;
            Console.BufferHeight = winHeight;
            Console.BufferWidth = winWidth;
            Console.Title = "DRAGONJACK";
            try
            {
                Console.SetWindowSize(winWidth, winHeight);
            }
            catch (ArgumentOutOfRangeException)
            {

                Console.WriteLine("\nPlease decrease the console font size.");
                Console.ReadKey(true);
                return;
            }
            IntroScreen();
            // Get new deck made of 6 decks
            int[,] deck = NewDeck();
            float funds = 1000;

            while (true)
            {
                if (deck.Cast<int>().Sum() < 60) //if cards in deck <60 (arbitrary number, but large enough for another play) add discarded cards and "shuffle"
                {
                    deck = NewDeck();
                }
                InitializeConsoleScreen();
                
                float bet = 100;//PlaceBet(funds);

                Hand playerCards = new Hand();
                Hand dealerCards = new Hand();
                // Initial dealing
                playerCards.FillHand(new Card(deck));
                Thread.Sleep(dealingSpeed);
                playerCards.GetCardsInHand()[0].PrintCard(playerPosX, playerPosY);
                Console.SetCursorPosition(playerPosX - 7, playerPosY);
                playerCards.PrintSum();

                dealerCards.FillHand(new Card(deck));
                Thread.Sleep(dealingSpeed);
                dealerCards.GetCardsInHand()[0].PrintCard(dealerPosX, dealerPosY);
                Console.SetCursorPosition(dealerPosX - 7, dealerPosY);
                dealerCards.PrintSum();

                playerCards.FillHand(new Card(deck));
                Thread.Sleep(dealingSpeed);
                playerCards.GetCardsInHand()[1].PrintCard(playerPosX + cardOverlap, playerPosY);
                Console.SetCursorPosition(playerPosX - 7, playerPosY);
                playerCards.PrintSum();

                dealerCards.FillHand(new Card(deck));
                Thread.Sleep(dealingSpeed);
                dealerCards.GetCardsInHand()[1].PrintBack(dealerPosX + cardOverlap, dealerPosY);

                PrintLegend(playerCards.AreEqualCards(), playerCards.GetCardsInHand().Count);

                bool playerGameEnded = false;
                //bool isDragonJack = IsGameOver(playerCards, dealerCards, playerGameEnded);
                
                int[] currentResults = { dealerCards.GetSum(), playerCards.GetSum(), 0 };//contains the current sum of cards for delaer and player
                if (playerCards.IsDragonJack() || dealerCards.IsDragonJack())
                {
                    if (dealerCards.IsDragonJack())
                    {
                        currentResults[0] = dealerCards.GetSum() - 22;//if dragonjack, sum is -1
                    }
                    if (playerCards.IsDragonJack())
                    {
                        currentResults[1] = playerCards.GetSum() - 22;//if dragonjack, sum is -1
                    }
                    PrintDragonJack(dealerCards);
                    playerGameEnded = true;
                }
                ConsoleKeyInfo key = new ConsoleKeyInfo();
                while (!playerGameEnded)
                {
                    key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Spacebar:
                            DeleteLegend();
                            PrintLegend(false, playerCards.GetCardsInHand().Count + 1);
                            if (playerCards.AreEqualCards())
                            {
                                int[] splitResults = Splitting(playerCards, deck);
                                bet = bet + bet;
                                playerGameEnded = true;
                                DealerPlay(dealerCards, splitResults.Min(), deck);
                                currentResults[0] = dealerCards.GetSum();
                                currentResults[1] = splitResults[0];
                                currentResults[2] = splitResults[1];
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
                                playerGameEnded = true;
                                DeleteLegend();
                                DealerPlay(dealerCards, playerCards.GetSum(), deck);
                                currentResults[0] = dealerCards.GetSum();
                                currentResults[1] = playerCards.GetSum();
                            }
                            break;
                        case ConsoleKey.X:
                            playerGameEnded = true;
                            DeleteLegend();
                            DealerPlay(dealerCards, playerCards.GetSum(), deck);
                            currentResults[0] = dealerCards.GetSum();
                            currentResults[1] = playerCards.GetSum();
                            break;
                        case ConsoleKey.C: 
                            Hitting(playerCards, deck, playerPosX, playerPosY);
                            bet = bet + bet;
                            playerGameEnded = true;
                            DeleteLegend();
                            DealerPlay(dealerCards, playerCards.GetSum(), deck);
                            currentResults[0] = dealerCards.GetSum();
                            currentResults[1] = playerCards.GetSum();
                        /*TODO: Implement Double*/ break;
                        case ConsoleKey.Escape: return;
                        case ConsoleKey.Enter: break;
                        default: InvalidInput();
                            break;
                        // TODO: Bets
                        //Another TODO: Check win, push or bust...
                    }
                }
                DeleteLegend();
                if (currentResults[0] < 0 || currentResults[1] < 0 || currentResults[2] < 0)
                {
                    PrintDragonJack(dealerCards);
                }
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(string.Join(" ", currentResults));
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("Collect: {0}", CollectBet(bet, currentResults));
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                { 
                    continue;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else
                {
                    while (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Escape)
                    {
                        //InvalidInput();
                        key = Console.ReadKey(true);
                    }
                }
                
            }
            // TODO: Make whole main in one while loop(until escape key is pressed)

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void InitializeConsoleScreen()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            PrintDeck(winWidth - winWidth / cardWidth, (winHeight - 5) / 2);
        }

        static float PlaceBet(float funds)////////////////////////////////////////////////////////
        {
            float bet = 0;

            return bet;
        }
        static float CollectBet(float bet, int[] results)
        {
            float collect = 0;
            if (results[0] < 0) //dealer dragonjack
            {
                if (results[2] == 0)
                {
                    if (results[1] < 0)
                    {
                        return collect = bet;
                    }
                    else
                    {
                        return collect = 0;
                    }
                }
                else
                {
                    if (results[1] < 0 ^ results[2] < 0)
                    {
                        return collect = bet / 2;
                    }
                    else
                    {
                        return collect = bet + 1.5f * bet;
                    }
                }
            }
            else if (results[0] > 0 && results[0] <= 21) //dealer dragonjack
            {
                if (results[2] == 0)
                {
                    if ((results[1] < results[0] && results[1] > 0) || results[1] > 21)
                    {
                        return collect = 0;
                    }
                    else if (results[1] == results[0])
                    {
                        return collect = bet;
                    }
                    else if (results[1] > results[0])
                    {
                        return collect = bet + bet;
                    }
                    else if (results[1] < 0)
                    {
                        return collect = bet + 1.5f * bet;
                    }
                }
                else
                {
                    if (results[1] < results[0] && results[2] < results[0])
                    {
                        return collect = 0;
                    }
                    else if (results[1] == results[0] && results[2] == results[0])
                    {
                        return collect = bet;
                    }
                    else if (results[1] > results[0] && results[2] > results[0])
                    {
                        return collect = bet + bet;
                    }
                    else if (results[1] < results[0] ^ results[2] < results[0])
                    {
                        if (results[1] > results[0] || results[2] > results[0])
                        {
                            return collect = bet;
                        }
                        else if (results[1] == results[0] || results[2] == results[0])
                        {
                            return collect = bet / 2;
                        }
                    }
                }
            }
            else
            {
                return collect = bet + bet;
            }

            return collect;
        }

        static void Hitting(Hand playerCards, int[,] deck, int x, int y)
        {
            playerCards.FillHand(new Card(deck));
            int playerCardIndex = playerCards.GetCardsInHand().Count - 1;

            playerCards.GetCardsInHand()[playerCardIndex]
                       .PrintCard(x + playerCardIndex * cardOverlap, y);

            Console.SetCursorPosition(x - 7, y);
            playerCards.PrintSum();
        }

        static void DealerPlay(Hand dealerCards, int playerCardsSum, int[,] deck)
        {
            Thread.Sleep(dealingSpeed);
            dealerCards.GetCardsInHand()[1].PrintCard(dealerPosX + cardOverlap, dealerPosY);
            Console.SetCursorPosition(dealerPosX - 7, dealerPosY);
            dealerCards.PrintSum();

            if (playerCardsSum > 21)
            {
                return;
            }
            while (dealerCards.GetSum() < 17)
            {

                Thread.Sleep(dealingSpeed);
                dealerCards.FillHand(new Card(deck));
                int dealerCardIndex = dealerCards.GetCardsInHand().Count - 1;

                dealerCards.GetCardsInHand()[dealerCardIndex]
                           .PrintCard(dealerPosX + dealerCardIndex * cardOverlap, dealerPosY);
                Console.SetCursorPosition(dealerPosX - 7, dealerPosY);
                dealerCards.PrintSum();
            }
        }

        static int[] Splitting(Hand playerCards, int[,] deck)
        {
            DeleteCardAndSum(playerPosX, playerPosY);
            DeleteCardAndSum(playerPosX + cardOverlap, playerPosY);
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
            Thread.Sleep(dealingSpeed);
            playerCards1.GetCardsInHand()[1].PrintCard(doublePosX1 + cardOverlap, doublePosY1);
            Console.SetCursorPosition(doublePosX1 - 7, doublePosY1);
            playerCards1.PrintSum();

            playerCards2.FillHand(new Card(deck));
            Thread.Sleep(dealingSpeed);
            playerCards2.GetCardsInHand()[1].PrintCard(doublePosX2 + cardOverlap, doublePosY2);
            Console.SetCursorPosition(doublePosX2 - 7, doublePosY2);
            playerCards2.PrintSum();

            int[] playerSums = { playerCards1.GetSum(), playerCards2.GetSum() };
            if (playerCards1.IsDragonJack())
            {
                playerSums[0] = playerCards1.GetSum() - 22;//if dragonjack, sum is -1
            }
            if (playerCards2.IsDragonJack())
            {
                playerSums[1] = playerCards2.GetSum() - 22;//if dragonjack, sum is -1
            }
            if (playerCards1.GetSum() < 21 || playerCards2.GetSum() < 21)
            {
                bool playSplitGame = true;
                bool firstHandEnded = false;
                while (playSplitGame)
                {
                    if (playerCards1.GetSum() < 21 && !firstHandEnded)
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
                                            playSplitGame = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        playSplitGame = false;
                                        break;
                                    }
                                }
                                break;
                            case ConsoleKey.X:
                                firstHandEnded = true;
                                DeleteArrow(doublePosX1, doublePosY1);
                                if (playerCards2.GetSum() < 21)
                                {
                                    PrintArrow(doublePosX2, doublePosY2);
                                    key = Console.ReadKey(true);
                                    switch (key.Key)
                                    {
                                        case ConsoleKey.Z:
                                            Hitting(playerCards2, deck, doublePosX2, doublePosY2);
                                            if (playerCards2.GetSum() >= 21)
                                            {
                                                DeleteArrow(doublePosX2, doublePosY2);
                                                playSplitGame = false;
                                                break;
                                            }
                                            break;
                                        case ConsoleKey.X:
                                            DeleteArrow(doublePosX2, doublePosY2);
                                            //playerSums[0] = playerCards1.GetSum(); 
                                            //playerSums[1] = playerCards2.GetSum();
                                            playSplitGame = false;
                                            //return playerSums;
                                            break;
                                        default: InvalidInput(); 
                                            break;
                                    }
                                }
                                break;
                            default: InvalidInput(); break;
                        }
                        if (playerCards2.GetSum() >= 21)
                        {
                            break;
                        }
                    }
                    else
                    {
                        DeleteArrow(doublePosX1, doublePosY1);
                        PrintArrow(doublePosX2, doublePosY2);
                        ConsoleKeyInfo key = Console.ReadKey(true);

                        switch (key.Key)
                        {
                            case ConsoleKey.Z:
                                Hitting(playerCards2, deck, doublePosX2, doublePosY2);
                                if (playerCards2.GetSum() >= 21)
                                {
                                    playSplitGame = false;
                                    break;
                                }
                                break;
                            case ConsoleKey.X: break;
                            default: InvalidInput(); break;
                        }
                        if (key.Key == ConsoleKey.X || playerCards2.GetSum() >= 21)
                        {
                            DeleteArrow(doublePosX2, doublePosY2);
                            playSplitGame = false;
                            break;
                        }
                    }

                    if (!playSplitGame)
                    {
                        break;
                    }
                }
            }
            if (playerSums[0] != -1)
            {
                playerSums[0] = playerCards1.GetSum();
            }
            if (playerSums[1] != -1)
            {
                playerSums[1] = playerCards2.GetSum();
            }
            return playerSums;
        }

        //method Doubling

        private static void PrintDragonJack(Hand dealerCards)
        {
            DeleteLegend();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Thread.Sleep(500);
            dealerCards.GetCardsInHand()[1].PrintCard(dealerPosX + 1 * DragonJackGame.cardOverlap, dealerPosY);
            Console.SetCursorPosition(dealerPosX - 7, dealerPosY);
            dealerCards.PrintSum();
            string[] dragonjacking = new string[8];
            dragonjacking[0] = "▄▄▄▄▄▄     ▄▄▄▄▄▄    ▄▄▄▄▄▄    ▄▄▄▄▄   ▄▄▄▄▄  ▄▄           ▄    ▄▄▄▄▄▄  ▄▄▄▄▄▄    ▄   ▄";
            dragonjacking[1] = "██  ▀██   ██   ██   ██   ██   ██   ██ ██   ██ ██▀▀▀█▄     ██   ██   ██ ██   ██   ██ ▄█▀";
            dragonjacking[2] = "██   ██   ██   ██   ██   ██   ██   █  ██   ██ ██   ██     ██   ██   ██ ██   ██   ██▐█▀ ";
            dragonjacking[3] = "██   ██  ▄██▄▄▄█▀   ██   ██  ▄██      ██   ██ ██   ██     ██   ██   ██ ██       ▄███▀  ";
            dragonjacking[4] = "██   ██ ▀▀██▀▀▀▀  ▀████████ ▀▀██ ███  ██   ██ ██   ██     ██ ▀████████ ██      ▀▀████  ";
            dragonjacking[5] = "██   ██ ▀████████   ██   ██   ██   ██ ██   ██ ██   ██     ██   ██   ██ ██   █    ██▐█▄ ";
            dragonjacking[6] = "██  ▄██   ██   ██   ██   ██   ██   ██ ██   ██ ██   ██ █▄ ▄██   ██   ██ ██   ██   ██ ▀█▄";
            dragonjacking[7] = "▀▀▀▀▀▀    ██   █▀   ▀▀   ▀    ▀▀▀▀▀▀   ▀▀▀▀▀   ▀   ▀  ▀▀▀▀▀    ▀▀   ▀  ▀▀▀▀▀▀    ▀    ▀";
            for (int i = 0; i < dragonjacking.Length; i++)
            {
                Console.SetCursorPosition((winWidth - dragonjacking.Length) / 2 - 40, winHeight / 2 - dragonjacking.Length / 2 + i);
                Console.WriteLine(dragonjacking[i]);
            }
        }


        private static void IntroScreen()
        {
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
            dragon2[0] = @"  <>=======()";
            dragon2[1] = @" (/\___   /|\\          ()==========<>_ ";
            dragon2[2] = @"(      \_/ | \\        //|\   ______/ \)";
            dragon2[3] = @"         \_|  \\      // | \_/";
            dragon2[4] = @"           \|\/|\_   //  /\/";
            dragon2[5] = @"           (66)\ \_//  /";
            dragon2[6] = @"           //_/\_\/ /  |";
            dragon2[7] = @"          @@/  |=\  \  |";
            dragon2[8] = @"               \_=\_ \ |";
            dragon2[9] = @"                 \==\ \|\_";
            dragon2[10] = @"              __(\===\(  )\";
            dragon2[11] = @"             (((~) __(_/   |";
            dragon2[12] = @"                  (((~) \  /";
            dragon2[13] = @"                  ______/ /";
            dragon2[14] = @"                  '------'";

            int titleLength = Enumerable.Range(0, letterDragon.GetLength(1)).Sum(i => letterDragon[0, i].Length);
            int titlePos = (winWidth - titleLength) / 2;
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
                    Console.WriteLine(letterDragon[j, i]);
                }
                titlePos += letterDragon[0, i].Length;
            }
            titlePos = (winWidth - titleLength) / 2;
            Thread.Sleep(500);
            Console.Clear();
            Thread.Sleep(400);

            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.SetCursorPosition(titlePos, (winHeight - titleDragon.Length) / 2 + j);
                    Console.WriteLine(letterDragon[j, i]);
                    titlePos += letterDragon[0, i].Length;
                }
                titlePos = (winWidth - titleLength) / 2;
            }

            //for (int i = 0; i < titleDragon.Length; i++)
            //{
            //    Console.SetCursorPosition(titlePos, (winHeight - titleDragon.Length) / 2 + i);
            //    Console.WriteLine(titleDragon[i]);
            //}
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
            Console.SetCursorPosition((winWidth - "Wrong key!".Length) / 2, winHeight / 2);
            Console.WriteLine("Wrong key!");
            Thread.Sleep(500);
            Console.SetCursorPosition((winWidth - "Wrong key!".Length) / 2, winHeight / 2);
            Console.WriteLine("                ");
        }
        // TODO: New method - Print available options
        // Parameters - Player's cards, Output - void
        static int[,] NewDeck()
        {
            int[,] deck = new int[suitsCount, cardStrengthsCount * DragonJackGame.decksCount];
            for (int i = 0; i < deck.GetLength(0); i++)
            {
                for (int j = 0; j < deck.GetLength(1); j++)
                {
                    deck[i, j] = 1;
                }
            }
            return deck;
        }

        static void PrintDeck(int x, int y)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            string[] deckLines = new string[3];
            deckLines[0] = "┌" + "".PadRight(DragonJackGame.cardWidth - 2, '─') + "┐";
            deckLines[1] = "║" + "".PadRight(DragonJackGame.cardWidth - 2, '░') + "│";
            deckLines[2] = "╚" + "".PadRight(DragonJackGame.cardWidth - 2, '═') + "┘";

            Console.SetCursorPosition(x, y - 1);
            Console.WriteLine(deckLines[0]);
            for (int i = 1; i < cardHeight - 1; i++)
            {
                Console.SetCursorPosition(x, y - 1 + i);
                Console.WriteLine(deckLines[1]);
            }
            Console.SetCursorPosition(x, y - 1 + cardHeight - 1);
            Console.WriteLine(deckLines[2]);
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
