using System;
using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class MainCamera : MonoBehaviour
    {
        [SerializeField]
        public Camera Camera;
        public bool IsMain;
        public Animator Animator;

        private void Start()
        {
            IsMain = true;
        }

        public IEnumerator UpdateCameraOnPlayer()
        {
            while (IsMain)
            {
                var playerPosition = GameController.Instance.player.GetComponent<Transform>().position;
                Camera.transform.position = new Vector3(playerPosition.x, 31, playerPosition.z - 50);
                Quaternion rot = Quaternion.AngleAxis(23.0f, new Vector3(1.0f, 0f, 0.0f));
                Camera.transform.rotation = rot;
                yield return null;
            }
        }
    }
}