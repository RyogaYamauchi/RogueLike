using System;
using DefaultNamespace;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public Field field;
    public Player player;

    public static GameController Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        MasterField.Init();
        Field.MakeInstance();
        field = GameObject.Find("Field").GetComponent<Field>();
        field.Init();
        player = GameObject.Find("Player").GetComponent<Player>();
        player.Init();
    }
}