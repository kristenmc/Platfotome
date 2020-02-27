using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platfotome {
    public class SteppedOn : MonoBehaviour {
        PlayerController player = null;
        Rigidbody2D rb = null;

        [SerializeField] UnityEvent onStep;

        void Start() {
            player = PlayerController.Instance;
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject == player.gameObject) {
                ContactPoint2D[] contactList = new ContactPoint2D[collision.contactCount];
                collision.GetContacts(contactList);

                for (int i = 0; i < collision.contactCount; i++) {
                    ContactPoint2D point = contactList[i];
                    if (Mathf.Abs(point.point.y - contactList[0].point.y) > 0.01f || point.point.y < rb.position.y) {
                        return;
                    }
                }

                onStep.Invoke();
            }
        }
    }
}