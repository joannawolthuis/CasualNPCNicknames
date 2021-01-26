using StardewModdingAPI;
using StardewValley;
using System;
using System.Text.RegularExpressions;

namespace CasualNPCNicknames
{
    public static class Patches
    {
        private static IMonitor Monitor;

        public static string withNicknames = ModEntry.config.Nicknames;
        public static int minHeartLevel = ModEntry.config.HeartLevel;
        public static string[] nameNickPairs = withNicknames.Split(';');

        // call this method from your Entry class
        public static void Initialize(IMonitor monitor)
        {
            Monitor = monitor;
        }

        public static void DrawString_prefix(ref string s)
        {
            if (Game1.hasLoadedGame )
            {
                if(!Regex.IsMatch(s, " \\w+ ")) // one worders only
                {
                    try
                    {
                        foreach (var pair in nameNickPairs)
                        {
                            string[] nameNickPair = pair.Split(':');
                            string origName = nameNickPair[0];
                            string nickName = nameNickPair[1];

                            if (Game1.player.getFriendshipLevelForNPC(origName) > minHeartLevel)
                            {
                                string[] regexParts = { "^", origName, "$" };
                                string name2regex = String.Join("", regexParts);
                                bool isTarget = Regex.IsMatch(s, name2regex);
                                if (isTarget)
                                {
                                    // replace string with nickname
                                    // Monitor.Log($"New nickname for {origName}: {nickName}", LogLevel.Debug);
                                    s = nickName;
                                };
                            };
                        };
                    }
                    catch (Exception ex)
                    {
                        Monitor.Log($"Failed to replace name with nickname in dialog", LogLevel.Debug);
                    };

                }
            }

        }

    }
}