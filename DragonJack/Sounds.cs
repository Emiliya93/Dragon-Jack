namespace DragonJack
{
    using System;
    using System.Media;
    using System.Diagnostics;
    using System.Collections.Generic;

    public class Sounds
    {
        private static readonly Dictionary<string, SoundPlayer> playersRef = new Dictionary<string, SoundPlayer>
        {
            { "placeCard", new SoundPlayer(@"../../SoundFiles/cardPlace1.wav") },
            { "placeChips", new SoundPlayer(@"../../SoundFiles/chipsHandle6.wav") },
            { "scoreMusic", new SoundPlayer(@"../../SoundFiles/Loop_23.wav") },
            { "swordClash", new SoundPlayer(@"../../SoundFiles/SwordClash.wav") }
        };
        public static void LoadSounds(Dictionary<string, SoundPlayer> playersRef)
        {
            try
            {
                foreach (KeyValuePair<string, SoundPlayer> entry in playersRef)
                {
                    entry.Value.Load();
                }
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
            playersRef[sound].Play();
            
        }
        public static void PlayMusic(string sound)
        {
            LoadSounds(playersRef);
            SoundPlayer player =  playersRef[sound];
            player.PlayLooping();
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.Enter || key.Key != ConsoleKey.Escape)
            {
                key = Console.ReadKey(true);
            }
            player.Stop();
        }
    }
}
