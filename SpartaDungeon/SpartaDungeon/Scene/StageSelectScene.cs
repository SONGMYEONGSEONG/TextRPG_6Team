﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SpartaDungeon
{
    internal enum SummonArea
    {
        None = 0,
        Forest,
        Beach,
        Temple,
        End
    }

    internal class StageSelectScene
    {
        //Stage 임시 설정
       
        StringBuilder _strbuilder = new StringBuilder(); //문자열 최적화를 위한 스트링빌더 선언
        SummonArea _curArea; //현재 선택 되있는 스테이지
        SummonArea _selectedArea; //선택 한 스테이지
        Character _curPlayer; //현재 

        public StageSelectScene()
        {
            _selectedArea = SummonArea.None;
        }

        public void Initialize(Character _player)
        {
            _curPlayer = _player;
        }

        public void Initialize(SummonArea Area)
        {
            _selectedArea = Area;
            _curArea = Area;
        }

        public void SceneStart() 
        {
            Update();
        }

        public void Print() 
        {
            SummonArea _area;

            _strbuilder.Clear();
            _strbuilder.AppendLine($"Stage Select\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(_strbuilder.ToString());

            _strbuilder.Clear();
            for (int i = 1; i < (int)(SummonArea.End) ; i++)
            {
                _strbuilder.AppendLine($"{i}. {(SummonArea)i}");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(_strbuilder.ToString());

            _strbuilder.Clear();
            _strbuilder.AppendLine("\n0. 취소");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(_strbuilder.ToString());

            _strbuilder.Clear();
            _strbuilder.AppendLine("\n원하시는 스테이지를 선택해주세요. \n >>");
            Console.ResetColor();
            Console.Write(_strbuilder.ToString());
        }

        public void ErrorInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("잘못된 입력입니다.");
            Console.WriteLine();
            Console.ResetColor();
            Console.ReadLine();
        }

        public void Update()
        {
            while (true)
            {
                Console.Clear();
                Print();

                string _input = Console.ReadLine();

                if (!int.TryParse(_input, out int _select)) //숫자가 아닌 문자,문자열 입력시 예외처리
                {
                    ErrorInput();
                    continue;
                }
                if (_select < 0 || _select > (int)(SummonArea.End))
                {
                    ErrorInput();
                    continue;
                }

                _selectedArea = (SummonArea)_select;

                if(_selectedArea == SummonArea.None)
                {
                    _selectedArea = _curArea;   
                }

                break;
            }

        }

        public void SceneExit(ref Character player)
        {
            player = _curPlayer;
        }

        public void SceneExit(ref SummonArea Area)
        {
            Area = _selectedArea;
        }
    }
}
