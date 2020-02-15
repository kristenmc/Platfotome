using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {
    public class ParallaxBackground : MonoBehaviour {
        [SerializeField] float parallax = 0.95f;
        [SerializeField] float origin = 0;

        void Awake() {
            CameraController.OnCameraMove += UpdatePosition;
        }

        void UpdatePosition(Vector2 position) {
            transform.position = Vector2.right * (position.x * parallax + origin);
        }

        private void OnDestroy() {
            CameraController.OnCameraMove -= UpdatePosition;
        }
    }

}