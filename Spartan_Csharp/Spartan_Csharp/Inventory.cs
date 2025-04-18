using System.Collections.Generic;

namespace Spartan_Csharp
{
    internal class Inventory
    {
        // 인벤토리의 아이템 종류는 플레이 도중 변할 수 있게 하기 위해 리스트!
        List<Item> items;
        // items의 리스트 아이템 번호 중 장착중인 아이템의 인덱스들만 모아놓음 
        // 중복 요소는 들어가지 않는 해시세트 + 자동 오름차순 정렬
        SortedSet<int> equipIndex;
        // SortedList<T> : 자동으로 정렬하는 리스트
        // SortedDictionary<T> : 자동으로 정렬하는 딕셔너리
        // 리스트는 값, 딕셔너리는 Key값을 기준으로 오름차순(기본) 정렬

        // 내림차순은 비교자의 람다식 a,b 순서를 바꿔주어 오름차순의 역순으로 정렬
        // 예시) var descendingSet = new SortedSet<int>(Comparer<int>.Create((a, b) => b.CompareTo(a)))

        internal Inventory()
        {
            items = new List<Item>(); // Inventory 객체 생성 때 인벤토리 아이템 리스트 초기화
            equipIndex = new SortedSet<int>(); // Inventory 객체 생성 때 장비 목록 초기화

            InvenTest(); // 테스트를 위해 생성 후 아이템 추가, 장비 구문 !!!
        }

        internal void InvenTest()
        {
            Obtain(item.grammerBook);
            Obtain(item.blueLightFilter);
            EquipOrUnequip(0); // 인벤토리에 0번. 제일 처음 들어간 문법책을 장착
        }

        internal void Obtain(item item)
        {
            items.Add(SpartaDungeon.item_Dictionary.CreateItem(item));
        }
        internal void Obtain(Item item)
        {
            items.Add(item);
        }

        // 아이템 장착/해제
        internal void EquipOrUnequip(int itemIndex)
        {
            if (itemIndex >= items.Count || itemIndex < 0)
                return;

            if (items[itemIndex] is Item_equip equipment)
            {
                if (equipIndex.Contains(itemIndex))
                {
                    equipIndex.Remove(itemIndex); // 장비 품목에서 제외
                    SpartaDungeon.player.SetStatus(equipment.GetStatusSort, -equipment.GetStatusAmount); // 제외한 장비품의 스텟 빼기
                }
                else
                {
                    equipIndex.Add(itemIndex); // 장비 품목에 더하기
                    SpartaDungeon.player.SetStatus(equipment.GetStatusSort, equipment.GetStatusAmount); // 장착한 장비품의 스텟 더하기
                }
            }

        }

        // 소비 아이템 사용
        internal void Use(Item_usable usableItem)
        {
            usableItem.UseMessage(); // 사용 메세지로 알림
            usableItem.Ability(); // 아이템 효과를 발동하고
            items.Remove(usableItem); // 인벤토리에서 빼기
        }

        // 인벤토리 창에서 인벤토리 정보를 읽어오기 위해 쓰는 메서드
        internal string ItemNames()
        {
            string ItemHave = "";

            for (int i = 0; i < items.Count; i++)
            {
                // 타입 검사와 캐스팅을 동시에! 
                if (items[i] is Item_equip item_Equip) // >> 장비인지?
                {
                    ItemHave += " - ";
                    if (equipIndex.Contains(i)) // 장착 중인 장비라면, 장착 중임을 표시
                        ItemHave += "[E] ";
                    ItemHave += $"{item_Equip.GetName} ㅣ {item_Equip.GetStatusSort} +{item_Equip.GetStatusAmount} ㅣ {item_Equip.GetInfo}\n";
                }
                else // >> 소모품, 그 외인지? >> 스테이터스가 없기에 이름, 설명만
                    ItemHave += $" - {items[i].GetName} ㅣ {items[i].GetInfo}\n";
            }

            return ItemHave;
        }

        // 장착 창에서 인벤토리+장착 정보를 읽어오기 위해 쓰는 메서드
        internal string ItemEquip()
        {
            string equipItems = "";

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] is Item_equip item_Equip) // >> 장비인지?
                {
                    equipItems += $" - {(i + 1)} ";
                    if (equipIndex.Contains(i)) // 장착 중인 장비라면, 장착 중임을 표시
                        equipItems += "[E] ";
                    equipItems += $"{item_Equip.GetName} ㅣ {item_Equip.GetStatusSort} +{item_Equip.GetStatusAmount} ㅣ {item_Equip.GetInfo}\n";
                }
                else // >> 소모품, 그 외인지? >> 스테이터스가 없기에 이름, 설명만
                    equipItems += $" - {(i + 1)} {items[i].GetName} ㅣ {items[i].GetInfo}\n";
            }

            return equipItems;
        }

        // 인벤토리 내 아이템 갯수 반환
        internal int ItemCount() => items.Count;
    }
}
