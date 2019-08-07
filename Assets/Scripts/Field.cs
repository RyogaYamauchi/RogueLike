using System;
using UnityEngine;
using UnityEngine.Windows;


public class Field : MonoBehaviour
{
    [SerializeField] public int FieldSize = 30;
    public Cell[,] Cells;
    private Cells _cells;

    public void Init()
    {
        GameObject cells = new GameObject("Cells");
        cells.transform.parent = transform;
        cells.AddComponent<Cells>();
        cells.GetComponent<Cells>().Init(FieldSize);

        Cells = new Cell[FieldSize, FieldSize];
        _cells = GetComponentInChildren<Cells>();
        _cells.Init(FieldSize);
        int k = 0;
        for (var i = 0; i < FieldSize; i++)
        {
            for (var j = 0; j < FieldSize; j++)
            {
                Cells[i, j] = _cells.cells[k].GetComponent<Cell>();
                k++;
            }
        } 
    }

    public static void MakeInstance()
    {
        GameObject field = new GameObject("Field");
        field.AddComponent<Field>();
    }
}