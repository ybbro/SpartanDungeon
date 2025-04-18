using System;
using System.Collections.Generic;

namespace Spartan_Csharp
{
    internal enum item
    {
        // 소모품
        energyDrink, // 그날 CSG 버프. 체력 깎임
        coffee, // 체력 회복 아이템.
        snack, // 체력 회복 아이템.
        // 공격력 장비
        grammerBook,
        spartaLecture1,
        spartaLecture2,
        spartaLecture3,
        spartaLecture4,
        spartaLecture5,
        // 방어력 장비
        blueLightFilter,
        spiritOfSparta,
        gpt,
        tutor
    }

    // 아이템 공통적인 부분만
    internal class Item
    {
        // 이름, 정보
        protected string name;
        protected string info;
        protected int price;

        internal string GetName { get { return name; } }
        internal string GetInfo { get { return info; } }

        internal int GetPrice { get { return price; } }
    }

    // 소비 아이템
    internal class Item_usable : Item
    {
        // 아이템 효과를 덮어쓸 수 있도록 Action으로 선언
        internal Action Ability;

        internal Item_usable(string _name, string _info, int _price, Action _ability)
        {
            name = _name;
            info = _info;
            price = _price;
            Ability += _ability;
        }

        // 아이템 사용 메시지 + 효과 설명
        internal void UseMessage()
        {
            Console.WriteLine("사용: {0} ({1})", name, info);
        }
    }

    // 사용 아이템들의 효과들의 실제 적용 메서드를 모아놓은 클래스
    internal class Usable_Abilities
    {
        internal void Ability_EnergyDrink()
        {
            SpartaDungeon.player.SetStatus(statusSort.HP_MAX, -1);
            SpartaDungeon.player.ChangeHP(30);
        }

        internal void Ability_Coffee()
        {
            int healAmount = 15 - SpartaDungeon.player.GetStatus(statusSort.CAF);
            SpartaDungeon.player.ChangeHP(healAmount);
        }

        internal void Ability_Snack()
        {
            SpartaDungeon.player.ChangeHP(5);
        }
    }

    internal class Item_equip : Item
    {
        statusSort statusSort;
        int statusAmount;

        internal Item_equip(string _name, string _info, int _price, statusSort _statusSort, int _amount)
        {
            name = _name;
            info = _info;
            price = _price;
            statusSort = _statusSort;
            statusAmount = _amount;
        }

        internal statusSort GetStatusSort { get { return statusSort; } }
        internal int GetStatusAmount { get { return statusAmount; } }
    }

    internal class Item_Dictionary
    {
        // 아이템 데이터 딕셔너리!
        // 아이템들의 부모 클래스 형태로 집어넣어서 아이템 모두 한번에 관리 가능
        Dictionary<item, Item> itemDictionary = new Dictionary<item, Item>
        {
            // 소비 아이템
            {item.energyDrink, new Item_usable("에너지드링크", "최대체력-1, 체력 20 회복", 150, SpartaDungeon.usable_Abilities.Ability_Coffee) },
            {item.coffee, new Item_usable("커피", "(15 - 카페인 내성 (CAF)) 만큼 체력 회복", 100, SpartaDungeon.usable_Abilities.Ability_Coffee) },
            {item.snack, new Item_usable("간식", "체력 5 회복, 칼로리는 코딩으로 날렸다!", 50, SpartaDungeon.usable_Abilities.Ability_Coffee) },

            //장비 아이템
            // CSG(C# 문법력). 공격력과 비슷
            {item.grammerBook, new Item_equip("C# 문법책", "무 겁 다. (라면 받침대)", 38000, statusSort.CSG, 1) },
            {item.spartaLecture1, new Item_equip("스파르타 문법 강의 챕터 1", "프로그래밍 고수가 될 거야!", 0, statusSort.CSG, 4) },
            {item.spartaLecture2, new Item_equip("스파르타 문법 강의 챕터 2", "조..건? 반복..반복..무한반복", 0, statusSort.CSG, 3) },
            {item.spartaLecture3, new Item_equip("스파르타 문법 강의 챕터 3", "튜터님 진도가 너무 빨라요", 0, statusSort.CSG, 2) },
            {item.spartaLecture4, new Item_equip("스파르타 문법 강의 챕터 4", "델리..델리만쥬 맛있어", 0, statusSort.CSG, 1) },
            {item.spartaLecture5, new Item_equip("스파르타 문법 강의 챕터 5", "#^!($&!!(*$%", 0, statusSort.CSG, 1) },

            // DBG(디버그력). 방어력과 비슷
            {item.blueLightFilter, new Item_equip("블루라이트 필터", "아마도 에러가 편안하게 보여요. 시각적으로만", 50, statusSort.DBG, 1) },
            {item.spiritOfSparta, new Item_equip("스파르타 정신", "디스! 이즈! 스파르타!!!", 0, statusSort.DBG, 2) },
            {item.gpt, new Item_equip("GPT", "이상한 질문을 해도 둥기둥기 해준다. 어둠의 튜터님", 28000, statusSort.DBG, 3) },
            {item.tutor, new Item_equip("튜터님들", "튜터님들이 계시면 너희 버그는 전멸이다", 0, statusSort.DBG, 10) },
        };

        internal Item CreateItem(item itemKey) // 아이템 데이터로부터 생성
        {
            Item itemToCreate;
            itemDictionary.TryGetValue(itemKey, out itemToCreate);
            return itemToCreate;
        }
    }
}
