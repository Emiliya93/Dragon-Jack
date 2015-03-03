namespace DragonJack
{
    using System;
    using System.Media;
    using System.Diagnostics;
    using System.Collections.Generic;

    public class Sounds
    {
        private static readonly Dictionary<string, string> playersRef = new Dictionary<string, string>
        {
            { "placeCard", @"../../SoundFiles/cardPlace1.wav" },
            { "placeChips", @"../../SoundFiles/chipsHandle6.wav" },
            { "scoreMusic", @"../../SoundFiles/Loop_23.wav" },
            { "swordClash", @"../../SoundFiles/SwordClash.wav" },
            { "swordSwoosh", @"../../SoundFiles/Swoosh01.wav" },
            { "dragonjack", @"../../SoundFiles/Drum.wav" }
        };
        private static void LoadSound(SoundPlayer player)
        {
            try
            {
                player.Load();
            }
            catch (TimeoutException ex)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                return;
            }
        }

        public static void PlaySound(string sound)
        {
            SoundPlayer player = new SoundPlayer(playersRef[sound]);
            LoadSound(player);
            player.Play();
            player.Dispose();
            
        }
        public static void PlayMusic(string sound)
        {
            SoundPlayer player = new SoundPlayer(playersRef[sound]);
            LoadSound(player);
            player.PlayLooping();
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.Enter || key.Key != ConsoleKey.Escape)
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Escape)
                {
                    player.Stop();
                    player.Dispose();
                    break;
                }
            }
        }
    }
}
