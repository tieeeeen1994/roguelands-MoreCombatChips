using GadgetCore.API;
using HarmonyLib;
using UnityEngine;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will fix the Quadcopter's movement in multiplayer.
    /// </summary>
    [HarmonyPatch(typeof(Copter))]
    [HarmonyPatch("Start")]
    [HarmonyGadget("More Combat Chips")]
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