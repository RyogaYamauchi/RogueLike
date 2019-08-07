using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cells : MonoBehaviour
{
    public Cell[] cells;

    public void Init()
    {
        cells = GetComponentsInChildren<Cell>();
        int id = 0;
        foreach (var cell in cells)
        {
            cell.Init();
            cell.Id = id;
            id++;
        }
    }
}