using System;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public GameObject field;
    public static GameController Instance;

    private void Start()
    { 
        Field.MakeInstance();
        field = GameObject.Find("Field");
        field.GetComponent<Field>().Init();
        foreach (var cell in field.GetComponent<Field>().Cells)
        {
            Debug.Log(cell.Position);
        }

    }
}