using GadgetCore.API;
using HarmonyLib;
using TienContentMod.Gadgets;
using TienContentMod.Scripts;

namespace TienContentMod.Patches.MoreCombatChipsPatches
{
    [HarmonyPatch(typeof(PlayerScript))]
    [HarmonyPatch("Awake")]
    [HarmonyGadget(MoreCombatChips.GADGET_NAME)]
    public static class Patch_PlayerScript_Awake
    {
        [HarmonyPostfix]
        public static void Postfix(PlayerScript __instance)
        {
            MoreCombatChips.Log("Patch_PlayerScript_Awake: Adding HealWardTracker.");
            __instance.gameObject.AddComponent<HealWardTracker>();
        }
    }
}
