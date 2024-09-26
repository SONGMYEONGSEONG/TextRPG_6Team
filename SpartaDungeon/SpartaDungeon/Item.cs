using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public enum ITEMTYPE
    {
        MainWeapon,
        SubWeapon,
        Armor
    }
    internal class Item
    {
        public ITEMTYPE ItemType { get; set; }
        public string ItemTypeKorean { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int AdditionalHP { get; set; }
        public string Description { get; set; }
        public bool isPurchased { get; set; }

        public Item()
        {
            ItemTypeKorean = "";
            Name = "";
            Price = 0;
            Atk = 0;
            Def = 0;
            AdditionalHP = 0;
            Description = "";
        }

        public Item(ITEMTYPE itemType, string name, int price, int atk, int def, int additionalHP, string description, bool isPurchased)
        {
            ItemType = itemType;
            if (itemType == ITEMTYPE.MainWeapon) ItemTypeKorean = "주무기";
            else if (itemType == ITEMTYPE.SubWeapon) ItemTypeKorean = "보조무기";
            else if (itemType == ITEMTYPE.Armor) ItemTypeKorean = "갑옷";
            Name = name;
            Price = price;
            Atk = atk;
            Def = def;
            AdditionalHP = additionalHP;
            Description = description;
            this.isPurchased = isPurchased;
        }
    }
}
