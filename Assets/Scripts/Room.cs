using DefaultNamespace;
using UnityEngine;

public class Room
{
    public int RoomId;
    public int X;
    public int Y;
    public int XRange;
    public int YRange;

    public int RoomMinSize = 5;

    public void Create(Parcel parcel)
    {
        RoomId = parcel.ParcelId;
        XRange = Random.Range(RoomMinSize, parcel.XRange-1);
        YRange = Random.Range(RoomMinSize, parcel.YRange-1);
        X = Random.Range(parcel.X+1, parcel.X + parcel.XRange - XRange);
        Y = Random.Range(parcel.Y+1, parcel.Y + parcel.YRange - YRange);

        for (int y = Y;y < Y + YRange; y++)
        {
            for (int x = X; x < X + XRange; x++)
            {
                GameController.Instance.field.Cells.ArrayCells2D[x, y].State = MasterFieldData.floor;
            }
        }

    }
}