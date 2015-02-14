using System;
using System.Collections.Generic;

//AAAA
class NumbersFormOneToN
{
    static Random random = new Random();
    static int counter = 0;
    static int winWidth = 120;
    static int winHeight = 40;
    static int dealerPositionX = (winWidth - 105) / 2;
    static int dealerPositionY = winHeight / 10;
    static int cardHeight = 7;
    static int cardWidth = 10;
    static int playerPositionX = (winWidth - 105) / 2;
    static int playerPositionY = winHeight - winHeight / 10 - cardHeight;

    static void Main()
    {
        int[,] deck = NewDeck();

        Console.BufferHeight = winHeight;
        Console.BufferWidth = winWidth;
        Console.SetWindowSize(winWidth, winHeight);
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Black;
        printDeck(winWidth - winWidth / 10, (winHeight - 3) / 2);
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        //for (int i = 0; i < 11; i++)
        //{
        //    printCard(i, (winWidth - 55) / 2 + i * 5, winHeight - 7 - winHeight / 10);
        //}

        int[] startScreenInfo = new int[10];
        startScreenInfo = StartScreen(deck);

        int currentPlayerSum = startScreenInfo[0];
        int currentDealerSum = startScreenInfo[1];
        List<int> playerCards = new List<int>();
        List<int> dealerCards = new List<int>();
        playerCards.Add(startScreenInfo[2]);
        playerCards.Add(startScreenInfo[3]);
        dealerCards.Add(startScreenInfo[4]);
        dealerCards.Add(startScreenInfo[5]);
       
        // Print options acording to the cards we have
        
    }

    // TODO: New method - Print available options
    // Parameters - Player's cards, Output - void
    static int[,] NewDeck()
    {
        int[,] deck = new int[4, 13];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                deck[i, j] = 6;
            }
        }
        return deck;
    }
    static int[] StartScreen(int [,] deck)
    {
        int[] resultSumAndCards = new int[10];

        int[] card1 = GetCard(deck);
        int[] card2 = GetCard(deck);
        int[] card3 = GetCard(deck);
        int[] card4 = GetCard(deck);

        int dealerSum = 0;
        dealerSum += card2[2];
        dealerSum += card4[2];

        int playerSum = 0;
        playerSum += card1[2];
        playerSum += card3[2];

        resultSumAndCards[0] = playerSum;
        resultSumAndCards[1] = dealerSum;
        resultSumAndCards[2] = card1[1];
        resultSumAndCards[3] = card3[1];
        resultSumAndCards[4] = card2[1];
        resultSumAndCards[5] = card4[1];
        resultSumAndCards[6] = card1[0];
        resultSumAndCards[7] = card3[0];
        resultSumAndCards[8] = card2[0];
        resultSumAndCards[9] = card4[0];

        printCard(new int[] { resultSumAndCards[8], resultSumAndCards[4] }, dealerPositionX + 0 * 5, dealerPositionY);
        // Print back side of second dealer card 
        printDeck(dealerPositionX + 1 * 5, dealerPositionY);
        printCard(card1, playerPositionX + 0 * 5, playerPositionY);
        printCard(card3, playerPositionX + 1 * 5, playerPositionY);
        return resultSumAndCards;
    }
    static int[] GetCard(int[,] deck)
    {
        //if (newDeck)
        //{
        //    int[,] deck2 = new int[4, 13];
        //    for (int i = 0; i < 4; i++)
        //    {
        //        for (int j = 0; j < 13; j++)
        //        {
        //            deck2[i,j] = 6;
        //        }
        //    }
        //    deck = deck2;
        //}
        int[] result = new int[3];
        int row;
        int col;
        do
        {
            //deck[random.Next(0, 4), random.Next(0, 13)]--;
            row = random.Next(0, 4);
            col = random.Next(0, 13);
            result[0] = row;
            result[1] = col;
            switch (col)
            {
                case 0: result[2] = 11; break;
                case 1: result[2] = 2; break;
                case 2: result[2] = 3; break;
                case 3: result[2] = 4; break;
                case 4: result[2] = 5; break;
                case 5: result[2] = 6; break;
                case 6: result[2] = 7; break;
                case 7: result[2] = 8; break;
                case 8: result[2] = 9; break;
                case 9: result[2] = 10; break;
                case 10: result[2] = 10; break;
                case 11: result[2] = 10; break;
                case 12: result[2] = 10; break;
            }
            
        } while (deck[row, col] == 0);
        deck[row, col]--;
        return result;

    }
    static void printCard(int[] card, int x, int y)
    {
        string[] cardStrengths = { "A ", "2 ", "3 ", "4 ", "5 ", "6 ", "7 ", "8 ", "9 ", "10", "J ", "Q ", "K " };
        string[] cardLines = new string[7];
        string cardStrength = cardStrengths[card[1]];
        if (card[0] == 0)
        {
            cardLines[0] = "┌───────┐";
            cardLines[1] = "│" + cardStrength + " _   │";
            cardLines[2] = "│  ( )  │";
            cardLines[3] = "| (_X_) │";
            cardLines[4] = "│   I   │";
            cardLines[5] = "│     " + cardStrength + "│";
            cardLines[6] = "└───────┘";
        }
        else if (card[0] == 1)
        {
            cardLines[0] = "┌───────┐";
            cardLines[1] = "│" + cardStrength + " ^   │";
            cardLines[2] = "│  / \\  │";
            cardLines[3] = "|  \\ /  │";
            cardLines[4] = "│   v   │";
            cardLines[5] = "│     " + cardStrength + "│";
            cardLines[6] = "└───────┘";
        }
        else if (card[0] == 2)
        {
            cardLines[0] = "┌───────┐";
            cardLines[1] = "│" + cardStrength + " ^   │";
            cardLines[2] = "│  / \\  │";
            cardLines[3] = "| (_^_) │";
            cardLines[4] = "│   I   │";
            cardLines[5] = "│     " + cardStrength + "│";
            cardLines[6] = "└───────┘";
        }
        else
        {
            cardLines[0] = "┌───────┐";
            cardLines[1] = "│" + cardStrength + "_ _  │";
            cardLines[2] = "│ ( V ) │";
            cardLines[3] = "|  \\ /  │";
            cardLines[4] = "│   V   │";
            cardLines[5] = "│     " + cardStrength + "│";
            cardLines[6] = "└───────┘";
        }

        for (int i = 0; i < cardHeight; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.WriteLine(cardLines[i], cardWidth);
        }
    }

    static void printDeck(int x, int y)
    {
        string[] deckLines = new string[7];
        deckLines[0] = "┌───────┐";
        deckLines[1] = "║░░░░░░░│";
        deckLines[2] = "║░░░░░░░│";
        deckLines[3] = "║░░░░░░░│";
        deckLines[4] = "║░░░░░░░│";
        deckLines[5] = "║░░░░░░░│";
        deckLines[6] = "╚═══════┘";

        for (int i = 0; i < 7; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.WriteLine(deckLines[i]);
        }
    }
    static int GetCardValue(int card)
    {
        int value = 0;
        switch (card / 4)
        {
            case 0: value = 11; break;
            case 1: value = 2; break;
            case 2: value = 3; break;
            case 3: value = 4; break;
            case 4: value = 5; break;
            case 5: value = 6; break;
            case 6: value = 7; break;
            case 7: value = 8; break;
            case 8: value = 9; break;
            case 9: value = 10; break;
            case 10: value = 10; break;
            case 11: value = 10; break;
            case 12: value = 10; break;
            default:
                break;
        }
        return value;
    }
}



