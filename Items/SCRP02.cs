using GadgetCore.API;
using TienContentMod.Gadgets;
using UnityEngine;

namespace TienContentMod.Items
{
    public class SCRP02 : Item
    {
        protected override string Gadget => Miscellaneous.GADGET_NAME;

        protected override string Name => "SCRP 02";

        protected override string Description => "An old second-hand basic droid for basic usage.";

        protected override Texture2D ObjectTexture => GadgetCoreAPI.LoadTexture2D("Droids/BasicDroidIcon");

        protected override Texture HeadTex => GadgetCoreAPI.LoadTexture2D("Droids/BasicDroidHead");

        protected override Texture BodyTex => GadgetCoreAPI.LoadTexture2D("Droids/BasicDroidBody");

        protected override ItemType Type => ItemType.DROID;
    }
}
