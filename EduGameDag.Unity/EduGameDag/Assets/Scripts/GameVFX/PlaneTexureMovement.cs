using UnityEngine;

namespace GameVFX
{
    public class PlaneTexureMovement : MonoBehaviour
    {
        [SerializeField]
        private float scrollSpeed = 0.5f;

        [SerializeField]
        private Renderer planeRenderer;

        private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");

        void Update()
        {
            float offset = Time.time * scrollSpeed;
            planeRenderer.material.SetTextureOffset(BaseMap, new Vector2(0, -offset));
        }
    }
}