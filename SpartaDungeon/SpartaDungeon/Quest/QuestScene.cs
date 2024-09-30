using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SpartaDungeon
{
    internal class QuestScene
    {
        StringBuilder _strbuilder = new StringBuilder(); //문자열 최적화를 위한 스트링빌더 선언
        QuestManager _questManager;
        Player _curPlayer; //현재 

        public QuestScene()
        {
            _questManager = new QuestManager();
        }

        public void Initialize(Player _player)
        {
            _curPlayer = _player;
        }

        public void SceneStart()
        {
            Update();
        }

        private void Print()
        {
            _strbuilder.Clear();
            _strbuilder.AppendLine($"Quest!!");

            for (int i = 0; i < _questManager.Quests.Count; i++)
            {
                _strbuilder.AppendLine($"{i + 1} . {_questManager.Quests[i + 1].Label}");
            }

            _strbuilder.AppendLine("\n원하시는 퀘스트를 선택해주세요. \n >>");
            Console.Write(_strbuilder.ToString());
        }

        private void QuestAcceptCheck(int _select)
        {
            while (true)
            {
                Console.Clear();
                _questManager.Print(_select);
                QuestAcceptCheckPrint();

                string _input = Console.ReadLine();

                switch(_input)
                {
                    case "1": //수락

                        break;

                    case "2": //거절
                        return;

                    default:
                        ErrorInput();
                        continue;
                }
            }
        }

        private void QuestAcceptCheckPrint()
        {
            _strbuilder.Clear();
            _strbuilder.AppendLine("1. 수락");
            _strbuilder.AppendLine("2. 거절");
            _strbuilder.AppendLine("\n원하시는 행동을 입력해주세요.\n >>");
            Console.Write(_strbuilder.ToString());
        }

        private void ErrorInput()
        {
            Console.WriteLine("잘못된 입력입니다.");
            Console.WriteLine();
            Console.ReadLine();
        }

        private void Update()
        {
            while (true)
            {
                Console.Clear();
                Print();

                string _input = Console.ReadLine();

                if (!int.TryParse(_input, out int _select)) //숫자가 아닌 문자,문자열 입력시 예외처리
                {
                    ErrorInput();
                }
                if (_select < 0 || _select > _questManager.Quests.Count)
                {
                    ErrorInput();
                    continue;
                }

                switch (_select)
                {
                    case 0:
                        //메인화면으로 나가기
                        return;

                    default:
                        _strbuilder.Clear();
                        _strbuilder.AppendLine($"Quest!!");
                        Console.Write(_strbuilder.ToString());

                        QuestAcceptCheck(_select);
                        break;
                }

            }
        }



        public void SceneExit(ref Player player)
        {
            player = _curPlayer;
        }


    }
}
