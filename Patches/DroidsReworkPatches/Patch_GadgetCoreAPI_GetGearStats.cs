using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using TienContentMod.Gadgets;

namespace TienContentMod.Patches.DroidsReworkPatches
{
    [HarmonyPatch(typeof(GadgetCoreAPI))]
    [HarmonyPatch("GetGearStats")]
    [HarmonyGadget(DroidsRework.GADGET_NAME)]
    public class Patch_GadgetCoreAPI_GetGearStats
    {
        private static FieldInfo ItemInfoType
        {
            get => typeof(ItemInfo).GetField("Type", BindingFlags.Public | BindingFlags.Instance);
        }

        private static MethodInfo IncludeDroidsComputationMethod
        {
            get => typeof(Patch_GadgetCoreAPI_GetGearStats).GetMethod(
                nameof(IncludeDroidsComputation),
                BindingFlags.NonPublic | BindingFlags.Static
            );
        }

        [HarmonyTranspiler]
        [HarmonyOverridden("BigNumberCore.BigNumberCore.gadget")]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            EmitModifiedCheck1st(p);
            EmitModifiedCheck2nd(p);
            return p.Insns;
        }

        private static void IncludeDroidsComputation(ref bool itemLevels, ItemInfo itemInfo)
        {
            itemLevels = (itemInfo.Type & ItemType.LEVELING) == ItemType.LEVELING;
        }

        private static void EmitModifiedCheck1st(TranspilerHelper.CILProcessor p)
        {
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldfld, ItemInfoType),
                new CodeInstruction(OpCodes.Ldc_I4_S, (byte)64),
                new CodeInstruction(OpCodes.And),
                new CodeInstruction(OpCodes.Ldc_I4_S, (byte)64),
                new CodeInstruction(OpCodes.Bne_Un_S),
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldfld, ItemInfoType),
                new CodeInstruction(OpCodes.Ldc_I4_S, (byte)31),
                new CodeInstruction(OpCodes.And),
                new CodeInstruction(OpCodes.Ldc_I4_S, (byte)21),
                new CodeInstruction(OpCodes.Ceq),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Ceq),
                new CodeInstruction(OpCodes.Br_S),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Stloc_2)
            });
            if (ilRef == null)
                DroidsRework.Log("Patch_GadgetCoreAPI_GetGearStats: References not found. (EmitModifiedCheck1st)");
            else
            {
                ilRef = p.InjectInsns(ilRef, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldloca_S, 2),
                    new CodeInstruction(OpCodes.Ldloc_1),
                    new CodeInstruction(OpCodes.Call, IncludeDroidsComputationMethod),
                }, insert: true);
                p.RemoveInsns(ilRef + 3, 17);
            }
        }

        private static void EmitModifiedCheck2nd(TranspilerHelper.CILProcessor p)
        {
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldfld, ItemInfoType),
                new CodeInstruction(OpCodes.Ldc_I4_S, (byte)31),
                new CodeInstruction(OpCodes.And),
                new CodeInstruction(OpCodes.Ldc_I4_S, (byte)21),
                new CodeInstruction(OpCodes.Beq_S),
            });
            if (ilRef == null)
                DroidsRework.Log("Patch_GadgetCoreAPI_GetGearStats: References not found. (EmitModifiedCheck2nd)");
            else
            {
                p.RemoveInsns(ilRef, 6);
            }
        }
    }
}
