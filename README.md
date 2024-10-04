# TextRPG_6Team
SSC_Unity_6Gen_6Team_TextRPG

[Team Notion]
https://www.notion.so/HotSix-6-106ebfb92b13800f845ee472b86d85a5

### [프로젝트 소개]
TextRPG - Sparta Dungeon   
협업을 통해 C# Console Text RPG 만들기   

개발환경 - C#, Visual Studio

---
### [프로젝트 목표]
- C# 프로그래밍을 숙달
- 팀 단위의 의사결정과 소통을 통한 결과물을 도출
- GitHub을 이용한 협업 방식 이해

---
### [개발 기간]
2024.09.26 - 2024.10.04

---
### [멤버]
- 팀장 송명성 - 던전 전투, 몬스터, 던전 스테이지, 퀘스트, 레벨업, 스킬, 콘솔 꾸미기
- 팀원 이지혜 - 아이템, 인벤토리, 회복 아이템, 상점, 아이템 보상
- 팀원 안성찬 - 회피, 치명타, 캐릭터 생성(이름 및 직업 선택), 메인 Scene, 저장/불러오기

---

### [Scene]
#### 1. 게임 시작 화면

![image](https://github.com/user-attachments/assets/a713b80b-0352-4b1f-bbb2-80c927e715b3)

저장된 게임을 불러오거나 새로운 게임을 시작할 수 있습니다. 저장된 게임이 없다면 새 게임을 시작합니다.

---

#### 2. 캐릭터 생성
새 게임을 시작하면 먼저 플레이어의 이름과 직업을 선택하여 캐릭터를 생성합니다.

이름 설정   
![SetName2](https://github.com/user-attachments/assets/a87407c1-7666-4c39-b089-eec283d493bf)

이름 설정 완료 후 직업 선택   
![SelectJob](https://github.com/user-attachments/assets/172bab66-0625-4b15-b264-bc2af0fee099)

---

#### 3. MainScene 메인 선택 화면
캐릭터 생성을 완료하거나 저장된 게임을 불러오면 주요 활동을 선택할 수 있는 MainScene이 호출됩니다.   
무한루프 구간으로 모든 Scene과 연결되어 있고 선택한 Scene이 종료되면 다시 MainScene으로 돌아옵니다.   
다음 이미지와 같은 선택지가 주어집니다.   
![MainScene](https://github.com/user-attachments/assets/35012668-5407-4bd6-a1f6-6c4be44374e4)

---

#### 4. 상태 보기

플레이어의 상태를 확인할 수 있습니다.   
던전 전투, 아이템 착용 등으로 상태가 변했을 때 UpdataStat메서드로 스탯을 갱신하고 상태 보기에 반영됩니다.   
![StatusScene](https://github.com/user-attachments/assets/bf82c91f-1b11-4081-bd48-a44da4e7be21)

---

#### 5. 인벤토리

인벤토리에서 무기나 갑옷 아이템을 장착할 수 있습니다 장착된 아이템은 [E]로 표시됩니다.   
회복 아이템을 선택하여 사용할 수 있습니다.
![Inventory](https://github.com/user-attachments/assets/3e0ae8b3-74ea-45e3-9605-7585993ec074)

![UseHealingItem](https://github.com/user-attachments/assets/07e516f4-3904-459a-a6d8-d5e79e39fdc8)

---

#### 6. 던전

![DungeonScene](https://github.com/user-attachments/assets/7b96e554-0ced-431a-b927-eb47954eb0e1)

모든 몬스터 데이터는 csv파일로 저장되어 있고, 스테이지(Forest, Beach, Temple)로 구분해 리스트로 만들었습니다.   
던전을 입장하면 해당 스테이지의 몬스터 리스트를 불러와 랜덤으로 1 ~ 4마리 생성합니다.   
플레이어는 기본 공격, 스킬, 도망가기 중 선택할 수 있습니다.   

---
**6-1. 전투**

- Player Turn

공격 선택 시 공격할 적을 선택할 수 있습니다.   
![Battle_Attack](https://github.com/user-attachments/assets/0d825db0-f095-48ae-8f25-2b7a24e10c8e)


플레이어는 각 직업에 해당하는 스킬리스트를 갖고 있습니다.   
스킬 선택 시 플레이어가 갖고 있는 스킬리스트가 표시됩니다.   
사용할 스킬 선택하고 스킬 대상을 선택할 수 있습니다.   
스킬은 단일 대상 스킬, 랜덤 대상 스킬(여러 대상), 전체 대상 스킬이 있습니다.   
![BattleSelectSkill](https://github.com/user-attachments/assets/0e141487-034b-464d-89fd-ebea400224ed)   
![UseSkill](https://github.com/user-attachments/assets/0ee461c0-8a74-4b63-97df-ddf373c2d504)

- Enemy Turn

![EnemyTurn](https://github.com/user-attachments/assets/37f73a64-f12d-4bf3-9347-3438ff9bcf29)   
살아있는 모든 Enemy가 플레이어를 공격합니다.   
플레이어와 적의 공격 모두 치명타, 회피가 발생할 수 있고 그 결과가 표시됩니다.

---
**6-2. 전투 결과**

![DungeonResultScene](https://github.com/user-attachments/assets/8a10c177-ccc7-4df8-a504-bc8ce53cf0a9)

![RewardAddInventory](https://github.com/user-attachments/assets/4ca25b0b-988f-426c-aaf2-c2003a155629)   
전투 보상으로 받은 아이템은 인벤토리에 추가됩니다.


---

#### 7. 던전 스테이지 선택

![SelectStage](https://github.com/user-attachments/assets/0fca3610-cf40-48f8-8bb5-330d6e7d96fa)
![StageChange](https://github.com/user-attachments/assets/d29653a4-120a-49ac-ba81-2cd96e59a72d)   
던전 스테이지(던전 지역)를 선택할 수 있습니다. 각 지역마다 나오는 몬스터의 종류가 달라집니다.

---

#### 8. 퀘스트

퀘스트 목록이 표시되고 각 퀘스트를 확인하여 수락/거절을 선택할 수 있습니다.   
![QuestMain](https://github.com/user-attachments/assets/1d321f7f-7d10-4926-9cd0-9f05b6351778)   
![SlectQuest](https://github.com/user-attachments/assets/d7810ce1-5b11-4053-99aa-c760fc3b1b75)
![SlectQuestList](https://github.com/user-attachments/assets/299795b5-97bd-4918-bac9-19f1fd6db0ee)   
퀘스트를 수락하면 수락중인 퀘스트 목록이 갱신됩니다.   
수락중인 퀘스트 목록은 플레이어가 Dictionary로 갖고 있습니다.

퀘스트 완료   
![CompleteQuest](https://github.com/user-attachments/assets/e52194bd-adc2-4b85-bfa0-284e8d4b31af)   
![ShowCompleteQuest](https://github.com/user-attachments/assets/17be7d91-75d8-423e-9071-3776ff4ab4bb)   
퀘스트를 완료하면 퀘스트 Scene 입장 시 바로 보상이 주어지고 완료된 퀘스트로 표시가 바뀝니다.

---

#### 9. 상점

![EnterStore](https://github.com/user-attachments/assets/294d5525-8d4e-4226-946e-fdabb1df00b5)   
![store](https://github.com/user-attachments/assets/0d1e90ec-aba5-485b-9d4f-9d3a7bfcfdc0)   
상점에서는 아이템을 구매하거나 보유중인 아이템을 판매할 수 있습니다.   
아이템 구매를 선택하면 어떤 종류의 아이템을 구매할 건지 선택할 수 있습니다.   
아이템 판매를 선택하면 인벤토리 아이템 목록과 보유 개수가 표시됩니다.   

구매 상점 아이템 목록   
![ItemList](https://github.com/user-attachments/assets/9a467d65-30f6-47aa-80a5-68b49dbab9ce)   
![purchaseItem](https://github.com/user-attachments/assets/70e14f35-2d07-41ec-a2d6-b54f7df8ef2e)

판매   
![SellItem](https://github.com/user-attachments/assets/2952f855-0946-4cd5-90c6-8d26f1785748)

아이템 구매, 판매 시 보유 개수가 갱신됩니다.

---

#### 10. 저장 및 종료
Json 직렬화, 역직렬화를 통해 플레이어의 캐릭터 데이터를 저장하고 불러올 수 있게 했습니다.   
게임 종료 시 플레이어 데이터가 자동으로 저장됩니다.   
![endGame](https://github.com/user-attachments/assets/67de7c5b-b061-4967-9cc4-c06dac587b95)

---
#### 11. 아이템
게임 내 등장하는 모든 아이템을 리스트에 저장한 후 Json파일로 직렬화했습니다.   
아이템 리스트를 불러오고 조건을 설정해 인벤토리, 상점, 퀘스트 보상, 전투 보상 등 필요한 아이템을 꺼내옵니다.   
아이템 ID, 타입 등으로 조건을 설정해 아이템을 구분했습니다.   

---