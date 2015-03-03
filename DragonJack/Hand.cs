namespace DragonJack
{
    using System;
    using System.Collections.Generic;

    public class Hand
    {
        List<Card> cards = new List<Card>();

        public void FillHand(Card card)
        {
            this.cards.Add(card);
        }

        public List<Card> GetCardsInHand()
        {
            return this.cards;
        }

        private int AcesCount()
        {
            int acesCount = 0;

            foreach (var card in cards)
            {
                // There is an Ace
                if (card.CardStrength == 0)
                {
                    acesCount++;
                }
            }
            return acesCount;
        }

        public int GetSum()
        {
            int sum = 0;
            int acesSum = 0;
            foreach (var card in cards)
            {
                sum += card.CardValue;
                if (card.CardValue == 11)
                {
                    acesSum = sum - 10;
                }
            }
            if (sum > 21)
            {
                int acesCount = AcesCount();
                while (acesCount > 0 && sum > 21)
                {
                    sum -= 10;
                    acesCount--;
                }
            }
            return sum;
        }

        public void PrintSum()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Yellow;
            string sums;
            int fullSum = 0;
            foreach (var card in cards)
            {
                fullSum += card.CardValue;
            }
            sums = GetSum().ToString();
            int acesCount = AcesCount();
            if (fullSum - (acesCount - 1) * 10 < 21 && acesCount > 0)
            {
                sums = (fullSum - acesCount * 10).ToString().PadLeft(2, ' ') + "/" + GetSum().ToString() + " ";
            }

            Console.WriteLine(sums.PadLeft(5, ' '));
        }

        public bool AreEqualCards()
        {
            if (cards[0].CardValue == cards[1].CardValue)
            {
                return true;
            }
            return false;
        }

        public bool IsDragonJack()
        {
            if (cards[0].CardValue + cards[1].CardValue == 21)
            {
                return true;
            }
            return false;
        }
    }
}