using System.Collections;
using Scripts.MasterDatas;
using UnityEngine;

namespace Scripts
{


    public class Player : ICharacter
    {
        public void Init()
        {
            SetInitPosition();
            StartTurn();
        }

        public void SetInitPosition()
        {
            var roomId = Random.Range(1, GameController.Instance.field.RoomsDictionary.Count);
            var room = GameController.Instance.field.RoomsDictionary[roomId];
            var x = Random.Range(room.X, room.X + room.XRange);
            var y = Random.Range(room.Y, room.Y + room.YRange);
            Position = new Vector2Int(x, y);
        }
        private IEnumerator StartMove()
        {
            while (true)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    StartCoroutine(MoveRight(
                            () =>
                            {
                                if (GameController.Instance.field.Cells.ArrayCells2D[Position.x, Position.y].State ==
                                    MasterFieldData.stair)
                                {
                                    StartCoroutine(GameController.Instance.field.GoUpTheStair());
                                    return;
                                }
                                GameController.Instance.field.Cells.ArrayCells2D[Position.x, Position.y].OnState =
                                    MasterFieldOnState.None;
                                GameController.Instance.field.Cells.ArrayCells2D[Position.x+1, Position.y].OnState =
                                    MasterFieldOnState.Player;
                                EndTurn(); }, 
                            () => { StartCoroutine(StartMove()); }));
                    yield break;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    StartCoroutine(MoveLeft(() => { 
                        if (GameController.Instance.field.Cells.ArrayCells2D[Position.x, Position.y].State ==
                            MasterFieldData.stair)
                        {
                            StartCoroutine(GameController.Instance.field.GoUpTheStair());
                            return;
                        }
                        GameController.Instance.field.Cells.ArrayCells2D[Position.x, Position.y].OnState =
                            MasterFieldOnState.None;
                        GameController.Instance.field.Cells.ArrayCells2D[Position.x-1, Position.y].OnState =
                            MasterFieldOnState.Player;
                    EndTurn(); 
                    }, () => { StartCoroutine(StartMove()); }));
                    yield break;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    StartCoroutine(MoveUp(() =>
                    {
                        if (GameController.Instance.field.Cells.ArrayCells2D[Position.x, Position.y].State ==
                            MasterFieldData.stair)
                        {
                            StartCoroutine(GameController.Instance.field.GoUpTheStair());
                            return;
                        }
                        GameController.Instance.field.Cells.ArrayCells2D[Position.x, Position.y].OnState =
                            MasterFieldOnState.None;
                        GameController.Instance.field.Cells.ArrayCells2D[Position.x, Position.y+1].OnState =
                            MasterFieldOnState.Player;
                        EndTurn();
                    }, () => { StartCoroutine(StartMove()); }));
                    yield break;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    StartCoroutine(MoveDown(() =>
                    {
                        if (GameController.Instance.field.Cells.ArrayCells2D[Position.x, Position.y].State ==
                            MasterFieldData.stair)
                        {
                            StartCoroutine(GameController.Instance.field.GoUpTheStair());
                            return;
                        }
                        GameController.Instance.field.Cells.ArrayCells2D[Position.x, Position.y].OnState =
                            MasterFieldOnState.None;
                        GameController.Instance.field.Cells.ArrayCells2D[Position.x, Position.y-1].OnState =
                            MasterFieldOnState.Player;
                        EndTurn();
                    }, () => { StartCoroutine(StartMove()); }));
                    yield break;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }

        public void StartTurn()
        {
            Debug.Log("プレイヤーのターン");
            StartCoroutine(StartMove());
        }

        public void EndTurn()
        {
           GameController.Instance.enemies.StartTurn();
        }
    }
}