using GameCore;
using UnityEngine;

namespace AroundEnvironmentBehaivour
{
    public class EnvironmentController : MonoBehaviour
    {
        [SerializeField]
        private GameObject envMovement;

        [SerializeField]
        private GameObject envObject;//лень менять ту логику

        private void Start()
        {
            envMovement.SetActive(false);
            envObject.SetActive(false);
            GameStateController.instance.GameStarted += () =>
            {
                envObject.SetActive(true);
                envMovement.SetActive(true);
            };
        }
    }
}
