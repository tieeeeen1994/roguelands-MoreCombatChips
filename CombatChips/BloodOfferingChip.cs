using GadgetCore.API;
using UnityEngine;

namespace TienContentMod.CombatChips
{
    public class BloodOfferingChip : CombatChip
    {
        public override ChipType Type => ChipType.ACTIVE;

        public override string Name => "Blood Offering";

        public override string Description => "Exchanges 10 health for 50 mana.";

        public override int Cost => 10;

        public override ChipInfo.ChipCostType CostType => ChipInfo.ChipCostType.HEALTH_SAFE;

        public override bool Advanced => true;

        protected override void Action(int slot)
        {
            GameScript gameScript = InstanceTracker.GameScript;
            GameScript.mana = Mathf.Min(GameScript.mana + 50, GameScript.maxmana);
            gameScript.BARMANA.GetComponent<Animation>().Play();
            gameScript.UpdateMana();
            gameScript.GetComponent<AudioSource>()
                      .PlayOneShot((AudioClip)Resources.Load("Au/ow"), Menuu.soundLevel / 10f);
        }
    }
}
