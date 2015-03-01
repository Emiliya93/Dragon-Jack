namespace DragonJack
{
    using System;

    class GlobalConsts
    {
        public static Random random = new Random();
        public const int cardHeight = 8;
        public const int cardWidth = 9;
        public const int suitsCount = 4;
        public const int cardStrengthsCount = 13;
        public const int decksCount = 6;
        public const int cardOverlap = 4;
        public const int maxCardsWidth = (8 + (decksCount - 1)) * cardOverlap;
        public const int winWidth = 2 * maxCardsWidth + 30;
        public const int winHeight = 40;
        public const int dealerPosX = (winWidth - maxCardsWidth) / 2;
        public const int dealerPosY = winHeight / 10;
        public const int playerPosX = (winWidth - maxCardsWidth) / 2;
        public const int playerPosY = winHeight - winHeight / 10 - cardHeight;
        public const int doublePosX1 = (winWidth - (maxCardsWidth) * 2) / 4;
        public const int doublePosY1 = playerPosY;
        public const int doublePosX2 = doublePosX1 + maxCardsWidth + doublePosX1;
        public const int doublePosY2 = playerPosY;
        public const int legendPosX = 5;
        public const int legendPosY = 17;
        public const int dealingSpeed = 500;
    }
}
