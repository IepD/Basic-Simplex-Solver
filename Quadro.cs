using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritmo_Simplex
{
    public class Quadro
    {
        public string[] PictureVariables;
        public decimal[,] NumericPicture;
        public decimal[] ObjectiveVector;

        public Quadro(string[] variaveisQuadro, decimal[,] quadroNumerico, decimal[] objectiveVector)
        {
            this.PictureVariables = variaveisQuadro;
            this.NumericPicture = quadroNumerico;
            this.ObjectiveVector = objectiveVector;
        }
    }
}
