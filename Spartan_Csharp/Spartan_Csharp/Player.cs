using System;

namespace Spartan_Csharp
{
    internal enum statusSort
    {
        LV, // 레벨
        CSG, // C# Grammer. 구현해야 할 목적에 대한 추진력
        DBG, // 디버깅 능력(방어력과 비슷)
        CAF, // 카페인 내성(커피를 마실수록 높아짐. 높은 경우 hp 회복량 감소, 낮은 경우 카페인 중독 디버프). 커피를 마시지 않은 턴마다 내성 감소
        HP_MAX, // 체력 최대치
    }

    // 플레이어 스테이터스
    internal class PlayerStatus
    {
        string name;
        internal string Name { get { return name; } set { name = value; } }

        int HP;
        internal int GetHP() => HP;
        internal void ChangeHP(int amount) => HP = Math.Clamp(HP + amount, 0, status[(int)statusSort.HP_MAX]);

        int money;
        internal int Money { get { return money; } set { money = value; } }

        // 스테이터스 가짓수에 맞게 배열 생성
        // 이후 스테이터스 추가/제거해도 그대로 적용
        int[] status = new int[Enum.GetValues(typeof(statusSort)).Length];

        internal PlayerStatus() // new Player로 객체를 생성할 때 호출하여 초기화
        {
            //name = _name;

            // 수업을 시작할 때의 성취도는 다들 다르기에 스텟은 랜덤
            Random random = new Random();
            status[(int)statusSort.CSG] = random.Next(0, 10); // C Sharp Grammer. C# 문법 능력
            status[(int)statusSort.DBG] = random.Next(0, 10); // 디버그력, 버그 대처력
            status[(int)statusSort.CAF] = random.Next(0, 10); // 카페인 내성
            status[(int)statusSort.HP_MAX] = random.Next(5, 15); // 체력 최대치

            status[(int)statusSort.LV] = 1; // 초기 레벨 = 1
            HP = status[(int)statusSort.HP_MAX]; // 현재 체력을 최대 체력으로
            money = 0; // 소지금 초기화
        }

        // 원하는 스테이터스 가져오기
        internal int GetStatus(statusSort _statusSort) => status[(int)_statusSort];

        // 원하는 스테이터스 변화
        internal void SetStatus(statusSort _statusSort, int amount) => status[(int)_statusSort] += amount;
    }
}
