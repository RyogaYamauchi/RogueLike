using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Vector2Int _position;
    public int Id;
    public int State;
    public Vector2Int Position
    {
        get 
        {
            return _position;
        }
        set
        {
            _position = value;
            transform.position = new Vector3(value.x*11, 0, value.y*11);
        }
    }
}
