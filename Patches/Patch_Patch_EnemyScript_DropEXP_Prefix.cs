using System;
using System.Reflection;
using GadgetCore.API;
using HarmonyLib;
using MoreCombatChips.ID;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will add experience points modifications done by the mod.
    /// </summary>
    [HarmonyPatch]
    [HarmonyGadget("More Combat Chips")]
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
