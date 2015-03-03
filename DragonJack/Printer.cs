namespace DragonJack
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Printer
    {
        public static void PrintDeck(int x, int y)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            string[] deckLines = new string[3];
            deckLines[0] = "┌" + "".PadRight(GlobalConsts.cardWidth - 2, '─') + "┐";
            deckLines[1] = "║" + "".PadRight(GlobalConsts.cardWidth - 2, '░') + "│";
            deckLines[2] = "╚" + "".PadRight(GlobalConsts.cardWidth - 2, '═') + "┘";

            Console.SetCursorPosition(x, y - 1);
            Console.WriteLine(deckLines[0]);
            for (int i = 1; i < GlobalConsts.cardHeight - 1; i++)
            {
                Console.SetCursorPosition(x, y - 1 + i);
                Console.WriteLine(deckLines[1]);
            }
            Console.SetCursorPosition(x, y - 1 + GlobalConsts.cardHeight - 1);
            Console.WriteLine(deckLines[2]);
        }
        public static void PrintUpdatedMoney(float money)
        {

            Console.SetCursorPosition(2, GlobalConsts.winHeight - 2);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write(" ".PadRight(10, ' '));
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, GlobalConsts.winHeight - 2);
            Console.Write(string.Format(new System.Globalization.CultureInfo("en-ZW"), "{0:C}", money));
        }

        public static void PrintDragonJack(Hand dealerCards)
        {
            DeleteLegend();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Thread.Sleep(500);
            dealerCards.GetCardsInHand()[1].PrintCard(GlobalConsts.dealerPosX + 1 * GlobalConsts.cardOverlap, GlobalConsts.dealerPosY);
            Console.SetCursorPosition(GlobalConsts.dealerPosX - 7, GlobalConsts.dealerPosY);
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
                Console.SetCursorPosition((GlobalConsts.winWidth - dragonjacking.Length) / 2 - 40, GlobalConsts.winHeight / 2 - dragonjacking.Length / 2 + i);
                Console.WriteLine(dragonjacking[i]);
            }
        }
        
        // Print and delete legend methods
        public static void PrintArrow(int x, int y)
        {

            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(x - 4, y + 3);
            Console.WriteLine("►►►");

        }
        public static void DeleteArrow(int x, int y)
        {

            Console.SetCursorPosition(x - 4, y + 3);
            Console.WriteLine("   ");

        }
        public static void PrintLegend(bool isSplit, int cardCount, float funds, float bet)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Yellow;
            List<string> options = new List<string>();
            options.Add("Z ► Hit");
            options.Add("X ► Stand");
            if (funds >= bet)
            {
                options.Add("C ► Double Down");
            }
            if (isSplit && funds >= bet)
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
                Console.SetCursorPosition(GlobalConsts.legendPosX, GlobalConsts.legendPosY + (i * 2) + options.Count / 2);
                Console.WriteLine(options[i]);
                Console.WriteLine();
            }

        }
        public static void DeleteLegend()
        {
            string deleteLegend = "               ";
            for (int i = 0; i < GlobalConsts.playerPosY - (GlobalConsts.dealerPosY + GlobalConsts.cardHeight) - 2; i++)
            {
                Console.SetCursorPosition(GlobalConsts.legendPosX, GlobalConsts.dealerPosY + GlobalConsts.cardHeight + i);
                Console.WriteLine(deleteLegend);
            }
        }

        public static void InvalidInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((GlobalConsts.winWidth - "Wrong key!".Length) / 2, GlobalConsts.winHeight / 2);
            Console.WriteLine("Wrong key!");
            Thread.Sleep(500);
            Console.SetCursorPosition((GlobalConsts.winWidth - "Wrong key!".Length) / 2, GlobalConsts.winHeight / 2);
            Console.WriteLine("                ");
        }

        public static void DeleteCardAndSum(int x, int y)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            string delCardLines = "         ";
            string delSum = "     ";

            Console.SetCursorPosition(x - 7, y);
            Console.WriteLine(delSum);

            for (int i = 0; i < GlobalConsts.cardHeight; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(delCardLines);
            }
        }
    }
}
