using System;
using System.Collections;
using Scripts.MasterDatas;
using UnityEngine;

namespace Scripts
{
    public class ICharacter : MonoBehaviour
    {
        public int Id;
        Vector2Int _position;
        public int CurrentRoomId;
        public int AttackPower;
        public int DefensePower;
        public int Hp;

        public Vector2Int Position
        {
            get { return _position; }
            set
            {
                _position = value;
                CurrentRoomId = GameController.Instance.field.Cells.ArrayCells2D[value.x, value.y].ParcelId;
            }
        }
        
        protected IEnumerator MoveRight(Action correctAction,Action failedAction, GameObject obj)
        {
            if (OutOfRange(new Vector2Int(Position.x + 1, Position.y)))
            {
                yield return new WaitForSeconds(0.3f);
                failedAction?.Invoke();
                yield break;
            }
            Quaternion rot = Quaternion.AngleAxis(90.0f, new Vector3(0f,1.0f,0.0f));
            obj.transform.rotation = rot;
            obj.GetComponent<Animator>().SetTrigger("is_going");
            yield return new WaitForSeconds(0.5f);

            Position = new Vector2Int(Position.x + 1, Position.y);
            correctAction?.Invoke(); 
        }

        protected IEnumerator MoveLeft(Action correctAction,Action failedAction, GameObject obj)
        {
            if (OutOfRange(new Vector2Int(Position.x - 1, Position.y)))
            {
                yield return new WaitForSeconds(0.3f);
                failedAction?.Invoke();
                yield break;
            }
            Quaternion rot = Quaternion.AngleAxis(-90.0f, new Vector3(0f,1.0f,0f));
            obj.transform.rotation = rot;
            obj.GetComponent<Animator>().SetTrigger("is_going");
            yield return new WaitForSeconds(0.5f);
            correctAction?.Invoke();
            Position = new Vector2Int(Position.x - 1, Position.y);
        }

        protected IEnumerator MoveUp(Action correctAction,Action failedAction, GameObject obj)
        {
            if (OutOfRange(new Vector2Int(Position.x, Position.y + 1)))
            {
                yield return new WaitForSeconds(0.3f);
                failedAction?.Invoke();
                yield break;
            }
            Quaternion rot = Quaternion.AngleAxis(0.0f, new Vector3(1f,0.0f,1.0f));
            obj.transform.rotation = rot;
            obj.GetComponent<Animator>().SetTrigger("is_going");
            yield return new WaitForSeconds(0.5f);
            correctAction?.Invoke();
            Position = new Vector2Int(Position.x, Position.y + 1);
        }

        protected IEnumerator MoveDown(Action correctAction, Action failedAction, GameObject obj)
        {
            if (OutOfRange(new Vector2Int(Position.x, Position.y - 1)))
            {
                yield return new WaitForSeconds(0.3f);
                failedAction?.Invoke();
                yield break;
            }
            Quaternion rot = Quaternion.AngleAxis(180.0f, new Vector3(0f,1.0f,0f));
            obj.transform.rotation = rot;     
            obj.GetComponent<Animator>().SetTrigger("is_going");
            yield return new WaitForSeconds(0.5f);
            correctAction?.Invoke();
            Position = new Vector2Int(Position.x, Position.y - 1);
        }

        protected IEnumerator UpperRight(Action correctAction, Action failedAction, GameObject obj)
        {
            if (OutOfRange(new Vector2Int(Position.x+1, Position.y + 1)))
            {
                yield return new WaitForSeconds(0.3f);
                failedAction?.Invoke();
                yield break;
            }

            Quaternion rot = Quaternion.AngleAxis(45.0f, new Vector3(0f,1.0f,0.0f));
            obj.transform.rotation = rot;         
            obj.GetComponent<Animator>().SetTrigger("is_diagonal_going");
            yield return new WaitForSeconds(0.5f);
            correctAction?.Invoke();
            Position = new Vector2Int(Position.x + 1, Position.y + 1);
        }
        protected IEnumerator UpperLeft(Action correctAction, Action failedAction, GameObject obj)
        {
            if (OutOfRange(new Vector2Int(Position.x - 1, Position.y + 1)))
            {
                yield return new WaitForSeconds(0.3f);
                failedAction?.Invoke();
                yield break;
            }

            Quaternion rot = Quaternion.AngleAxis(-45.0f, new Vector3(0.0f,1.0f,0.0f));
            obj.transform.rotation = rot;  
            obj.GetComponent<Animator>().SetTrigger("is_diagonal_going");
            yield return new WaitForSeconds(0.5f);
            correctAction?.Invoke();
            Position = new Vector2Int(Position.x - 1, Position.y + 1);
        }
        protected IEnumerator BottomRight(Action correctAction, Action failedAction, GameObject obj)
        {
            if (OutOfRange(new Vector2Int(Position.x + 1, Position.y - 1)))
            {
                yield return new WaitForSeconds(0.3f);
                failedAction?.Invoke();
                yield break;
            }

            Quaternion rot = Quaternion.AngleAxis(135.0f, new Vector3(0f,1.0f,0.0f));
            obj.transform.rotation = rot;          
            obj.GetComponent<Animator>().SetTrigger("is_diagonal_going");
            yield return new WaitForSeconds(0.5f);
            correctAction?.Invoke();
            Position = new Vector2Int(Position.x + 1, Position.y - 1);
        }
        protected IEnumerator BottomLeft(Action correctAction, Action failedAction, GameObject obj)
        {
            if (OutOfRange(new Vector2Int(Position.x - 1, Position.y - 1)))
            {
                yield return new WaitForSeconds(0.3f);
                failedAction?.Invoke();
                yield break;
            }

            Quaternion rot = Quaternion.AngleAxis(-135.0f, new Vector3(0f,1.0f,0f));
            obj.transform.rotation = rot;        
            obj.GetComponent<Animator>().SetTrigger("is_diagonal_going");
            yield return new WaitForSeconds(0.5f);
            correctAction?.Invoke();
            Position = new Vector2Int(Position.x - 1, Position.y - 1);
        }

        public static bool OutOfRange(Vector2Int position)
        {
            var fieldSize = GameController.Instance.field.FieldSize;
            if (position.x < 0) return true;
            if (position.y < 0) return true;
            if (position.x > fieldSize - 1) return true;
            if (position.y > fieldSize - 1) return true;
            var state = GameController.Instance.field.Cells.ArrayCells2D[position.x, position.y].GetComponent<Cell>()
                .State;
            if (state != MasterFieldData.floor && state!= MasterFieldData.stair) return true;
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