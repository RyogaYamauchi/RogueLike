using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts
{
    public class Enemy : ICharacter
    {
        public int Id;
        
        public void Spawn(int id)
        {
            Id = id;
            while (true)
            {
                var roomId = Random.Range(1, GameController.Instance.field.RoomsDictionary.Count);
                if (roomId == GameController.Instance.player.CurrentRoomId) Spawn(id);
                var room = GameController.Instance.field.RoomsDictionary[roomId];
                var x = Random.Range(room.X, room.X + room.XRange);
                var y = Random.Range(room.Y, room.Y + room.YRange);
                Position = new Vector2Int(x, y);
                return;
            }
        }

        // TODO : AIの実装
        public IEnumerator Action(Action action)
        {
            Debug.Log("Moveするで");
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Moveしたで");
            action?.Invoke();
        }
    }
}