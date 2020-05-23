using System;

namespace Algoritmo_Simplex
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isMaximize = true;

            Console.WriteLine("Operação maximar digite max, caso seja minimizar digite min");
            if (Console.ReadLine().ToString().Equals("min"))
            {
                isMaximize = false;
                Console.WriteLine("Minimizar selecionado!");
            }
            else
                Console.WriteLine("Maximizar selecionado!");

            Console.Write("\nDigite a quantidade de variáveis: ");
            int resp = Convert.ToInt32(Console.ReadLine());
            decimal[] Variables = new decimal[resp];

            Console.WriteLine("Funcao objetivo, valores: ");
            for (int i = 0; i < resp; i++)
            {
                Console.Write("Variavel " + (i + 1).ToString() + " : ");
                Variables.SetValue(Convert.ToDecimal(Console.ReadLine()), i);
            }

            int columns = ((resp * 2) + 1);

            decimal[] objVector = new decimal[columns];
            for (int i = 0; i < resp; i++)
            {
                objVector.SetValue((isMaximize ? -Variables[i] : Variables[i]) , i);
            }
            for (int i = resp; i < columns; i++)
            {
                objVector.SetValue(Convert.ToDecimal(0), i);
            }

            Console.Write("\nDigite a quantidade de restrições: ");
            int resp2 = Convert.ToInt32(Console.ReadLine());

            decimal[,] Restricoes = new decimal[resp2, columns];

            for (int i = 0; i < resp2; i++)
            {
                Console.WriteLine("\nRestricao " + (i + 1).ToString());
                for (int j = 0; j < (resp + 1); j++)
                {
                    if (j == resp)
                    {
                        Console.Write("B = ");
                        j = columns - 1;
                    }
                    else
                        Console.Write("X" + j + " = ");
                    Restricoes[i, j] = Convert.ToDecimal(Console.ReadLine());
                }
                Console.WriteLine("");
            }

            for (int i = 0; i < resp; i++)
            {
                for (int j = resp, interacao2 = 1; j < columns; j++, interacao2 ++)
                {
                    if (j == resp + i)
                        Restricoes[i, j] = 1;
                }
            }

            string[] Var = new string[resp2 + columns];
            for (int i = 0, j = 1; i < (resp2 + columns); i++, j++)
            {
                if (j > resp)
                {
                    j = 1;
                }
                if (i < resp2)
                    Var[i] = "F" + j;
                else if (i < columns - 1)
                    Var[i] = "X" + j;
                else if (i < (resp2 + columns - 1))
                    Var[i] = "F" + j;
                else
                    Var[i] = "B";
            }


            Console.WriteLine("\n");

            Quadro quadroSimplex = new Quadro(Var, Restricoes, objVector);
            Simplex obj2 = new Simplex(quadroSimplex, isMaximize);
            obj2.Run2(true);



            //Trabalho vídeo
            //decimal[] objectiveVector2 = new decimal[7] { -10, -9, 0, 0, 0, 0, 0 };
            //decimal[,] constraintsMatrix2 = new decimal[4, 7];
            //constraintsMatrix2[0, 0] = Convert.ToDecimal(0.7); constraintsMatrix2[0, 1] = 1; constraintsMatrix2[0, 2] = 1; constraintsMatrix2[0, 3] = 0; constraintsMatrix2[0, 4] = 0; constraintsMatrix2[0, 5] = 0; constraintsMatrix2[0, 6] = 630;
            //constraintsMatrix2[1, 0] = Convert.ToDecimal(0.5); constraintsMatrix2[1, 1] = Convert.ToDecimal(0.833); constraintsMatrix2[1, 2] = 0; constraintsMatrix2[1, 3] = 1; constraintsMatrix2[1, 4] = 0; constraintsMatrix2[1, 5] = 0; constraintsMatrix2[1, 6] = 600;
            //constraintsMatrix2[2, 0] = 1; constraintsMatrix2[2, 1] = Convert.ToDecimal(0.667); constraintsMatrix2[2, 2] = 0; constraintsMatrix2[2, 3] = 0; constraintsMatrix2[2, 4] = 1; constraintsMatrix2[2, 5] = 0; constraintsMatrix2[2, 6] = 700;
            //constraintsMatrix2[3, 0] = Convert.ToDecimal(0.1); constraintsMatrix2[3, 1] = Convert.ToDecimal(0.25); constraintsMatrix2[3, 2] = 0; constraintsMatrix2[3, 3] = 0; constraintsMatrix2[3, 4] = 0; constraintsMatrix2[3, 5] = 1; constraintsMatrix2[3, 6] = 135;

            //string[] Variaveis2 = new string[11] { "F1", "F2", "F3", "F4", "X1", "X2", "F1", "F2", "F3", "F4", "B" };
            //Quadro quadroSimplex2 = new Quadro(Variaveis2, constraintsMatrix2, objectiveVector2);
            //Simplex obj3 = new Simplex(quadroSimplex2);
            //obj3.Run2();
        }
    }
}
