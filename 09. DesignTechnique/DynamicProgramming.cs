using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09._DesignTechnique
{
    internal class DynamicProgramming
    {
        /******************************************************
		 * 동적계획법 (Dynamic Programming)
		 * 
		 * 작은문제의 해답을 큰문제의 해답의 부분으로 이용하는 상향식 접근 방식
		 * 주어진 문제를 해결하기 위해 부분 문제에 대한 답을 계속적으로 활용해 나가는 기법
		 ******************************************************/
        //분할정복이랑 좀 반대의 애임 큰걸 작은 애로 나눈다(하향식)
        //작은 애들을 풀면 큰 애 답이 나온다(상향식)

        // 예시 - 피보나치 수열
        // 1 1 2 3 5 8 앞에꺼 2개를 더한 것을 적어나감


        //분할 정복 해볼까?
        int Fibonachi2(int x)
        {
            if (x == 1)
                return 1;
            if (x == 2)
                return 1;
            return Fibonachi2(x = 2) + Fibonachi2(x - 1);
        } //분할 정복으로 구현하면 두 배가 됨 O 2의 n승 됨
        //큰 거에서 작은 걸로 쪼개니까


        int Fibonachi(int x)
        {
            int[] fibonachi = new int[x + 1];
            fibonachi[1] = 1;
            fibonachi[2] = 1;

            for (int i = 3; i <= x; i++)
            {
                fibonachi[i] = fibonachi[i - 1] + fibonachi[i - 2];
            }

            return fibonachi[x];
        } //아래서 구현한 것들을 저장해서 위로 올라감 1은 1, 2는 1, 3은 2 저장해두면
        //조그만 것들 더해서 큰 것들 답안을 얻을 수 있음
    }
}
