using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {
    public class DeathBarrier : MonoBehaviour {
        enum Direction { Below, Left, Above, Right }

        [SerializeField] Direction direction = Direction.Below;

        PlayerController player = null;
        Rigidbody2D playerRb = null;

        Dictionary<Direction, Func<Vector2, bool>> checker = new Dictionary<Direction, Func<Vector2, bool>>();
        

        void Awake() {
            checker.Add(Direction.Below, (vec) => vec.y < transform.position.y);
            checker.Add(Direction.Left, (vec) => vec.x < transform.position.x);
            checker.Add(Direction.Above, (vec) => vec.y > transform.position.y);
            checker.Add(Direction.Right, (vec) => vec.x > transform.position.x);
        }

        void Start() {
            player = PlayerController.Instance;
            playerRb = player.GetComponent<Rigidbody2D>();
        }
        
        void FixedUpdate() {
            if (player != null && player.enabled && checker[direction](playerRb.position)) {
                player.Die();
            }
        }

        void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;

            switch (direction) {
                case Direction.Below:
                case Direction.Above:
                    Gizmos.DrawRay(transform.position + Vector3.left * 100, Vector3.right * 200);
                    break;
                case Direction.Left:
                case Direction.Right:
                    Gizmos.DrawRay(transform.position + Vector3.down * 100, Vector3.up * 200);
                    break;
            }
        }
    }
}