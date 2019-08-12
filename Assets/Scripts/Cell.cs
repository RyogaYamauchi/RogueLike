using Scripts.MasterDatas;
using UnityEngine;

namespace Scripts
{



    public class Cell : MonoBehaviour
    {
        public int ParcelId;
        private Vector2Int _position;
        private int _state;
        public int Id;

        public Vector2Int Position
        {
            get { return _position; }
            set
            {
                _position = value;
                transform.position = new Vector3(value.x * 11, 0, value.y * 11);
            }
        }

        public int State
        {
            get { return _state; }
            set
            {
                GetComponent<Renderer>().material.color = MasterField.GetColor(value);
                _state = value;
            }
        }
    }
}