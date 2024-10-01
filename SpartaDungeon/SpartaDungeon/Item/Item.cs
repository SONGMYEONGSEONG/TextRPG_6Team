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
        Armor,
        HealingItem,
        ExtraItem

    }
    internal class Item
    {
        public string ItemNum { get; set; }
        public ITEMTYPE ItemType { get; set; }
        public string ItemTypeKorean { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int AdditionalHP { get; set; }
        public string Description { get; set; }
        public bool IsPurchased { get; set; }
        public bool IsNotForSale { get; set; }


        public Item()
        {
            ItemNum = "";
            ItemTypeKorean = "";
            Name = "";
            Price = 0;
            Atk = 0;
            Def = 0;
            AdditionalHP = 0;
            Description = "";
        }

        public Item(string itemNum, ITEMTYPE itemType, string name, int price, int atk, int def, int additionalHP, string description, bool isPurchased, bool isNotForSale)
        {
            ItemNum = itemNum;
            ItemType = itemType;
            if (itemType == ITEMTYPE.MainWeapon) ItemTypeKorean = "주무기";
            else if (itemType == ITEMTYPE.SubWeapon) ItemTypeKorean = "보조무기";
            else if (itemType == ITEMTYPE.Armor) ItemTypeKorean = "갑옷";
            else if (itemType == ITEMTYPE.HealingItem) ItemTypeKorean = "회복아이템";
            else if (itemType == ITEMTYPE.HealingItem) ItemTypeKorean = "기타";
            Name = name;
            Price = price;
            Atk = atk;
            Def = def;
            AdditionalHP = additionalHP;
            Description = description;
            IsPurchased = isPurchased;
            IsNotForSale = isNotForSale;
        }
    }
}
