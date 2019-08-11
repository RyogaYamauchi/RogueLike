using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2Int _position;
    public int Attack;
    public int Defense;

    public Vector2Int Position
    {
        get
        {
            return _position;
            
        }
        set
        {
            _position = value;
            transform.position = new Vector3(value.x*11,5,value.y*11);
        }
    }
    

    public void Init()
    {
        SetInitPosition();
        StartCoroutine(StartMove());
    }

    public void SetInitPosition()
    {
        var roomId = Random.Range(0, GameController.Instance.field.RoomsDictionary.Count);
        var room = GameController.Instance.field.RoomsDictionary[roomId];
        var x = Random.Range(room.X, room.X + room.XRange);
        var y = Random.Range(room.Y, room.Y + room.YRange);
        Position = new Vector2Int(x,y);
    }

    private IEnumerator StartMove()
    {
        while (true)
        {
            if(Input.GetKey(KeyCode.D))
            {
                MoveRight();
            }
            if(Input.GetKey(KeyCode.A))
            {
                MoveLeft();
            }
            if(Input.GetKey(KeyCode.W))
            {
                MoveUp();
            }
            if(Input.GetKey(KeyCode.S))
            {
                MoveDown();
            }

            yield return new WaitForSeconds(0.15f);
        }
    }

    public void MoveRight()
    {
        if (OutOfRange(new Vector2Int(Position.x+1,Position.y)))
        {
            return;
        }
        Position = new Vector2Int(Position.x+1,Position.y);
    }
    public void MoveLeft()
    {
        if (OutOfRange(new Vector2Int(Position.x-1,Position.y)))
        {
            return;
        }
        Position = new Vector2Int(Position.x-1,Position.y);
    }
    public void MoveUp()
    {
        if (OutOfRange(new Vector2Int(Position.x,Position.y+1)))
        {
            return;
        }
        Position = new Vector2Int(Position.x,Position.y+1);
    }
    public void MoveDown()
    {
        if (OutOfRange(new Vector2Int(Position.x,Position.y-1)))
        {
            return;
        }
        Position = new Vector2Int(Position.x,Position.y-1);
    }

    private bool OutOfRange(Vector2Int position)
    {
        var fieldSize = GameController.Instance.field.FieldSize;
        if (position.x < 0) return true;
        if (position.y < 0) return true;
        if (position.x > fieldSize-1) return true;
        if (position.y > fieldSize-1) return true;
        var state = GameController.Instance.field.Cells.ArrayCells2D[position.x, position.y].GetComponent<Cell>().State;
        if (state != MasterFieldData.floor) return true;
        return false;
    }
}