namespace DragonJack
{
    ﻿using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Text;
    using System.Linq;

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
            Screen.IntroScreen();
            // Get new deck made of 6 decks
            int[,] deck = Action.NewDeck();
            float funds = 1000;

            while (true)
            {
                if (deck.Cast<int>().Sum() < 60) //if cards in deck <60 (arbitrary number, but large enough for another play) add discarded cards and "shuffle"
                {
                    deck = Action.NewDeck();
                }
                Screen.InitializeConsoleScreen();
                
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

                Printer.PrintLegend(playerCards.AreEqualCards(), playerCards.GetCardsInHand().Count);

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
                    Printer.PrintDragonJack(dealerCards);
                    playerGameEnded = true;
                }
                ConsoleKeyInfo key = new ConsoleKeyInfo();
                while (!playerGameEnded)
                {
                    key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Spacebar:
                            Printer.DeleteLegend();
                            Printer.PrintLegend(false, playerCards.GetCardsInHand().Count + 1);
                            if (playerCards.AreEqualCards())
                            {
                                int[] splitResults = Action.Splitting(playerCards, deck);
                                bet = bet + bet;
                                playerGameEnded = true;
                                Action.DealerPlay(dealerCards, splitResults.Min(), deck);
                                currentResults[0] = dealerCards.GetSum();
                                currentResults[1] = splitResults[0];
                                currentResults[2] = splitResults[1];
                            }
                            else
                            {
                                Printer.InvalidInput();
                            }
                            break;
                        case ConsoleKey.Z:
                            Action.Hitting(playerCards, deck, playerPosX, playerPosY);
                            Printer.DeleteLegend();
                            Printer.PrintLegend(false, playerCards.GetCardsInHand().Count);
                            if (playerCards.GetSum() >= 21)
                            {
                                playerGameEnded = true;
                                Printer.DeleteLegend();
                                Action.DealerPlay(dealerCards, playerCards.GetSum(), deck);
                                currentResults[0] = dealerCards.GetSum();
                                currentResults[1] = playerCards.GetSum();
                            }
                            break;
                        case ConsoleKey.X:
                            playerGameEnded = true;
                            Printer.DeleteLegend();
                            Action.DealerPlay(dealerCards, playerCards.GetSum(), deck);
                            currentResults[0] = dealerCards.GetSum();
                            currentResults[1] = playerCards.GetSum();
                            break;
                        case ConsoleKey.C:
                            Action.Hitting(playerCards, deck, playerPosX, playerPosY);
                            bet = bet + bet;
                            playerGameEnded = true;
                            Printer.DeleteLegend();
                            Action.DealerPlay(dealerCards, playerCards.GetSum(), deck);
                            currentResults[0] = dealerCards.GetSum();
                            currentResults[1] = playerCards.GetSum();
                        /*TODO: Implement Double*/ break;
                        case ConsoleKey.Escape: return;
                        case ConsoleKey.Enter: break;
                        default: Printer.InvalidInput();
                            break;
                        // TODO: Bets
                        //Another TODO: Check win, push or bust...
                    }
                }
                Printer.DeleteLegend();
                if (currentResults[0] < 0 || currentResults[1] < 0 || currentResults[2] < 0)
                {
                    Printer.PrintDragonJack(dealerCards);
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
    }

}
