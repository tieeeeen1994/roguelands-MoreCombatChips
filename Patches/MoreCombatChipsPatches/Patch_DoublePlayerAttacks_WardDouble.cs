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
    [HarmonyPatch("WardDouble")]
    [HarmonyGadget(MoreCombatChips.GADGET_NAME, "BigNumberCore")]
    public static class Patch_DoublePlayerAttacks_WardDouble
    {
        private static MethodInfo UpdateTrackerMethod
        {
            get => typeof(Patch_DoublePlayerAttacks_WardDouble).GetMethod(
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
                MoreCombatChips.Log("Patch_DoublePlayerAttacks_WardDouble: Reference not found.");
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

        private static void UpdateTracker(DoublePlayerAttacks instance, GameObject healWardObject)
        {
            MoreCombatChips.Log("Patch_DoublePlayerAttacks_WardDouble: UpdateTracker called.");
            HealWardTracker tracker = instance.GetComponent<HealWardTracker>();
            if (tracker != null)
            {
                Healward healWard = healWardObject.GetComponent<Healward>();
                if (healWard == null)
                {
                    MoreCombatChips.Log("Patch_DoublePlayerAttacks_WardDouble: Healward not found.");
                }
                else
                {
                    tracker.StartCoroutine(tracker.ReplaceWardWith(healWard));
                }
            }
            else
            {
                MoreCombatChips.Log("Patch_DoublePlayerAttacks_WardDouble: HealWardTracker not found.");
            }
        }
    }
}
