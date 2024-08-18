using GadgetCore.API;
using HarmonyLib;
using System.Collections.Generic;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will change the description of augments.
    /// </summary>
    [HarmonyPatch(typeof(Menuu))]
    [HarmonyPatch("GetRandomName")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_Menuu_GetRandomName
    {
        [HarmonyPrefix]
        public static bool Prefix(ref string __result)
        {
            if (UnityEngine.Random.Range(0, 2) >= 1) // Rebellion Headpiece
            {
                __result = ExtraRandomNames[UnityEngine.Random.Range(0, ExtraRandomNames.Count)];
                return false;
            }
            else
            {
                return true;
            }
        }

        private static List<string> ExtraRandomNames => new List<string>()
        {
            "Tien", "Miau", // Us
            "Denam", "Catiua", "Vyce", // Tactics Ogre
            "Oracle", "Cross", "Oblige", "Cerbus", "Blanc", // Internal
            "Lenneth", "Silmeria", "Hrist", // Valkyrie Profile
            "Marian", "Rapi", "Anis", "Neon", "Chatterbox", // Nikke
            "Ryu", "Nina", "Momo", "Garr", "Rei", // Breath of Fire
            "Revya", "Gig", "Danette", "Laharl", "Etna", "Flonne", "Valvatorez", "Artina", "Fenrich", "Desco",
            "Ash", "Marona", "Priere", "Culotte", "Alouette", "Zetta", "Petta", "Pram", // Nippon Ichi
            "Goku", "Vegeta", "Bulma", // Dragon Ball
            "Sora", "Riku", "Kairi", // Kingdom Hearts
            "Reimu", "Marisa", "Sanae", // Touhou
            "Pixy", "Blaze", "Chopper", "Archer", "Edge", "Trigger", "Talisman", "Shamrock", // Ace Combat
            "Alundra", "Meia", "Sybill", "Jess", "Ronan", "Septimus", // Alundra
            "Ramza", "Delita", "Agrias", "Ovelia", "Mustadio", "Orlandeau", "Alma","Cecil", "Kain", "Rydia",
            "Rosa", "Golbez", "Terra", "Kefka", "Locke", "Celes", // Final Fantasy
            "Skeletron", "Spazmatism", "Retinazer", "Plantera", // Terraria
            "Sonic", "Knuckles", "Tails", "Amy", "Shadow", "Rouge", "Robotnik", // Sonic
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
            "Isaac", "Nicole", "Kendra", "Mercer", "Hammond", "Mathius", "Kyne", // Dead Space
            "Dante", "Vergil", "Nero", // Devil May Cry
            "Kratos", "Atreus", "Freya", "Mimir", // God of War
            "Kiryu", "Majima", "Nishiki", "Haruka", "Daigo", "Saejima", "Akiyama", "Tanimura", // Yakuza
            "Raiden", "Snake", "Otacon", "Meryl", "Liquid", "Big Boss", "Eva", "The Boss", // Metal Gear
            "Crono", "Marle", "Lucca", // Chrono Trigger
            "Chen", "Phenx", "Urak" // Long Dead Project Transcendence...
        };
    }
}