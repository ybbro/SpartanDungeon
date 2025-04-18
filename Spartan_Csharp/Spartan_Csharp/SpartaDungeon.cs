using System;
using System.Collections.Generic;
using System.Threading;

namespace Spartan_Csharp
{
    // 목표: 스파르타 코딩 클럽 C# 문법 학습
    // 2~3주 문법 교육 내용에 따른 적 등장
    // 매일 (게임의 날짜, 요일 표시)
    // 1) 공부 or 과제
    // 2) 주중 (월~금) : 정시에 강의실 나가기(체력 회복량 높음) or 추가 공부(스텟 상승, 체력 회복량 낮음)
    // 3) 금요일까지 과제를 다 했는가? 못했는가? >> 바로 게임오버는 좀 그럼.. 어떻게 달성 필요를 줄까..
    // 4) 주말(토, 일): 휴식(체력 회복량 높음), 공부(스텟 상승, 체력 회복량 낮음) 선택 가능

    // 처음 스테이터스는 낮음. 각각 개인 편차가 있기에 랜덤

    internal static class SpartaDungeon
    {
        enum sceneNums
        {
            charCreate,
            start,
            status,
            inventory,
            equip,
            shop,
        }

        internal static PlayerStatus player { get; set; } // 플레이어 스텟
        internal static Usable_Abilities usable_Abilities = new Usable_Abilities(); // 사용 아이템의 효과들
        internal static Item_Dictionary item_Dictionary { get; set; } = new Item_Dictionary(); // 아이템 사전
        static Shop shop = new Shop();
        internal static Inventory inventory { get; set; } // 인벤토리
        static int sceneNum = 0; // 다음 씬 번호. 캐릭터 생성부터 시작

        // 안내문 대사들 모음
        static string[] infoTexts = new string[]
        {
            // 캐릭터 생성
            "스파르타 코딩클럽 던전에 오신 여러분 환영합니다.\n" +
            "원하는 이름을 입력하고 엔터를 눌러주세요.\n\n",

            // 시작
            "스파르타 코딩클럽에 당도한 것을 환영하오. 낯선이여.\n" +
            "이곳에서 던전으로 들어가기 전 준비를 할 수 있다네.\n\n" +
            "1. 상태창\n" +
            "2. 가방\n" +
            "3. 상점\n",

            // 상태... 이래도 괜찮은가?? 진짜??? C# 좋구나..
            "[ 상태창 ]\n\n" +
            "Lv. {0}\n" +
            "{1} ( 응애 )\n" +
            "체력 : {2} / {3}\n" +
            "C#문법력 (CSG) : {4}\n" +
            "디버깅력 (DBG) : {5}\n" +

            "돈 : {6} G\n\n" +
            "0. 나가기\n",

            // 인벤토리
            "[ 가방 ]\n" +
            "보유 중인 아이템을 관리할 수 있습니다.\n\n" +
            "[아이템 목록]\n"+
            "{0}\n\n" +
            "1. 장착 관리\n" +
            "0. 나가기\n",

            // 장착 관리
            "[ 가방 - 장착 관리 ]\n" +
            "보유 중인 아이템을 관리할 수 있습니다.\n\n" +
            "[아이템 목록]\n"+
            "{0}\n\n" +
            "0. 나가기\n",

            // 상점
            "[ 상점 ]\n" +
            "필요한 아이템을 얻을 수 있는 상점입니다.\n\n" +
            "[보유 골드]\n"+
            "{0} G\n\n" +
            "[아이템 목록]\n"+
            "{1}\n\n" +
            "0. 나가기\n",
        };

        static void Main()
        {
            bool isGamePlaying = true;

            // 플레이 데이터를 만들면 데이터가 없을때만 새로 생성
            player = new PlayerStatus(); // 새 플레이어 스텟
            inventory = new Inventory(); // 새 인벤토리

            while (isGamePlaying)
            {
                Scene(sceneNum);
            }
        }

        // 텍스트 rpg는 크게 아래의 반복
        // 안내 문자 출력
        // >> 입력(잘못된 입력의 경우 다시 입력하게끔)
        // >> 입력 내용에 따른 장면 변화
        static void Scene(int _sceneNumInt)
        {
            bool isScenePlaying = true; // 해당 씬에서 넘어갈지 여부
            sceneNums _sceneNum = (sceneNums)_sceneNumInt; // 해당 씬의 번호
            ConsoleKey enter; // 해당 씬에서 플레이어가 입력한 키
            Dictionary<ConsoleKey, sceneNums> choiceToSceneNum = new Dictionary<ConsoleKey, sceneNums>(); // 각 키 입력에 대응하는 씬 번호

            Console.Clear(); // 이전 씬의 내용 지우기


            // 어떤 씬인가에 따라 달라질 내용들
            switch (_sceneNum)
            {
                case sceneNums.charCreate:
                    Console.Write(infoTexts[_sceneNumInt]); // 안내문
                    player.Name = Console.ReadLine(); // 이름 입력받아서 플레이어 스테이터스에 써주기
                    string confirmationText = "\n입력하신 이름은 \"{0}\" 입니다.\n\n" +
                                               "1. 확인\n2. 다시 입력\n";
            
                    Console.Write(confirmationText, player.Name); // 이름 확인 텍스트
                    // 다음 씬 선택 버튼에 대한 입력 처리 준비
                    choiceToSceneNum.Add(ConsoleKey.D1, sceneNums.start); // 시작 씬
                    choiceToSceneNum.Add(ConsoleKey.D2, sceneNums.charCreate); // 취소하면 다시 캐릭터 이름 입력 씬(입력하면 덮어씀)
                    break;

                case sceneNums.start:
                    Console.Write(infoTexts[_sceneNumInt]);
                    choiceToSceneNum.Add(ConsoleKey.D1, sceneNums.status); // 1 >> 상태 씬
                    choiceToSceneNum.Add(ConsoleKey.D2, sceneNums.inventory); // 2 >> 인벤토리
                    choiceToSceneNum.Add(ConsoleKey.D3, sceneNums.shop); // 3 >> 상점
                    break;

                case sceneNums.status:
                    // 상태창 문자열의 각 위치에 알맞는 데이터 가져와서 써주기!
                    Console.Write(infoTexts[_sceneNumInt], 
                        player.GetStatus(statusSort.LV), 
                        player.Name, 
                        player.GetHP(), player.GetStatus(statusSort.HP_MAX), 
                        player.GetStatus(statusSort.CSG), 
                        player.GetStatus(statusSort.DBG),
                        player.Money
                        );
                    choiceToSceneNum.Add(ConsoleKey.D0, sceneNums.start);
                    break;

                case sceneNums.inventory:
                    Console.Write(infoTexts[_sceneNumInt], inventory.ItemNames());
                    choiceToSceneNum.Add(ConsoleKey.D1, sceneNums.equip);
                    choiceToSceneNum.Add(ConsoleKey.D0, sceneNums.start);
                    break;

                case sceneNums.equip: // 인벤토리랑 하나로 써도 될 것 같은데.. 우선 과제 그대로 ㄱㄱ
                    Console.Write(infoTexts[_sceneNumInt], inventory.ItemEquip());
                    choiceToSceneNum.Add(ConsoleKey.D0, sceneNums.inventory); // 뒤로가기(인벤토리로)
                    break;

                case sceneNums.shop:
                    Console.Write(infoTexts[_sceneNumInt], player.Money, shop.PrintShop()); // !!!!! 상점 아이템 목록 1번째에 넣기
                    if (shop.isPurchase) // 구매 화면을 보고 있다면
                        choiceToSceneNum.Add(ConsoleKey.D0, sceneNums.shop); // 상점 기본 화면으로 복귀
                    else
                    {
                        choiceToSceneNum.Add(ConsoleKey.D0, sceneNums.start); // 상점 기본이었다면 시작 화면으로
                        Console.Write("1. 아이템 구매\n\n"); // 구매 화면으로 진입할 수 있는 커맨드 알림 추가
                    }
                    break;

                default:
                    break;
            }

            string enterInfo = "\n원하시는 행동을 입력해주세요.\n>> ";
                
            // 다음 씬으로 이동 선택
            while (isScenePlaying)
            {
                // 입력 버퍼 비우기
                while (Console.KeyAvailable) { Console.ReadKey(true); } // true = 키 출력 안 되게
                Console.Write(enterInfo); // 입력 안내문 출력
                enter = Console.ReadKey().Key; // 키 입력까지 대기 (엔터를 누르지 않아도 됨!)

                if(_sceneNum == sceneNums.equip) // 장착씬에서만 동작하는 부분
                {
                    int value_diff = enter - ConsoleKey.D0; // 입력한 키가 몇번인지
                    if (value_diff > 9) // 숫자 9번 이후부터는 어떤 키인지 잘 모르겠음.. 우선 막아두기
                    {
                        WrongInfo();
                        break; // 장착/해제를 반영하기 위해 씬 재시작
                    }
                    else if (value_diff > 0)
                    {
                        int itemIndex = value_diff - 1; // 아이템은 0번부터 시작
                        inventory.EquipOrUnequip(itemIndex); // 해당 아이템 장착/해제 토글
                        break; // 장착/해제를 반영하기 위해 씬 재시작
                    }
                    else if(value_diff < 0)
                    {
                        WrongInfo();
                        break; // 장착/해제를 반영하기 위해 씬 재시작
                    }
                }
                // 상점 창이고 구매 화면으로 전환했다면
                else if(_sceneNum == sceneNums.shop)
                {
                    if(shop.isPurchase) // 구매 화면이라면
                    {
                        int value_diff = enter - ConsoleKey.D0; // 입력한 키가 몇번인지
                        if (value_diff > 9) // 숫자 9번 이후부터는 어떤 키인지 잘 모르겠음.. 우선 막아두기
                        {
                            WrongInfo();
                        }
                        else if (value_diff > 0)
                        {
                            int itemIndex = value_diff - 1; // 아이템은 0번부터 시작
                            if (!shop.StockCheck(itemIndex))
                            {
                                Console.WriteLine("\n이미 구매한 아이템입니다.\n");
                                Console.Beep(); // 기본 삐 소리 (800Hz, 200ms) // 이게 아니야! 하는 경고음
                            }
                            else if (shop.Purchase(itemIndex))
                            {
                                Console.WriteLine("\n구매를 완료했습니다.\n");
                            }
                            else
                            {
                                Console.WriteLine("\nGold가 부족합니다.\n");
                                Console.Beep(); // 기본 삐 소리 (800Hz, 200ms) // 이게 아니야! 하는 경고음
                            }
                            Thread.Sleep(500);// 구매 관련 안내를 보여주기 위해 잠시 대기
                        }
                        else if (value_diff < 0)
                        {
                            WrongInfo();
                        }
                        else
                            shop.isPurchase = false; // 구매 >> 상점 보기만 하기


                        break; // 구매 씬 재시작
                    }
                    // 상점 기본 화면이고 1을 눌렀다면 구매 화면으로 전환
                    else if(enter == ConsoleKey.D1)
                    {
                        shop.isPurchase = true;
                        break; // 전환을 반영하기 위해 씬 재시작
                    }
                }

                // 입력한 키가 해당 씬에서 다음 씬으로 넘어갈 수 있는 키라면
                if (choiceToSceneNum.ContainsKey(enter))
                {
                    // 다음으로 이동할 씬 값을 저장하고, 해당 씬 종료
                    sceneNums nextScene;
                    if (choiceToSceneNum.TryGetValue(enter, out nextScene))
                    {
                        sceneNum = (int)nextScene;
                        isScenePlaying = false;
                    }
                }
                // 해당 키 입력이 유효하지 않으면 안내문 출력하고 다시 키 입력 대기로
                else
                {
                    WrongInfo();
                    break; // 씬 재시작
                }
            }
        }

        static void WrongInfo()
        {
            string wrongEnterInfo = "\n잘못된 입력입니다.\n";
            Console.WriteLine(wrongEnterInfo);
            Console.Beep(); // 기본 삐 소리 (800Hz, 200ms) // 이게 아니야! 하는 경고음
            Thread.Sleep(500); // 시간 대기
        }

    }
}
