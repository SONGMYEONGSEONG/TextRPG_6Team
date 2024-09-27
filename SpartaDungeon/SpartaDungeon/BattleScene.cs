﻿using System;
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

    /*Debug용 Enemy List*/
    /*Enemy Class 만들때 iStatus 인터페이스 가져오게 선언해야됨*/
    public class EnemyTest
    {
        public string? Name { get; set; }
        public int Level { get; set; }
        public int CharacterAttack { get; set; }
        public int CharacterDefense { get; set; }
        public int CharacterMaxHP { get; set; }
        public int? CurrentHP { get; set; }

    }

    //전투 할때 사용되는 전투 Scene ( 턴제 전투)
    /*
     static 으로 할지 말지 고민 해봐야될듯 ,
     static으로 안하면 GameManager 쪽에 만들어야됨
     */
    internal class BattleScene
    {
        StringBuilder _strbuilder = new StringBuilder(); //문자열 최적화를 위한 스트링빌더 선언
        Turn _curTurn; //현재 진행중인 유저의 턴
        Player _curPlayer; //현재 전투에 참여한 플레이어 오브젝트
                           //Enemy[] _enemys;  //현재 전투에 참여한 적 오브젝트들

        /*Debug*/
        EnemyTest _enemyTest;

        public BattleScene()
        {
            _curTurn = Turn.Player; //Scene 생성되면 , 선턴은 바로 플레이어로 지정

            /*Debug - 테스트용 Enemy */
            _enemyTest = new EnemyTest();
            _enemyTest.Name = "Test용 몹";
            _enemyTest.Level = 10;
            _enemyTest.CharacterAttack = 10;
            _enemyTest.CharacterDefense = 10;
            _enemyTest.CharacterMaxHP = 100;
            _enemyTest.CurrentHP = 100;
            //

        }


        public void Initialize(Player _player /*,Enemy[] _enemys*/ )//나중에는 GameManager나 EnemyManager에서 배열로 적 받아와야됨
        {
            _curPlayer = _player;
        }

        public void SceneStart()
        {
            Update();
        }

        //전투 상황 프린트
        private void BattleStatusPrint()
        {
            //전투에 들어왔다는 출력 문구
            _strbuilder.Clear();
            _strbuilder.Append("Battle!!\n\n");
            Console.Write( _strbuilder.ToString() );

            //Enemy List를 불러와서 출력함
            string _enemyStatusStr = "";
            for(int i = 1; i < 4; i++) // Enemy 몹 소환, 나중에 List를 받아와서 형식화 해야됨
            {
                _strbuilder.Clear();
                _enemyStatusStr = $"{i} {_enemyTest.Level} {_enemyTest.Name}";
                _strbuilder.Append(_enemyStatusStr);

                switch (_enemyTest.CurrentHP <=0 )
                {
                    case true: //적의 HP가 0이하 -> 죽음상태를 표시
                        _strbuilder.Append(" Dead");
                        break;

                    case false:
                        _strbuilder.Append($" HP { _enemyTest.CurrentHP}");
                        break;
                }

                _strbuilder.Append("\n");
                Console.Write(_strbuilder.ToString());
            }


            //Player 정보 출력
            _strbuilder.Clear();
            _strbuilder.Append("\n\n");
            _strbuilder.AppendLine("[내 정보]");
            _strbuilder.AppendLine($"Lv.{_curPlayer.Level}  {_curPlayer.Name}  ({_curPlayer.ClassType})");
            _strbuilder.AppendLine($"HP {_curPlayer.CurrentHP} / {_curPlayer.CharacterMaxHP}");
            Console.Write(_strbuilder.ToString());
        }

        private void Update()
        {
            string input;

            while (true)
            {
                Console.Clear();
                BattleStatusPrint();

                switch (_curTurn)
                {
                    case Turn.Player :

                        input = Console.ReadLine(); //Player 선택지 입력 대기

                        _curTurn = Turn.Enemy; //턴 교체 (Player -> Enemy)
                        break;

                    case Turn.Enemy :

                        _curTurn = Turn.Player; //턴 교체 (Player -> Enemy)
                        break;

                }
            }
        }


    }
}
