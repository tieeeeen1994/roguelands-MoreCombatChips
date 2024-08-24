using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using MoreCombatChips.ID;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will modify the damage of Gold droid.
    /// </summary>
    [HarmonyPatch]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_DroidScript_Attack
    {
        private static FieldInfo EquippedIDsField
        {
            get => typeof(GameScript).GetField("equippedIDs", BindingFlags.Public | BindingFlags.Static);
        }

        private static FieldInfo MODSField
        {
            get => typeof(GameScript).GetField("MODS", BindingFlags.Public | BindingFlags.Static);
        }

        private static MethodInfo GetDamageMethod
        {
            get => typeof(Patch_DroidScript_Attack).GetMethod(
                nameof(GetDamage),
                BindingFlags.NonPublic | BindingFlags.Static
            );
        }

        [HarmonyTargetMethod]
        public static MethodBase TargetMethod()
        {
            return typeof(DroidScript).GetNestedType("<Attack>c__Iterator4", BindingFlags.NonPublic)
                                      .GetMethod("MoveNext", BindingFlags.Public | BindingFlags.Instance);
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldc_R4, 5),
                new CodeInstruction(OpCodes.Ldsfld, EquippedIDsField),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Ldelem_I4),
                new CodeInstruction(OpCodes.Ldsfld, MODSField),
                new CodeInstruction(OpCodes.Ldc_I4_S, 10),
                new CodeInstruction(OpCodes.Ldelem_I4),
                new CodeInstruction(OpCodes.Conv_R4),
            });
            if (ilRef == null)
            {
                MoreCombatChips.Log("Patch_DroidScript_Attack: Reference not found.");
            }
            else
            {
                ilRef = ilRef.GetRefByOffset(1);
                p.InjectInsn(ilRef, new CodeInstruction(OpCodes.Call, GetDamageMethod), insert: false);
            }
            return p.Insns;
        }

        private static float GetDamage()
        {
            return Mathf.Max(1f, InstanceTracker.GameScript.GetFinalStat(StatID.TEC) / 3);
        }
    }
}
