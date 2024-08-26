using GadgetCore.API;
using HarmonyLib;
using System;
using System.Reflection;
using TienContentMod.Gadgets;
using TienContentMod.ID;

namespace TienContentMod.Patches.BetterAugmentsPatches
{
    [HarmonyPatch]
    [HarmonyGadget(BetterAugments.GADGET_NAME)]
    public static class Patch_Patch_EnemyScript_DropEXP_Prefix
    {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(GadgetCore.CoreMod.GadgetCoreMod));
            Type type = assembly.GetType("GadgetCore.Patches.Patch_EnemyScript_DropEXP");
            return type.GetMethod("Prefix", BindingFlags.Static | BindingFlags.Public);
        }

        [HarmonyPrefix]
        public static bool Prefix(ref int __1)
        {
            switch (Menuu.curAugment)
            {
                case AugmentID.GlibglobHat:
                    __1 = (int)(__1 * 1.5f);
                    break;
            }
            return true;
        }
    }
}