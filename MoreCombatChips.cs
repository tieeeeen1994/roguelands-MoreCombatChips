using GadgetCore.API;
using TienContentMod.CombatChips;
using static TienContentMod.TienContentMod;

namespace TienContentMod
{
    [Gadget(GADGET_NAME, true)]
    public class MoreCombatChips : Gadget<MoreCombatChips>
    {
        public const string GADGET_NAME = "More Combat Chips";
        public const string CONFIG_VERSION = "2.0.0"; // Increment this whenever you change your mod's config file.

        internal static bool QuadracopterCost = true;

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

            Config.Save();
        }

        public override string GetModDescription()
        {
            return "WIP";
        }

        protected override void Initialize()
        {
            Logger.Log($"{GADGET_NAME} v{Info.Mod.Version}");
            CombatChip<AttackerDroneChip>.I.Register();
            CombatChip<MessyMkIChip>.I.Register();
            CombatChip<VitalityXXChip>.I.Register();
            CombatChip<DexterityXXChip>.I.Register();
            CombatChip<StrengthXXChip>.I.Register();
            CombatChip<IntelligenceXXChip>.I.Register();
            CombatChip<TechXXChip>.I.Register();
            CombatChip<FaithXXChip>.I.Register();
            CombatChip<RejuvenationWaveChip>.I.Register();
        }
    }
}
