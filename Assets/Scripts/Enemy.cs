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

            RootQueue = GetRoute();

            //同じ部屋にプレイヤーがいるとき
            if (RootQueue.Peek() == GameController.Instance.player.Position)
            {
                Debug.Log("攻撃！！！！！");
                Attack();
            }
            //部屋にプレイヤーがいない時
            else
            {
                Move(RootQueue.Peek());
                if (RootQueue.Count == 0)
                {
                    TargetPosition = RandomPosition(CurrentRoomId);
                }
            }
            
            //敵の移動時間
            yield return new WaitForSeconds(0.5f);
            action?.Invoke();
        }

        private void CorrectCallBack(int dx, int dy)
        {
            GameController.Instance.field.Cells.ArrayCells2D[Position.x, Position.y].OnState =
                MasterFieldOnState.None;
            GameController.Instance.field.Cells.ArrayCells2D[Position.x + dx, Position.y + dy].OnState =
                MasterFieldOnState.Enemy;
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

        public bool Die()
        {
            Destroy(gameObject);
            return true;
        }

        public void Move(Vector2Int nextPosition)
        {
            var dx = nextPosition.x - Position.x;
            var dy = nextPosition.y - Position.y;
            Debug.Log($"今のポジション : {Position} , 行きたいのは{RootQueue.Peek()}");
            Debug.Log($"dx : {dx} , dy : {dy} ");
            
            if (dx == 1 && dy == 1) //upperright
            {
                StartCoroutine(UpperRight(() => { CorrectCallBack(1, 1); },
                    () => { },
                    gameObject));
                RootQueue.Dequeue();
            }

            else if (dx == -1 && dy == 1) //upperleft
            {
                StartCoroutine(UpperLeft(() => { CorrectCallBack(-1, 1); }, 
                    () => { },
                    gameObject));
                RootQueue.Dequeue();
            }

            else if (dx == -1 && dy == -1) //bottomleft
            {
                StartCoroutine(BottomLeft(() => { CorrectCallBack(-1, -1); },
                    () => { },
                    gameObject));
                RootQueue.Dequeue();
            }

            else if (dx == 1 && dy == -1) //bottomright
            {
                StartCoroutine(BottomRight(() => { CorrectCallBack(1, -1); }, 
                    () => { },
                    gameObject));
                RootQueue.Dequeue();
            }
            else if (dx == 1) //right
            {
                StartCoroutine(MoveRight(() => { CorrectCallBack(1, 0); }, 
                    () => { },
                    gameObject));
                RootQueue.Dequeue();
            }

            else if (dx == -1) //left
            {
                StartCoroutine(MoveLeft(() => { CorrectCallBack(-1, 0); }, 
                    () => { },
                    gameObject));
                RootQueue.Dequeue();
            }

            else if (dy == 1) //up
            {
                StartCoroutine(MoveUp(() => { CorrectCallBack(0, 1); },
                    () => { },
                    gameObject));
                RootQueue.Dequeue();
            }

            else if (dy == -1) //down
            {
                StartCoroutine(MoveDown(() => { CorrectCallBack(0, -1); }, 
                    () => { },
                    gameObject));
                RootQueue.Dequeue();
            }

        }
    }
}