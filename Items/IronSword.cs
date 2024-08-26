using GadgetCore.API;
using TienContentMod.Gadgets;
using TienContentMod.Services;
using UnityEngine;
using II = GadgetCore.API.ItemInfo;

namespace TienContentMod.Items
{
    public class IronSword : Item
    {
        protected override string Gadget => Miscellaneous.GADGET_NAME;

        protected override string Name => "Iron Sword";

        protected override string Description => "Primitive weapon made of iron.";

        protected override Texture2D ObjectTexture => GadgetCoreAPI.LoadTexture2D("Weapons/BasicIcon");

        protected override Texture HeldTex => GadgetCoreAPI.LoadTexture2D("Weapons/BasicSword");

        protected override ItemType Type => ItemType.WEAPON;

        protected override EquipStats Stats => StatService.EquipStats(STR: 1);

        protected override void CustomActions(II itemInfo)
        {
            itemInfo.OnAttack += itemInfo.SwingSword;
            itemInfo.SetWeaponInfo(
                StatService.Multipliers(STR: 1f),
                //GadgetCoreAPI.LoadAudioClip("Weapons/Swing")
                GadgetCoreAPI.GetAttackSound(300)
            );
        }
    }
}