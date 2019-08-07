using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cells : MonoBehaviour
{
    public List<GameObject> cells;
    
    public void Init(int FieldSize)
    {
        List<GameObject>_cells = new List<GameObject>();
        int x = 0;
        int y = 0;
        int id = 0;
        for (var i = 0; i < FieldSize; i++)
        {
            for (var j = 0; j < FieldSize; j++)
            {
                GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                plane.transform.parent = transform; //右が親,左が子
                plane.AddComponent<Cell>();
                plane.GetComponent<Cell>().Id = id;
                plane.GetComponent<Cell>().Position = new Vector2Int(x, y);
                id++;
                x += 1;
                _cells.Add(plane);
            }
            y += 1;
            x = 0;
        }
        cells = _cells;
    }
}