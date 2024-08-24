using GadgetCore.API;
using HarmonyLib;
using UnityEngine;

namespace TienContentMod.Patches.MoreCombatChipsPatches
{
    [HarmonyPatch(typeof(Copter))]
    [HarmonyPatch("Start")]
    [HarmonyGadget(MoreCombatChips.GADGET_NAME)]
    public static class Patch_Copter_Start
    {
        [HarmonyPrefix]
        public static bool Prefix(Copter __instance, ref Rigidbody ___r)
        {
            ___r = __instance.GetComponent<Rigidbody>();
            __instance.StartCoroutine(__instance.Move());
            return false;
        }
    }
}
