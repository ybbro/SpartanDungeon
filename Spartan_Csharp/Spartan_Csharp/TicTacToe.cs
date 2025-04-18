using System;

namespace Spartan_Csharp
{
    public class TicTacToe
    {
        static int[,] table = new int[3, 3]; // 보드에 어떤 돌이 놓였는지 기억하기 위한 배열. 0: 빈칸, 1:1P >> o돌, -1:2P >> x돌
        static int[] cursorPos = new int[] { 0, 0 }; // 커서 위치
        static int spaceLeft = 9;
        static bool is1P = true, isGamePlaying = true;
        //static void Main(string[] args)
        //{
        //    Game();
        //}

        static void Game()
        {
            Console.CursorVisible = false;

            bool isKeyEnterDelay;

            while (isGamePlaying)
            {
                DrawBoard3x3();
                Console.SetCursorPosition(spaceLeft + 2 + 4 * cursorPos[0], 2 + 2 * cursorPos[1]); // 틱택토 0,0 칸으로 커서 이동

                if (is1P)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // 1p 글자 색 빨간색

                    if (table[cursorPos[1], cursorPos[0]] != 0) // 이미 말이 배치되어 있다면 말을 배치할 수 없다는 커서 표시
                        Console.Write("■");
                    else
                        Console.Write("o"); // 커서의 위치를 표시할 문자 출력
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // 2p 글자 색 노란색
                    if (table[cursorPos[1], cursorPos[0]] != 0) // 이미 말이 배치되어 있다면 말을 배치할 수 없다는 커서 표시
                        Console.Write("■");
                    else
                        Console.Write("x"); // 커서의 위치를 표시할 문자 출력
                }

                Console.ResetColor(); // 다시 글자 색상 초기화

                isKeyEnterDelay = true;
                while (isKeyEnterDelay)
                {
                    ConsoleKeyInfo enter = Console.ReadKey();
                    switch (enter.Key)
                    {
                        case ConsoleKey.Enter:
                        case ConsoleKey.Spacebar:
                            if (table[cursorPos[1], cursorPos[0]] != 0) // 이미 말이 배치되어 있다면
                            {
                                Console.Beep(); // 기본 삐 소리 (800Hz, 200ms)
                            }
                            else
                            {
                                // 1p, 2p의 턴에 맞게 말 배치
                                if (is1P)
                                    table[cursorPos[1], cursorPos[0]] = 1;
                                else
                                    table[cursorPos[1], cursorPos[0]] = -1;

                                // 게임이 끝났는지 체크
                                Check();

                                is1P = !is1P; // 턴 전환
                            }
                            isKeyEnterDelay = false;
                            break;


                        // 방향키 입력에 따라 커서 이동
                        case ConsoleKey.LeftArrow:
                            cursorPos[0] = Math.Clamp(--cursorPos[0], 0, 2);
                            isKeyEnterDelay = false;
                            break;
                        case ConsoleKey.UpArrow:
                            cursorPos[1] = Math.Clamp(--cursorPos[1], 0, 2);
                            isKeyEnterDelay = false;
                            break;
                        case ConsoleKey.RightArrow:
                            cursorPos[0] = Math.Clamp(++cursorPos[0], 0, 2);
                            isKeyEnterDelay = false;
                            break;
                        case ConsoleKey.DownArrow:
                            cursorPos[1] = Math.Clamp(++cursorPos[1], 0, 2);
                            isKeyEnterDelay = false;
                            break;
                        default:
                            break;
                    }
                }
            }

        }

        static void DrawBoard3x3()
        {
            Console.Clear(); // 화면 지우기
            ShowPlayer(is1P);
            // 보드와 놓여진 말 그리기
            DrawUpperLine3x3();
            DrawRow3x3(0);
            DrawUpperLine3x3();
            DrawRow3x3(1);
            DrawUpperLine3x3();
            DrawRow3x3(2);
            DrawUpperLine3x3();
        }

        static void ShowPlayer(bool is1P)
        {
            if (is1P)
                Console.WriteLine("1P 턴");
            else
                Console.WriteLine("2P 턴");
        }
        static void DrawUpperLine3x3() // 위아래 칸을 구분하는 줄 그리기
        {
            Console.Write(new string(' ', spaceLeft)); // 왼쪽으로부터 이격
            Console.WriteLine("-------------");
        }

        static void DrawRow3x3(int row) // 열(Row) 하나를 그리기
        {
            char[] column = new char[3];
            for (int i = 0; i < column.Length; i++)
            {
                if (table[row, i] == 0)
                    column[i] = ' ';
                else if (table[row, i] == 1)
                    column[i] = 'o';
                else if (table[row, i] == -1)
                    column[i] = 'x';
            }
            Console.Write(new string(' ', spaceLeft)); // 왼쪽으로부터 이격
            Console.WriteLine("| {0} | {1} | {2} |", column[0], column[1], column[2]);
        }

        static void Check()
        {
            int value;
            if (is1P)
                value = 1;
            else
                value = -1;

            if ((table[0, 0] == value && table[0, 1] == value && table[0, 2] == value) ||
                (table[1, 0] == value && table[1, 1] == value && table[1, 2] == value) ||
                (table[2, 0] == value && table[2, 1] == value && table[2, 2] == value) ||

                (table[0, 0] == value && table[1, 0] == value && table[2, 0] == value) ||
                (table[0, 1] == value && table[1, 1] == value && table[2, 1] == value) ||
                (table[0, 2] == value && table[1, 2] == value && table[2, 2] == value) ||

                (table[0, 0] == value && table[1, 1] == value && table[2, 2] == value) ||
                (table[2, 0] == value && table[1, 1] == value && table[0, 2] == value))
            {
                isGamePlaying = false;

                Console.SetCursorPosition(13, 9);

                if (is1P)
                    Console.WriteLine("1P WIN");
                else
                    Console.WriteLine("2P WIN");

                Console.ReadKey();
            }
        }
    }
}
