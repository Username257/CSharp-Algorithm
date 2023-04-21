namespace _05._Queue
{
    internal class Program
    {
        static void Test()
        {
            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < 5; i++) queue.Enqueue(i); // 0 1 2 3 4

            Console.WriteLine(queue.Peek()); // 최전방(보통 가로로 묘사하니까) : 0

            while (queue.Count > 0)
                Console.WriteLine(queue.Dequeue()); // 0 1 2 3 4
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}