using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algoritmo_Simplex
{
    class Simplex
    {
        //decimal[] ObjectiveVector;
        //decimal[,] Matriz;
        Quadro quadroSimplex;
        bool isMaximize = true;
        int qtdInterations = 0;
        #region "old constructor"
        //public Simplex(decimal[] objectiveVector, decimal[,] matriz)
        //{
        //    this.ObjectiveVector = objectiveVector;
        //    this.Matriz = matriz;
        //    if (!PreValidateFields(this.ObjectiveVector, this.Matriz))
        //        throw new Exception("Valores invalidos");
        //}
        #endregion

        public Simplex(Quadro quadrosimplex, bool ismaximize)
        {
            this.quadroSimplex = quadrosimplex;
            this.isMaximize = ismaximize;
            //if (!PreValidateFields(this.quadroSimplex.NumericPicture))
            //    throw new Exception("Valores invalidos");
        }

        #region  "old code"
        //public bool Run()
        //{
        //    while (ObjectiveVector.Where(x => x < 0).Any())
        //    {
        //        decimal lowestValueToOut = 0;
        //        int positionOutColumn = 0;

        //        int positionEnteringElement;
        //        //Buscando coluna do elemento que entrará na base
        //        positionEnteringElement = FindPositionLowestValue(ObjectiveVector);
        //        if(!(positionEnteringElement >= 0 ))
        //            throw new Exception("Algo deu errado na busca da posicao");

        //        //Buscando linha do elemento que sairá da base
        //        //if(Matriz[0, positionEnteringElement] != 0)
        //        //    lowestValueToOut = Matriz[0, Matriz.GetLength(1) - 1] / Matriz[0, positionEnteringElement];
        //        for (int i=0; i < Matriz.GetLength(0); i++)
        //        {
        //            if (Matriz[i, positionEnteringElement] != 0)
        //            {
        //                decimal resultAux = Matriz[i, Matriz.GetLength(1) - 1] / Matriz[i, positionEnteringElement];
        //                if (resultAux < lowestValueToOut || lowestValueToOut == 0)
        //                {
        //                    lowestValueToOut = resultAux;
        //                    positionOutColumn = i;
        //                }
        //            }
        //            else
        //                continue;
        //        }


        //        if (!(Matriz[positionOutColumn, positionEnteringElement] == 0))
        //        {

        //            //Monta nova linha do pivo
        //            int len = Matriz.GetLength(1);
        //            decimal[] NewLinePivo = {Matriz[positionOutColumn, 0] / Matriz[positionOutColumn, positionEnteringElement], Matriz[positionOutColumn, 1] / Matriz[positionOutColumn, positionEnteringElement], Matriz[positionOutColumn, 2] / Matriz[positionOutColumn, positionEnteringElement],
        //            Matriz[positionOutColumn, 3] / Matriz[positionOutColumn, positionEnteringElement], Matriz[positionOutColumn, 4] / Matriz[positionOutColumn, positionEnteringElement], Matriz[positionOutColumn, 5] / Matriz[positionOutColumn, positionEnteringElement], Matriz[positionOutColumn, 6] / Matriz[positionOutColumn, positionEnteringElement] };

        //            Matriz[positionOutColumn, 0] = NewLinePivo[0];
        //            Matriz[positionOutColumn, 1] = NewLinePivo[1];
        //            Matriz[positionOutColumn, 2] = NewLinePivo[2];
        //            Matriz[positionOutColumn, 3] = NewLinePivo[3];
        //            Matriz[positionOutColumn, 4] = NewLinePivo[4];
        //            Matriz[positionOutColumn, 5] = NewLinePivo[5];
        //            Matriz[positionOutColumn, 6] = NewLinePivo[6];

        //            for (int i = 0; i < Matriz.GetLength(0); i++)
        //                if (i == positionOutColumn)
        //                    continue;
        //                else
        //                {
        //                    decimal valoraux = -Matriz[i, positionEnteringElement];
        //                    for (int j = 0; j < Matriz.GetLength(1); j++)
        //                    {
        //                        Matriz[i, j] = (Matriz[positionOutColumn, j] * valoraux) + Matriz[i, j];
        //                    }
        //                }
        //        }

        //        decimal valueLoadLine = -ObjectiveVector[positionEnteringElement];
        //        for (int i=0; i < Matriz.GetLength(1); i++)
        //            ObjectiveVector[i] = Matriz[positionOutColumn, i] * valueLoadLine + ObjectiveVector[i];
        //    }
        //    Console.WriteLine("Lucro Maximo  " + ObjectiveVector[ObjectiveVector.Length - 1]);
        //    return true;
        //}
        #endregion

        public bool Run2(bool showSpetByStep)
        {
            while (quadroSimplex.ObjectiveVector.Where(x => x < 0).Any())
            {
                qtdInterations++;
                decimal lowestValueToOut = 0;
                int positionOutLine = 0;

                int positionEnteringElement;
                //Buscando coluna do elemento que entrará na base
                positionEnteringElement = FindPositionLowestValue(quadroSimplex.ObjectiveVector);
                if (!(positionEnteringElement >= 0))
                    throw new Exception("Algo deu errado na busca da posicao");

                //Buscando linha do elemento que sairá da base
                for (int i = 0; i < quadroSimplex.NumericPicture.GetLength(0); i++)
                {
                    if (quadroSimplex.NumericPicture[i, positionEnteringElement] != 0)
                    {
                        decimal resultAux = quadroSimplex.NumericPicture[i, quadroSimplex.NumericPicture.GetLength(1) - 1] / quadroSimplex.NumericPicture[i, positionEnteringElement];
                        if ((resultAux < lowestValueToOut && resultAux >= 0) || lowestValueToOut == 0)
                        {
                            lowestValueToOut = resultAux;
                            positionOutLine = i;
                        }
                    }
                    else
                        continue;
                }


                if (!(quadroSimplex.NumericPicture[positionOutLine, positionEnteringElement] == 0))
                {
                    decimal pivo = quadroSimplex.NumericPicture[positionOutLine, positionEnteringElement];

                    for (int i = 0; i < quadroSimplex.NumericPicture.GetLength(1); i++)
                    {
                        quadroSimplex.NumericPicture[positionOutLine, i] = Convert.ToDecimal(quadroSimplex.NumericPicture[positionOutLine, i] / pivo);
                    }

                    for (int i = 0; i < quadroSimplex.NumericPicture.GetLength(0); i++)
                        if (i == positionOutLine)
                            continue;
                        else
                        {
                            decimal valoraux = -quadroSimplex.NumericPicture[i, positionEnteringElement];
                            for (int j = 0; j < quadroSimplex.NumericPicture.GetLength(1); j++)
                            {
                                quadroSimplex.NumericPicture[i, j] = (quadroSimplex.NumericPicture[positionOutLine, j] * valoraux) + quadroSimplex.NumericPicture[i, j];
                            }
                        }
                }

                var Element = quadroSimplex.PictureVariables[quadroSimplex.NumericPicture.GetLength(0) + positionEnteringElement];
                quadroSimplex.PictureVariables[quadroSimplex.NumericPicture.GetLength(0) + positionEnteringElement] = quadroSimplex.PictureVariables[positionOutLine];
                quadroSimplex.PictureVariables[positionOutLine] = Element;

                decimal valueLoadLine = -quadroSimplex.ObjectiveVector[positionEnteringElement];

                for (int i = 0; i < quadroSimplex.NumericPicture.GetLength(1); i++)
                    quadroSimplex.ObjectiveVector[i] = quadroSimplex.NumericPicture[positionOutLine, i] * valueLoadLine + quadroSimplex.ObjectiveVector[i];

                if(showSpetByStep)
                {
                    Console.WriteLine("Interação " + qtdInterations);
                    for(int i=0; i < quadroSimplex.NumericPicture.GetLength(0); i++)
                    {
                        if(i == 0 )
                        {
                            Console.Write("Base\t");
                            for(int k = quadroSimplex.NumericPicture.GetLength(0); k < quadroSimplex.PictureVariables.Length; k++)
                            {
                                Console.Write(quadroSimplex.PictureVariables[k] + "\t");
                            }
                            Console.Write("\n");
                        }
                        Console.Write(quadroSimplex.PictureVariables[i] + "\t");
                        for (int j = 0; j < quadroSimplex.NumericPicture.GetLength(1); j++)
                        {
                            Console.Write(Math.Round(quadroSimplex.NumericPicture[i, j], 2) + "\t");
                        }
                        Console.Write("\n");
                    }
                    Console.Write("Z\t");
                    for (int i = 0; i < quadroSimplex.ObjectiveVector.Length; i++)
                    {
                        if (i == quadroSimplex.ObjectiveVector.Length - 1)
                            Console.Write(Math.Round((isMaximize ? quadroSimplex.ObjectiveVector[i] : - quadroSimplex.ObjectiveVector[i]), 2) + "\n\n\n");
                        else
                            Console.Write(Math.Round(quadroSimplex.ObjectiveVector[i],2) + "\t");
                    }
                }
            }
            Console.WriteLine("\n\n\n\nLucro Maximo  " + quadroSimplex.ObjectiveVector[quadroSimplex.ObjectiveVector.Length - 1]);
            for (int i = 0; i < quadroSimplex.NumericPicture.GetLength(0); i++)
            {
                Console.WriteLine(quadroSimplex.PictureVariables[i] + " - " + quadroSimplex.NumericPicture[i, quadroSimplex.NumericPicture.GetLength(1)-1]);
            }
            return true;
        }


        public int FindPositionLowestValue(decimal[] objectiveVector)
        {
            int position = -1;
            decimal LowestValue = 0;
            for (int i=0; i < objectiveVector.Length - 1; i++)
            {
                if(objectiveVector[i] < LowestValue)
                {
                    LowestValue = objectiveVector[i];
                    position = i;
                }
            }
            return position;
        }




        public bool PreValidateFields(decimal[,] matriz)
        {
            for (int i = 0; i< matriz.GetLength(0); i ++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (!(matriz[i, j] >= 0))
                        throw new Exception("Valores devem ser maiores ou iguais a zero");
                }
            }

            return true;
        }
    }
}
