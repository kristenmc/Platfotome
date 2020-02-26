using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {
	public class PlayerController : MonoBehaviour {
        public static PlayerController Instance { get; private set; }

		[SerializeField] float walkSpeed = 8f;
		[SerializeField] float runSpeed = 15f;
		[SerializeField] float jump = 7f;
		[SerializeField] float jumpTime = 1.5f;
        [SerializeField] float gravityScaleOnHold = 0.2f;
        [SerializeField] float maxFallSpeed = 20f;
		[SerializeField] float acceleration = 60f;
		[SerializeField] float decelerationFactor = 0.85f;
		[SerializeField] LayerMask groundCheckLayers = 1 << 8;
        [SerializeField] float jumpBuffer = 0.2f;
        [SerializeField] float coyoteTime = 0.15f;

		Rigidbody2D rb = null;
		Collider2D col = null;
        Animator anim = null;

		private SpriteRenderer spriteRenderer;

        float jumpButton = 0;
		bool jumping = false;
		float holdTime = 0;
        float airTime = 0;

		float horizontal = 0;
		float axis = 0;

		void Awake() {
            Instance = this;
            rb = GetComponent<Rigidbody2D>();
			col = GetComponent<Collider2D>();
            anim = GetComponent<Animator>();
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		void Update() {
			if (jumpButton > 0 && airTime < coyoteTime) {
				jumping = true;
                jumpButton = 0;
				holdTime = jumpTime;
			} else if (Input.GetButton("Jump") && holdTime > 0) {
				holdTime -= Time.deltaTime;
			} else {
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
					} else if (horizontal * Mathf.Sign(axis) < walkSpeed) {
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

            anim.SetFloat("Horizontal", Mathf.Abs(horizontal));

			if (jumping) {
				velocity.y = jump;
                jumping = false;
			}

            if (velocity.y < -maxFallSpeed) {
                velocity.y = -maxFallSpeed;
            }

            if (holdTime > 0) {
                rb.gravityScale = Mathf.Lerp(1, gravityScaleOnHold, holdTime / jumpTime);
            } else {
                rb.gravityScale = 1f;
            }

            anim.SetFloat("Vertical", velocity.y);

            rb.velocity = velocity;

            if (IsGrounded()) {
                anim.SetBool("Grounded", true);
                airTime = 0;
            } else {
                anim.SetBool("Grounded", false);
                airTime += Time.fixedDeltaTime;
            }
        }

		public bool IsGrounded() {
            return Physics2D.OverlapBox(new Vector2(col.bounds.center.x, col.bounds.min.y), new Vector2(col.bounds.size.x - 0.15f, 0.01f), 0, groundCheckLayers.value) != null;
        }

        void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.layer == 9) {
                Die();
            }
        }

        public void Die() {
            anim.SetTrigger("Die");
            enabled = false;
            rb.simulated = false;
			CameraController.Instance.RequestScreenShake(Constants.Screenshake.PlayerDeath);
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

        private void OnDestroy() {
            Instance = null;
        }

        private void OnCollisionStay2D(Collision2D collision) {
            if (((1 << collision.gameObject.layer) & groundCheckLayers.value) != 0) {
                ContactPoint2D[] contactList = new ContactPoint2D[collision.contactCount];
                collision.GetContacts(contactList);

                float dist = 0;

                for (int i = 0; i < collision.contactCount; i++) {
                    ContactPoint2D point = contactList[i];

                    if (Mathf.Abs(point.point.y - contactList[0].point.y) > 0.01f || point.point.y > rb.position.y) {
                        return;
                    }
                    dist += Mathf.Abs(point.point.x - contactList[0].point.x);
                }
                
                if (dist < 0.15f && (contactList[0].point.x - rb.position.x) * horizontal >= 0) {
                    rb.MovePosition(rb.position + (contactList[0].point.x < rb.position.x ? Vector2.left : Vector2.right) * (0.15f - dist));
                }
            }
        }
    }
}