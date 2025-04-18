using System.Collections.Generic;

namespace Spartan_Csharp
{
    internal class Shop
    {
        bool isInPurchaseScene = false; // 구매, 둘러보기 전환
        internal bool isPurchase
        {
            get { return isInPurchaseScene; }
            set { isInPurchaseScene = value; }
        }

        // 게임 플레이 도중 보통 상점에는 물품이 추가되거나 변경될 수 있기에 유동적인 리스트로 선언
        List<Item> salesStand;

        // 각 물품 재고 여부
        List<bool> isInStock;

        internal Shop()
        {
            Item_Dictionary item_Dictionary = SpartaDungeon.item_Dictionary;

            salesStand = new List<Item>
            {
                item_Dictionary.CreateItem(item.snack),
                item_Dictionary.CreateItem(item.coffee),
                item_Dictionary.CreateItem(item.energyDrink),

                item_Dictionary.CreateItem(item.grammerBook),
                item_Dictionary.CreateItem(item.spartaLecture1),
                item_Dictionary.CreateItem(item.spartaLecture2),
                item_Dictionary.CreateItem(item.spartaLecture3),
                item_Dictionary.CreateItem(item.spartaLecture4),
                item_Dictionary.CreateItem(item.spartaLecture5),

                item_Dictionary.CreateItem(item.blueLightFilter),
                item_Dictionary.CreateItem(item.spiritOfSparta),
                item_Dictionary.CreateItem(item.gpt),
                item_Dictionary.CreateItem(item.tutor),
            };

            // 모든 품목에 대해 재고가 있는지 여부만 저장
            // 재고 물건 갯수가 있다면 bool을 int로 바꾸면 바로 적용 가능한 구조
            isInStock = new List<bool>();
            for (int i = 0; i < salesStand.Count; i++)
            {
                isInStock.Add(true);
            }
        }

        internal string PrintShop()
        {
            string shopText = "";
            string soldOut = "구매완료";

            // 이전의 인벤토리, 장비와 비슷하지만 가격도 표시!
            for (int i = 0; i < salesStand.Count; i++)
            {
                shopText += " - ";
                if (isInPurchaseScene)
                {
                    shopText += $"{(i + 1)} ";
                }
                // 해당 품목의 재고가 있다면 가격 / 없다면 구매완료 알림
                string priceOrSoldOut = isInStock[i] ? (salesStand[i].GetPrice.ToString() + " G") : soldOut;

                if (salesStand[i] is Item_equip item_Equip) // >> 장비인지?
                {
                    shopText += $"{item_Equip.GetName} ㅣ {item_Equip.GetStatusSort} +{item_Equip.GetStatusAmount} ㅣ {item_Equip.GetInfo} | {priceOrSoldOut}\n";
                }
                else // >> 소모품, 그 외인지? >> 스테이터스가 없기에 이름, 설명만
                    shopText += $" - {salesStand[i].GetName} ㅣ {salesStand[i].GetInfo} | {priceOrSoldOut}\n";
            }
            return shopText;
        }

        internal bool StockCheck(int _index)
        {
            if (_index >= isInStock.Count || _index < 0)
                return false;

            return isInStock[_index];
        }

        internal bool Purchase(int _index)
        {
            // 가격이 부족하면 구매 실패 알림
            if (salesStand[_index].GetPrice > SpartaDungeon.player.Money)
                return false;
            else
            {
                SpartaDungeon.player.Money -= salesStand[_index].GetPrice; // 값을 지불하고
                SpartaDungeon.inventory.Obtain(salesStand[_index]); // 인벤토리에 추가
                isInStock[_index] = false; // 해당 품목 팔림
                return true;
            }
        }
    }
}
