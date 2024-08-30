using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using TienContentMod.Gadgets;
using TienContentMod.ID;

namespace TienContentMod.Patches.DroidsReworkPatches
{
    [HarmonyPatch]
    [HarmonyGadget(DroidsRework.GADGET_NAME, "Tiers+")]
    public static class Patch_Recipes_CreationMachine
    {
        [HarmonyTargetMethod]
        public static MethodBase TargetMethod()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(TiersPlus.TiersPlus));
            Type type = assembly.GetType("TiersPlus.Recipes");
            return type.GetMethod("CreationMachine", BindingFlags.Static | BindingFlags.NonPublic);
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            if (DroidsRework.TiersPlusGold15)
            {
                var ilRef = p.FindRefByInsns(new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Call),
                    new CodeInstruction(OpCodes.Ldstr, "Tiers+:poweremblem"),
                    new CodeInstruction(OpCodes.Callvirt),
                    new CodeInstruction(OpCodes.Callvirt),
                    new CodeInstruction(OpCodes.Ldc_I4, ItemID.GOLD15),
                    new CodeInstruction(OpCodes.Ldc_I4_1),
                    new CodeInstruction(OpCodes.Ldc_I4_0),
                    new CodeInstruction(OpCodes.Ldc_I4_3),
                    new CodeInstruction(OpCodes.Ldc_I4_0),
                    new CodeInstruction(OpCodes.Ldc_I4_3),
                    new CodeInstruction(OpCodes.Newarr, typeof(int)),
                    new CodeInstruction(OpCodes.Ldc_I4_3),
                    new CodeInstruction(OpCodes.Newarr, typeof(int)),
                    new CodeInstruction(OpCodes.Newobj),
                    new CodeInstruction(OpCodes.Ldc_I4_0),
                    new CodeInstruction(OpCodes.Call)
                });
                if (ilRef == null)
                {
                    DroidsRework.Log("Patch_Recipes_CreationMachine: Failed to find reference.");
                }
                else
                {
                    p.RemoveInsns(ilRef, 16);
                }
            }
            return p.Insns;
        }
    }
}
