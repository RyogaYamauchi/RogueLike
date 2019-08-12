using System;
using System.Collections;
using Scripts.MasterDatas;
using UnityEngine;
using UnityEngine.UIElements;

namespace Scripts
{
    public class ICharacter : MonoBehaviour
    {
        private int Id;
        
        Vector2Int _position;
        public int CurrentRoomId;
        public int AttackPower;
        public int DefensePower;
        public int Hp;

        protected Vector2Int Position
        {
            get { return _position; }
            set
            {
                _position = value;
                CurrentRoomId = GameController.Instance.field.Cells.ArrayCells2D[value.x, value.y].ParcelId;
                transform.position = new Vector3(value.x * 11, 5, value.y * 11);
            }
        }
        
        protected IEnumerator MoveRight(Action correctAction,Action failedAction)
        {
            if (OutOfRange(new Vector2Int(Position.x + 1, Position.y)))
            {
                // TODO : アニメーションの作成
                yield return new WaitForSeconds(0.3f);
                failedAction?.Invoke();
                yield break;
            }
            Position = new Vector2Int(Position.x + 1, Position.y);
            correctAction?.Invoke(); 
        }

        protected IEnumerator MoveLeft(Action correctAction,Action failedAction)
        {
            if (OutOfRange(new Vector2Int(Position.x - 1, Position.y)))
            {
                // TODO : アニメーションの作成
                yield return new WaitForSeconds(0.3f);
                failedAction?.Invoke();
                yield break;
            }
            correctAction?.Invoke();
            Position = new Vector2Int(Position.x - 1, Position.y);
        }

        protected IEnumerator MoveUp(Action correctAction,Action failedAction)
        {
            if (OutOfRange(new Vector2Int(Position.x, Position.y + 1)))
            {
                // TODO : アニメーションの作成
                yield return new WaitForSeconds(0.3f);
                failedAction?.Invoke();
                yield break;
            }
            correctAction?.Invoke();
            Position = new Vector2Int(Position.x, Position.y + 1);
        }

        protected IEnumerator MoveDown(Action correctAction,Action failedAction)
        {
            if (OutOfRange(new Vector2Int(Position.x, Position.y - 1)))
            {
                // TODO : アニメーションの作成
                yield return new WaitForSeconds(0.3f);
                failedAction?.Invoke();
                yield break;
            }
            correctAction?.Invoke();
            Position = new Vector2Int(Position.x, Position.y - 1);
        }

        protected bool OutOfRange(Vector2Int position)
        {
            var fieldSize = GameController.Instance.field.FieldSize;
            if (position.x < 0) return true;
            if (position.y < 0) return true;
            if (position.x > fieldSize - 1) return true;
            if (position.y > fieldSize - 1) return true;
            var state = GameController.Instance.field.Cells.ArrayCells2D[position.x, position.y].GetComponent<Cell>()
                .State;
            if (state != MasterFieldData.floor) return true;
            foreach (var enemy in GameController.Instance.enemies.EnemyDictionary.Values)
            {
                if (enemy.Position == position) return true;
            }

            if (GameController.Instance.player.Position == position) return true;
            
            return false;
        }
        public IEnumerator Attack()
        {
            yield break;
        }

    }
}