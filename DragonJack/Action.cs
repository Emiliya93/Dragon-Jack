namespace DragonJack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    class Action
    {
        public static void Hitting(Hand playerCards, int[,] deck, int x, int y)
        {
            playerCards.FillHand(new Card(deck));
            int playerCardIndex = playerCards.GetCardsInHand().Count - 1;

            playerCards.GetCardsInHand()[playerCardIndex]
                       .PrintCard(x + playerCardIndex * GlobalConsts.cardOverlap, y);

            Console.SetCursorPosition(x - 7, y);
            playerCards.PrintSum();
        }
        
        public static void DealerPlay(Hand dealerCards, int playerCardsSum, int[,] deck)
        {
            Thread.Sleep(GlobalConsts.dealingSpeed);
            dealerCards.GetCardsInHand()[1].PrintCard(GlobalConsts.dealerPosX + GlobalConsts.cardOverlap, GlobalConsts.dealerPosY);
            Console.SetCursorPosition(GlobalConsts.dealerPosX - 7, GlobalConsts.dealerPosY);
            dealerCards.PrintSum();

            if (playerCardsSum > 21)
            {
                return;
            }
            while (dealerCards.GetSum() < 17)
            {

                Thread.Sleep(GlobalConsts.dealingSpeed);
                dealerCards.FillHand(new Card(deck));
                int dealerCardIndex = dealerCards.GetCardsInHand().Count - 1;

                dealerCards.GetCardsInHand()[dealerCardIndex]
                           .PrintCard(GlobalConsts.dealerPosX + dealerCardIndex * GlobalConsts.cardOverlap, GlobalConsts.dealerPosY);
                Console.SetCursorPosition(GlobalConsts.dealerPosX - 7, GlobalConsts.dealerPosY);
                dealerCards.PrintSum();
            }
        }
        
        public static int[] Splitting(Hand playerCards, int[,] deck)
        {
            Printer.DeleteCardAndSum(GlobalConsts.playerPosX, GlobalConsts.playerPosY);
            Printer.DeleteCardAndSum(GlobalConsts.playerPosX + GlobalConsts.cardOverlap, GlobalConsts.playerPosY);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Hand playerCards1 = new Hand();
            Hand playerCards2 = new Hand();

            playerCards1.FillHand(playerCards.GetCardsInHand()[0]);
            playerCards1.GetCardsInHand()[0].PrintCard(GlobalConsts.doublePosX1, GlobalConsts.doublePosY1);
            Console.SetCursorPosition(GlobalConsts.doublePosX1 - 7, GlobalConsts.doublePosY1);
            playerCards1.PrintSum();

            playerCards2.FillHand(playerCards.GetCardsInHand()[1]);
            playerCards2.GetCardsInHand()[0].PrintCard(GlobalConsts.doublePosX2, GlobalConsts.doublePosY2);
            Console.SetCursorPosition(GlobalConsts.doublePosX2 - 7, GlobalConsts.doublePosY2);
            playerCards2.PrintSum();

            playerCards1.FillHand(new Card(deck));
            Thread.Sleep(GlobalConsts.dealingSpeed);
            playerCards1.GetCardsInHand()[1].PrintCard(GlobalConsts.doublePosX1 + GlobalConsts.cardOverlap, GlobalConsts.doublePosY1);
            Console.SetCursorPosition(GlobalConsts.doublePosX1 - 7, GlobalConsts.doublePosY1);
            playerCards1.PrintSum();

            playerCards2.FillHand(new Card(deck));
            Thread.Sleep(GlobalConsts.dealingSpeed);
            playerCards2.GetCardsInHand()[1].PrintCard(GlobalConsts.doublePosX2 + GlobalConsts.cardOverlap, GlobalConsts.doublePosY2);
            Console.SetCursorPosition(GlobalConsts.doublePosX2 - 7, GlobalConsts.doublePosY2);
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
                        Printer.PrintArrow(GlobalConsts.doublePosX1, GlobalConsts.doublePosY1);
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        switch (key.Key)
                        {
                            case ConsoleKey.Z:
                                Hitting(playerCards1, deck, GlobalConsts.doublePosX1, GlobalConsts.doublePosY1);
                                if (playerCards1.GetSum() >= 21)
                                {
                                    Printer.DeleteArrow(GlobalConsts.doublePosX1, GlobalConsts.doublePosY1);
                                    if (playerCards2.GetSum() < 21)
                                    {
                                        Printer.PrintArrow(GlobalConsts.doublePosX2, GlobalConsts.doublePosY2);
                                        key = Console.ReadKey(true);

                                        switch (key.Key)
                                        {
                                            case ConsoleKey.Z:
                                                Hitting(playerCards2, deck, GlobalConsts.doublePosX2, GlobalConsts.doublePosY2);
                                                break;
                                            case ConsoleKey.X: break;
                                            case ConsoleKey.Escape: break;
                                            default: Printer.InvalidInput();
                                                break;
                                        }
                                        if (key.Key == ConsoleKey.X || playerCards2.GetSum() >= 21)
                                        {
                                            Printer.DeleteArrow(GlobalConsts.doublePosX2, GlobalConsts.doublePosY2);
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
                                Printer.DeleteArrow(GlobalConsts.doublePosX1, GlobalConsts.doublePosY1);
                                if (playerCards2.GetSum() < 21)
                                {
                                    Printer.PrintArrow(GlobalConsts.doublePosX2, GlobalConsts.doublePosY2);
                                    key = Console.ReadKey(true);
                                    switch (key.Key)
                                    {
                                        case ConsoleKey.Z:
                                            Hitting(playerCards2, deck, GlobalConsts.doublePosX2, GlobalConsts.doublePosY2);
                                            if (playerCards2.GetSum() >= 21)
                                            {
                                                Printer.DeleteArrow(GlobalConsts.doublePosX2, GlobalConsts.doublePosY2);
                                                playSplitGame = false;
                                                break;
                                            }
                                            break;
                                        case ConsoleKey.X:
                                            Printer.DeleteArrow(GlobalConsts.doublePosX2, GlobalConsts.doublePosY2);
                                            //playerSums[0] = playerCards1.GetSum(); 
                                            //playerSums[1] = playerCards2.GetSum();
                                            playSplitGame = false;
                                            //return playerSums;
                                            break;
                                        default: Printer.InvalidInput();
                                            break;
                                    }
                                }
                                break;
                            default: Printer.InvalidInput(); break;
                        }
                        if (playerCards2.GetSum() >= 21)
                        {
                            break;
                        }
                    }
                    else
                    {
                        Printer.DeleteArrow(GlobalConsts.doublePosX1, GlobalConsts.doublePosY1);
                        Printer.PrintArrow(GlobalConsts.doublePosX2, GlobalConsts.doublePosY2);
                        ConsoleKeyInfo key = Console.ReadKey(true);

                        switch (key.Key)
                        {
                            case ConsoleKey.Z:
                                Hitting(playerCards2, deck, GlobalConsts.doublePosX2, GlobalConsts.doublePosY2);
                                if (playerCards2.GetSum() >= 21)
                                {
                                    playSplitGame = false;
                                    break;
                                }
                                break;
                            case ConsoleKey.X: break;
                            default: Printer.InvalidInput(); break;
                        }
                        if (key.Key == ConsoleKey.X || playerCards2.GetSum() >= 21)
                        {
                            Printer.DeleteArrow(GlobalConsts.doublePosX2, GlobalConsts.doublePosY2);
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

        //TODO: method Doubling

        public static int[,] NewDeck()
        {
            int[,] deck = new int[GlobalConsts.suitsCount, GlobalConsts.cardStrengthsCount * GlobalConsts.decksCount];
            for (int i = 0; i < deck.GetLength(0); i++)
            {
                for (int j = 0; j < deck.GetLength(1); j++)
                {
                    deck[i, j] = 1;
                }
            }
            return deck;
        }
    }
}
