using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace WindowsFormsApp1
{
    public partial class FastFactorial : Component
    {

       
        public FastFactorial()
        {
            InitializeComponent();
        }

        public FastFactorial(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        public BigInteger Factorial(string i = "0")
        {
            
            BigInteger result = 1;

            int userEntry = UserEntryCheck(i);


            if (userEntry < 7)
            {
                result= RecNormalFactorial(userEntry);



            }
            else
            {
                result = SpeedFactorial(userEntry);


            }




            return result;

        }

        private static BigInteger SpeedFactorial(int userEntry)
        {
            int eCount = userEntry % 2 == 1 ? 1 : 0;
            int i, s, r;



            int Loop = userEntry / 2;

            BigInteger[] F = new BigInteger[Loop + eCount];

            F[0] = Loop;
            if (eCount == 1)
            {
                F[Loop] = userEntry;

            }
            i = 1;
            s = Loop;
            r = 1;
            for (int c = Loop - 1; c > 0; c--)
            {

                F[i] = s = s + c;

                while (F[i] % 2 == 0)
                {
                    Loop = Loop + 1;
                    F[i] = F[i] / 2;

                }
                i++;
            }
            return SyncBinariTree(F) * BigInteger.Pow(2, Loop);
        }

        private static BigInteger SyncBinariTree(BigInteger[] f)
        {

            BigInteger right = RecBinaryTree(f, (f.Length - 1) / 2 + 1, f.Length - 1);

            BigInteger left = RecBinaryTree(f, 0, (f.Length - 1) / 2);

            return left * right;



        }

        private static BigInteger RecBinaryTree(BigInteger[] f, int n, int m)
        {

            int k;

            if (m == n + 1)
            {
                return f[n] * f[m];
            }

            if (m == n + 2)
            {

                return f[n] * f[n + 1] * f[m];

            }
            k = (n + m) / 2;

            return RecBinaryTree(f, n, k) * RecBinaryTree(f, k + 1, m);



        }

        private static BigInteger RecNormalFactorial(int userEntry)
        {


            BigInteger result = 1;

            if (userEntry > 0)
            {
                result = userEntry * RecNormalFactorial(userEntry - 1);


            }





            return result;


        }

        private static int UserEntryCheck(string i)
        {

            int islem = 0;


            if (int.TryParse(i, out int s))
            {
                islem = int.Parse(i);






            }
            else
            {

                islem = 0;


            }

            if (islem < 0)
            {

                islem = 0;
            }
            return islem;

        }   
    }
}
