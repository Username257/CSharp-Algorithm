namespace LinkedListAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList<string> linkedList = new LinkedList<string>();
            linkedList.AddLast("1번째 데이터");
            linkedList.AddLast("2번째 데이터");
            linkedList.AddLast("3번째 데이터");
            linkedList.AddLast("4번째 데이터");

            Console.WriteLine(linkedList.First.Item);
            Console.WriteLine(linkedList.Last.Item);

            if (linkedList.Find("8번째 데이터") != null)
                Console.WriteLine("데이터가 있습니다.");
            else
                Console.WriteLine("데이터가 없습니다.");
        }
    }
}