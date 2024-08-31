using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using BigNumberCore;
using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using TienContentMod.Gadgets;
using TienContentMod.Scripts;
using UnityEngine;

namespace TienContentMod.Patches.MoreCombatChipsPatches
{
    [HarmonyPatch(typeof(DoublePlayerAttacks))]
    [HarmonyPatch("AugurDouble")]
    [HarmonyGadget(MoreCombatChips.GADGET_NAME)]
    public static class Patch_DoublePlayerAttacks_AugurDouble
    {
        private static MethodInfo UpdateTrackerMethod
        {
            get => typeof(Patch_DoublePlayerAttacks_AugurDouble).GetMethod(
                nameof(UpdateTracker),
                BindingFlags.NonPublic | BindingFlags.Static
            );
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            var ilRef = p.GetRefByIndex(p.Insns.Count - 1);
            if (ilRef == null)
            {
                MoreCombatChips.Log("Patch_DoublePlayerAttacks_AugurDouble: Reference not found.");
            }
            else
            {
                p.InjectInsns(ilRef, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldloc_1),
                    new CodeInstruction(OpCodes.Call, UpdateTrackerMethod)
                }, insert: true);
            }
            return p.Insns;
        }

        private static void UpdateTracker(PlayerScript instance, GameObject augurObject)
        {
            MoreCombatChips.Log("Patch_DoublePlayerAttacks_AugurDouble: UpdateTracker called.");
            HealWardTracker tracker = instance.GetComponent<HealWardTracker>();
            if (tracker != null)
            {
                Healward healWard = augurObject.GetComponent<Healward>();
                if (healWard == null)
                {
                    MoreCombatChips.Log("Patch_DoublePlayerAttacks_AugurDouble: Healward not found.");
                }
                else
                {
                    tracker.StartCoroutine(tracker.ReplaceAugurWith(healWard));
                }
            }
            else
            {
                MoreCombatChips.Log("Patch_DoublePlayerAttacks_AugurDouble: HealWardTracker not found.");
            }
        }
    }
}
