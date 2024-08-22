using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using MoreCombatChips.ID;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will add modifications when tyring to roll for gear tier.
    /// </summary>
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("GetRandomTier")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_GameScript_GetRandomTier
    {
        private static MethodInfo ExtraAugmentEffectsMethod
        {
            get => typeof(Patch_GameScript_GetRandomTier).GetMethod(
                nameof(ExtraAugmentEffects),
                BindingFlags.Static | BindingFlags.NonPublic
            );
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Ldloc_3),
                new CodeInstruction(OpCodes.Sub),
                new CodeInstruction(OpCodes.Stloc_0),
                new CodeInstruction(OpCodes.Ldloc_0)
            });
            if (ilRef == null)
            {
                MoreCombatChips.Log("Patch_GameScript_GetRandomTier: Transpiler could not find any reference point.");
            }
            else
            {
                ilRef = ilRef.GetRefByOffset(4);
                p.InjectInsns(ilRef, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldloca, 0),
                    new CodeInstruction(OpCodes.Call, ExtraAugmentEffectsMethod)
                }, insert: true);
            }
            return p.Insns;
        }

        private static void ExtraAugmentEffects(ref int currentRoll)
        {
            MoreCombatChips.Log("Patch_GameScript_GetRandomTier: Trying to modify roll.");
            switch (Menuu.curAugment)
            {
                case AugmentID.ChamchamHat:
                    MoreCombatChips.Log("Patch_GameScript_GetRandomTier: Chamcham Hat found. BE LUCKIER!");
                    currentRoll -= 3;
                    break;
            }
        }
    }
}
