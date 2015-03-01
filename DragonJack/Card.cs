namespace DragonJack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Card
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
}
