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
        public float Atk { get; set; }
        public float Def { get; set; }
        public float Agl { get; set; }
        public float Acc { get; set; }
        public float Luc { get; set; }
        public int AdditionalHP { get; set; }
        public string Description { get; set; }
        public bool IsPurchased { get; set; }   //속성 삭제고려중
        public bool IsNotForSale { get; set; }
        public int Count{ get; set; }


        public Item()
        {
            ItemNum = "";
            ItemTypeKorean = "";
            Name = "";
            Price = 0;
            Atk = 0;
            Def = 0;
            Agl = 0;
            Acc = 0;
            Luc = 0;
            AdditionalHP = 0;
            Description = "";
            Count = 0;
        }

        public Item(string itemNum, ITEMTYPE itemType, string name, int price, int atk, int def, int agl, int acc, int luc, int additionalHP, string description, bool isPurchased, bool isNotForSale, int count)
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
            Agl = agl;
            Acc = acc;
            Luc = luc;
            AdditionalHP = additionalHP;
            Description = description;
            IsPurchased = isPurchased;
            IsNotForSale = isNotForSale;
            Count = count;
        }
    }
}
