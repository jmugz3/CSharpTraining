using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training
{
    class ChessBoard
    {
        static void Main(string[] args)
        {
            int x = 0;            
            for (x = 0; x < 8; x++)
            {
                switch (x % 2 == 0)
                {
                    case true:
                        GenerateLine("X");
                        break;
                    default:
                        GenerateLine("O");
                        break;
                }
            }
            Console.ReadKey();
        }




        static void GenerateLine(string first)
        {
            string line = string.Empty;
            string square;
            int y = 0;
            for (y = 0; y < 8; y++)
            {
                switch (y % 2 == 0)
                {
                    case true:
                        square = "X";
                        break;
                    default:
                        square = "O";
                        break;
                }

                line += square;
            }
            Console.WriteLine(line);


        }
    }
}
