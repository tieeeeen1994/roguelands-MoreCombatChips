using System;
using GadgetCore.API;
using II = GadgetCore.API.ItemInfo;

namespace TienContentMod.Items
{
    public class Item
    {
        public II ItemInfo => _itemInfo;

        private readonly II _itemInfo;

        protected Item()
        {
            _itemInfo = new II(Type, Name, Description, );
        }

        protected virtual ItemType Type => ItemType.GENERIC;

        protected virtual string Name => "Tien's Item";

        protected virtual string Description => "Tien's Modded Item.";

        public void Register()
        {
            GadgetCoreAPI.AddCustomResource
        }
    }
}
