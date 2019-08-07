using System;
using UnityEngine;
using UnityEngine.Windows;

namespace DefaultNamespace
{
    public class Field : MonoBehaviour
    {
        public int FieldSize = 10;
        public Cell[,] Cells;
        private Cells _cells;
        
        public Field Init()
        {
            Cells = new Cell[FieldSize,FieldSize];
            _cells =  GetComponentInChildren<Cells>();
            _cells.Init();
            int k = 0;
            for (int i = 0; i < FieldSize; i++)
            {
                for (int j = 0; j < FieldSize; j++)
                {
                    Cells[i, j] = _cells.cells[k];
                    k++;
                }
            }

            return this;
        }
    }
}