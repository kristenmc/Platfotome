using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {
    public class CameraViewport : MonoBehaviour {
        public static float WidescreenRatio { get { return 16f / 9f; } }

        Camera cam;

        void Awake() {
            cam = GetComponent<Camera>();
        }

        void Update() {
            float ratio = (float) Screen.width / Screen.height;

            if (ratio > WidescreenRatio) {
                float width = WidescreenRatio * Screen.height;
                cam.pixelRect = new Rect((Screen.width - width) / 2, 0, width, Screen.height);
            } else if (ratio < WidescreenRatio) {
                float height = Screen.width / WidescreenRatio;
                cam.pixelRect = new Rect(0, (Screen.height - height) / 2, Screen.width, height);
            } else {
                cam.rect = new Rect(0, 0, 1, 1);
            }
        }
    }
}