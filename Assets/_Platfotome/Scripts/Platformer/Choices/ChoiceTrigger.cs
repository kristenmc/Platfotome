using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {
    public class ChoiceTrigger : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("Player")) {
                GameManager.RequestStateTransition(new OverworldState());
            }
        }
    }
}