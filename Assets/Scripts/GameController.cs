using System;
using Scripts.MasterDatas;
using UnityEngine;

namespace Scripts
{
    public class GameController : MonoBehaviour
    {
        public Field field;
        public Player player;
        public Enemies enemies;
        
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
            enemies = GameObject.Find("Enemies").GetComponent<Enemies>();
            enemies.Init();
            Instance.enemies.SpawnEnemy();
        }
    }
}