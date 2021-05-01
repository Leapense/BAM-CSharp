using System;
using System.Linq;
using System.Text;

namespace BAM
{
    class Program
    {
        const int width = 3;
        const int height = 3;
        static StringBuilder sb = new StringBuilder();
        static int[] NET_X = new int[width * height] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static int[] newX = new int[width * height] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static int[] NET_Y = new int[2] { 0, 0 };
        static int[] newY = new int[2] { 0, 0 };
        static int[] tempY = new int[2] { 0, 0 };
        static void Main(string[] args)
        {
            // step 1. Set weights to store P patterns
            int[] s1 = new int[width * height] { 1, 1, 1, 1, -1, 1, 1, 1, 1 };
            int[] t1 = new int[2] { -1, -1 };
            int[] s2 = new int[width * height] { -1, -1, 1, -1, -1, 1, -1, -1, 1 };
            int[] t2 = new int[2] { -1, 1 };

            int[,] w = new int[width * height, 2];

            Console.Write("Weight = ");
            sb.Append("[");
            int temp, temp2;
            for(int i = 0; i < width * height; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    temp = (s1[i] * t1[j]);
                    temp2 = (s2[i] * t2[j]);
                    w[i, j] = temp + temp2;
                    sb.Append(w[i, j] + " ");
                }
            }
            sb.Append("]");
            Console.WriteLine(sb);

            // Step 2. 임의의 x 패턴을 입력한다.
            int[] x = new int[width * height] { 1, -1,-1, 1, -1, -1, 1, -1, 1 };
            // do-while문으로 조건 검사를 시행한다. do-while문은 최소 한번은 실행하고 조건 검사를 하기 때문에 이 반복문이 낫다.
            do
            {
                for(int i = 0; i < width * height; i++)
                {
                    for(int j = 0; j < 2; j++)
                    {
                        newY[j] += x[i] * w[i, j];
                    }
                }
                for (int j = 0; j < 2; j++)
                {
                    if (newY[j] > 0) newY[j] = 1;
                    else newY[j] = -1;
                    NET_Y[j] = newY[j];
                    
                }
                for(int i = 0; i < 2; i++)
                {
                    for(int j = 0; j < width * height; j++)
                    {
                        newX[j] += NET_Y[i] * w[j, i]; 
                    }
                }
                for(int j = 0; j < width * height; j++)
                {
                    if (newX[j] > 0) newX[j] = 1;
                    else if (newX[j] == 0) newX[j] = x[j];
                    else newX[j] = -1;
                    NET_X[j] = newX[j];
                }
                Console.Write("NETy: ");
                for (int i = 0; i < 2; i++)
                {
                    Console.Write(NET_Y[i] + " ");
                }
                Console.WriteLine();

                Console.Write("NETx: ");
                for (int i = 0; i < width * height; i++)
                {
                    Console.Write(NET_X[i] + " ");
                }
                Console.WriteLine();
            } while (tempY.SequenceEqual(NET_Y));

            /*
             * 내가 코드를 작성하고 실행했을 때, y층은 복구가 되었는데, x층이 문제가 생긴 것 같다.
             * 원래 이런 건지 알 수가 없다.
             */
        }
    }
}
