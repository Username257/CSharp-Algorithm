using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09._DesignTechnique
{
    internal class Backtracking
    {
        /******************************************************
		 * 백트래킹 (Backtracking)
		 * 
		 * 모든 경우의 수를 전부 고려하는 알고리즘
		 * 후보해(정답가능성있는 애)를 검증 도중 해(정답)가 아닌경우 되돌아가서 다시 해를 찾아가는 기법
		 ******************************************************/
        //느리지만 무조건 최고의 수를 구할 수 있음

        // 예시 - N개의 퀸(체스판의 서로 공격할 수 없는 퀸 N개 놓기)

        //1, 1 놓고 2, 3 넣으면 3번째 줄에 못 두기 때문에 안 됨
        //1, 1 놓고 2, 4 놓고 3, 2 놓으면 4번째 줄에 못 두기 때문에 안 됨
        //아무튼 다 하고 1, 1 놓는 게 안 된다는 결론 -> 그럼 1, 2 놓아보자
        //1, 2 놓고 2, 4놓고 3, 1놓고 4, 3 놓는 게 정답(해) n개의 퀸을 놓아도 다 산다
        //아무튼 이렇게 전부 판단해서 답 도출해내는 거임
        public static bool NQueen(bool[,] board, int y = 0)
        {
            int ySize = board.GetLength(0);
            int xSize = board.GetLength(1);

            for (int i = 0; i < xSize; i++)
            {
                if (!CanPlace(board, i, y))
                    continue;

                if (y >= ySize - 1)
                {
                    board[y, i] = true;
                    PrintBoard(board);
                    return true;
                }

                bool[,] copyBoard = board.Clone() as bool[,];
                copyBoard[y, i] = true;

                if (NQueen(copyBoard, y + 1))
                    return true;
            }

            return false;
        }

        public static bool CanPlace(bool[,] board, int x, int y)
        {
            int ySize = board.GetLength(0);
            int xSize = board.GetLength(1);

            // 직선 검증
            for (int i = 0; i < xSize; i++)
            {
                if (board[y, i])
                    return false;
            }
            for (int j = 0; j < ySize; j++)
            {
                if (board[j, x])
                    return false;
            }

            // 대각선 검증
            for (int j = 0; j < ySize; j++)
            {
                for (int i = 0; i < xSize; i++)
                {
                    if (board[j, i] && y - x == j - i)
                        return false;
                    if (board[j, i] && y + x == j + i)
                        return false;
                }
            }

            return true;
        }

        public static void PrintBoard(bool[,] board)
        {
            for (int y = 0; y < board.GetLength(0); y++)
            {
                for (int x = 0; x < board.GetLength(1); x++)
                {
                    if (board[y, x])
                        Console.Write("Q");
                    else
                        Console.Write('.');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
