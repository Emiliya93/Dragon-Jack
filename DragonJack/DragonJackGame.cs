﻿namespace DragonJack
 {
﻿     using System;
     using System.Threading;
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
             float placedBet = 0;
             while (true)
             {
                 if (deck.Cast<int>().Sum() < 60) //if cards in deck <60 (arbitrary number, but large enough for another play) add discarded cards and "shuffle"
                 {
                     deck = Action.GetDeck();
                 }
                 float bet = Action.PlaceBet(funds, placedBet);
                 placedBet = bet;
                 Screen.SetTable();
                 funds = funds - bet;
                 Printer.PrintUpdatedMoney(funds);
                 Hand playerCards = new Hand();
                 Hand dealerCards = new Hand();
                 // Initial dealing
                 Action.DealInitialCards(playerCards, dealerCards, deck, funds, bet);
                 bool playerGameEnded = false;
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
                     playerGameEnded = true;
                 }
                 ConsoleKeyInfo key = new ConsoleKeyInfo();
                 while (!playerGameEnded && (funds + bet) > 0)
                 {
                     key = Console.ReadKey(true);
                     switch (key.Key)
                     {
                         case ConsoleKey.Spacebar://Split
                             if (playerCards.AreEqualCards() && funds >= bet)
                             {
                                 Printer.DeleteLegend();
                                 Printer.PrintLegend(false, playerCards.GetCardsInHand().Count + 1, funds, bet);
                                 funds = funds - bet;
                                 bet = bet + bet;
                                 Printer.PrintUpdatedMoney(funds);
                                 int[] splitResults = Action.Splitting(playerCards, deck);
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
                         case ConsoleKey.Z://Hit
                             Action.Hitting(playerCards, deck, GlobalConsts.playerPosX, GlobalConsts.playerPosY);
                             Printer.DeleteLegend();
                             Printer.PrintLegend(false, playerCards.GetCardsInHand().Count, funds, bet);
                             if (playerCards.GetSum() >= 21)
                             {
                                 playerGameEnded = true;
                                 Printer.DeleteLegend();
                                 Action.DealerPlay(dealerCards, playerCards.GetSum(), deck);
                                 currentResults[0] = dealerCards.GetSum();
                                 currentResults[1] = playerCards.GetSum();
                             }
                             break;
                         case ConsoleKey.X://Stand
                             playerGameEnded = true;
                             Printer.DeleteLegend();
                             Action.DealerPlay(dealerCards, playerCards.GetSum(), deck);
                             currentResults[0] = dealerCards.GetSum();
                             currentResults[1] = playerCards.GetSum();
                             break;
                         case ConsoleKey.C://Double Down
                             if (funds >= bet)
                             {
                                 funds = funds - bet;
                                 bet = bet + bet;
                                 Printer.PrintUpdatedMoney(funds);
                                 Action.Hitting(playerCards, deck, GlobalConsts.playerPosX, GlobalConsts.playerPosY);
                                 playerGameEnded = true;
                                 Printer.DeleteLegend();
                                 Action.DealerPlay(dealerCards, playerCards.GetSum(), deck);
                                 currentResults[0] = dealerCards.GetSum();
                                 currentResults[1] = playerCards.GetSum();
                             }
                             else
                             {
                                 Printer.InvalidInput();
                             }
                             break;
                         case ConsoleKey.Escape: Environment.Exit(0); break;
                         case ConsoleKey.Enter: break;
                         default: Printer.InvalidInput();
                             break;
                     }
                 }
                 Printer.DeleteLegend();
                 if (currentResults[0] < 0 || currentResults[1] < 0 || currentResults[2] < 0)
                 {
                     Printer.PrintDragonJack(dealerCards);
                 }
                 float collect = Action.CollectBet(bet, currentResults);
                 funds = funds + collect;
                 Printer.PrintUpdatedMoney(funds);

                 Console.BackgroundColor = ConsoleColor.DarkGreen;
                 Console.ForegroundColor = ConsoleColor.White;
                 Console.SetCursorPosition((GlobalConsts.winWidth - "Press F12 to leave the table".Length) / 2, GlobalConsts.winHeight - 1);
                 Console.Write("Press F12 to leave the table");
                 Console.SetCursorPosition((GlobalConsts.winWidth - "Press ENTER to continue".Length) / 2, GlobalConsts.winHeight - 2);
                 Console.Write("Press ENTER to continue");

                 key = Console.ReadKey(true);

                 if (funds <= 0)
                 {
                     break;
                 }
                 
                 switch (key.Key)
                 {
                     case ConsoleKey.Enter: break;
                     case ConsoleKey.F12: break;
                     default: key = Console.ReadKey(true);
                         break;
                 }
                 if (key.Key == ConsoleKey.F12)
                 {
                     break;
                 }
                 
             }
             Action.KeepingScore(funds);
         }
     }
 }