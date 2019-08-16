using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Scripts.MasterDatas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts
{
    public class Enemy : ICharacter
    {
        public string Name;
        public int Id;
        public Vector2Int TargetPosition;
        Queue<Vector2Int> RootQueue;
        public void Spawn(int id)
        {
            Id = id;
            var roomId = Random.Range(1, GameController.Instance.field.RoomsDictionary.Count);
            if (roomId == GameController.Instance.player.CurrentRoomId) Spawn(id);
            CurrentRoomId = roomId;
            var room = GameController.Instance.field.RoomsDictionary[roomId]; 
            var x = Random.Range(room.X, room.X + room.XRange); 
            var y = Random.Range(room.Y, room.Y + room.YRange); 
            Position = new Vector2Int(x, y);
            RootQueue = new Queue<Vector2Int>();
            TargetPosition = RandomPosition(CurrentRoomId);
        }

        public IEnumerator Action(Action action)
        {
            if (CurrentRoomId == GameController.Instance.player.CurrentRoomId)
            {
                TargetPosition = GameController.Instance.player.Position;
            }
            // TODO : ルート上に何もなく通れる時に再計算しないように
            RootQueue = GetRoute();
            
            if(RootQueue.Count == 0){Debug.Log("バグ");}

            //同じ部屋にプレイヤーがいるとき
            if (RootQueue.Peek() == GameController.Instance.player.Position)
            {
                Debug.Log("攻撃！！！！！");
                Attack();
            }
            //部屋にプレイヤーがいない時
            else
            {
                Debug.Log($"今が{Position}, ターゲットが {TargetPosition} , 移動するぜ！ {RootQueue.Peek()}");
                Position = RootQueue.Dequeue();
                if (RootQueue.Count == 0)
                {
                    TargetPosition = RandomPosition(CurrentRoomId);
                }
            }

            yield return new WaitForSeconds(0.5f);
            action?.Invoke();
        }

        private Queue<Vector2Int> GetRoute()
        {
            Aster aster = new Aster();
            aster.Init();
            var list = new List<Vector2Int>();
            aster.SearchRoute(Position, TargetPosition, list);
            RootQueue = new Queue<Vector2Int>(list.ToArray());
            return RootQueue;
        }

        private Vector2Int RandomPosition(int roomId)
        {
            var room = GameController.Instance.field.RoomsDictionary[roomId];
            var cells = GameController.Instance.field.Cells.ArrayCells2D;
            var targetPositionX = Random.Range(room.X, room.X + room.XRange);
            var targetPositionY = Random.Range(room.Y, room.Y + room.YRange);
            if (cells[targetPositionX, targetPositionY].State != MasterFieldData.floor) RandomPosition(roomId);
            if (targetPositionX == Position.x && targetPositionY == Position.y) RandomPosition(roomId);
            return new Vector2Int(targetPositionX, targetPositionY);
        }

        public void Die()
        {
            var obj = GameObject.Find(Name + "(Clone)");
            Destroy(obj);
        }
    }
}