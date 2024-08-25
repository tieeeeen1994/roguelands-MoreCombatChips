using TienContentMod.Gadgets;

namespace TienContentMod
{
    public static class TienContentMod
    {
        public const string MOD_NAME = "Tien Content Mod";
        public const string MOD_VERSION = "2.0.0"; // Set this to the version of your mod.
        public const string MOD_DESCRIPTION = "WIP";
        public const string MOD_AUTHOR = "Tien";
        public const string COPYRIGHT = "© 2020 Tien. All rights reserved.";

        internal static bool TEST = false;

        internal static void Log(string gadget, string message)
        {
            switch (gadget)
            {
                case MoreCombatChips.GADGET_NAME:
                    MoreCombatChips.Log(message);
                    break;
                case Miscellaneous.GADGET_NAME:
                    Miscellaneous.Log(message);
                    break;
                case BetterAugments.GADGET_NAME:
                    Miscellaneous.Log(message);
                    break;
                case DroidsRework.GADGET_NAME:
                    DroidsRework.Log(message);
                    break;
            }
        }
    }
}
