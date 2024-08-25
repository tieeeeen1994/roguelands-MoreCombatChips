using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using TienContentMod.Gadgets;

namespace TienContentMod.Patches.MiscellaneousPatches
{
    [HarmonyPatch(typeof(Menuu))]
    [HarmonyPatch("GetRandomName")]
    [HarmonyGadget(Miscellaneous.GADGET_NAME)]
    public static class Patch_Menuu_GetRandomName
    {
        private const int VanillaRandomNamesCount = 50;

        private static MethodInfo RandomRangeOperand
        {
            get
            {
                return typeof(UnityEngine.Random).GetMethod(
                    "Range",
                    BindingFlags.Public | BindingFlags.Static,
                    null,
                    new System.Type[] { typeof(int), typeof(int) },
                    null);
            }
        }

        private static MethodInfo ChooseExtraRandomNameOperand
        {
            get
            {
                return typeof(Patch_Menuu_GetRandomName).GetMethod(
                    nameof(ChooseExtraRandomName),
                    BindingFlags.NonPublic | BindingFlags.Static
                );
            }
        }

        private static MethodInfo CheckIfVanillaOrExtraOperand
        {
            get
            {
                return typeof(Patch_Menuu_GetRandomName).GetMethod(
                    nameof(CheckIfVanillaOrExtra),
                    BindingFlags.NonPublic | BindingFlags.Static
                );
            }
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Ldc_I4_S, (byte)50),
                new CodeInstruction(OpCodes.Call, RandomRangeOperand),
                new CodeInstruction(OpCodes.Stloc_0)
            });
            if (ilRef == null)
            {
                Miscellaneous.Log("Patch_Menuu_GetRandomName: Cannot find reference. (1)");
            }
            else
            {
                p.InjectInsn(
                    ilRef + 1,
                    new CodeInstruction(OpCodes.Ldc_I4, VanillaRandomNamesCount + ExtraRandomNames.Count),
                    insert: false
                );
                ilRef = ilRef.GetRefByOffset(4);
                ilRef = p.InjectInsns(ilRef, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldloc_0),
                    new CodeInstruction(OpCodes.Call, CheckIfVanillaOrExtraOperand),
                    new CodeInstruction(OpCodes.Brfalse),
                    new CodeInstruction(OpCodes.Ldloc_0),
                    new CodeInstruction(OpCodes.Call, ChooseExtraRandomNameOperand),
                    new CodeInstruction(OpCodes.Ret)
                }, insert: true);
                ilRef.GetRefByOffset(2).GetInsn().operand = p.MarkLabel(ilRef + 6);
            }
            return p.Insns;
        }

        private static bool CheckIfVanillaOrExtra(int chosenNumber)
        {
            return chosenNumber >= VanillaRandomNamesCount;
        }

        private static string ChooseExtraRandomName(int chosenNumber)
        {
            int extraNameIndex = chosenNumber - VanillaRandomNamesCount;
            return ExtraRandomNames[extraNameIndex];
        }

        private static List<string> ExtraRandomNames => new List<string>()
        {
            "Tien", "Miau", // Us
            "Denam", "Catiua", "Vyce", // Tactics Ogre
            "Oracle", "Cross", "Oblige", "Cerbus", "Blanc", // Internal
            "Lenneth", "Silmeria", "Hrist", // Valkyrie Profile
            "Marian", "Rapi", "Anis", "Neon", "Chatterbox", "Blanc", "Noir", "Laplace", "Scarlet", "Dorothy",
            "Soline", "Epinel", "Tia", "Naga", "Marciana", "Zwei", "Ein", "Anchor", "Liter", "", // Nikke
            "Ryu", "Nina", "Momo", "Garr", "Rei", // Breath of Fire
            "Revya", "Gig", "Danette", "Laharl", "Etna", "Flonne", "Valvatorez", "Artina", "Fenrich", "Desco",
            "Marona", "Priere", "Culotte", "Alouette", "Zetta", "Petta", "Pram", // Nippon Ichi
            "Goku", "Vegeta", "Bulma", // Dragon Ball
            "Reimu", "Marisa", "Sanae", // Touhou
            "Pixy", "Blaze", "Chopper", "Archer", "Edge", "Trigger", "Talisman", "Shamrock", // Ace Combat
            "Alundra", "Meia", "Sybill", "Jess", "Ronan", "Septimus", // Alundra
            "Ramza", "Delita", "Agrias", "Ovelia", "Mustadio", "Orlandeau", "Alma", "Cecil", "Kain", "Rydia",
            "Rosa", "Golbez", "Terra", "Kefka", "Locke", "Celes", "Galuf", "Bartz", "Lenna",
            "Faris", "Krile", // Final Fantasy
            "Skeletron", "Spazmatism", "Retinazer", "Plantera", // Terraria
            "Sonic", "Knuckles", "Tails", "Amy", "Robotnik", // Sonic
            "Mario", "Luigi", "Peach", "Bowser", "Yoshi", // Mario
            "Marco", "Tarma", "Eri", "Fio", "Morden", "Allen", // Metal Slug
            "Isaac", "Garet", "Ivan", "Mia", "Felix", "Jenna", "Sheba", "Piers", // Golden Sun
            "Recette", "Tear", // Recettear
            "Peacock", "Parasoul", "Valentine", "Squigly", "Filia", "Cerebella", "Beowulf", "Eliza", // Skullgirls
            "Axton", "Maya", "Salvador", "Zero", "Gaige", "Krieg", "Athena", "Wilhelm", "Nisha",
            "Claptrap", "Jack", "Roland", "Lilith", "Mordecai", "Brick", // Borderlands
            "Yugi", "Judai", "Yusei", "Yuma", "Yuya", // Yu-Gi-Oh!
            "Maggie", "Momoi", "Beau", "Phantom", "Ducksyde", "Maychelle", "Yong", // Farlight 84
            "Arthas", "Jaina", "Thrall", "Sylvanas", "Illidan", "Malfurion", "Tyrande", // Warcraft
            "Price", "Soap", "Ghost", "Makarov", "Gaz", "Shepherd",  // Call of Duty
            "Isaac", "Nicole", "Kendra", "Mercer", "Hammond", "Kyne", // Dead Space
            "Dante", "Vergil", "Nero", // Devil May Cry
            "Kiryu", "Majima", "Nishiki", // Yakuza
            "Crono", "Marle", "Lucca", // Chrono Trigger
            "Shinji", "Asuka", "Rei", "Misato", "Kaworu", "Mari", "Gendo", // Evangelion
            "Chen", "Phenx", "Urak", // Long Dead Project Transcendence...
        };
    }
}
