using GadgetCore.API;
using HarmonyLib;
using TienContentMod.Gadgets;
using TienContentMod.Services;

namespace TienContentMod.Patches.MiscellaneousPatches
{
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("StartingGear")]
    [HarmonyGadget(Miscellaneous.GADGET_NAME)]
    public static class Patch_GameScript_StartingGear
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            var stats = StatService.NewStats();
            for (int i = 0; i < Menuu.raceStat.Length; i++)
            {
                stats[i] += Menuu.raceStat[i];
                if (Menuu.curTrait0 == i || Menuu.curTrait1 == i)
                {
                    stats[i] += 1;
                }
            }
            // Do something with stats here and return false.
            return true;
        }
    }
}
