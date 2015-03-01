﻿namespace DragonJack
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
            Screen.SetWindow();
            Screen.IntroScreen();
            // Get new deck made of 6 decks
            int[,] deck = Action.GetDeck();
            float funds = 1000;

            while (true)
            {
                if (deck.Cast<int>().Sum() < 60) //if cards in deck <60 (arbitrary number, but large enough for another play) add discarded cards and "shuffle"
                {
                    deck = Action.GetDeck();
                }
                Screen.InitializeConsoleScreen();
                
                float bet = 100;//Action.PlaceBet(funds);

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
                            if (playerCards.AreEqualCards())
                            {
                                Printer.DeleteLegend();
                                Printer.PrintLegend(false, playerCards.GetCardsInHand().Count + 1);
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
                Console.WriteLine("Collect: {0}", Action.CollectBet(bet, currentResults));
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

            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.Clear();
            //Console.ForegroundColor = ConsoleColor.White;
        }
    }
}