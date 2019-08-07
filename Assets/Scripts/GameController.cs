using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        public Field Field;
        public GameController Instance;
        private void Start()
        {
            GameController Instance = new GameController();
            Field = Field.Init();
            foreach (var cell in Field.Cells)
            {
                Debug.Log(cell.Id);
            }
        }
        
        
    }
}