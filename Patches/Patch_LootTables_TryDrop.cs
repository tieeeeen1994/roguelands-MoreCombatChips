using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using MoreCombatChips.ID;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace MoreCombatChips.Patches
{
    [HarmonyPatch]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_LootTables_TryDrop
    {
        private static FieldInfo ItemQ
        {
            get => typeof(Item).GetField("q", BindingFlags.Public);
        }

        private static MethodInfo DropEffectsMethod
        {
            get => typeof(Patch_LootTables_TryDrop).GetMethod(
                nameof(DropEffects),
                BindingFlags.NonPublic | BindingFlags.Static
            );
        }

        private static Type LootTableEntryType
        {
            get => typeof(LootTables).GetNestedType("LootTableEntry", BindingFlags.NonPublic);
        }

        private static FieldInfo ItemToDropField
        {
            get => LootTableEntryType.GetField("itemToDrop", BindingFlags.Public | BindingFlags.Instance);
        }

        [HarmonyTargetMethod]
        public static MethodBase TargetMethod()
        {
            return typeof(LootTables).GetNestedType("LootTableEntry", BindingFlags.NonPublic)
                                     .GetMethod("TryDrop", BindingFlags.Public | BindingFlags.Instance);
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Br_S),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Call),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Sub),
                new CodeInstruction(OpCodes.Conv_R4),
                new CodeInstruction(OpCodes.Mul),
                new CodeInstruction(OpCodes.Call),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Add),
                new CodeInstruction(OpCodes.Stfld, ItemQ),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Brfalse_S)
            });
            if (ilRef == null)
            {
                MoreCombatChips.Log("Patch_LootTables_TryDrop: No reference point.");
            }
            else
            {
                ilRef = ilRef.GetRefByOffset(16);
                p.InjectInsns(ilRef, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldfld, ItemToDropField),
                    new CodeInstruction(OpCodes.Call, DropEffectsMethod),
                }, insert: true);
            }
            return p.Insns;
        }

        private static void DropEffects(Item item)
        {
            if (Menuu.curAugment == AugmentID.ShmooHat && IsMonsterLoot(item.id))
            {
                item.q *= 3;
            }
        }

        private static bool IsMonsterLoot(int id)
        {
            return (ItemRegistry.GetTypeByID(id) & (ItemType.EMBLEM | ItemType.LOOT_MASK)) ==
                   (ItemType.LOOT | ItemType.MONSTER);
        }
    }
}
