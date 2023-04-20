namespace IteratorAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            IEnumerator<string> iter = list.GetEnumerator();

            for (int i = 0; i < 5; i++)
            {
                list.Add($"{i}번째 데이터");
            }

            Console.WriteLine(iter.Current);
            while (iter.MoveNext())
            {
                Console.WriteLine(iter.Current);

            }
            //예외처리 안 됨 -> foreach문 돌리고 있을 때 요소들이 삭제가 되면 안 된다
            //컬렉션이 수정됐다. 열거 작업 진행 안 한다
            //하하하하 왜 일까
            Console.WriteLine(iter.Current);
        }
    }
}