using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using TienContentMod.Gadgets;

namespace TienContentMod.Patches.MoreCombatChipsPatches
{
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("UseSkill")]
    [HarmonyGadget(MoreCombatChips.GADGET_NAME)]
    public static class Patch_GameScript_UseSkill
    {
        private static FieldInfo WardingField
        {
            get => typeof(GameScript).GetField("warding", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private static FieldInfo AuguringField
        {
            get => typeof(GameScript).GetField("auguring", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Ldc_I4_S, 16),
                new CodeInstruction(OpCodes.Bne_Un),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, WardingField),
                new CodeInstruction(OpCodes.Brfalse),
                new CodeInstruction(OpCodes.Ret),
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Ldc_I4_S, 16),
                new CodeInstruction(OpCodes.Bne_Un),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldc_I4_1),
                new CodeInstruction(OpCodes.Stfld, WardingField),
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Ldc_I4_S, 26),
                new CodeInstruction(OpCodes.Bne_Un),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, AuguringField),
                new CodeInstruction(OpCodes.Brfalse),
                new CodeInstruction(OpCodes.Ret),
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Ldc_I4_S, 26),
                new CodeInstruction(OpCodes.Bne_Un),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldc_I4_1),
                new CodeInstruction(OpCodes.Stfld, AuguringField)
            });
            if (ilRef == null)
            {
                MoreCombatChips.Log("Patch_GameScript_UseSkill: Reference not found.");
            }
            else
            {
                p.RemoveInsns(ilRef, 26);
            }
            return p.Insns;
        }
    }
}
