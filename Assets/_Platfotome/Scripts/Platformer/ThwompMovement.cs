using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {
    public class ThwompMovement : MonoBehaviour {
        [SerializeField] float detection = 2;
        [SerializeField] float raiseSpeed = 2;
        [SerializeField] SimpleTimer groundWait = new SimpleTimer(1);
        [SerializeField] LayerMask groundCheckLayers = (1 << 8) | (1 << 9);

        Vector2 initPosition = Vector2.zero;

        enum State { Ready, Fall, Landed, Raise }

        State state = State.Ready;

        Rigidbody2D rb = null;
        Collider2D col = null;
        PlayerController player = null;
        Rigidbody2D playerRb = null;

        private void Awake() {
            initPosition = transform.position;
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        private void Start() {
            player = PlayerController.Instance;
            playerRb = player.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            groundWait.Update(Time.fixedDeltaTime);

            switch (state) {
                case State.Ready:
                    if (playerRb != null && rb.position.y > playerRb.position.y && Mathf.Abs(rb.position.x - playerRb.position.x) < detection) {
                        rb.bodyType = RigidbodyType2D.Dynamic;
                        state = State.Fall;
                    }
                    break;
                case State.Landed:
                    if (groundWait.Done) {
                        state = State.Raise;
                    }
                    break;
                case State.Raise:
                    if (rb.position.y + raiseSpeed * Time.fixedDeltaTime > initPosition.y) {
                        rb.MovePosition(initPosition);
                        state = State.Ready;
                    } else {
                        rb.MovePosition(rb.position + new Vector2(0, raiseSpeed * Time.fixedDeltaTime));
                    }
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (state == State.Fall) {
                if (((1 << collision.gameObject.layer) & groundCheckLayers.value) != 0) {
                    List<ContactPoint2D> contactList = new List<ContactPoint2D>();
                    collision.GetContacts(contactList);

                    foreach (ContactPoint2D point in contactList) {
                        if (point.point.y == contactList[0].point.y && point.point.y > rb.position.y) {
                            return;
                        }
                    }

                    groundWait.Start();
                    state = State.Landed;
                    rb.velocity = Vector2.zero;
                    rb.bodyType = RigidbodyType2D.Kinematic;
                } else if (collision.gameObject == player.gameObject && player.IsGrounded()) {
                    List<ContactPoint2D> contactList = new List<ContactPoint2D>();
                    collision.GetContacts(contactList);

                    foreach (ContactPoint2D point in contactList) {
                        if (point.point.y == contactList[0].point.y && point.point.y > rb.position.y) {
                            return;
                        }
                    }

                    player.Die();
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision) {
            if (state == State.Fall) {
                if (collision.gameObject == player.gameObject && player.IsGrounded()) {
                    List<ContactPoint2D> contactList = new List<ContactPoint2D>();
                    collision.GetContacts(contactList);

                    foreach (ContactPoint2D point in contactList) {
                        if (point.point.y == contactList[0].point.y && point.point.y > rb.position.y) {
                            return;
                        }
                    }

                    player.Die();
                }
            }
        }
    }
}