using System;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public GameObject field;
    public static GameController Instance;
    public GameObject player;

    private void Start()
    { 
        Field.MakeInstance();
        field = GameObject.Find("Field");
        field.GetComponent<Field>().Init();
        player = GameObject.Find("Player");
        player.GetComponent<Player>().Init();

    }
}