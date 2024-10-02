using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaDungeon
{
    enum JobType
    {
        None,
        Warrior,
        Rogue,
        Archer,
        Mage
    }

    internal class Character : Creature
    {
        public JobType CharacterJobType { get; set; }
        public string Job { get; set; }
        public int MaxExp { get; set; } // EXP(경험치) 작업하면서 변수 추가 - 20241001송명성

        public float ExtraHp { get; set; }
        public float TotalMaxHp { get; set; }

        public float ExtraMp { get; set; }
        public float TotalMaxMp { get; set; }

        public float ExtraAttack { get; set; }
        public float TotalAttack { get; set; }

        public float ExtraDefense { get; set; }
        public float TotalDefense { get; set; }

        public float ExtraAgility { get; set; }
        public float TotalAgility { get; set; }

        public float ExtraAccuracy { get; set; }
        public float TotalAccuracy { get; set; }

        public float ExtraLuck { get; set; }
        public float TotalLuck { get; set; }

        public List<Item> Inventory = new List<Item>();
        public Item EquipWeapon = new Item();
        public Item EquipArmor = new Item();

        /*Test - 20240930.송명성 추가*/
        public Dictionary<int, Quest> PlayerQuest = new Dictionary<int, Quest>();
        public SkillDeck SkillDeck;
        /**/

        public Character(string _name, int _jobTypeNum)
        {
            // 디폴트 값
            Name = _name;
            Level = 1;
            Exp = 0;// EXP(경험치) 작업하면서 변수 추가 - 20241001송명성
            MaxExp = 100;// EXP(경험치) 작업하면서 변수 추가 - 20241001송명성
            MaxHp = 100f;
            MaxMp = 100f;
            Attack = 10f;
            Defense = 10f;
            Agility = 10f;
            Accuracy = 10f;
            Luck = 10f;
            Gold = 500;

            if (_jobTypeNum == (int)JobType.Warrior)
            {
                CharacterJobType = JobType.Warrior;
                Job = "전사";
                MaxHp = 150f;
                MaxMp = 50f;
                Attack = 15f;
                Defense = 15f;
                SkillDeck = new WarriorSkill();
            }
            else if (_jobTypeNum == (int)JobType.Rogue)
            {
                CharacterJobType = JobType.Rogue;
                Job = "도적";
                Attack = 13f;
                Agility = 15f;
                Luck = 20f;
                SkillDeck = new RogueSkill();
            }
            else if (_jobTypeNum == (int)JobType.Archer)
            {
                CharacterJobType = JobType.Archer;
                Job = "궁수";
                MaxHp = 80f;
                MaxMp = 120f;
                Attack = 15f;
                Defense = 8f;
                Agility = 20f;
                Accuracy = 15f;
                SkillDeck = new ArcherSkill();
            }
            else if (_jobTypeNum == (int)JobType.Mage)
            {
                CharacterJobType = JobType.Mage;
                Job = "마법사";
                MaxHp = 70f;
                MaxMp = 200f;
                Attack = 20f;
                Defense = 5f;
                Agility = 5f;
                Accuracy = 5f;
                Luck = 15f;
                SkillDeck = new MageSkill();
            }

            //-----플레이어 생성 시 인벤토리 초기화------
            MyInventory myInventory = new MyInventory(Job);
            Inventory = myInventory.Inventory;
        }

        // 플레이어 스탯 갱신
        public void UpdateStat()
        {
            // 레벨업 능력치 반영
            ExtraHp = EquipWeapon.AdditionalHP + EquipArmor.AdditionalHP;
            TotalMaxHp = MaxHp + ExtraHp;

            //ExtraMp = EquipWeapon.AdditionalMP + EquipArmor.AdditionalMP;
            TotalMaxMp = MaxMp + ExtraMp;

            ExtraAttack = EquipWeapon.Atk + EquipArmor.Atk;
            TotalAttack = Attack + ExtraAttack;

            ExtraDefense = EquipWeapon.Def + EquipArmor.Def;
            TotalDefense = Defense + ExtraDefense;

            ExtraAgility = EquipWeapon.Agl + EquipArmor.Agl;
            TotalAgility = Agility + ExtraAgility;

            ExtraAccuracy = EquipWeapon.Acc + EquipArmor.Acc;
            TotalAccuracy = Accuracy + ExtraAccuracy;

            ExtraLuck = EquipWeapon.Luc + EquipArmor.Luc;
            TotalLuck = Luck + ExtraLuck;

            if (CurrentHp == null)
            {
                CurrentHp = TotalMaxHp;
            }
            if (CurrentMp == null)
            {
                CurrentMp = TotalMaxMp;
            }

            //HP ,MP 음수로 내려가는거 방지 코드
            if (CurrentHp <= 0)
            {
                CurrentHp = 1;
            }
            if (CurrentMp < 0)
            {
                CurrentMp = 0;
            }

        }

        // 플레이어 스탯 Load (저장/불러오기 기능)
        public void LoadData(/*Character _charcter*/)
        {
            //.json으로 역직렬화 통해서 Data 전부 초기화함

            //Name = _charcter.Name;
            //Level = _charcter.Level;
            //Exp = _charcter.Exp;
            //MaxExp = _charcter.MaxExp;
            //CurrentHp = _charcter.CurrentHp;
            //MaxHp = _charcter.MaxHp;
            //CurrentMp = _charcter.CurrentMp;
            //MaxMp = _charcter.MaxMp;
            //Attack = _charcter.Attack;
            //Defense = _charcter.Defense;
            //Agility = _charcter.Agility;
            //Accuracy = _charcter.Accuracy;
            //Luck = _charcter.Luck;
            //Gold = _charcter.Gold;
            //CharacterJobType = _charcter.CharacterJobType;
            //Job = _charcter.Job;

            //스킬 로드
            switch (CharacterJobType)
            {
                case JobType.Warrior:
                    //SkillDeck = new Warriorkill();
                    break;

                case JobType.Rogue:
                    SkillDeck = new RogueSkill();
                    break;

                case JobType.Archer:
                    SkillDeck = new ArcherSkill();
                    break;

                case JobType.Mage:
                    SkillDeck = new MageSkill();
                    break;
            }

            //퀘스트 로드


            //인벤토리 로드


            //장착템 로드


        }

        // 플레이어 상태 보기
        public void DisplayStatus()
        {
            while (true)
            {
                Console.WriteLine($"\n[플레이어의 현재 상태]\n");
                Console.WriteLine($"이름: {Name}");
                Console.WriteLine($"레벨: {Level}");
                Console.WriteLine($"경험치: {Exp} / {MaxExp}"); // EXP(경험치) 작업하면서 변수 추가 - 20241001송명성
                Console.WriteLine($"직업: {Job}");
                Console.WriteLine($"체력: {CurrentHp} / {TotalMaxHp} ({MaxHp} +{ExtraHp})");
                Console.WriteLine($"마력: {CurrentMp} / {TotalMaxMp} ({MaxMp} +{ExtraMp})");
                Console.WriteLine($"공격력: {TotalAttack} ({Attack} +{ExtraAttack})");
                Console.WriteLine($"방어력: {TotalDefense} ({Defense} +{ExtraDefense})");
                Console.WriteLine($"민첩: {TotalAgility} ({Agility} +{ExtraAgility})");
                Console.WriteLine($"명중: {TotalAccuracy} ({Accuracy} +{ExtraAccuracy})");
                Console.WriteLine($"행운: {TotalLuck} ({Luck} +{ExtraLuck})");
                Console.WriteLine($"소지금: {Gold}G");

                Console.WriteLine("\n[0] 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }
        public void DisplayInventory()
        {
            while (true)
            {
                Console.WriteLine($"[{Name}]의 인벤토리");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < Inventory.Count; i++)
                {
                    string itemEquipState = (Inventory[i] == EquipWeapon || Inventory[i] == EquipArmor) ? "[E]" : "";

                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine($" [{i + 1}] {itemEquipState}{Inventory[i].Name}({Inventory[i].ItemTypeKorean})" +
                                      $" | {Inventory[i].Description}" +
                                      $" | 공격력 +{Inventory[i].Atk}" +
                                      $" 방어력 +{Inventory[i].Def}" +
                                      $" 추가체력 +{Inventory[i].AdditionalHP}" +
                                      $" | {Inventory[i].Price}G |");
                }
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("아이템 번호를 입력하면 해당 아이템을 장착하거나 해제할 수 있습니다.");
                Console.WriteLine("주무기, 갑옷을 한 개씩 장착할 수 있습니다.");

                Console.WriteLine("\n[0] 나가기\n");
                Console.Write(">> ");

                string input = Console.ReadLine();

                int select;
                bool isNum = int.TryParse(input, out select);

                if (isNum)
                {
                    if (select == 0) break;
                    else if (select > 0 && select <= Inventory.Count)
                    {
                        Item selectItem = Inventory[select - 1];
                        if (selectItem.ItemType == ITEMTYPE.MainWeapon || selectItem.ItemType == ITEMTYPE.Armor)
                        {
                            ManageItemEquip(selectItem);
                            Console.Clear();
                        }
                        else if (selectItem.ItemType == ITEMTYPE.HealingItem)
                        {
                            ManageRecovery(selectItem);
                            Console.Clear();
                        }

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("목록에 없는 숫자를 입력했습니다.");
                        Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
                    Console.WriteLine();
                }
            }
            Console.Clear();
        }

        void ManageItemEquip(Item _selectItem)
        {
            if (_selectItem.ItemType == ITEMTYPE.MainWeapon && _selectItem != EquipWeapon)
            {
                CurrentHp -= EquipWeapon.AdditionalHP;
                EquipWeapon = _selectItem;
                CurrentHp += _selectItem.AdditionalHP;
            }
            else if (_selectItem.ItemType == ITEMTYPE.Armor && _selectItem != EquipArmor)
            {
                CurrentHp -= EquipArmor.AdditionalHP;
                EquipArmor = _selectItem;
                CurrentHp += _selectItem.AdditionalHP;
            }
            else if (_selectItem == EquipWeapon)
            {
                EquipWeapon = new Item();
                CurrentHp -= _selectItem.AdditionalHP;
            }
            else if (_selectItem == EquipArmor)
            {
                EquipArmor = new Item();
                CurrentHp -= _selectItem.AdditionalHP;
            }
        }

        void ManageRecovery(Item selectItem)
        {
            RecoverySystem recoverySystem = new RecoverySystem();
            if (selectItem.Name.Contains("HP"))
            {
                recoverySystem.InitializeHp(MaxHp, CurrentHp);
                CurrentHp = recoverySystem.HpRecover(selectItem.ItemNum);
            }
            else if (selectItem.Name.Contains("MP"))
            {
                recoverySystem.InitializeMp(MaxMp, CurrentMp);
                CurrentMp = recoverySystem.HpRecover(selectItem.ItemNum);
            }

        }

        /* EXP(경험치) 작업하면서 변수 추가 - 20241001송명성*/
        //외부에서 호출되는 레벨업 체크 함수
        public bool LevelUpCheck()
        {
            if (Exp >= MaxExp)
            {
                Exp -= MaxExp;
                Level++;
                LevelUpStat();
                return true;
            }

            return false;
        }

        //레벨업시 스텟 변화량 함수
        private void LevelUpStat()
        {
            //레벨업시 스텟 증가량 회의 필요
            MaxExp = (int)MathF.Floor(MaxExp * 1.5f); //현재 경험치의 1.5배,내림처리
            MaxHp += 10f;
            MaxMp += 5f;
            Attack += 10f;
            Defense += 10f;
            Agility += 3f;
            Accuracy += 3f;
            Luck += 3f;
        }


        /* !EXP(경험치) 작업하면서 변수 추가 - 20241001송명성*/
    }
}
