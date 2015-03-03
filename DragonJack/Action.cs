namespace DragonJack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.IO;
    using System.Globalization;
    using System.Media;
    class Action
    {

        public static float PlaceBet(float funds, float placedBet)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
            Printer.PrintUpdatedMoney(funds);
            Console.ForegroundColor = ConsoleColor.White;
            int x = GlobalConsts.winWidth / 2;
            int y = GlobalConsts.winHeight / 2 + 1;
            Console.SetCursorPosition(x - "Place your bet or press ENTER to rebet".Length/2, y - 4);
            Console.WriteLine("Place your bet or press ENTER to rebet");
            bool betEntered = false;
            float bet = 0;
            while (!betEntered)
            {
                try
                {
                    Console.WriteLine("".PadRight(10, ' '));
                    string input = "";
                    Console.SetCursorPosition(x - funds.ToString().Length / 2, y);
                    input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        input = placedBet.ToString();
                    }
                    bet = float.Parse(input.Replace(",", "."), CultureInfo.InvariantCulture);
                    betEntered = true;
                    Console.SetCursorPosition(x - funds.ToString().Length / 2, y);
                    Console.Write("".PadRight(10, ' '));
                    
                    if (bet > funds)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    if (bet <= 0)
                    {
                        throw new ArgumentNullException();
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.SetCursorPosition(x - "You can't bet more than you have!".Length / 2, y);
                    Console.Write("You can't bet more than you have!");
                    Console.SetCursorPosition(x - "You can't bet more than you have!".Length / 2, y);
                    Console.ReadKey(true);
                    Console.Write("".PadRight("You can't bet more than you have!".Length, ' '));
                    betEntered = false;
                    bet = 0;
                }
                catch (ArgumentNullException)
                {
                    Console.SetCursorPosition(x - "You didn't enter any bet.".Length / 2, y);
                    Console.Write("You didn't enter any bet.");
                    Console.SetCursorPosition(x - "You didn't enter any bet.".Length / 2, y);
                    Console.ReadKey(true);
                    Console.Write("".PadRight("You didn't enter any bet.".Length, ' '));
                    betEntered = false;
                    bet = 0;
                }
                catch (FormatException)
                {
                    Console.SetCursorPosition(x - "The bet is not valid.".Length / 2, y);
                    Console.Write("The bet is not valid.");
                    Console.SetCursorPosition(x - "The bet is not valid.".Length / 2, y);
                    Console.ReadKey(true);
                    Console.Write("".PadRight("The bet is not valid.".Length, ' '));
                    betEntered = false;
                    bet = 0;
                }
                catch (OverflowException)
                {
                    Console.SetCursorPosition(x - "The bet is too big.".Length / 2, y);
                    Console.Write("The bet is too big.");
                    Console.SetCursorPosition(x - "The bet is too big.".Length / 2, y);
                    Console.ReadKey(true);
                    Console.Write("".PadRight("The bet is too big.".Length, ' '));
                    betEntered = false;
                    bet = 0;
                }
            }
            Sounds.PlaySound("placeChips");
            return bet;
        }

        public static void DealInitialCards(Hand playerCards, Hand dealerCards, int[,] deck, float funds, float bet)
        {
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
            Printer.PrintLegend(playerCards.AreEqualCards(), playerCards.GetCardsInHand().Count, funds, bet);
        }
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

        public static float CollectBet(float bet, int[] results)
        {
            float collect = 0;
            if (results[2] == 0)
            {
                if ((results[1] < results[0] && results[1] <= 21 && results[0] <= 21 && results[1] > 0) || results[1] > 21 || (results[0] < 0 && !(results[1] < 0)))
                {
                    collect = 0;
                }
                else if (results[1] == results[0] && results[1] <= 21)
                {
                    collect = bet;
                }
                else if ((results[1] > results[0] && results[1] <= 21 && results[1] <= 21) || (results[0] > 21 && results[1] <= 21))
                {
                    collect = bet + bet;
                }
                else if ((results[1] < 0 && !(results[0] < 0)))
                {
                    collect = bet + 1.5f * bet;
                }
            }
            else
            {
                if ((results[1] < results[0] && results[1] <= 21 && results[0] <= 21 && results[1] > 0) || results[1] > 21 || (results[0] < 0 && !(results[1] < 0)))
                {
                    collect = 0;
                }
                else if (results[1] == results[0] && results[1] <= 21)
                {
                    collect = bet / 2;
                }
                else if ((results[1] > results[0] && results[1] <= 21 && results[1] <= 21) || (results[0] > 21 && results[1] <= 21))
                {
                    collect = (bet + bet) / 2;
                }
                else if ((results[1] < 0 && !(results[0] < 0)))
                {
                    collect = (bet + 1.5f * bet) / 2;
                }

                if ((results[2] < results[0] && results[2] <= 21 && results[0] <= 21 && results[2] > 0) || results[2] > 21 || (results[0] < 0 && !(results[2] < 0)))
                {
                    collect += 0;
                }
                else if (results[2] == results[0] && results[2] <= 21)
                {
                    collect += bet / 2;
                }
                else if ((results[2] > results[0] && results[2] <= 21 && results[2] <= 21) || (results[0] > 21 && results[2] <= 21))
                {
                    collect += (bet + bet) / 2;
                }
                else if ((results[2] < 0 && !(results[0] < 0)))
                {
                    collect += (bet + 1.5f * bet) / 2;
                }
            }
            if (collect > 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(1, GlobalConsts.winHeight - 3);
                string collecting = "+" + string.Format(new System.Globalization.CultureInfo("en-ZW"), "{0:C}", collect);
                Console.Write(collecting);
                Console.ReadKey(true);
                Console.SetCursorPosition(1, GlobalConsts.winHeight - 3);
                Console.Write("".PadRight(collecting.Length, ' ' ));
            }
            return collect;
        }

        public static int[,] GetDeck()
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
        public static void KeepingScore(float score)
        {
            List<KeyValuePair<string, float>> list = new List<KeyValuePair<string, float>>();
            string filePath = @"..\..\Score.txt";
            string[] lines = ReadingFile(filePath);
            string[] nameScore = new string[1];
            foreach (string l in lines)
            {
                nameScore = l.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                list.Add(new KeyValuePair<string, float>(nameScore[0], float.Parse(nameScore[1])));
            }

            string name = "";
            Screen.OutroScreen(score, float.Parse(nameScore[1]));

            list.Add(new KeyValuePair<string, float>(name, score));
            list = list.OrderByDescending(x => x.Value).ToList();
            list.RemoveAt(10);
            StringBuilder sb = new StringBuilder();
            Console.SetCursorPosition((GlobalConsts.winWidth - "Dragonjack Masters".Length) / 2, (GlobalConsts.winHeight - 10) / 2 - 2);
            Console.WriteLine("Dragonjack Masters");
            Console.WriteLine();
            for (int i = 0; i < list.Count; i++)
            {
                Console.SetCursorPosition((GlobalConsts.winWidth - "Dragonjack Masters".Length) / 2, (GlobalConsts.winHeight - 10) / 2 + i);
                Console.WriteLine(list[i].Key.PadRight(10, ' ') + string.Format(new System.Globalization.CultureInfo("en-ZW"), "{0:C}", Convert.ToDecimal(list[i].Value.ToString())));
                sb.Append(list[i].Key.PadRight(10, ' ') + list[i].Value.ToString() + "\r\n");
            }
            WritingFile(filePath, sb.ToString());
            Sounds.PlayMusic("scoreMusic");
        }

        private static string[] ReadingFile(string path)
        {
            string[] fileLines = new string[1];
            try
            {
                fileLines = File.ReadAllLines(path);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file \"{0}\" was not found", Path.GetFullPath(path));
                Console.ReadKey(true);
                Environment.Exit(0);
            }
            catch (IOException)
            {
                Console.WriteLine("An I/O error occurred while opening the file {0}", Path.GetFullPath(path));
                Console.ReadKey(true);
                Environment.Exit(0);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("The file {0} is read-only, or points to a directory, or you don't have the required premission", Path.GetFullPath(path));
                Console.ReadKey(true);
                Environment.Exit(0);
            }
            return fileLines;
            
        }

        private static string[] WritingFile(string path, string text)
        {
            string[] fileLines = new string[1];
            try
            {
                File.WriteAllText(path, text);
            }
            catch (IOException)
            {
                Console.WriteLine("An I/O error occurred while opening the file {0}", Path.GetFullPath(path));
                Console.ReadKey(true);
                Environment.Exit(0);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("The file {0} is read-only, or points to a directory, or you don't have the required premission", Path.GetFullPath(path));
                Console.ReadKey(true);
                Environment.Exit(0);
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("The path {0} is in an invalid format", Path.GetFullPath(path));
                Console.ReadKey(true);
                Environment.Exit(0);
            }
            return fileLines;

        }
    }
}
