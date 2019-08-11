using System;
using DefaultNamespace;
using UnityEngine;

public class Road
{
    public bool IsConnected = false;

    public Road(Room room1, Room room2, Vector2Int direction)
    {
        var room1Center = new Vector2Int(room1.X + (int) Math.Floor((double) room1.XRange / 2),
            room1.Y + (int) Math.Floor((double) room1.YRange / 2));
        var room2Center = new Vector2Int(room2.X + (int) Math.Floor((double) room2.XRange / 2),
            room2.Y + (int) Math.Floor((double) room2.YRange / 2));
        int x = room1Center.x;
        int y = room1Center.y;
        for (x = room1Center.x; x < room2Center.x; x++)
        {
            if (GameController.Instance.field.Cells.ArrayCells2D[x, y].State != MasterFieldData.floor)
            {
                GameController.Instance.field.Cells.ArrayCells2D[x, y].State = MasterFieldData.emptiness;
            }
        }
        for (y = room1Center.y; y < room2Center.y; y++)
        {
            if (GameController.Instance.field.Cells.ArrayCells2D[x, y].State != MasterFieldData.floor)
            {
                GameController.Instance.field.Cells.ArrayCells2D[x, y].State = MasterFieldData.emptiness;
            }
        }
    }
}

