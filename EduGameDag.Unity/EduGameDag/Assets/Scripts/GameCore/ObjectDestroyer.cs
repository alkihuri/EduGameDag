using UnityEngine;

namespace GameCore
{
    public class ObjectDestroyer : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}
