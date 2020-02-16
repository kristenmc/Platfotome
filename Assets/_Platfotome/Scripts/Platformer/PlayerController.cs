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
        [SerializeField] float jumpBuffer = 0.2f;
        [SerializeField] float coyoteTime = 0.15f;

		Rigidbody2D rb = null;
		Collider2D col = null;

		private SpriteRenderer spriteRenderer;

        float jumpButton = 0;
		bool jumping = false;
		float holdTime = 0;
        float airTime = 0;

		float horizontal = 0;
		float axis = 0;

		void Awake() {
			rb = GetComponent<Rigidbody2D>();
			col = GetComponent<Collider2D>();
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		void Update() {
			if (jumpButton > 0 && airTime < coyoteTime) {
				jumping = true;
				holdTime = jumpTime;
			} else if (jumpButton > 0 && holdTime > 0) {
				holdTime -= Time.deltaTime;
			} else {
				jumping = false;
				holdTime = 0;
			}

			axis = Input.GetAxisRaw("Horizontal");

			if (horizontal * axis < 0 && Mathf.Abs(horizontal) > 0.2f && airTime == 0) {
				horizontal = 0;
			}

			if (axis != 0) {
				if (Input.GetButton("Fire1")) {
					if (airTime == 0) {
						horizontal = Mathf.MoveTowards(horizontal, axis * runSpeed, acceleration * Time.deltaTime);
					} else if (Mathf.Abs(horizontal) < walkSpeed) {
						horizontal = Mathf.MoveTowards(horizontal, axis * walkSpeed, acceleration * Time.deltaTime);
					}
				} else {
					horizontal = Mathf.MoveTowards(horizontal, axis * walkSpeed, acceleration * Time.deltaTime);
				}
			}

            if (Input.GetButtonDown("Jump")) {
                jumpButton = jumpBuffer;
            } else {
                jumpButton -= Time.deltaTime;
            }

			// Flip the direction of the sprite depending on the horizontal velocity
			if (Mathf.Abs(horizontal) > 0.01f) {
				spriteRenderer.flipX = horizontal < 0f;
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
				velocity.y = jump;
			}

			rb.velocity = velocity;

            if (rb.position.y < minYPosition) {
                Die();
            }

            if (IsGrounded()) {
                airTime = 0;
            } else {
                airTime += Time.fixedDeltaTime;
            }
		}

		public bool IsGrounded() {
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
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			CameraController.Instance.RequestScreenShake(Constants.Screenshake.PlayerDeath, Vector2.down);
			Invoke("DieInternal", 0.4f);
        }

		private void DieInternal() {
			if (GameManager.InGameState(typeof(ChoiceWorldState))) {
				if (GameManager.StateArgs.TryGetValue(ChoiceWorldState.LevelKey, out string val)) {
					GameManager.RequestStateTransition(new ChoiceWorldState(val));
				}
			}
			Destroy(gameObject);
		}

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(new Vector3(-100, minYPosition), new Vector3(200, 0));
        }
    }
}