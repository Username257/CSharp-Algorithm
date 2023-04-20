using System.Collections.Generic; //자료구조 들고 있는 애
using System.ComponentModel;

namespace _01._List
{
    internal class Program
    {
        /*
         * 배열
         * 
         * 연속적인 메모리상에 동일한 타입의 요소를 일렬로 저장하는 자료구조
         * 초기화떄 정한 크기가 소멸까지 유지됨
         * 배열의 요소는 인덱스를 사용 해 직접적으로 액세스 가능
         */

        // 배열의 사용
        void Arr()
        {
            int[] intArr = new int[100];

            intArr[0] = 10;
            int value = intArr[0];
        }

        // 배열은 연속적으로 공간을 둔다 - 0번째 방을 알면 3번째 방을 알기 쉽다
        // int 배열에서 0번째 다음 방은 4바이트 뒤에 있을 거니까 100번째라 치면 104번째에 있겠구나
        // 스택에 변수 하나를 만들고 그걸로 연속된 데이터에 접근

        // <배열의 시간복잡도>
        // 접근		탐색
        // O(1)		O(N)

        // int 배열 20번째 자료 접근 : 20번째 자료의 주소 = 배열의 주소 + int 자료형의 크기 * 19
        // 100번째면 배열의 주소 + int 자료형의 크기 * 99 더하기, 곱하기 2번이면 끝

        // 데이터가 n개 있을 때 탐색
        public int Find(int[] intArr, int data)
        {
            for (int i = 0; i < intArr.Length; i++)
            {
                if (intArr[i] == data)
                    return i;
            }
            return -1;
        }
        //선형 탐색을 한다 n번방에 어떤 데이터가 있을 지는 모르는 거니까?
        //배열 복사는 새로운 인스턴스를 만드는 거다


        /*
         * 리스트 || 동적 배열 Dynamic Array
         * 
         * 런타임 중 크기를 확장할 수 있는 배열기반의 자료구조
         * 배열요소의 갯수를 특정할 수 없는 경우사용
         */

        //리스트의 사용
        void List()
        {
            List<string> list = new List<string>();

            // 배열 요소 삽입
            list.Add("0번 데이터");
            list.Add("1번 데이터");
            list.Add("2번 데이터");

            // 배열 요소 삭제
            list.Remove("1번 데이터");
            //중간께 사라지면 당겨옴

            // 배열 요소 접근
            list[0] = "데이터0";
            string value = list[0];

            // 배열 요소 탐색
            string? findValue = list.Find(x => x.Contains('2'));
            int findIndex = list.FindIndex(x => x.Contains('0'));
           
        }
        //일단 크게 잡고 Add할때마다 카운트를 올림
        //삭제는 그 자리 안 쓴다
        //데이터가 3개만 있어도 99번째 위치 특정 가능이지만 카운트를 넘어서 접근할려하면 예외처리
        //그래서 list.Count임
        //여유공간(허용량)은 Capacity 크기를 조정하지 않고 가질 수 있는 양
        //배열 크게 만드는 거랑 똑같은데 다만 Add랑 Remove할 수 있으니까

        // <List의 시간복잡도>
        // 접근		탐색		삽입		삭제
        // O(1)		O(n)	O(n)	O(n)
        //얘도 선형적으로 찾는다

        //ArrayList 리스트 명 = new ArrayList(); 는 여러 자료형 저장 가능 <- 이 경우 기본 저장 용량 10으로 되고 괄호 안에 숫자 적으면 그 만큼 할당
        //근데 이럴 경우 0번째에 string이 있는 지 없는 지 확신 불가 -> 그래서 자료형 특정하자
        //그리고 서로 다른 데이터를 가져올 때 박싱, 언박싱이 발생함

        static void Main(string[] args)
        {
            DataStructure.List<string> list = new DataStructure.List<string>();

            list.Add("1번 데이터");
            list.Add("2번 데이터");
            list.Add("3번 데이터");

            list.Remove("2번 데이터");

            list[0] = "첫 번째 데이터";
            string str = list[0];

            for (int i = 0; i < list.Count; i++)
                Console.WriteLine(list[i]);

            string? findValue = list.Find(x => x.Contains('4'));
            bool a = str.Contains('4');
            int findIndex = list.FindIndex(x => x.Contains('1'));
        }
    }
}