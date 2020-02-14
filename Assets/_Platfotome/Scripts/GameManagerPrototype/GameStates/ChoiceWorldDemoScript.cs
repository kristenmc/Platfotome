using UnityEngine;

namespace Platfotome {

    public class ChoiceWorldDemoScript : MonoBehaviour {

        public Transform player;
        public CameraTrackObject track;
        public Canvas choiceTextCanvas;

        private void Awake() {
            CameraController.Instance.RequestTrackMode(player);
            CameraController.Instance.SetCameraTrack(track);
            choiceTextCanvas.worldCamera = CameraController.MainCamera;
        }

    }

}
