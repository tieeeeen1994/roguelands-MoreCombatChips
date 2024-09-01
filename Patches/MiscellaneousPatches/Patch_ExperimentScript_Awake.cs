using GadgetCore.API;
using HarmonyLib;
using TienContentMod.Gadgets;
using UnityEngine;

namespace TienContentMod.Patches.MiscellaneousPatches
{
    [HarmonyPatch(typeof(ExperimentScript))]
    [HarmonyPatch("Awake")]
    [HarmonyGadget(Miscellaneous.GADGET_NAME)]
    public static class Patch_ExperimentScript_Awake
    {
        [HarmonyPrefix]
        public static bool Prefix(ExperimentScript __instance)
        {
            if (Network.isServer)
            {
                __instance.StartCoroutine(__instance.Charge());
            }
            __instance.drops = new int[] { 26, 26, 26 };
            __instance.Initialize(1500, 70, 500, __instance.drops, 500);
            __instance.networkR2 = (NetworkR2)__instance.gameObject.GetComponent("NetworkR2");
            return false;
        }
    }
}
