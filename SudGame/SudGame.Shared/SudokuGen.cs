using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gen
{
    public class SudokuGen
    {
        //ATRIBUTOS
        static int[,] grid = new int[9, 9];
        static string s;

        //INICIAR
        static void Init(ref int[,] grid)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = (i * 3 + i / 3 + j) % 9 + 1;
                }
            }
        }

        //FUNÇÃO PARA APLICACAO DO TIPO CONSOLE
        static void Draw(ref int[,] grid, out string _s)
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    s += grid[x, y].ToString() + " ";
                }
                s += "\n";
            }
            
            //Console.WriteLine(s);
            _s = s;
            s = "";
        }


        static void ChangeTwoCell(ref int[,] grid, int findValue1, int findValue2)
        {
            int xParam1, yParam1, xParam2, yParam2;
            xParam1 = yParam1 = xParam2 = yParam2 = 0;

            for (int i = 0; i < 9; i+=3)
            {
                for (int k = 0; k < 9; k+=3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            if (grid[i + j, k + z] == findValue1)
                            {
                                xParam1 = i + j;
                                yParam1 = k + z;
                            }
                            if (grid[i + j, k + z] == findValue2)
                            {
                                xParam2 = i + j;
                                yParam2 = k + z;
                            }
                        }                        
                    }
                    grid[xParam1, yParam1] = findValue2;
                    grid[xParam2, yParam2] = findValue1;
                }
                
            }
        }

        //ATUALIZAR
        static void Update(ref int[,] grid, int shuffleLevel)
        {
            for (int repeat = 0; repeat < shuffleLevel; repeat++)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                Random rand2 = new Random(Guid.NewGuid().GetHashCode());
                ChangeTwoCell(ref grid, rand.Next(1, 9), rand2.Next(1, 9));

            }
        }

        //FUNCAO QUE GERA UM SUDOKU E RETORNA MATRIZ
        public static int[,] startAndGetSudoku()
        {
            s = "";
            string getGrid;

            Init(ref grid);
            Update(ref grid, 10);
            Draw(ref grid, out getGrid);

            return grid;
        }

        //FUNCAO QUE GERA UM SUDOKU E RETORNA STRING
        public static string startAndGetStringSudoku()
        {
            s = "";
            string temp;

            Init(ref grid);
            Update(ref grid, 10);
            Draw(ref grid, out temp);

            return temp;
        }
    }
}
