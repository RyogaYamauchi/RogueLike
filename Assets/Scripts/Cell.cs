using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int Position;
    public int Id;
    public int State;

    public void Init()
    {
        Position = new Vector2Int((int)GetComponent<Transform>().position.x, (int)GetComponent<Transform>().position.y);
    }
}
