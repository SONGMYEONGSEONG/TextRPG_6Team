using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{

    internal class EnemyManager
    {

        List<List<Enemy>> _enemies;

        public EnemyManager()
        {
            if (_enemies == null)
            {
                _enemies = new List<List<Enemy>>();

                for (int i = 0; i < (int)SummonArea.End; i++)
                {
                    _enemies.Add(new List<Enemy>());
                }
            }
        }

        public List<Enemy> GetEnemies(SummonArea summonArea)
        {
            return _enemies[(int)summonArea];
        }

        private string ApplyEscapeCharacters(string input)
        {
            // 예시로, 줄바꿈 문자를 이스케이프 화시킴
            input = input.Replace("\\n", "\n");
            input = input.Replace("\\t", "\t");

            // 다른 이스케이프 문자도 필요에 따라 처리할 수 있다.
            return input;
        }

        public void Initialize()
        {
            StringBuilder _strbuilder = new StringBuilder(); //문자열 최적화를 위한 스트링빌더 선언
            string csvFilePath = @"..\..\..\Data\TextRPG_EnemyList.csv";

            // 1. CSV 파일을 UTF-8 인코딩으로 읽기
            List<List<Enemy>> csvData = new List<List<Enemy>>();
            using (var reader = new StreamReader(csvFilePath, Encoding.UTF8))
            {
                string headerLine = reader.ReadLine(); //카테고리
                string[] headers = headerLine.Split(','); //문자열 카테고리 분류

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    Enemy enemy;

                    //0 몬스터 등장 지역
                    //1 몬스터 종류
                    //2 몬스터 이름
                    //3 몬스터 레벨
                    //4 몬스터 경험치
                    //5 몬스터 최대 HP
                    //6 몬스터 최대 MP
                    //7 몬스터 공격력
                    //8 몬스터 방어력
                    //9 몬스터 민첩 : 회피 확률에 영향
                    //10몬스터 명중 : 상대의 회피 확률과 내 공격 크리티컬 확률에 영향
                    //11 몬스터 행운 : 공격 크리티컬 확률, 보상에 영향
                    //12 몬스터 드랍 골드
                    //13 몬스터 드랍 아이템

                    switch (Enum.Parse(typeof(SummonArea), values[0]))
                    {
                        case SummonArea.Forest:
                            enemy = new Enemy(
                                                SummonArea.Forest,
                                                (EnemyType)Enum.Parse(typeof(EnemyType), values[1]),
                                                values[2],
                                                int.Parse(values[3]),
                                                int.Parse(values[4]),
                                                int.Parse(values[5]),
                                                int.Parse(values[6]),
                                                int.Parse(values[7]),
                                                int.Parse(values[8]),
                                                int.Parse(values[9]),
                                                int.Parse(values[10]),
                                                int.Parse(values[11]),
                                                int.Parse(values[12]),
                                                values[13]
                                                );

                            _enemies[(int)SummonArea.Forest].Add(enemy);
                            break;

                        case SummonArea.Temple:
                            enemy = new Enemy(
                                               SummonArea.Temple,
                                                (EnemyType)Enum.Parse(typeof(EnemyType), values[1]),
                                                values[2],
                                                int.Parse(values[3]),
                                                int.Parse(values[4]),
                                                int.Parse(values[5]),
                                                int.Parse(values[6]),
                                                int.Parse(values[7]),
                                                int.Parse(values[8]),
                                                int.Parse(values[9]),
                                                int.Parse(values[10]),
                                                int.Parse(values[11]),
                                                int.Parse(values[12]),
                                                values[13]
                                                );

                            _enemies[(int)SummonArea.Temple].Add(enemy);
                            break;

                        case SummonArea.Beach:
                            enemy = new Enemy(
                                                SummonArea.Beach,
                                                (EnemyType)Enum.Parse(typeof(EnemyType), values[1]),
                                                values[2],
                                                int.Parse(values[3]),
                                                int.Parse(values[4]),
                                                int.Parse(values[5]),
                                                int.Parse(values[6]),
                                                int.Parse(values[7]),
                                                int.Parse(values[8]),
                                                int.Parse(values[9]),
                                                int.Parse(values[10]),
                                                int.Parse(values[11]),
                                                int.Parse(values[12]),
                                                values[13]
                                                );

                            _enemies[(int)SummonArea.Beach].Add(enemy);
                            break;

                        default:

                            break;
                    }

                    //row[int.Parse(values[0])] = quest;
                }
            }
        }
    }
}
