using GadgetCore.API;
using HarmonyLib;
using TienContentMod.Gadgets;
using TienContentMod.ID;

namespace TienContentMod.Patches.MoreCombatChipsPatches
{
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("GetChipDesc")]
    [HarmonyGadget(MoreCombatChips.GADGET_NAME)]
    public static class Patch_GameScript_GetChipDesc
    {
        [HarmonyPrefix]
        public static bool Prefix(int id, ref string __result)
        {
            switch (id)
            {
                case CombatChipID.Quadracopter:
                    __result = "Summon a Quadracopter that shoots 15 projectiles.\nScales with 3x TEC.";
                    return false;

                default:
                    return true;
            }
        }
    }
}