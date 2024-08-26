//#define TEST

#if TEST
#warning TEST is defined. Set this to false before release.
#endif

using System;
using System.Collections.Generic;
using TienContentMod.Gadgets;

namespace TienContentMod
{
    public static class TienContentMod
    {
        public const string MOD_NAME = "Tien Content Mod";
        public const string MOD_VERSION = "2.1.0"; // Set this to the version of your mod.
        public const string MOD_DESCRIPTION = "WIP";
        public const string MOD_AUTHOR = "Tien";
        public const string COPYRIGHT = "© 2020 Tien. All rights reserved.";

        internal const bool TEST =
#if TEST
            true;

#else
            false;

#endif

        private static readonly Dictionary<string, Action<string>> GadgetLoggers =
            new Dictionary<string, Action<string>>()
            {
                { MoreCombatChips.GADGET_NAME, MoreCombatChips.Log },
                { Miscellaneous.GADGET_NAME, Miscellaneous.Log },
                { BetterAugments.GADGET_NAME, Miscellaneous.Log },
                { DroidsRework.GADGET_NAME, DroidsRework.Log }
            };

        internal static void Log(string gadget, string message)
        {
            if (GadgetLoggers.TryGetValue(gadget, out var logAction))
            {
                logAction(message);
            }
            else
            {
                throw new ArgumentException($"Unknown Gadget: {gadget}");
            }
        }
    }
}