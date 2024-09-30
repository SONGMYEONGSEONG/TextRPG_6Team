using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  코드 컨벤션
camelCase 소문자로 시작, 띄어쓰기 생략, 대문자로 구분
PascalCase 대문자로 시작, 띄어쓰기 생략, 대문자로 구분
snake_Case 소문자만 사용 ,띄어쓰기 대신 _ 사용
kebab-case 소문자만 사용 ,띄어쓰기 대신 - 사용

5-1. [namespace], [class] , [struct] 는 파스칼을 사용
5-2. [함수]는 파스칼을 사용 , 함수 내부는 카멜을 사용
5-3. [Enum]는 파스칼을 사용
5-4. public 변수는 파스칼을 사용 ex) public int Num;
5-5. 나머지 변수는 _ + 카멜을 사용 ex) private int _num;
 */

namespace SpartaDungeon
{
    //누구의 턴인지 표시하는 열거형 변수
    enum Turn { Player = 1, Enemy = 2 }
    
    /*Debug*/
    //Debug용 EnemyTest
    //public class EnemyTest /*Enemy Class 만들때 iStatus 인터페이스 가져오게 선언해야됨*/
    //{
    //    public string? Name { get; set; }
    //    public int Level { get; set; }
    //    public int CharacterAttack { get; set; }
    //    public int CharacterDefense { get; set; }
    //    public int CharacterMaxHP { get; set; }
    //    public int CurrentHP { get; set; }

    //}
    /*!Debug*/

    //전투 할때 사용되는 전투 Scene ( 턴제 전투)
    /*
     static 으로 할지 말지 고민 해봐야될듯 ,
     static으로 안하면 GameManager 쪽에 만들어야됨
     */
    internal class BattleScene
    {
        StringBuilder _strbuilder = new StringBuilder(); //문자열 최적화를 위한 스트링빌더 선언
        Turn _curTurn; //현재 진행중인 유저의 턴
        Character _curPlayer; //현재 전투에 참여한 플레이어 오브젝트
        //Enemy[] _enemys;  //현재 전투에 참여한 적 오브젝트들 

        bool isAttack;//[1.공격]을 선택 체크 변수
        bool isSkill;//[2.스킬]을 선택 체크 변수

        bool isPlayerWin; //Player가 승리했는지 패배했는지 체크하는 변수
        public bool IsPlayerWin { get { return isPlayerWin; } private set { } } //전투 결과 Scene에서 사용하시면 됩니다. -이지혜님
        
        List<Enemy> _enemyList; //이번 전투에 참여하는 적
        int _allEnemySumHP; //이번 전투에 참여한 모든 적의 HP 총합

        /*Debug*/
        //List<EnemyTest> _enemyTestList;
        /*!Debug*/

        public void StateInit() //State를 세팅하는 함수
        {
            //스테이지 세팅 코드 작성
        }

        public void Initialize(Character _player, List<Enemy> enemies)//나중에는 GameManager나 EnemyManager에서 배열로 적 받아와야됨
        {
            _curPlayer = _player;

            _curTurn = Turn.Player; //선턴은 바로 플레이어로 지정
            isAttack = false;
            isSkill = false;
            isPlayerWin = false;
            _allEnemySumHP = 0;

            /*Debug - 테스트용 Enemy */
            //for (int i = 0; i < 4; i++)
            //{
            //    EnemyTest _enemyTest = new EnemyTest();
            //    _enemyTest.Name = $"Test{i + 1}";
            //    _enemyTest.Level = 10;
            //    _enemyTest.CharacterAttack = 10;
            //    _enemyTest.CharacterDefense = 10;
            //    _enemyTest.CharacterMaxHP = 100;
            //    _enemyTest.CurrentHP = 100;

            //    _allEnemySumHP += _enemyTest.CurrentHP;
            //    _enemyTestList.Add(_enemyTest);
            //}
            //

            _enemyList = new List<Enemy>();
            Random random = new Random();
            int _battleEnemyCount = random.Next(1, 5); //전투에 참여되는 적 숫자
            for (int i = 0; i < _battleEnemyCount; i++)
            {
                int enemyIndex = random.Next(0, enemies.Count()); //해당 지역(SummonArea)에서 랜덤으로 적을 선택
                //int enemyIndex = 1;

                Enemy _battleEnemy = new Enemy(enemies[enemyIndex]);//리스트에서 랜덤으로 적 객체를 생성

                _battleEnemy.SetStatRandom(); //리스트에서 불러온 적 객체를 랜덤 스텟 조정
                _allEnemySumHP += (int)_battleEnemy.MaxHp; //이번 전투에 참여한 모든 적의 HP 총합

                _enemyList.Add(_battleEnemy);
            }
        }

        public void SceneStart()
        {
            Update();
        }

        private void EnemysSelectPrint(int enemyIndex)
        {
            _strbuilder.Append($"{enemyIndex}. ");
        }

        //Enemy List를 불러와서 출력함 
        private void EnemysPrint() //함수 안에 내용 변수들 테스트 목적이기에 병합할때 수정해야됨
        {
            string _enemyStatusStr = "";
            for (int i = 0; i < _enemyList.Count(); i++) // Enemy 몹 소환, 나중에 List를 받아와서 형식화 해야됨
            {
                _strbuilder.Clear();

                if (isAttack || isSkill)
                {
                    EnemysSelectPrint(i + 1); //나중에 공격 상태일떄 true변경되어 사용하게해야됨
                }
                //_enemyStatusStr = $"Lv.{_enemyTestList[i].Level} {_enemyTestList[i].Name}";
                _enemyStatusStr = $"Lv.{_enemyList[i].Level} {_enemyList[i].Name}";
                _strbuilder.Append(_enemyStatusStr);

                //switch (_enemyTestList[i].CurrentHP <= 0)
                switch (_enemyList[i].CurrentHp <= 0)
                {
                    case true: //적의 HP가 0이하 -> 죽음상태를 표시
                        _strbuilder.Append(" Dead");
                        break;

                    case false:
                        //_strbuilder.Append($" HP {_enemyTestList[i].CurrentHP}");
                        _strbuilder.Append($" HP {_enemyList[i].CurrentHp}");
                        break;
                }

                _strbuilder.Append("\n");
                Console.Write(_strbuilder.ToString());
            }
        }

        //전투 상황 프린트
        private void BattleStatusPrint()
        {
            //전투에 들어왔다는 출력 문구
            _strbuilder.Clear();
            _strbuilder.Append("Battle!!\n\n");
            Console.Write(_strbuilder.ToString());

            EnemysPrint();

            //Player 정보 출력
            _strbuilder.Clear();
            _strbuilder.Append("\n\n");
            _strbuilder.AppendLine("[내 정보]");
            _strbuilder.AppendLine($"Lv.{_curPlayer.Level}  {_curPlayer.Name}  ({_curPlayer.CharacterJobType})");
            _strbuilder.AppendLine($"HP {_curPlayer.CurrentHp} / {_curPlayer.TotalMaxHp}");
            Console.Write(_strbuilder.ToString());
        }

        private void PlayerTurnStatusPrint()
        {
            //Player 선택지 출력
            _strbuilder.Clear();
            _strbuilder.AppendLine();
            if (!(isAttack || isSkill))
            {
                _strbuilder.AppendLine($"1. 공격");
                _strbuilder.AppendLine($"2. 스킬");
            }

            _strbuilder.AppendLine($"0. 취소");

            if (!(isAttack || isSkill))
            {
                _strbuilder.AppendLine("원하시는 행동을 입력해주세요.");
            }
            else
            {
                _strbuilder.AppendLine("원하시는 대상을 입력해주세요.");
            }

            _strbuilder.Append(">>");
            Console.Write(_strbuilder.ToString());
        }

        private void Update()
        {
            while (_curPlayer.CurrentHp > 0 && _allEnemySumHP > 0)
            {
                if (_allEnemySumHP <= 0)
                {
                    //플레이어 승리
                    isPlayerWin = true;
                    break;
                }
                else if(_curPlayer.CurrentHp <= 0)
                {
                    //플레이어 패배
                    isPlayerWin = false;
                    break;
                }

                switch (_curTurn)
                {
                    case Turn.Player:

                        while (_curTurn == Turn.Player)
                        {
                            Console.Clear();
                            BattleStatusPrint();
                            PlayerTurnStatusPrint();

                            if (isAttack)
                            {
                                if(PlayerAttackSelect())
                                {
                                    isAttack = false;
                                    _curTurn = Turn.Enemy; //턴 교체 (Player -> Enemy)
                                }
                            }
                            else if (isSkill)
                            {
                                //PlayerSkillSelect();
                                _curTurn = Turn.Enemy; //턴 교체 (Player -> Enemy)
                            }
                            else
                            {
                                PlayerSelect(); 
                            }
                        }
                        break;

                    case Turn.Enemy:
                        int _damage = 0;
         
                        //for (int i = 0; i < _enemyTestList.Count; i++)
                        for (int i = 0; i < _enemyList.Count; i++)
                        {
                            Console.Clear();

                            //_damage = DamageCaculate(_curPlayer, _enemyTestList[i]);
                            _damage = DamageCaculate(_curPlayer, _enemyList[i]);

                            //Enemy Attack Print
                            _strbuilder.Clear();
                            //_strbuilder.AppendLine($"Lv.{_enemyTestList[i].Level} {_enemyTestList[i].Name} 의 공격!");
                            _strbuilder.AppendLine($"Lv.{_enemyList[i].Level} {_enemyList[i].Name} 의 공격!");
                            _strbuilder.AppendLine($"{_curPlayer.Name} 을(를) 맞췄습니다. [데미지 : {_damage}]");
                            _strbuilder.AppendLine();

                            _strbuilder.AppendLine($"LV.{_curPlayer.Level} {_curPlayer.Name}");
                            _strbuilder.AppendLine($"HP {_curPlayer.CurrentHp} -> {_curPlayer.CurrentHp - _damage}");
                            Console.Write(_strbuilder.ToString());

                            _curPlayer.CurrentHp -= _damage;

                            Console.ReadLine();
                        }
                        
                        _curTurn = Turn.Player; //턴 교체 (Player -> Enemy)
                        break;
                }

            }
        }

        public void SceneExit(ref Character player)
        {
            player = _curPlayer;
        }

        private void ErrorInput()
        {
            Console.WriteLine("잘못된 입력입니다.");
            Console.WriteLine();
            Console.ReadLine();
        }

        // 회피 확인
        //private bool CheckDodge(Player _curPlayer, EnemyTest _enemy)
        private bool CheckDodge(Character _curPlayer, Enemy _enemy)
        {
            Random rand = new Random();
            float numPossibility = rand.Next(1, 101);

            // 회피확률 어떻게 계산할지 필요함
            // bool isDodge;
            //switch (_curTurn)
            //{
            //    case Turn.Player:
            //        return isDodge = dodgeJudgement <= 기본확률 + player.명중 - enemy.민첩 ? true : false;
            //
            //    case Turn.Enemy:
            //        return isDodge = dodgeJudgement <= 기본확률 + enemy.명중 - player.민첩 ? true : false;
            //}

            return numPossibility <= 10;
        }

        //private bool CheakCritical(Player _curPlayer, EnemyTest _enemy)
        private bool CheakCritical(Character _curPlayer, Enemy _enemy)
        {
            Random rand = new Random();
            float numPossibility = rand.Next(1, 101);

            // 치명타 확률 어떻게 계산할지 필요함
            // bool isCritical;
            //switch (_curTurn)
            //{
            //    case Turn.Player:
            //        return iscritical = iscriticalJudgement <= 기본확률 + player.명중 + 행운 - enemy.민첩 ? true : false;
            //
            //    case Turn.Enemy:
            //        return iscritical = iscriticalJudgement <= 기본확률 + enemy.명중 + 행운 - player.민첩 ? true : false;
            //}

            return numPossibility <= 15;
        }

        //private int DamageCaculate(Player _curPlayer , EnemyTest _enemy)
        private int DamageCaculate(Character _curPlayer , Enemy _enemy)
        {
            Random rand = new Random();
            int _damage = 0;

            //회피 확률 계산 Class 합병해야됨 - 안성찬님 

            //크리티컬 확률 계산 Class 합병해야됨 - 안성찬님

            float marginRange; //오차 범위
            //Player - > Enemy 공격
            switch (_curTurn)
            {
                case Turn.Player:
                    marginRange = MathF.Ceiling(_curPlayer.TotalAttack * 0.1f);
                    _damage = rand.Next((int)(_curPlayer.TotalAttack - marginRange), (int)(_curPlayer.TotalAttack + marginRange + 1));
                    _allEnemySumHP -= _damage;
                    break;
                case Turn.Enemy:
                    //marginRange = MathF.Ceiling(_enemy.CharacterAttack * 0.1f);
                    marginRange = MathF.Ceiling(_enemy.Attack * 0.1f);
                    //_damage = rand.Next(_enemy.CharacterAttack - (int)marginRange, _enemy.CharacterAttack + (int)marginRange + 1);
                    _damage = rand.Next((int)(_enemy.Attack - marginRange), (int)(_enemy.Attack + marginRange + 1.0f));
                    break;
            }

            return _damage;
        }

        private bool PlayerAttackSelect()
        {
            string _input = Console.ReadLine(); //Player 선택지 입력 대기
            if (!int.TryParse(_input, out int _select)) //숫자가 아닌 문자,문자열 입력시 예외처리
            {
                ErrorInput();
                return false;
            }
            //else if (_select < 0 || _select > _enemyTestList.Count()) //적 번호를 초과하거나 미만으로 입력시 예외처리
            else if (_select < 0 || _select > _enemyList.Count()) //적 번호를 초과하거나 미만으로 입력시 예외처리
            {
                ErrorInput();
                return false;
            }
            else if (_select == 0) //공격 선택지에서 취소 선택
            {
                isAttack = false;
                return false;
            }

            //피격 몬스터 초기화 , 배열/리스트는 참조형식이기에 자동으로 얕은복사 된 상태
            //EnemyTest _hitEnemy = _enemyTestList[_select - 1];
            Enemy _hitEnemy = _enemyList[_select - 1];

            //데미지 적용
            //int _damage = DamageCaculate(_curPlayer, _hitEnemy);
            int _damage = DamageCaculate(_curPlayer, _hitEnemy);
            //_hitEnemy.CurrentHP -= _damage;
            _hitEnemy.CurrentHp -= _damage;

            //Player Attack Print
            //전투에 들어왔다는 출력 문구
            _strbuilder.Clear();
            _strbuilder.Append("Battle!!\n\n");
            Console.Write(_strbuilder.ToString());

            _strbuilder.Clear();
            _strbuilder.AppendLine($"{_curPlayer.Name}의 공격!");
            //방어력 적용해서 데미지 경감 시킬건지 결정해야됨 ( 회의 필요)
            _strbuilder.AppendLine($"Lv.{_hitEnemy.Level}{_hitEnemy.Name} 을(를) 맞췄습니다. [데미지 : {_damage}]");
            
            Console.WriteLine(_strbuilder.ToString());

            Console.ReadLine();
            return true;
        }

        private void PlayerSelect()
        {
            string _input = Console.ReadLine(); //Player 선택지 입력 대기

            switch (_input)
            {
                case "1":
                    if (!isAttack) //공격을 선택하면 공격 화면으로 변경
                    {
                        isAttack = true;
                    }
                    break;

                case "2":

                    break;

                case "0":
                    if (isAttack)
                    {
                        isAttack = false;
                    }
                    else if (isSkill)
                    {
                        isSkill = false;
                    }
                    break;

                default:
                    ErrorInput();
                    break;

            }
        }
    }

}
