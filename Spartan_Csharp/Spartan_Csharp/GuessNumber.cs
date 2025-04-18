using System;
public class GuessNumber
{
    void Main(string[] args)
    {
        bool isWin = false;

        Random random = new Random();
        int min = 1,
            max = 101,
            answer = random.Next(min, max),
            tryCount = 10, 
            enter;

        Console.Clear(); // 화면을 지우기

        while (true)
        {
            Console.Write("남은 횟수: {0}\n1~100 사이의 숫자 중 하나를 입력하세요 : ", tryCount);
            string enterdStr = Console.ReadLine();
            if(int.TryParse(enterdStr, out enter))
            {
                if (enter < min || enter >= max) // 정수는 맞는데 값이 범위 밖인 경우
                {
                    Console.WriteLine("잘못된 입력이에요.\n");
                }
                else
                {
                    --tryCount; // 게임 승리 메시지로 인해 먼저 계산
                    int diff = answer - enter;
                    if (diff > 0)
                    {
                        if(diff > 40)
                            Console.WriteLine("더! 더! 더! 높은 수에요.\n");
                        else if (diff > 20)
                            Console.WriteLine("더! 더! 높은 수에요.\n");
                        else
                            Console.WriteLine("더! 높은 수에요.\n");
                    }
                    else if (diff < 0)
                    {
                        if (diff < -40)
                            Console.WriteLine("더! 더! 더! 낮은 수에요.\n");
                        else if (diff < -20)
                            Console.WriteLine("더! 더! 낮은 수에요.\n");
                        else
                            Console.WriteLine("더! 낮은 수에요.\n");
                    }
                    else
                    {
                        isWin = true; // 게임 승리
                        break; // 루프 종료
                    }

                    if(tryCount == 0) // 횟수 내 맞추지 못했다면
                        break; // 게임 패배로 루프 종료
                }
                
            }
            else
                Console.WriteLine("잘못된 입력이에요.\n");
        }
        if(isWin)
            Console.WriteLine("정답: {0}! {1}번만에 맞췄어요!\n", answer, 10-tryCount);
        else
            Console.WriteLine("횟수 초과! 제가 이겼어요!\n");
        Console.ReadKey(); // 프로그램이 바로 종료되는 것을 방지. 키 하나만 누르면 종료
    }
}
