using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    //누구의 턴인지 표시하는 열거형 변수
    enum Turn { Player = 1, Enemy = 2 }


    //전투 할때 사용되는 전투 Scene ( 턴제 전투)
    /*
     static 으로 할지 말지 고민 해봐야될듯 ,
     static으로 안하면 GameManager 쪽에 만들어야됨
     */
    internal class BattleScene
    {
        /*Debug*/
        bool isRun; //도주하기 기능

        MageSkill _mageSkill; //스킬 테스트
        int _gainExp; //해당 전투에서 얻은 총 경험치
        /*Debug*/

        StringBuilder _strbuilder = new StringBuilder(); //문자열 최적화를 위한 스트링빌더 선언
        Turn _curTurn; //현재 진행중인 유저의 턴
        Character _curPlayer; //현재 전투에 참여한 플레이어 오브젝트
        float _curPlayerBattleHP; //전투에 참여했을떄의 플레이어 HP

        //획득 아이템 저장 컨테이너
        int _gainGold; //몬스터를 쓰러트릴때마다 골드를 저장
        List<string> _gainItem; //Item 완성되는데로 수정 필요함 - 이지혜 님

        bool isAttack;//[1.공격]을 선택 체크 변수
        bool isSkill;//[2.스킬]을 선택 체크 변수

        bool isPlayerWin; //Player가 승리했는지 패배했는지 체크하는 변수
        public bool IsPlayerWin { get { return isPlayerWin; } private set { } } //전투 결과 Scene에서 사용하시면 됩니다. -이지혜님

        List<Enemy> _enemyList; //이번 전투에 참여하는 적
        int _curBattleEnemyCount; //이번 전투에 참여한 적의 갯수
        //int _allEnemySumHP; //이번 전투에 참여한 모든 적의 HP 총합
        int _allDeadCount; //이번 전투에 참여한 적의 죽음 카운트 변수

        Dictionary<int, Quest> _playerQuest; //몬스터 처치 관련 퀘스트를 보관하는 컨테이너

        public void Initialize(Character _player, List<Enemy> enemies)//나중에는 GameManager나 EnemyManager에서 배열로 적 받아와야됨
        {
            /*Debug*/
            isRun = false;

            _mageSkill = new MageSkill(); //스킬 테스트
            _gainExp = 0;
            /*Debug*/

            _curPlayer = _player;
            _curPlayerBattleHP = (float)_curPlayer.CurrentHp;
            _gainGold = 0;
            _gainItem = new List<string>();

            _curTurn = Turn.Player; //선턴은 바로 플레이어로 지정
            isAttack = false;
            isSkill = false;
            isPlayerWin = false;
            //_allEnemySumHP = 0;
            _allDeadCount = 0;

            _enemyList = new List<Enemy>();
            Random random = new Random();
            int _battleEnemyCount = random.Next(1, 5); //전투에 참여되는 적 숫자
            _curBattleEnemyCount = _battleEnemyCount;
            for (int i = 0; i < _battleEnemyCount; i++)
            {
                int enemyIndex = random.Next(0, enemies.Count()); //해당 지역(SummonArea)에서 랜덤으로 적을 선택

                Enemy _battleEnemy = new Enemy(enemies[enemyIndex]);//리스트에서 랜덤으로 적 객체를 생성
                //Enemy _battleEnemy = new Enemy(enemies[1]);//리스트에서 랜덤으로 적 객체를 생성

                _battleEnemy.SetStatRandom(); //리스트에서 불러온 적 객체를 랜덤 스텟 조정
                //_allEnemySumHP += (int)_battleEnemy.MaxHp; //이번 전투에 참여한 모든 적의 HP 총합

                _enemyList.Add(_battleEnemy);
            }

            //Battle에서만 사용되는 퀘스트 목록 정리
            _playerQuest = new Dictionary<int, Quest>();
            foreach (KeyValuePair<int, Quest> questData in _curPlayer.PlayerQuest)
            {
                if (questData.Value.Type == QuestType.MonsterKillCount)
                {
                    _playerQuest.Add(questData.Key, questData.Value);
                }
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
                _enemyStatusStr = $"Lv.{_enemyList[i].Level} {_enemyList[i].Name}";
                _strbuilder.Append(_enemyStatusStr);

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
            _strbuilder.AppendLine($"Lv.{_curPlayer.Level}  {_curPlayer.Name}  ({_curPlayer.Job})");
            _strbuilder.AppendLine($"HP {_curPlayer.CurrentHp} / {_curPlayer.TotalMaxHp}");
            _strbuilder.AppendLine($"MP {_curPlayer.CurrentMp} / {_curPlayer.TotalMaxMp}");
            _strbuilder.AppendLine($"EXP {_curPlayer.Exp} / {_curPlayer.MaxExp}");
            Console.Write(_strbuilder.ToString());
        }

        private void PlayerTurnPrint()
        {
            //Player 선택지 출력
            _strbuilder.Clear();
            _strbuilder.AppendLine();
            if (!(isAttack || isSkill))
            {
                _strbuilder.AppendLine("1. 공격");
                _strbuilder.AppendLine("2. 스킬");
            }

            _strbuilder.AppendLine("\n0. 취소");

            if (isAttack && !isSkill)
            {
                _strbuilder.AppendLine("원하시는 대상을 입력해주세요.");
            }
            else if (!isAttack && isSkill)
            {
                //플레이어 스킬 리스트 출력
                _mageSkill.Print();
                _strbuilder.AppendLine("원하시는 스킬을 입력해주세요.");
            }
            else
            {
                _strbuilder.AppendLine("원하시는 행동을 입력해주세요.");
            }

            _strbuilder.Append(">>");
            Console.Write(_strbuilder.ToString());
        }

        private void Update()
        {
            while (_curPlayer.CurrentHp > 0 && /*_allEnemySumHP > 0*/ _allDeadCount < _enemyList.Count() && !isRun)
            {

                switch (_curTurn)
                {
                    case Turn.Player:

                        while ((_curTurn == Turn.Player) && !isRun)
                        {
                            Console.Clear();
                            BattleStatusPrint();
                            PlayerTurnPrint();

                            if (isAttack)
                            {
                                if (PlayerAttackSelect())
                                {
                                    isAttack = false;
                                    _curTurn = Turn.Enemy; //턴 교체 (Player -> Enemy)
                                    Console.ReadLine();
                                }

                            }
                            else if (isSkill)
                            {
                                //PlayerSkillSelect();
                                if (PlayerSkillSelect())
                                {
                                    isSkill = false;
                                    _curTurn = Turn.Enemy; //턴 교체 (Player -> Enemy)
                                    Console.ReadLine();
                                }
                            }
                            else
                            {
                                PlayerSelect();
                            }
                        }
                        break;

                    case Turn.Enemy:
                        float _damage = 0;

                        for (int i = 0; i < _enemyList.Count; i++)
                        {
                            //플레이어 체력 0 되면 반복문 탈출
                            if (_curPlayer.CurrentHp <= 0)
                            {
                                break;
                            }

                            Console.Clear();

                            if (!_enemyList[i].IsDead) //Enemy가 죽어있으면 동작하지 않게하기
                            {
                                _damage = DamageCaculate(_curPlayer, _enemyList[i], out bool _isCritical);

                                //Enemy Attack Print
                                _strbuilder.Clear();
                                _strbuilder.AppendLine("===================================================");
                                _strbuilder.AppendLine($"Battle!! - Enemy(Lv.{_enemyList[i].Level} {_enemyList[i].Name})의 턴 \n");

                                if (_damage <= 0) //공격을 회피한경우 
                                {
                                    _strbuilder.AppendLine($"Lv.{_enemyList[i].Level} {_enemyList[i].Name} 의 공격!");
                                    _strbuilder.AppendLine($"{_curPlayer.Name}는 회피 하였습니다. [데미지 : 0]");
                                    _strbuilder.AppendLine();
                                }
                                else
                                {
                                    if (_isCritical)
                                    {
                                        _strbuilder.AppendLine($"크리티컬!!! - 1.6배 데미지 증가\n");
                                    }
                                    _strbuilder.AppendLine($"Lv.{_enemyList[i].Level} {_enemyList[i].Name} 의 공격!");
                                    _strbuilder.AppendLine($"{_curPlayer.Name} 을(를) 맞췄습니다. [데미지 : {_damage}]");
                                    _strbuilder.AppendLine();

                                    _strbuilder.AppendLine($"LV.{_curPlayer.Level} {_curPlayer.Name}");

                                    //플레이어 체력을 초과한 데미지를 받을경우 0으로 표시
                                    if (_curPlayer.CurrentHp - _damage <= 0)
                                    {
                                        _strbuilder.AppendLine($"HP {_curPlayer.CurrentHp} -> {0}");
                                    }
                                    else
                                    {
                                        _strbuilder.AppendLine($"HP {_curPlayer.CurrentHp} -> {_curPlayer.CurrentHp - _damage}");
                                    }
                                    _strbuilder.AppendLine("===================================================");
                                    _strbuilder.AppendLine(">> 엔터를 누르면 다음 화면으로 넘어갑니다.");
                                    _curPlayer.CurrentHp -= _damage;
                                }
                                Console.Write(_strbuilder.ToString());
                                Console.ReadLine();
                            }
                        }

                        _curTurn = Turn.Player; //턴 교체 (Player -> Enemy)
                        break;
                }

            }

            if (/*_allEnemySumHP <= 0*/ _allDeadCount >= _enemyList.Count())
            {
                //플레이어 승리
                BattleResult(true);
            }
            else if (_curPlayer.CurrentHp <= 0 || isRun)
            {
                //플레이어 패배
                BattleResult(false);
            }
        }

        public void SceneExit(ref Character player)
        {
            foreach (KeyValuePair<int, Quest> questData in _playerQuest)
            {
                _curPlayer.PlayerQuest[questData.Key] = questData.Value;
            }


            player = _curPlayer;
        }

        private void ErrorInput()
        {
            Console.WriteLine("잘못된 입력입니다.");
            Console.WriteLine();
            Console.ReadLine();
        }

        // 회피 확인
        private bool CheckDodge(Character _curPlayer, Enemy _enemy)
        {
            Random rand = new Random();
            float numPossibility = rand.Next(1, 101);
            //float numPossibility = 0;
            float defaultDodgePercent = 5.0f;


            switch (_curTurn)
            {
                case Turn.Player:
                    return numPossibility <= defaultDodgePercent + _curPlayer.TotalAccuracy - _enemy.Agility;

                case Turn.Enemy:
                    return numPossibility <= defaultDodgePercent + _enemy.Accuracy - _curPlayer.TotalAgility;
            }

            return false;
        }

        //크리티컬 확인
        private bool CheakCritical(Character _curPlayer, Enemy _enemy)
        {
            Random rand = new Random();
            float numPossibility = rand.Next(1, 101);
            //float numPossibility = 0;
            float defaultDodgePercent = 3f;


            switch (_curTurn)
            {
                case Turn.Player:
                    return numPossibility <= defaultDodgePercent + _curPlayer.TotalAccuracy + _curPlayer.TotalLuck - _enemy.Agility;

                case Turn.Enemy:
                    return numPossibility <= defaultDodgePercent + _enemy.Accuracy + _enemy.Luck - _curPlayer.Agility;
            }

            return false;
        }

        private float DamageCaculate(Character _curPlayer, Enemy _enemy, out bool _isCritical)
        {
            Random rand = new Random();
            float _damage = 0;
            float _criticalValue = 1.6f; //크리티컬 계수
            _isCritical = false;

            //회피 확률 계산 Class 합병해야됨 - 안성찬님 
            if (CheckDodge(_curPlayer, _enemy))
            {
                return 0;
            }

            float marginRange; //오차 범위
            //Player - > Enemy 공격
            switch (_curTurn)
            {
                case Turn.Player:
                    marginRange = MathF.Ceiling(_curPlayer.TotalAttack * 0.1f);
                    _damage = rand.Next((int)(_curPlayer.TotalAttack - marginRange), (int)(_curPlayer.TotalAttack + marginRange + 1));

                    //크리티컬 확률 계산
                    if (CheakCritical(_curPlayer, _enemy))
                    {
                        _isCritical = true;
                        _damage *= _criticalValue;
                    }

                    //방어력 적용 : 방어력 수치 * 0.1f 적용되어 데미지 경감 
                    _damage -= (MathF.Ceiling(_enemy.Defense * 0.1f));

                    break;

                case Turn.Enemy:
                    marginRange = MathF.Ceiling(_enemy.Attack * 0.1f);
                    _damage = rand.Next((int)(_enemy.Attack - marginRange), (int)(_enemy.Attack + marginRange + 1.0f));

                    //크리티컬 확률 계산
                    if (CheakCritical(_curPlayer, _enemy))
                    {
                        _isCritical = true;
                        _damage *= _criticalValue;
                    }

                    //방어력 적용 : 방어력 수치 * 0.1f 적용되어 데미지 경감 
                    _damage -= (MathF.Ceiling(_curPlayer.TotalDefense * 0.1f));
                    break;
            }

            return (int)MathF.Ceiling(_damage);
        }

        private bool OnHitReaction(int _enemyIndex, float _skillValue = 1f, bool _isMultiTarget = false, string _skillName = "")
        {
            //피격 몬스터 초기화 , 배열/리스트는 참조형식이기에 자동으로 얕은복사 된 상태
            Enemy _hitEnemy = _enemyList[_enemyIndex];

            //몬스터가 죽어있으면 체크 안되게 하기
            if (_hitEnemy.IsDead)
            {
                if (!_isMultiTarget)
                {
                    _strbuilder.Clear();
                    _strbuilder.Append("해당 몬스터는 죽어 있습니다.");
                    Console.WriteLine(_strbuilder.ToString());
                    Console.ReadLine();
                }
                return false;
            }

            //데미지 적용 + Skill 계수 적용(스킬계수 적용후 올림 처리)
            int _damage = (int)MathF.Ceiling((DamageCaculate(_curPlayer, _hitEnemy, out bool _isCritical) * _skillValue));


            _hitEnemy.CurrentHp -= _damage;

            //Player Attack Print
            //전투에 들어왔다는 출력 문구
            _strbuilder.Clear();
            _strbuilder.AppendLine("\n===================================================");
            _strbuilder.Append("Battle!! - Player의 턴 \n\n");
            Console.Write(_strbuilder.ToString());

            _strbuilder.Clear();
            if (isSkill)
            {
                _strbuilder.AppendLine($"{_curPlayer.Name}의 {_skillName}!");
            }
            else
            {
                _strbuilder.AppendLine($"{_curPlayer.Name}의 공격!");
            }

            if (_damage <= 0)//적이 내 공격을 회피 했을 경우
            {
                _strbuilder.AppendLine($"Lv.{_hitEnemy.Level}{_hitEnemy.Name} 을(를) 회피했습니다. [데미지 : 0]");
                _strbuilder.AppendLine();
            }
            else
            {
                if (_isCritical)
                {
                    _strbuilder.AppendLine($"크리티컬!!! - 1.6배 데미지 증가\n");
                }

                _strbuilder.AppendLine($"Lv.{_hitEnemy.Level}{_hitEnemy.Name} 을(를) 맞췄습니다. [데미지 : {_damage}]");
                if (_hitEnemy.CurrentHp <= 0)
                {
                    _strbuilder.AppendLine($"{_hitEnemy.Name} HP : {_hitEnemy.CurrentHp + _damage} -> 0");
                }
                else
                {
                    _strbuilder.AppendLine($"{_hitEnemy.Name} HP : {_hitEnemy.CurrentHp + _damage} ->  {_hitEnemy.CurrentHp}");
                }

                //피격당하는 상대방의 체력이 0이 된 경우
                if (_hitEnemy.CurrentHp <= 0)
                {
                    _hitEnemy.IsDead = true;
                    _allDeadCount++;
                    _strbuilder.AppendLine($"\nLv.{_hitEnemy.Level}{_hitEnemy.Name} 가 쓰러졌습니다!");

                    //쓰러트린 몬스터의 경험치 획득
                    int _enemyExp = _hitEnemy.Exp + (int)(_hitEnemy.Level * 0.5f);

                    _gainExp += _enemyExp;
                    _strbuilder.AppendLine($"경험치 {_enemyExp}을 획득하였습니다. \n");

                    //쓰러트린 몬스터의 골드 획득
                    _gainGold += _hitEnemy.Gold;

                    //쓰러트린 몬스터의 아이템 획득
                    _gainItem.Add(_hitEnemy.GainItem);

                    //전투 관련 퀘스트 스택 증가
                    foreach (KeyValuePair<int, Quest> questData in _playerQuest)
                    {
                        if (questData.Value.Purpose == _hitEnemy.Name)
                        {
                            if (questData.Value.CurProgressRequired < questData.Value.EndProgressRequired)
                            {
                                questData.Value.CurProgressRequired++;
                            }
                        }
                    }
                }
            }
            _strbuilder.AppendLine("===================================================");
            Console.WriteLine(_strbuilder.ToString());

            return true;
        }

        private bool PlayerAttackSelect()
        {
            string _input = Console.ReadLine(); //Player 선택지 입력 대기
            if (!int.TryParse(_input, out int _select)) //숫자가 아닌 문자,문자열 입력시 예외처리
            {
                ErrorInput();
                return false;
            }
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

            return OnHitReaction(_select - 1);
        }

        private bool PlayerSkillSelect()
        {
            string _input = Console.ReadLine(); //Player 선택지 입력 대기
            if (!int.TryParse(_input, out int _select)) //숫자가 아닌 문자,문자열 입력시 예외처리
            {
                ErrorInput();
                return false;
            }
            else if (_select < 0 || _select > _mageSkill.SkillList.Count()) //적 번호를 초과하거나 미만으로 입력시 예외처리
            {
                ErrorInput();
                return false;
            }
            else if (_select == 0) //스킬 선택지에서 취소 선택
            {
                isSkill = false;
                return false;
            }

            //사용할 스킬 선택
            Skill _useSkill = _mageSkill.SkillList[int.Parse(_input) - 1];

            //사용할 스킬 MP와 현재 플레이어 MP 비교 체크
            if (_useSkill.MpCost > _curPlayer.CurrentMp)
            {
                _strbuilder.Clear();
                _strbuilder.AppendLine($"{_curPlayer.Name}의 MP가 모자랍니다.");
                Console.WriteLine(_strbuilder.ToString());
                Console.ReadLine();
                return false;
            }

            //사용할 스킬의 대상 선택
            if (PlayerSkillTargetSet(_useSkill))
            {
                Console.ReadLine();
                return true;
            }

            return false;
        }

        //플레이어 스킬 대상 설정
        private bool PlayerSkillTargetSet(Skill _useSkill)
        {
            Random randTarget;

            //공격 타겟 대상에 의한 스킬 분류
            switch (_useSkill.TargetType)
            {
                case Skill.SkillTargetType.OneTarget:
                    {
                        _strbuilder.Clear();
                        _strbuilder.AppendLine("\n===================================================");
                        _strbuilder.AppendLine($"사용 하는 스킬 : {_useSkill.Name}\n");
                        _strbuilder.AppendLine("0. 취소\n");
                        _strbuilder.AppendLine("원하는 대상을 입력해주십시오.\n >>");
                        Console.WriteLine(_strbuilder.ToString());

                        string _input = Console.ReadLine(); //Player 선택지 입력 대기
                        if (!int.TryParse(_input, out int _select)) //숫자가 아닌 문자,문자열 입력시 예외처리
                        {
                            ErrorInput();
                            return false;
                        }
                        else if (_select < 0 || _select > _enemyList.Count()) //적 번호를 초과하거나 미만으로 입력시 예외처리
                        {
                            ErrorInput();
                            return false;
                        }
                        else if (_select == 0) //공격 선택지에서 취소 선택
                        {
                            isSkill = false;
                            return false;
                        }

                        _curPlayer.CurrentMp -= _useSkill.MpCost;
                        return OnHitReaction(_select - 1, _useSkill.SkillValue, true, _useSkill.Name);
                    }

                case Skill.SkillTargetType.RandomTarget:
                    {
                        randTarget = new Random();
                        _curPlayer.CurrentMp -= _useSkill.MpCost;

                        for (int i = 0; i < _useSkill.TargetCount; i++)
                        {
                            int _ememyIndex = randTarget.Next(0, _enemyList.Count);

                            //false인경우 해당 몬스터는 이미 죽어있는 대상을 공격대상으로 하였음
                            if (!OnHitReaction(_ememyIndex, _useSkill.SkillValue, true, _useSkill.Name))
                            {
                                //살아있는 몬스터가 1마리인 상태에서 공격 기회가 남았는데 죽어버렸을경우 무한루프가 발생
                                //해당 현상 탈출을 위한 예외처리
                                if (/*_allEnemySumHP <= 0*/ _allDeadCount >= _enemyList.Count())
                                {
                                    break;
                                }

                                //공격기회를 복구하고 다시 공격대상 랜덤 선택
                                i--;
                                continue;
                            }
                        }

                        return true;
                    }
                case Skill.SkillTargetType.AllTarget:
                    {
                        _curPlayer.CurrentMp -= _useSkill.MpCost;
                        for (int i = 0; i < _enemyList.Count; i++)
                        {
                            OnHitReaction(i, _useSkill.SkillValue, true, _useSkill.Name);
                        }

                        return true;
                    }
            }

            return false;
        }

        private void PlayerSelect()
        {
            string _input = Console.ReadLine(); //Player 선택지 입력 대기

            switch (_input)
            {
                case "1": //공격 선택지
                    if (!isAttack) //공격을 선택하면 공격 화면으로 변경
                    {
                        isAttack = true;
                    }
                    break;

                case "2": // 스킬 선택지
                    if (!isSkill) //스킬을 선택하면 스킬 화면으로 변경
                    {
                        isSkill = true;
                    }
                    break;

                /*debug - 도망가기 기능*/
                case "3":
                    isRun = true;
                    break;
                /*debug - 도망가기 기능*/

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

        //전투결과 화면 
        private void BattleResult(bool isPlayerWin)
        {
            //BattleResult - Print
            while (true)
            {
                Console.Clear();
                _strbuilder.Clear();
                _strbuilder.AppendLine("Battle!! - Result\n");

                switch (isPlayerWin)
                {
                    case true:
                        _strbuilder.AppendLine("Victory\n");
                        _strbuilder.AppendLine($"던전에서 몬스터 {_curBattleEnemyCount}마리를 잡았습니다.\n");
                        _strbuilder.AppendLine("[캐릭터 정보]");
                        _strbuilder.AppendLine($"Lv.{_curPlayer.Level} {_curPlayer.Name}");
                        _strbuilder.AppendLine($"HP {_curPlayerBattleHP} -> {_curPlayer.CurrentHp}\n");
                        break;

                    case false:
                        _strbuilder.AppendLine("You Lose\n");
                        _strbuilder.AppendLine($"Lv.{_curPlayer.Level} {_curPlayer.Name}");
                        _strbuilder.AppendLine($"HP {_curPlayerBattleHP} -> 0\n");
                        break;
                }

                //잡은 몬스터에 대한 경험치 지급
                _strbuilder.AppendLine("[획득 경험치]");
                _strbuilder.AppendLine($"EXP {_curPlayer.Exp} -> {_curPlayer.Exp + _gainExp}");
                _curPlayer.Exp += _gainExp;
                if (_curPlayer.LevelUpCheck())
                {
                    _strbuilder.AppendLine($"\n축하합니다!! \n{_curPlayer.Name}의 Lv이 {_curPlayer.Level}로 레벨업 하였습니다!!");
                }

                //잡은 몬스터에 대한 골드보상 지급
                _strbuilder.AppendLine("\n[획득 아이템]");
                _strbuilder.AppendLine($"{_gainGold} Gold");
                _curPlayer.Gold += _gainGold;

                //나중에 Item 코드 Merge 할때 변경 필요 + Player에게 적용하게 변경되야함
                foreach (string ItemName in _gainItem)
                {
                    _strbuilder.AppendLine($"{ItemName} - 1");
                    //_curPlayer.Inventory.Add(/*Item*/);
                }

                _strbuilder.AppendLine("\n0. 다음\n");
                _strbuilder.AppendLine(">>");

                Console.Write(_strbuilder.ToString());
                string _input = Console.ReadLine(); //Player 선택지 입력 대기
                switch (_input)
                {
                    case "0":
                        return;

                    default:
                        ErrorInput();
                        break;
                }
            }
        }


    }
}
