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
            //클리어된 퀘스트 있는지 체크
            foreach (KeyValuePair<int, Quest> _questData in _questManager.AcceptedQuest)
            {
               if( _questData.Value.QuestCheck(_curPlayer))
                {
                    _strbuilder.Clear();
                    _questData.Value.IsFinish = true; //퀘스트 클리어 처리
                    _strbuilder.AppendLine($"{_questData.Value.Label}의 퀘스트를 완료 하였습니다.\n");
                    
                    //퀘스트 클리어 보상
                    _strbuilder.AppendLine($"퀘스트 보상 : ");

                    /*Debug*/
                    //아이템 퀘스트 보상
                    if (_questData.Value.RewardType != "")
                    {
                        _strbuilder.AppendLine($"{_questData.Value.RewardType} x {_questData.Value.RewardValue} ");
                        Item rewardItem = new Item(ITEMTYPE.Armor, _questData.Value.RewardType, 300, 0, 3, 10, "쓸만해보이는 방패", true);
                        _curPlayer.inventory.Add(rewardItem);
                      
                    }
                    /*!Debug*/

                    // 골드보상
                    if (_questData.Value.RewardGold != 0)
                    {
                        _curPlayer.Gold += _questData.Value.RewardGold;
                        _strbuilder.AppendLine($"{_questData.Value.RewardGold} G");
                    }

                    Console.WriteLine(_strbuilder.ToString());
                    //수락한 퀘스트 리스트에서 제거
                    _questManager.AcceptedQuest.Remove(_questData.Key);

                    Console.ReadLine();
                }
                
            }


            Update();
        }

        private void Print()
        {
            _strbuilder.Clear();
            _strbuilder.AppendLine($"Quest!!\n");

            for (int i = 0; i < _questManager.Quests.Count; i++)
            {
                _strbuilder.Append($"{i + 1} . {_questManager.Quests[i + 1].Label}");
                if (_questManager.Quests[i+1].IsFinish)
                {
                    _strbuilder.AppendLine("[ 퀘스트 완료 ]");
                }
                else
                {
                    _strbuilder.AppendLine();
                }
            }

            _strbuilder.AppendLine("\n 0. 취소");
            _strbuilder.AppendLine("원하시는 퀘스트를 선택해주세요. \n >> \n");

            _strbuilder.AppendLine("[현재 수락중인 퀘스트]\n");

            foreach (KeyValuePair<int, Quest> _questData in _questManager.AcceptedQuest)
            {
                _strbuilder.AppendLine($"- {_questData.Value.Label}");
            }

            Console.Write(_strbuilder.ToString());
        }

        private void QuestAcceptCheck(int _questselectID)
        {
            while (true)
            {
                Console.Clear();
                _questManager.Print(_questselectID);
                QuestAcceptCheckPrint();

                string _input = Console.ReadLine();

                switch(_input)
                {
                    case "1": //수락
                        if (!_questManager.Quests[_questselectID].IsFinish)
                        {
                            _questManager.QuestAccept(_questselectID);
                            
                            //Test : 플레이어 class 안에 수락퀘스트 넣어보기
                            _curPlayer.PlayerQuest = _questManager.AcceptedQuest;
                        }
                        else
                        {
                            _strbuilder.Clear();
                            _strbuilder.AppendLine("해당 퀘스트는 이미 완료 하셨습니다.");
                            Console.WriteLine(_strbuilder.ToString());
                        }
                        Console.ReadLine();
                        return;

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
            _curPlayer.PlayerQuest = _questManager.AcceptedQuest;
            player = _curPlayer;
        }
    }
}
