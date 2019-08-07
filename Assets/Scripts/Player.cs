using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

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
        Position = new Vector2Int(0,0);
        StartCoroutine(StartMove());
    }

    private IEnumerator StartMove()
    {
        while (true)
        {
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveUp();
            }
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveDown();
            }
            yield return null;
        }
    }

    public void MoveRight()
    {
        Position = new Vector2Int(Position.x+1,Position.y);
    }
    public void MoveLeft()
    {
        Position = new Vector2Int(Position.x-1,Position.y);
    }
    public void MoveUp()
    {
        Position = new Vector2Int(Position.x,Position.y+1);
    }
    public void MoveDown()
    {
        Position = new Vector2Int(Position.x,Position.y-1);
    }
}