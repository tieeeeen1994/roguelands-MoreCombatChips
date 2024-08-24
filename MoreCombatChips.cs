using GadgetCore.API;
using MoreCombatChips.CombatChips;
using MoreCombatChips.ID;

namespace MoreCombatChips
{
    [Gadget("More Combat Chips", true)]
    public class MoreCombatChips : Gadget<MoreCombatChips>
    {
        public const string MOD_VERSION = "1.10.1"; // Set this to the version of your mod.
        public const string CONFIG_VERSION = "1.10.0"; // Increment this whenever you change your mod's config file.

        internal static bool TEST = false;

        internal static bool QuadracopterCost = true;
        internal static bool EyepodHatChange = true;
        internal static bool GlibglobHatChange = true;
        internal static bool ChamchamHatChange = true;
        internal static bool ShmooHatChange = true;
        internal static bool DroidsRework = true;
        internal static bool DebugLog = true;

        public static void Log(string message)
        {
            if (TEST)
            {
                GetLogger().LogConsole(message);
            }
            else if (DebugLog)
            {
                GetLogger().Log(message);
            }
        }

        public static void Error(string message)
        {
            GetLogger().LogError(message);
        }

        protected override void LoadConfig()
        {
            Config.Load();

            string fileVersion = Config.ReadString(
                "ConfigVersion", CONFIG_VERSION,
                comments: "The Config Version (not to be confused with mod version)"
            );

            if (fileVersion != CONFIG_VERSION)
            {
                Config.Reset();
                Config.WriteString(
                    "ConfigVersion", CONFIG_VERSION,
                    comments: "The Config Version (not to be confused with mod version)"
                );
            }

            DebugLog = Config.ReadBool(
                "DebugLog", false,
                comments: "Enable debug logging."
            );

            QuadracopterCost = Config.ReadBool(
                "CopterCost", true,
                requiresRestart: true,
                comments: "Changes mana cost of Quadracopter to 30."
            );

            EyepodHatChange = Config.ReadBool(
                "EyepodHat", true,
                requiresRestart: true,
                comments: "Changes effect of Eyepod Hat."
            );

            GlibglobHatChange = Config.ReadBool(
                "GlibglobHat", true,
                requiresRestart: true,
                comments: "Changes effect of Glibglob Hat."
            );

            ChamchamHatChange = Config.ReadBool(
                "ChamchamHat", true,
                requiresRestart: true,
                comments: "Changes effect of Chamcham Hat."
            );

            ShmooHatChange = Config.ReadBool(
                "ShmooHat", true,
                requiresRestart: true,
                comments: "Changes effect of Shmoo Hat."
            );

            DroidsRework = Config.ReadBool(
                "DroidsRework", true,
                requiresRestart: true,
                comments: "Droids have adjusted stats and now scale."
            );

            Config.Save();
        }

        public override string GetModDescription()
        {
            return "This mod adds a wide array of custom Combat Chips, all available in Old Earth.\n" +
                   "It also applies a few changes and fixes.\n" +
                   "- Gadget RPG now increases TEC instead of DEX and FTH.\n" +
                   "- Useless augments now have a useful effect. (Configurable)\n" +
                   "- Quadracopter is functionally fixed for multiplayer.\n" +
                   "- Quadracopter now only costs 30 MP. (Configurable)\n" +
                   "- Add more random names from franchises for character creation.\n" +
                   "- Droids have adjusted stats and now scale. (Configurable)";
        }

        protected override void Initialize()
        {
            Logger.Log("MoreCombatChips v" + Info.Mod.Version);
            CombatChip<AttackerDroneChip>.I.Register();
            CombatChip<MessyMkIChip>.I.Register();
            CombatChip<VitalityXXChip>.I.Register();
            CombatChip<DexterityXXChip>.I.Register();
            CombatChip<StrengthXXChip>.I.Register();
            CombatChip<IntelligenceXXChip>.I.Register();
            CombatChip<TechXXChip>.I.Register();
            CombatChip<FaithXXChip>.I.Register();
            CombatChip<RejuvenationWaveChip>.I.Register();
            GadgetCoreAPI.RegisterStatModifier(AugmentModifiers, StatModifierType.FinalExpMult);
        }

        private EquipStatsDouble AugmentModifiers(Item _)
        {
            var equipStats = EquipStatsDouble.ONE;
            switch (Menuu.curAugment)
            {
                case AugmentID.EyepodHat:
                    if (EyepodHatChange)
                    {
                        equipStats = new EquipStatsDouble(0.5);
                    }
                    break;
            }
            return equipStats;
        }
    }
}
