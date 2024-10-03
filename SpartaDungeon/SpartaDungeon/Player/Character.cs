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

        //public float? Intelligence { get; set; }
        //public MyInventory myInventory = new MyInventory();
        public MyInventory myInventory;
        public Item EquipWeapon = new Item();
        public Item EquipArmor = new Item();

        /*Test - 20240930.송명성 추가*/
        public Dictionary<int, Quest> PlayerQuest = new Dictionary<int, Quest>();
        public SkillDeck SkillDeck;

        //클리어한 퀘스트 ID 목록
        public HashSet<int> ClearQuestsID { get; set; }
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
                SkillDeck.SkillSet();
            }
            else if (_jobTypeNum == (int)JobType.Rogue)
            {
                CharacterJobType = JobType.Rogue;
                Job = "도적";
                Attack = 13f;
                Agility = 15f;
                Luck = 20f;
                SkillDeck = new RogueSkill();
                SkillDeck.SkillSet();
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
                SkillDeck.SkillSet();
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
                SkillDeck.SkillSet();
            }

            //-----플레이어 생성 시 인벤토리 초기화------
            myInventory = new MyInventory(Job);
            //-----플레이어 생성시 플레이어가 완료한 퀘스트 컨테이너 초기화------
            ClearQuestsID = new HashSet<int>();

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

        // 플레이어 상태 보기
        public void DisplayStatus()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n[플레이어의 현재 상태]\n");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"이름: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{Name}");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"레벨: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{Level}");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"경험치: "); // EXP(경험치) 작업하면서 변수 추가 - 20241001송명성
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{Exp} / {MaxExp}"); // EXP(경험치) 작업하면서 변수 추가 - 20241001송명성

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"직업: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{Job}");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"체력: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{CurrentHp} / {TotalMaxHp} ({MaxHp} +{ExtraHp})");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"마력: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{CurrentMp} / {TotalMaxMp} ({MaxMp} +{ExtraMp})");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"공격력: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{TotalAttack} ({Attack} +{ExtraAttack})");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"방어력: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{TotalDefense} ({Defense} +{ExtraDefense})");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"민첩: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{TotalAgility} ({Agility} +{ExtraAgility})");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"명중: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{TotalAccuracy} ({Accuracy} +{ExtraAccuracy})");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"행운: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{TotalLuck} ({Luck} +{ExtraLuck})");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"소지금: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{Gold}G");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n[0] 나가기\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.ResetColor();

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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                }
            }
        }
        public void DisplayInventory()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"[{Name}]");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"의 인벤토리");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < myInventory.Inventory.Count; i++)
                {
                    string itemEquipState = (myInventory.Inventory[i].ItemNum == EquipWeapon.ItemNum || myInventory.Inventory[i].ItemNum == EquipArmor.ItemNum) ? "[E]" : "";
                    
                    Console.ResetColor();
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                   
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" [{i + 1}]");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($" { itemEquipState }");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" {myInventory.Inventory[i].Name}({myInventory.Inventory[i].ItemTypeKorean}");
                    Console.WriteLine(
                                      $" | {myInventory.Inventory[i].Description}" +
                                      $" | 공격력 +{myInventory.Inventory[i].Atk}" +
                                      $" 방어력 +{myInventory.Inventory[i].Def}" +
                                      $" 추가체력 +{myInventory.Inventory[i].AdditionalHP}" +
                                      $" | {myInventory.Inventory[i].Price}G |" +
                                      $" | {myInventory.Inventory[i].Count}개 |");
                }
                Console.ResetColor();
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("아이템 번호를 입력하면 해당 아이템을 장착하거나 해제할 수 있습니다.");
                Console.WriteLine("주무기, 갑옷을 한 개씩 장착할 수 있습니다.");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n[0] 나가기\n");
                Console.ResetColor();

                Console.Write(">> ");

                string input = Console.ReadLine();

                int select;
                bool isNum = int.TryParse(input, out select);

                if (isNum)
                {
                    if (select == 0) break;
                    else if (select > 0 && select <= myInventory.Inventory.Count)
                    {
                        Item selectItem = myInventory.Inventory[select - 1];
                        if (selectItem.ItemType == ITEMTYPE.MainWeapon || selectItem.ItemType == ITEMTYPE.Armor)
                        {
                            Console.Clear();
                            ManageItemEquip(selectItem);
                        } 
                        else if(selectItem.ItemType == ITEMTYPE.HealingItem)
                        {
                            Console.Clear();
                            ManageRecovery(selectItem);
                        }
                        else
                        {
                            Console.Clear();

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("해당 아이템 기타 아이템이므로 사용 하실 수 없습니다.");
                            Console.WriteLine("다른 아이템을 선택해 주세요.");
                            Console.ResetColor();
                            Console.WriteLine();
                          
                        }

                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("목록에 없는 숫자를 입력했습니다.");
                        Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
                    Console.ResetColor();
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
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"포션을 사용하면 회복 할 수 있습니다.({selectItem.Name}:{selectItem.Count}개)");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n[1] 사용하기\n");
                Console.WriteLine("\n[0] 나가기\n");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.ResetColor();

                Console.Write(">>");
                string input = Console.ReadLine();

                int select;
                bool isNum = int.TryParse(input, out select);

                if (isNum)
                {
                    if (select == 0) { Console.Clear(); break; } 
                    else if (select == 1)
                    {
                        if (selectItem.Count > 0)
                        {
                            RecoverySystem recoverySystem = new RecoverySystem();
                            if (selectItem.Name.Contains("HP"))
                            {
                                float? _oldHP = CurrentHp;

                                recoverySystem.InitializeHp(MaxHp, CurrentHp);
                                CurrentHp = recoverySystem.HpRecover(selectItem.ItemNum);

                                myInventory.DelInventory(selectItem);
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("HP가 회복되었습니다.");
                                Console.WriteLine($"HP : {_oldHP} -> {CurrentHp}");
                                Console.ResetColor();

                            }

                            else if (selectItem.Name.Contains("MP"))
                            {
                                float? _oldMP = CurrentMp;

                                recoverySystem.InitializeMp(MaxMp, CurrentMp);
                                CurrentMp = recoverySystem.MpRecover(selectItem.ItemNum);

                                myInventory.DelInventory(selectItem);
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("MP가 회복되었습니다.");
                                Console.WriteLine($"MP : {_oldMP} -> {CurrentMp}");
                                Console.ResetColor();
                            }
                        }
                        else 
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("해당 포션이 없습니다");
                            Console.ResetColor();
                            break;
                        }
                        
                    }
                    else
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("목록에 없는 숫자를 입력했습니다.");
                        Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
                        Console.WriteLine();
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("목록에 없는 숫자를 입력했습니다.");
                    Console.WriteLine("아이템 목록에 해당하는 번호나 0을 입력하세요.");
                    Console.WriteLine();
                    Console.ResetColor();
                }
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
