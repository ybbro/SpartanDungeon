using System;

public class Class1
{
	static void Main()
    {
		solution(new int[5] { 1, 2, 3, 4, 5 });

	}

	public int[] solution(int[] numbers)
	{
		int[] answer = new int[] { };
		answer = numbers * 2;
		return answer;
	}

	public Class1()
	{
		int potionCount = 5;
		string potionName = "HP 포션";

		if (potionCount > 0)
		{
			Console.WriteLine($"보유한 {potionName}의 수량: {potionCount}");
		}
		else
			Console.WriteLine($"{potionName}이 없습니다.");

		// else if 구문은 이전에 BMI 계산에서 다뤘기에 넘어갑니다.
	}
}
