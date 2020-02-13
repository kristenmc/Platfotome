using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {
	public class PlayerController : MonoBehaviour {
		[SerializeField] float walkSpeed = 4;
		[SerializeField] float runSpeed = 7;
		[SerializeField] float jump = 3;
		[SerializeField] float jumpTime = 0.5f;
		[SerializeField] float acceleration = 10;
		[SerializeField] float decelerationFactor = 0.85f;
		[SerializeField] LayerMask groundCheckLayers = 1 << 8;
        [SerializeField] float minYPosition = -50;

		Rigidbody2D rb = null;
		Collider2D col = null;

		bool jumping = false;
		float holdTime = 0;
		float horizontal = 0;
		float axis = 0;

		void Awake() {
			rb = GetComponent<Rigidbody2D>();
			col = GetComponent<Collider2D>();
		}

		void Update() {
			if (Input.GetButtonDown("Jump") && isGrounded()) {
				jumping = true;
				holdTime = jumpTime;
			} else if (Input.GetButton("Jump") && holdTime > 0) {
				holdTime -= Time.deltaTime;
			} else {
				jumping = false;
				holdTime = 0;
			}

			axis = Input.GetAxisRaw("Horizontal");

			if (horizontal * axis < 0 && Mathf.Abs(horizontal) > 0.2f && isGrounded()) {
				horizontal = 0;
			}

			if (axis != 0) {
				if (Input.GetButton("Fire1")) {
					if (isGrounded()) {
						horizontal = Mathf.MoveTowards(horizontal, axis * runSpeed, acceleration * Time.deltaTime);
					} else if (Mathf.Abs(horizontal) < walkSpeed) {
						horizontal = Mathf.MoveTowards(horizontal, axis * walkSpeed, acceleration * Time.deltaTime);
					}
				} else {
					horizontal = Mathf.MoveTowards(horizontal, axis * walkSpeed, acceleration * Time.deltaTime);
				}
			}
		}

		void FixedUpdate() {
			Vector2 velocity = rb.velocity;
			velocity.x = horizontal;

			if (axis == 0) {
				if (Mathf.Abs(horizontal) > 0.2f) {
					horizontal *= decelerationFactor;
				} else {
					horizontal = 0;
				}
			}

			if (jumping) {
				velocity.y = 5;
			}

			rb.velocity = velocity;

            if (rb.position.y < minYPosition) {
                Die();
            }
		}

		public bool isGrounded() {
			RaycastHit2D hit = Physics2D.BoxCast(new Vector2(col.bounds.center.x, col.bounds.min.y), new Vector2(col.bounds.size.x, 0.01f), 0, Vector2.down, 0.01f, groundCheckLayers.value);
			return hit.transform != null;
		}

        void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.layer == 9) {
                Die();
            }
        }

        void Die() {
            enabled = false;
            Destroy(gameObject, 1f);
            if (GameManager.InGameState(typeof(ChoiceWorldState))) {
                if (GameManager.StateArgs.TryGetValue(ChoiceWorldState.LevelKey, out string val)) {
                    GameManager.RequestStateTransition(new ChoiceWorldState(val, ""));
                }
            }
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(new Vector3(-100, minYPosition), new Vector3(200, 0));
        }
    }
}