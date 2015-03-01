namespace DragonJack
{
    ﻿using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Text;
    using System.Linq;

    public class DragonJackGame
    {
        static void Main()
        {
            // Catch unhandled exception: System.ArgumentOutOfRangeException: 
            // The value must be less than the console's current maximum window size of 113 in that dimensio
            // Note that this value depends on screen resolution and the console font.
            Console.CursorVisible = false;
            Console.WindowHeight = GlobalConsts.winHeight;
            Console.WindowWidth = GlobalConsts.winWidth;
            Console.BufferHeight = GlobalConsts.winHeight;
            Console.BufferWidth = GlobalConsts.winWidth;
            Console.Title = "DRAGONJACK";
            try
            {
                Console.SetWindowSize(GlobalConsts.winWidth, GlobalConsts.winHeight);
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
                Thread.Sleep(GlobalConsts.dealingSpeed);
                playerCards.GetCardsInHand()[0].PrintCard(GlobalConsts.playerPosX, GlobalConsts.playerPosY);
                Console.SetCursorPosition(GlobalConsts.playerPosX - 7, GlobalConsts.playerPosY);
                playerCards.PrintSum();

                dealerCards.FillHand(new Card(deck));
                Thread.Sleep(GlobalConsts.dealingSpeed);
                dealerCards.GetCardsInHand()[0].PrintCard(GlobalConsts.dealerPosX, GlobalConsts.dealerPosY);
                Console.SetCursorPosition(GlobalConsts.dealerPosX - 7, GlobalConsts.dealerPosY);
                dealerCards.PrintSum();

                playerCards.FillHand(new Card(deck));
                Thread.Sleep(GlobalConsts.dealingSpeed);
                playerCards.GetCardsInHand()[1].PrintCard(GlobalConsts.playerPosX + GlobalConsts.cardOverlap, GlobalConsts.playerPosY);
                Console.SetCursorPosition(GlobalConsts.playerPosX - 7, GlobalConsts.playerPosY);
                playerCards.PrintSum();

                dealerCards.FillHand(new Card(deck));
                Thread.Sleep(GlobalConsts.dealingSpeed);
                dealerCards.GetCardsInHand()[1].PrintBack(GlobalConsts.dealerPosX + GlobalConsts.cardOverlap, GlobalConsts.dealerPosY);

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
                            Action.Hitting(playerCards, deck, GlobalConsts.playerPosX, GlobalConsts.playerPosY);
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
                            Action.Hitting(playerCards, deck, GlobalConsts.playerPosX, GlobalConsts.playerPosY);
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