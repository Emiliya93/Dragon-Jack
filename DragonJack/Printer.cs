namespace DragonJack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class Printer
    {
        public static void PrintDeck(int x, int y)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            string[] deckLines = new string[3];
            deckLines[0] = "┌" + "".PadRight(DragonJackGame.cardWidth - 2, '─') + "┐";
            deckLines[1] = "║" + "".PadRight(DragonJackGame.cardWidth - 2, '░') + "│";
            deckLines[2] = "╚" + "".PadRight(DragonJackGame.cardWidth - 2, '═') + "┘";

            Console.SetCursorPosition(x, y - 1);
            Console.WriteLine(deckLines[0]);
            for (int i = 1; i < DragonJackGame.cardHeight - 1; i++)
            {
                Console.SetCursorPosition(x, y - 1 + i);
                Console.WriteLine(deckLines[1]);
            }
            Console.SetCursorPosition(x, y - 1 + DragonJackGame.cardHeight - 1);
            Console.WriteLine(deckLines[2]);
        }

        public static void PrintDragonJack(Hand dealerCards)
        {
            DeleteLegend();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Thread.Sleep(500);
            dealerCards.GetCardsInHand()[1].PrintCard(DragonJackGame.dealerPosX + 1 * DragonJackGame.cardOverlap, DragonJackGame.dealerPosY);
            Console.SetCursorPosition(DragonJackGame.dealerPosX - 7, DragonJackGame.dealerPosY);
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
                Console.SetCursorPosition((DragonJackGame.winWidth - dragonjacking.Length) / 2 - 40, DragonJackGame.winHeight / 2 - dragonjacking.Length / 2 + i);
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
        public static void PrintLegend(bool isSplit, int cardCount)
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
                Console.SetCursorPosition(DragonJackGame.legendPosX, DragonJackGame.legendPosY + (i * 2) + options.Count / 2);
                Console.WriteLine(options[i]);
                Console.WriteLine();
            }

        }
        public static void DeleteLegend()
        {
            string deleteLegend = "               ";
            for (int i = 0; i < DragonJackGame.playerPosY - (DragonJackGame.dealerPosY + DragonJackGame.cardHeight) - 2; i++)
            {
                Console.SetCursorPosition(DragonJackGame.legendPosX, DragonJackGame.dealerPosY + DragonJackGame.cardHeight + i);
                Console.WriteLine(deleteLegend);
            }
        }

        public static void InvalidInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((DragonJackGame.winWidth - "Wrong key!".Length) / 2, DragonJackGame.winHeight / 2);
            Console.WriteLine("Wrong key!");
            Thread.Sleep(500);
            Console.SetCursorPosition((DragonJackGame.winWidth - "Wrong key!".Length) / 2, DragonJackGame.winHeight / 2);
            Console.WriteLine("                ");
        }

        public static void DeleteCardAndSum(int x, int y)
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
