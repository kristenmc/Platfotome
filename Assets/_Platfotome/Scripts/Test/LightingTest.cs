using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platfotome {
    public class LightingTest : MonoBehaviour {
        [SerializeField] Texture2D texture = null;
        [SerializeField] Rigidbody2D source = null;
        [SerializeField] Vector2 offset = Vector2.zero;
        [SerializeField] float range = 8;
        [SerializeField] int raycasts = 360;
        [SerializeField] LayerMask groundCheckLayers = (1 << 8) | (1 << 9);

        MeshRenderer mr = null;
        MeshFilter mf = null;

        Vector2[] angles = new Vector2[0];
        float[] distances = new float[0];

        void Awake() {
            mr = GetComponent<MeshRenderer>();
            mr.material.mainTexture = texture;

            mf = GetComponent<MeshFilter>();

            angles = new Vector2[raycasts];
            distances = new float[raycasts];

            for (int i = 0; i < raycasts; i++) {
                float angle = Mathf.PI * 2 * i / raycasts;
                angles[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * range;
            }
        }

        void FixedUpdate() {
            if (source == null) {
                return;
            }

            for (int i = 0; i < raycasts; i++) {
                RaycastHit2D hit = Physics2D.Linecast(source.position + offset, source.position + offset + angles[i], groundCheckLayers.value);
                distances[i] = hit.transform == null ? 1 : hit.distance / range;
            }

            Mesh mesh = new Mesh();
            Vector3[] vertices = new Vector3[raycasts * 2 + 1];
            Vector2[] uvs = new Vector2[raycasts * 2 + 1];
            int[] triangles = new int[raycasts * 3];
            vertices[0] = source.position + offset;
            uvs[0] = Vector2.zero;

            for (int i = 0; i < raycasts; i++) {
                vertices[i * 2 + 1] = source.position + offset + angles[i] * distances[i];
                vertices[i * 2 + 2] = source.position + offset + angles[(i + 1) % raycasts] * distances[(i + 1) % raycasts];
                uvs[i * 2 + 1] = Vector2.right * distances[i];
                uvs[i * 2 + 2] = Vector2.right * distances[(i + 1) % raycasts];

                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i * 2 + 1;
                triangles[i * 3 + 2] = i * 2 + 2;
            }

            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;
            mf.mesh = mesh;
        }
    }
}