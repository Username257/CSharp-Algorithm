namespace _04._Stack
{
    internal class Program
    {
        /*
         *  스택
         *  선입후출, 후입선출 방식의 자료구조
         *  가장 최신 입력된 순서대로 처리해야 하는 상황에 이용
         */

        public void Test()
        {
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < 5; i++) stack.Push(i); // 0 1 2 3 4

            Console.WriteLine(stack.Peek()); //최상단 확인 : 4

            while (stack.Count > 0)
            {
                Console.WriteLine(stack.Pop()); // 4 3 2 1 0
            }
        }
        static void Main(string[] args)
        {
            Test();
        }
    }
}