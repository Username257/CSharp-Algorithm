namespace _09._DesignTechnique
{
    internal class Program
    {
        /******************************************************
		 * 알고리즘 설계기법 (Algorithm Design Technique)
		 * 
		 * 어떤 문제를 해결하는 과정에서 해당 문제의 답을 효과적으로 찾아가기 위한 전략과 접근 방식
		 * 문제를 풀 때 어떤 알고리즘 설계 기법을 쓰는지에 따라 효율성이 막대하게 차이
		 * 문제의 성질과 조건에 따라 알맞은 알고리즘 설계기법을 선택하여 사용
		 ******************************************************/

        //분할정복 - 하노이탑
        //하노이탑에서 가장 작은 해결할 수 잇는 단위는 1개
        //하나 움직인다(최소 단위)
        //n-1개의 움직임도 하나하나 움직이는 거니까

        public static void Move(int count, int start, int end)
        {
            if (count == 1) 
            {
                int node = stick[start].Pop();
                stick[end].Push(node);
                Console.WriteLine($"{start} 스틱에서 {end} 스틱으로 {node} 이동");
                return;
            }
            int other = 3 -  start - end; //?인덱스 가지고 뺄셈

            Move(count - 1, start, other); //n - 1을 중간으로 옮긴다
            Move(1, start, end); //n을 끝으로 옮긴다
            Move(count - 1, other, end); //n - 1을 끝으로 옮긴다
            //n개를 0에서 2로 옮겨야한다 
        }
        public static Stack<int>[] stick;

        static void Main(string[] args)
        {
            int nodeCount = 7;
            stick = new Stack<int>[3]; //스택으로 스틱 3개 만들기
            for (int i = 0; i < stick.Length; i++)
            {
                stick[i] = new Stack<int>();
            }

            for (int i = nodeCount; i > 0; i--)
            {
                stick[0].Push(i); // 0번 스틱에 5, 4, 3, 2, 1
            }

            Move(nodeCount, 0, 2);
        }

        //동적계획법 문제
        //연속 된 숫자의 합 중 가장 큰 숫자 구하기
        //2개끼리 더한 거 가지고 3개끼리 더하고 3개 더한걸로 4개 구하고 다 더한 거에서 가장 큰 값 찾으면 됨

        static void Main2()
        {
            int[,] result;
            /*
            result["시작위치", "끝위치"];
            result[0, 0] = 10;
            */

        }
    }
}