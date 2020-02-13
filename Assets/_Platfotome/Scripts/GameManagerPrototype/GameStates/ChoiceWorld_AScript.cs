using UnityEngine;

namespace Platfotome {
    public class ChoiceWorld_AScript : MonoBehaviour {

        public Transform player;
        public CameraTrackObject track;

        private void Awake() {
            CameraController.Instance.RequestTrackMode(player);
            CameraController.Instance.SetCameraTrack(track);
        }
    }
}
