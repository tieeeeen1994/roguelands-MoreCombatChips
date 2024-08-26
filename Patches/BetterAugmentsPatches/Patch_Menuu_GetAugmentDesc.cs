using GadgetCore.API;
using HarmonyLib;
using TienContentMod.Gadgets;
using TienContentMod.ID;

namespace TienContentMod.Patches.BetterAugmentsPatches
{
    [HarmonyPatch(typeof(Menuu))]
    [HarmonyPatch("GetAugmentDesc")]
    [HarmonyGadget(BetterAugments.GADGET_NAME)]
    public static class Patch_Menuu_GetAugmentDesc
    {
        [HarmonyPrefix]
        public static bool Prefix(int a, ref string __result)
        {
            switch (a)
            {
                case AugmentID.RebellionHeadpiece:
                    __result = "Headgear worn by a true Rebel of the Starlight Empire.\n" +
                               "Max HP is now 75. Gain 2 DEX per level.";
                    return false;

                case AugmentID.CreatorMask:
                    __result = "A mask crafted by the fabric of time and space.\n" +
                               "Upon level up, gain 1 MAG with a 25% chance or 1 FTH with a 75% chance.";
                    return false;

                case AugmentID.EyepodHat:
                    __result = "A hat made from the remains of an Eyepod. Yikes.\n" +
                               "On level up, stats gained are double, but equipment is only 50% effective.";
                    return false;

                case AugmentID.GasMask:
                    __result = "Gray Enigma's standard gas mask to protect the wearer from noxious gases.\n" +
                               "When using a Combat Chip with insufficent mana, consume Stamina instead.";
                    return false;

                case AugmentID.GlibglobHat:
                    __result = "Beheaded Glibglob, now used as a hat! It is actually quite comfortable.\n" +
                               "Hallucinations occur when wearing it. Some say it was the Glibglob's memories.\n" +
                               "Gain 50% more EXP from enemies.";
                    return false;

                case AugmentID.ChamchamHat:
                    __result = "A decorative hat worn during the Aether Festival. No Chamchams were harmed.\n" +
                               "Gain 3% more chance into crafting higher tiered gear because why not?";
                    return false;

                case AugmentID.ShmooHat:
                    __result = "It terrifyingly looks like a real Shmoo. It's not, right?\n" +
                               "Monster drops are tripled.";
                    return false;

                case AugmentID.ShroomHat:
                    __result = "Peeled off the cap of a mushroom, only to find out it is an actual hat!\n" +
                               "Quite a pungent smell.\n" +
                               "Combat Chips provide twice the effect when enhancing stats.";
                    return false;

                case AugmentID.SlimeHat:
                    __result = "A hat made from a Slime. It's squishy. You look awesome.\n" +
                               "Gain double EXP from the Gear Chalice.";
                    return false;

                case AugmentID.MechCityBeanie:
                    __result = "Mech City is all about the fashion, and this is what the cool kids are wearing.\n" +
                               "Beaming to and from planets will fully recover your health and mana.";
                    return false;

                default:
                    return true;
            }
        }
    }
}