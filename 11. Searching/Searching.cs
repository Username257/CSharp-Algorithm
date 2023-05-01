using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11._Searching
{
    internal class Searching
    {
        // 순차 탐색
        public static int SequentialSearch<T>(in IList<T> list, in T item) where T : IEquatable<T>
        { 
            //in은 입력 전용
            for(int i = 0; i < list.Count; i++)
            {
                if (item.Equals(list[i]))
                    return i;
            }
            return -1;
        } //자료가 정렬이 안 되어있어도 탐색 가능

        // 이진 탐색
        public static int BinarySearch<T>(in IList<T> list, in T item) where T : IComparable<T>
        {
            int low = 0;
            int high = list.Count - 1;

            while(low <= high)
            {
                int middle = (low + high) / 2; //나누기 연산은 느리다. 곱셈, 플러스는 빠르고 빼기는 더하기로 처리 *0.5f이 차라리 빠름
                //비트연산자가 제일 빠르다 >> 1, << 1
                int compare = list[middle].CompareTo(item);

                if (compare < 0)
                    low = middle + 1;
                else if (compare > 0)
                    high = middle - 1;
                else
                    return middle;
            }
            return -1;
        } //정렬이 되어있어야 탐색 가능
        //이진 탐색트리는 논외?

        // DFS BFS (구현방법, 차이점 알아놓기)

        // <깊이 우선 탐색> Depth First Search
        // 그래프의 분기를 만났을 때 최대한 깊이 내려간 뒤,
        // 더이상 깊이 갈 곳이 없을 경우 다음 분기를 탐색
        // 백트래킹(다 해보고 안 되면 되돌아옴) 분할정복
        public static void DFS(bool[,] graph, int start, out bool[] visited, out int[] parents)
        {
            visited = new bool[graph.GetLength(0)];
            parents = new int[graph.GetLength(0)];

            for (int i = 0; i < graph.GetLength(0); i++)
            {
                visited[i] = false;
                parents[i] = -1;
            }
            SearchNode(graph, start, visited, parents);
        }
        private static void SearchNode(bool[,] graph, int start, bool[] visited, int[] parents)
        {
            visited[start] = true;
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                if (graph[start, i] && //연결 되어있는 정점이며,
                     !visited[i])      //방문한 적 없는 정렬
                {
                    parents[i] = start;
                    SearchNode(graph, i, visited, parents);
                }
            }
        }

        // <너비 우선 탐색> Breadth First Search
        // 그래프의 분기를 만났을 때 모든 분기를 하나씩 저장하고,
        // 저장한 분기를 한 번씩 거치면서 탐색
        // 균등하게 한 칸씩 다 가 봄
        // 동적 계획법
        public static void BFS(bool[,] graph, int start, out bool[] visited, int[] parents)
        {
            visited = new bool[graph.GetLength(0)];
            parents = new int[graph.GetLength(0)];
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                visited[i] = false;
                parents[i] = -1;
            }

            Queue<int> bfsQueue = new Queue<int>();

            bfsQueue.Enqueue(start);
            while(bfsQueue.Count > 0)
            {
                int next = bfsQueue.Dequeue();
                visited[next] = true;

                for (int i = 0; i < graph.GetLength(0); i++)
                {
                    if (graph[start, i] && //연결 되어있는 정점이며,
                         !visited[i])      //방문한 적 없는 정렬
                    {
                        parents[i] = start;
                        bfsQueue.Enqueue(i);
                    }
                }
            }
        }
    }
}
