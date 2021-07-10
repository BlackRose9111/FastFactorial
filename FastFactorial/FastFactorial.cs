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
    /// <summary>
    /// Ömer Faruk BARAN - Balıkesir Üniversitesi - 201913709034
    /// Source material is included in the github repos.
    /// https://github.com/BlackRose9111/FastFactorial/
    /// </summary>
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

        /// <summary>
        /// The entry point of the component. Use of a string input was arbitrary. The component will convert the string input to a int
        /// The use of string was mainly because of the basic input from a textbox or another is string. This is not very important.
        /// The source material uses a variable for numbers below 7 as the algorithm is incapable of calculation such low values due to the number 
        /// of leaves in the binary tree. For that, this component uses a familiar standard recursive factorial function.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
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
        /// <summary>
        /// User entries above 7 are sent here. This function determines the factors of the number to be multiplied depending on 
        /// If they are odd or even. The factors are then put in an Array and sent to the syncronised binary tree to be multiplied
        /// The result is then multiplied with 2^(n/2) due to the factors being twice as many.
        /// </summary>
        /// <param name="userEntry"></param>
        /// <returns></returns>
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
        /// <summary>
        /// To increase the speed, the syncronised binary tree runs both sides of the tree at the same time. Although the original document uses a variable
        /// to store the length of the array storing the factors, C# arrays already contain their own sizes, that was not necessary.
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private static BigInteger SyncBinariTree(BigInteger[] f)
        {

            BigInteger right = RecBinaryTree(f, (f.Length - 1) / 2 + 1, f.Length - 1);

            BigInteger left = RecBinaryTree(f, 0, (f.Length - 1) / 2);

            return left * right;



        }
        /// <summary>
        /// In the recursive tree, at least two numbers are multiplied and the result is returned.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Standard factorial for numbers below 7. Although useful for normal factorial needs, this creates issues with big numbers as the number
        /// of recursion get too high.
        /// </summary>
        /// <param name="userEntry"></param>
        /// <returns></returns>
        private static BigInteger RecNormalFactorial(int userEntry)
        {


            BigInteger result = 1;

            if (userEntry > 0)
            {
                result = userEntry * RecNormalFactorial(userEntry - 1);


            }





            return result;


        }
        /// <summary>
        /// Parses the user input to a useful value even if the input is otherwise useless.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
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
