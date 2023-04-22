namespace _03._Iterator
{
    internal class Program
    {
        /* 
         * 반복기 (Enumerator(Iterator))
         * 
         * 자료구조에 저장되어 있는 요소들을 순회하는 인터페이스
         * 구조랑 상관없이 처음부터 끝까지 본다
         */
        static void Main2(string[] args)
        {   
            //대부분의 자료구조가 반복기를 지원함
            //반복기를 이용한 기능을 구현할 경우, 그 기능의 대부분의 자료구조를 호환할 수 있음
            List<int> list = new List<int>();
            LinkedList<int> linkedList = new LinkedList<int>();

            //자료구조마다 다루는 방법이 다르다

            for(int i = 1; i <= 5; i++)
            {
                list.Add(i);
                linkedList.AddLast(i);
            }

            for (int i = 0; i < list.Count; i++)
                Console.WriteLine(list[i]);

            LinkedListNode<int> node = linkedList.First;

            while(node != null)
            {
                Console.WriteLine(node.Item);
                node = node.Next;
            }

            //반복기를 이용한 순회
            //foreach 반복문은 데이터집합의 반복기를 통해서 단계별로 반복
            //즉, 반복기가 있다면 foreach 반복문으로 순회 가능

            //인터페이스로 IEnumerable을 지원함
            //foreach 안에 있는 거 하나하나 다 꺼내본다 -> 여러 기능에 대해서 다 호환
            foreach (int i in list) { } 
            foreach (int i in linkedList) { }


            //반복기 직접조작
            List<string> strings = new List<string>();
            for (int i = 0; i < 5; i++) strings.Add($"{i}데이터");

            IEnumerator<string> iter = strings.GetEnumerator(); //반복가능한 자료구조에 반복자를 가져와라
            iter.MoveNext();
            Console.WriteLine(iter.Current); // 0데이터
            iter.MoveNext();
            Console.WriteLine(iter.Current); // 1데이터

            iter.Reset(); //반복기가 처음걸로 돌아감
            //반복기의 기능 : 현재값 알려주는 애, 다음으로 갈 수 있는 애, 처음으로 돌아가는 애
            //다음으로 가는 건 bool임
            while (iter.MoveNext())
            {
                Console.WriteLine(iter.Current);
            }
            iter.Dispose();
        }

        public void FindInt(IEnumerable<int> container, int value)
        {
            IEnumerator<int> iter = container.GetEnumerator();
            iter.Reset();
            while(iter.MoveNext())
            {
                if(iter.Current == value)
                {
                    //찾음
                }
            }
            iter.Dispose();
            //못찾음
        }
        
        IEnumerable<int> IterFunc()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            for(int i = 1; i <= 5; i++) list.Add(i);
        }

    }
}