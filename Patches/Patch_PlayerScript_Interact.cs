using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will add modifications when tyring to roll for gear tier.
    /// </summary>
    [HarmonyPatch]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_PlayerScript_Interact
    {
        private static FieldInfo CurAugment
        {
            get => typeof(Menuu).GetField("curAugment", BindingFlags.Public | BindingFlags.Static);
        }

        private static MethodInfo CombatChipMenu
        {
            get => typeof(GameScript).GetMethod("CombatChipMenu", BindingFlags.Instance | BindingFlags.Public);
        }

        [HarmonyTargetMethod]
        public static MethodBase TargetMethod()
        {
            return typeof(PlayerScript).GetNestedType("<Interact>c__Iterator3D", BindingFlags.NonPublic)
                                       .GetMethod("MoveNext", BindingFlags.Instance | BindingFlags.Public);
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            if (MoreCombatChips.ChamchamHatChange)
            {
                var ilRef = p.FindRefByInsns(new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldsfld, CurAugment),
                    new CodeInstruction(OpCodes.Ldc_I4_S, (byte)16),
                    new CodeInstruction(OpCodes.Beq),
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldfld),
                    new CodeInstruction(OpCodes.Ldfld),
                    new CodeInstruction(OpCodes.Callvirt, CombatChipMenu)
                });
                if (ilRef == null)
                {
                    MoreCombatChips.Log("Patch_PlayerScript_Interact: Transpiler could not find any reference point.");
                }
                else
                {
                    p.RemoveInsns(ilRef, 3);
                }
            }
            return p.Insns;
        }
    }
}
