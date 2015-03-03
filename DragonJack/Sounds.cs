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
            { "placeCard", new SoundPlayer(@"../../CasinoSoundPackage/cardPlace1.wav") },
            { "placeChips", new SoundPlayer(@"../../CasinoSoundPackage/cardPlace1.wav") },
            { "music", new SoundPlayer(@"../../CasinoSoundPackage/cardPlace1.wav") }
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
            LoadSounds(playersRef);
            playersRef[sound].Play();
        }
    }
}
