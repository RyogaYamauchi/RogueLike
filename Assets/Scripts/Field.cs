using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.MasterDatas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts
{
    public class Field : MonoBehaviour
    {
        [SerializeField] public int FieldSize = 30;
        [SerializeField] public int DivideMinSize = 7;
        public Cells Cells;
        public Dictionary<int, Parcel> ParcelsDictionary;
        public Dictionary<int, Room> RoomsDictionary;

        public void Init()
        {
            RoomsDictionary = new Dictionary<int, Room>();
            ParcelsDictionary = new Dictionary<int, Parcel>();
            GameObject cells = new GameObject("Cells");
            cells.transform.parent = transform;
            cells.AddComponent<Cells>();
            cells.GetComponent<Cells>().Init(FieldSize);
            Cells = cells.GetComponent<Cells>();
            CreateField();
            CreateStair();
        }

        public IEnumerator GoUpTheStair()
        {
            //階段を登った時のロード時間
            yield return new WaitForSeconds(2.0f);
            RoomsDictionary.Clear();
            ParcelsDictionary.Clear();
            Cells.Clear(FieldSize);
            CreateField();
            CreateStair();
            GameController.Instance.enemies.InitEnemies();
            GameController.Instance.player.Init();
        }

        void CreateStair()
        {
            var roomId = Random.Range(1, RoomsDictionary.Count - 1);
            var room = RoomsDictionary[roomId];
            var x = Random.Range(room.X, room.X + room.XRange);
            var y = Random.Range(room.Y, room.Y + room.YRange);
            if (Cells.ArrayCells2D[x, y].State == MasterFieldData.floor &&
                Cells.ArrayCells2D[x, y].OnState == MasterFieldOnState.None)
            {
                Cells.ArrayCells2D[x, y].State = MasterFieldData.stair;
            }
            else
            {
                CreateStair();
            }
        }

        void CreateField()
        {
            var firstParcel = new Parcel();
            firstParcel.SetParcel(1, 0, 0, FieldSize, FieldSize);
            ParcelsDictionary.Add(1, firstParcel);
            int DivisionNum = Random.Range(4, 10); //分割数(部屋数)
            for (int id = 2; id < DivisionNum + 1; id++)
            {
                var currentParcel = ParcelsDictionary.Values.OrderByDescending(s => s.Volume).FirstOrDefault();
                var parcel = new Parcel();
                if (currentParcel.Volume < 50) break;
                if (currentParcel.XRange > currentParcel.YRange) //Xの方が大きいとき
                {
                    int divideX = Random.Range(DivideMinSize, currentParcel.XRange - DivideMinSize);
                    parcel.SetParcel(id, currentParcel.X, currentParcel.Y, divideX, currentParcel.YRange);
                    ParcelsDictionary.Add(id, parcel);
                    currentParcel.XRange = currentParcel.XRange - divideX;
                    currentParcel.X = currentParcel.X + divideX;
                }
                else
                {
                    int divideY = Random.Range(DivideMinSize, currentParcel.YRange - DivideMinSize);
                    parcel.SetParcel(id, currentParcel.X, currentParcel.Y, currentParcel.XRange, divideY);
                    ParcelsDictionary.Add(id, parcel);
                    currentParcel.YRange = currentParcel.YRange - divideY;
                    currentParcel.Y = currentParcel.Y + divideY;

                }
            }

            //Debug color
//        foreach (var cell in Cells.ArrayCells2D)
//        {
//            cell.State = cell.ParcelId;
//        }
            //部屋の作成
            foreach (var parcel in ParcelsDictionary.Values)
            {
                var room = new Room();
                room.Create(parcel);
                RoomsDictionary.Add(parcel.ParcelId, room);
            }

            //道の作成
            var Room = new Room();
            foreach (var currentToom in RoomsDictionary.Values)
            {
                SetRoad(0, 1, currentToom); //東
                SetRoad(0, -1, currentToom); //西
                SetRoad(1, 0, currentToom); //北
                SetRoad(-1, 0, currentToom); //南
            }
        }

        private void SetRoad(int x, int y, Room currentRoom)
        {
            var CenterCell = new Vector2Int(currentRoom.X + (int) Math.Floor((double) currentRoom.XRange / 2),
                currentRoom.Y + (int) Math.Floor((double) currentRoom.YRange / 2));
            var direction = new Vector2Int(x, y);
            var currentCell = CenterCell;
            var TargetRoom = new Room();

            while (true)
            {
                currentCell += direction;
                if (currentCell.x >= GameController.Instance.field.FieldSize) break;
                if (currentCell.x < 0) break;
                if (currentCell.y >= GameController.Instance.field.FieldSize) break;
                if (currentCell.y < 0) break;
                if (GameController.Instance.field.Cells.ArrayCells2D[currentCell.x, currentCell.y].ParcelId !=
                    currentRoom.RoomId)
                {
                    TargetRoom =
                        RoomsDictionary[
                            GameController.Instance.field.Cells.ArrayCells2D[currentCell.x, currentCell.y].ParcelId];
                    Road.SetRoad(currentRoom, TargetRoom);
                    break;

                }

            }

        }

        public static void MakeInstance()
        {
            GameObject field = new GameObject("Field");
            field.AddComponent<Field>();
        }
    }
}