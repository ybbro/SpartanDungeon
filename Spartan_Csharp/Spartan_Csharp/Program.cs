using System;

namespace Spartan_Csharp
{
    class Program
    {
        //static void Main(string[] args)
        //{
            //EnterName_AgeAndShow();

            //FourBasicOperation();

            //CelsiusToFahrenheit();

            // BMI_Calculator();

            //Console.WriteLine(OperationOrderTest());
        //}
        static int OperationOrderTest()
        {
            return 10 + 2 / 2;
        }
        void Study()
        {
            // 1. 콘솔창에 출력
            Console.Write("Hello "); // 문자열 출력. 줄은 바꾸지 않고 커서는 문자열 뒤에 위치
            Console.WriteLine("World"); // 문자열 출력 후 줄바꿈

            // 2. 콘솔창에서 입력 받기
            //string input = Console.ReadLine(); // 문자열을 입력하고 엔터


            // 3. 자동 완성
            // 1) 탭 키를 눌러서 완성
            // 2) for까지만 쓰고 탭 2번
            // 3) 자동완성이 나타나지 않는다면 컨트롤+스페이스


            // 4. 자료형
            // 1) 정수형
            // sbyte, short, int, long : 왼쪽부터 1, 2, 4, 8바이트. 데이터 크기가 커지면서 표현 가능한 범위가 넓어짐
            // byte, ushort, uint, ulong : u는 unsigned(부호가 없는)의 약자. 0부터 2^(8*바이트 수)-1 까지 표현 가능. 음수를 사용하지 않을 경우 더 넓은 범위를 적은 데이터로 표현하기 위해 사용

            // 2) 실수형
            // float, double, decimal : 왼쪽부터 4, 8, 16바이트
            // decimal은 값의 정밀도를 높이고 싶을 때 사용. 예를 들면 정확히 0.1을 표현. 하지만 성능을 많이 먹음

            // 3) 그 외
            // char: 문자, string: 문자열, bool: 참/거짓


            // 4) 리터럴
            // (1) 정수 : 10(int), 0x10(16진수 int), 0b10(2진수 int), 10L(long), 10UL(ulong)
            // (2) 부동소수점 : 3.14(double), 3.14f(float), 3.14m(decimal)
            // (3) 문자형 : 'A' (char), '\n' (줄바꿈 문자), '\u0022' (유니코드 문자)
            // (4) 문자열 : "Hello World!" (string)


            // 5. 변수
            // 데이터를 저장/수정/사용하기 위해 할당받은 공간
            // 변수 이름 : 첫 글자는 영어, _ (언더바) 사용 가능. 둘째 글자부터 숫자 사용 가능, 키워드 명칭과 동일하게 선언 불가. 특수문자 사용 불가
            int num1, num2, num3 = 3; // 동시 선언, 선언과 동시에 num3에만 3 할당
            num1 = num2 = num3 = 5; // 동시에 같은 값 할당

            // 변수 이름짓기
            // PascalCase : 클래스, 메서드, 프로퍼티 이름 등에 사용. 단어 첫글자 대문자, 이후 단어의 첫 글자도 대문자 ex) ClassName, SpartanClass 등
            // camelCase : 변수, 매개변수, 로컬변수 이름 등에 사용. 단어 첫글자 소문자, 이후 단어의 첫 글자는 대문자 ex) spartanVariable
            // 대문자 약어 : ID, HTTP, FTP 등 익숙한 줄임말의 경우

            // 6. 형 변환
            // 1) 명시적 형 변환
            int num4 = 10;
            long num5;
            num5 = (long)num4; // 변환할 (자료형)을 앞에 붙여서

            // 2) 암시적 형 변환
            num5 = num4; // 작은 데이터 타입에서 더 큰 타입으로 대입할 때는 따로 변환을 붙여주지 않더라도 암시적으로 형변환
            float num_float = 1; // 리터럴 값도 더 큰 데이터 타입에 넣을 때는 암시적 변환
            num_float = 1.1f;
            float num_float2 = num_float + num4; // 정수 + 실수 = 실수형

            // 3) var : 초기화하는 값의 자료형에 따라 변수의 자료형을 자동으로
            var varNumber = 1;
            var varName = "Name";
            var varFloat = 1.2f;


            // 7. 각종 연산자 및 우선순위
            // 동일한 우선순위가 여러개면 좌에서 우 순서로
            // 1순위: 괄호 내부, 
            // 2순위: 단항 연산자(++, --, ! 등)
            // 3순위 : 산술 곱 연산자(*, /, %), 강의에서는 산술 연산자를 모두 동일한 순위에 놓았으나 실제로는 곱, 나눗셈이 우선순위가 높다.
            // 4순위 : 산술 증감 연산자(+, -) 
            // 5순위 : 시프트 연산자(<<, >>). 유니티에서는 자주 쓸 일이 없기는 한데 전에 프로그래머스 문제에 사용해 본 결과 실행 시간이 함께 스터디를 하던 다른 분들에 비해 짧게 나왔었다. 성능을 위해 사용을 고려
            // 6순위 : 논리 연산자(&&, ||)
            // 7순위 : 할당 연산자(=, +=, -=, *=, /= 등)


            // 8. string 문자열 관련 메서드
            string testString = "Hello, World!"; // 리터럴 문자열을 이용한 초기화
            string newTest = new string('H', 5); // 문자 'H' 5개로 이루어진 문자열

            // 1) 연결
            string hello = "Hello", world = "World";
            string helloWorld = hello + " " + world;

            // 2) 분할 : .Split(char a)의 매개변수 문자를 기준으로 쪼개어 문자열 배열로 나눔
            string[] splitString = testString.Split(',');

            // 3) 검색 : .IndexOf(string a)의 매개변수 문자열과 똑같은 문자열을 찾아서 그 첫 글자의 인덱스를 반환
            int index = testString.IndexOf("World");

            // 4) 대체 : .Replace(string a, string b). a 문자열들을 b 문자열로 대체
            string replacedStr = testString.Replace("World", "Universe");

            // 5) 변환
            // (1) 문자열 >> 숫자
            string numString = "123";
            int stringToNUm = int.Parse(numString);

            // (2)  숫자 >> 문자열
            int num = 123;
            string str = num.ToString();

            // 6) 비교
            string str1 = "Hello", str2 = "World";
            bool isEqual = str1 == str2; // false 둘은 다른 단어를 담은 문자열
            int compare = string.Compare(str1, str2); // compare < 0 이면 st1이 작음, compare > 0 이면 str1이 큼. compare =0 이면 동일. 대소 비교는 사전식으로 비교. 대소문자 구분. 아스키 코드표에 따라 소문자가 대문자보다 큼

            // 7) 문자열 포멧팅
            string name = "John";
            int age = 30;
            string message = string.Format("My Name is {0} and I'm {1} years old.", name, age);
            // 문자열 보간으로도 똑같은 결과를 얻을 수 있다.
            message = $"My name is {name} and I'm {age} years old.";
        }

        // 이름, 나이를 입력하면 그걸 다시 확인차 보여주는 메서드
        static void EnterName_AgeAndShow()
        {
            Console.Write("이름을 입력하세요: ");
            string name = Console.ReadLine();

            Console.Write("나이를 입력하세요: ");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("어서오세요. {0} 님 ({1}세)", name, age);
        }

        // 수를 둘 입력하면 사칙연산을 수행하여 그 값을 보여주는 메서드
        static void FourBasicOperation()
        {
            Console.Write("빈칸(스페이스)로 구분되는 두 정수를 입력하세요 :");
            string[] input = Console.ReadLine().Split(' ');
            int[] num = new int[2] { int.Parse(input[0]), int.Parse(input[1]) };
            Console.WriteLine($"{num[0]} + {num[1]} = {num[0] + num[1]}");
            Console.WriteLine("{0} - {1} = {2}", num[0], num[1], num[0] - num[1]);
            Console.WriteLine(num[0] + " x " + num[1] + " = "  + (num[0] * num[1]));
            Console.WriteLine($"{num[0]} / {num[1]} = {num[0] / num[1]}");
            Console.WriteLine($"{num[0]} % {num[1]} = {num[0] % num[1]}");
        }

        // 섭씨 온도를 입력하는 화씨 온도로 바꿔주는 메서드
        static void CelsiusToFahrenheit()
        {
            Console.Write("섭씨 온도를 입력해 주세요 :");
            // 입력받아 값의 정확도를 올리기 위해 decimal로 변환
            decimal celsius = Convert.ToDecimal(Console.ReadLine());

            // y (Fahrenheit degree) = x (Celsius degree) * 9 / 5 + 32
            Console.Write("화씨 온도로 변환하면 {0}도 입니다.\n", celsius * 9 / 5 + 32);
        }

        // 키, 몸무게를 입력하면 BMI를 계산해주는 메서드
        static void BMI_Calculator()
        {
            Console.WriteLine("BMI는 근육량, 유전, 개인적 차이를 반영하지 못합니다.\n참고용으로만 보길 바랍니다.\n");

            Console.Write("당신의 키를 입력해주세요 (cm) : ");
            decimal tall = Convert.ToDecimal(Console.ReadLine());
            tall /= 100; // cm를 m로 단위 변환
            Console.WriteLine("");

            Console.Write("당신의 몸무게를 입력해주세요 (kg) : ");
            decimal weight = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("");

            decimal BMI = weight / tall / tall;
            // BMI는 무게(kg)를 키(m)의 제곱으로 나눈 값입니다.
            Console.WriteLine("BMI 지수 : " + BMI.ToString("N2"));
            Console.Write("비만도 결과 :");
            if (BMI < 18.5m)
            {
                Console.WriteLine("저체중\n");
            }
            else if(BMI < 23m)
            {
                Console.WriteLine("정상\n");
            }
            else if (BMI < 25m)
            {
                Console.WriteLine("과체중\n");
            }
            else
            {
                Console.WriteLine("비만\n");
            }
        }


    }
}
