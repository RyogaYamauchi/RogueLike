using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;


public class Field : MonoBehaviour
{
    [SerializeField] public int FieldSize = 30;
    public Cells Cells;
    public Dictionary<int,Parcel> ParcelsDictionary;

    public void Init()
    {
        ParcelsDictionary = new Dictionary<int,Parcel>();
        GameObject cells = new GameObject("Cells");
        cells.transform.parent = transform;
        cells.AddComponent<Cells>();
        cells.GetComponent<Cells>().Init(FieldSize);
        Cells = cells.GetComponent<Cells>();
        CreateField();
    }

    void CreateField()
    {
        var firstParcel = new Parcel();
        firstParcel.SetParcel(1, 0, 0, FieldSize, FieldSize);
        ParcelsDictionary.Add(1, firstParcel);
        int DivisionNum = Random.Range(4,10); //分割数(部屋数)
        for (int id = 2; id < DivisionNum+1; id++)
        {
            var currentParcel = ParcelsDictionary.Values.OrderByDescending(s => s.Volume).FirstOrDefault();
            var parcel = new Parcel();
            if (currentParcel.Volume < 50) break;
            if (currentParcel.XRange > currentParcel.YRange) //Xの方が大きいとき
            {
                int divideX = Random.Range(5, currentParcel.XRange-5 );
                parcel.SetParcel(id , currentParcel.X, currentParcel.Y, divideX, currentParcel.YRange);
                ParcelsDictionary.Add(id , parcel);
                currentParcel.XRange = currentParcel.XRange - divideX;
                currentParcel.X = currentParcel.X + divideX;
            }
            else
            {
                int divideY = Random.Range(5, currentParcel.YRange-5 );
                parcel.SetParcel(id, currentParcel.X, currentParcel.Y, currentParcel.XRange, divideY);
                ParcelsDictionary.Add(id ,parcel);
                currentParcel.YRange = currentParcel.YRange - divideY;
                currentParcel.Y = currentParcel.Y + divideY;

            }
        }
        //Debug color
        foreach (var cell in Cells.ArrayCells2D)
        {
            cell.State = cell.ParcelId;
        }

        foreach (var parcel in ParcelsDictionary)
        {
            
        }
    }

    public static void MakeInstance()
    {
        GameObject field = new GameObject("Field");
        field.AddComponent<Field>();
    }
}