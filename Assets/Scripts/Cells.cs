using System.Collections.Generic;
using Scripts.MasterDatas;
using UnityEngine;

namespace Scripts
{



    public class Cells : MonoBehaviour
    {
//    public GameObject[] cells;
        public Cell[,] ArrayCells2D;

        public void Init(int FieldSize)
        {
            ArrayCells2D = new Cell[FieldSize, FieldSize];
            int x = 0;
            int y = 0;
            int id = 0;
            for (var i = 0; i < FieldSize; i++)
            {
                for (var j = 0; j < FieldSize; j++)
                {
                    GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    plane.name = "cell" + id;
                    plane.transform.parent = transform; //右が親,左が子
                    plane.AddComponent<Cell>();
                    plane.GetComponent<Cell>().Id = id;
                    plane.GetComponent<Cell>().Position = new Vector2Int(x, y);
                    plane.GetComponent<Cell>().State = MasterFieldData.wall; //初期値は壁
                    plane.GetComponent<Cell>().OnState = MasterFieldOnState.None;
                    ArrayCells2D[j, i] = plane.GetComponent<Cell>();
                    id++;
                    x += 1;
                }

                y += 1;
                x = 0;
            }
        }
    }
}