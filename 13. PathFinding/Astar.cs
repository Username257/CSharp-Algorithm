using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13._PathFinding
{
    internal class Astar
    {
		/******************************************************
		 * A* 알고리즘
		 * 
		 * 다익스트라 알고리즘을 확장하여 만든 최단경로 탐색알고리즘
		 * 경로 탐색의 우선순위를 두고 유망한 해(점수가 높은/최적인)부터 우선적으로 탐색
		 ******************************************************/
		//유망한(f가 높은)정점부터 먼저 탐색한다 == 에이스타 알고리즘의 의의

		/*
			f = g + h (총 거리)
			g = 실제로 간 거리
			h = 휴리스틱(예상거리)
			f 값이 가장 작은 정점을 선택해서 탐색한 정점의 f값
			대각선은 루트 2 곱해줌
			정점 탐색 시 h가 제일 작은 거
			컴터는 아래보다 위를 먼저 탐색
		*/
		const int CostStraight = 10;
		const int CostDiagonal = 14;

		static Point[] Direction =
		{
			new Point(0, +1), //상
			new Point(0, -1), //하
			new Point(-1, 0), //좌
            new Point(+1, 0), //우
			new Point(-1, +1), //좌상
			new Point(-1, -1), //좌하
			new Point(-1, +1), //우상
            new Point(+1, -1), //우하
		};
		public static bool PathFinding(bool[,] tileMap, Point start, Point end, out List<Point> path )
		{
			int ySize = tileMap.GetLength(0);
			int xSize = tileMap.GetLength(1);

			bool[,] visited = new bool[ySize, xSize];
			ASNode[,] nodes = new ASNode[ySize, xSize];
			PriorityQueue<ASNode, int> nextPointPQ = new PriorityQueue<ASNode, int>();

			// 0. 시작 정점을 생성하여 추가
			ASNode startNode = new ASNode(start, null, 0, Heuristic(start, end));
			nodes[startNode.point.y, startNode.point.x] = startNode;
			nextPointPQ.Enqueue(startNode, startNode.f);

			while (nextPointPQ.Count > 0)
			{
				// 1. 다음으로 탐색 할 정점 꺼내기
				ASNode nextNode = nextPointPQ.Dequeue();

				// 2. 방문한 정점은 방문 표시
				visited[nextNode.point.y, nextNode.point.x] = true;

				// 3. 탐색할 정점이 도착지인 경우
				// 도착했다고 판단해서 경로 반환
				if (nextNode.point.x == end.x && nextNode.point.y == end.y)
				{
					Point? pathPoint = end;
					path = new List<Point>();

					while (pathPoint != null)
					{
						Point point = pathPoint.GetValueOrDefault();
						path.Add(point);
						pathPoint = nodes[point.y, point.x].parent;
					}

					path.Reverse();
					return true;
				}

				// 4. Astar 탐색을 진행
				// 방향 탐색
				for (int i = 0; i < Direction.Length; i++)
				{
					int x = nextNode.point.x + Direction[i].x;
					int y = nextNode.point.y + Direction[i].y;

					// 4-1 탐색하면 안되는 경우 제외
					// 맵을 벗어나는 경우
					if (x < 0 || x >= xSize || y < 0 || y >= ySize)
						continue;
					// 탐색할 수 없는 정점일 경우 제외
					else if (tileMap[y, x] == false)
						continue;
					// 이미 방문한 정점일 경우
					else if (visited[y, x])
						continue;

					// 4-2. 탐색 점수 계산
					int g = nextNode.g + ((nextNode.point.x == x || nextNode.point.y == y) ? CostStraight : CostDiagonal);
                    int h = Heuristic(new Point(x, y), end);
					ASNode newNode = new ASNode(new Point(x, y), nextNode.point, g, h);

					// 4-3. 정점의 갱신이 필요한 경우 새로운 정점으로 할당
					if (nodes[y, x] == null || //점수 계산이 되지 않은 정점이거나
						nodes[y, x].f > newNode.f) //가중치가 더 높은 정점인 경우
					{
						nodes[y, x] = newNode;
						nextPointPQ.Enqueue(newNode, newNode.f);
					}
				}
                
			}
            path = null;
            return false;
        }

		//휴리스틱 : 최상의 경로를 추정하는 순위값, 휴리스틱에 의해 경로탐색 효율이 결정됨
		private static int Heuristic(Point start, Point end)
		{
            int xSize = Math.Abs(start.x - end.x);  // 가로로 가야하는 횟수
            int ySize = Math.Abs(start.y - end.y);  // 세로로 가야하는 횟수

            // 맨해튼 거리 : 가로 세로를 통해 이동하는 거리
            // return CostStraight * (xSize + ySize);

            // 유클리드 거리 : 대각선을 통해 이동하는 거리
            return CostStraight * (int)Math.Sqrt(xSize * xSize + ySize * ySize);

        }

		private class ASNode
		{
			//정점 왜 구조체 안 썼냐 - 구조체는 빈 데이터를 가질 수 없음(탐색 안 한 빈 정점)
			public Point point; //현재 정점 위치
			public Point? parent; //이 정점을 탐색한 정점(없을 수도 있다)
			
			public int f; //f(x) = g(x) + h(x) : 총 거리
			public int g; //현재까지의 거리, 지금까지 경로 가중치
			public int h; //앞으로 예상되는 거리, 목표까지 추정 경로 가중치 (휴리스틱)

			public ASNode(Point point, Point? parent, int g, int h)
			{
				this.point = point;
				this.parent = parent;
				this.g = g;
				this.h = h;
				this.f = g + h;
			}
		}
    }

	public struct Point
	{
		public int x;
		public int y;

		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}
}
