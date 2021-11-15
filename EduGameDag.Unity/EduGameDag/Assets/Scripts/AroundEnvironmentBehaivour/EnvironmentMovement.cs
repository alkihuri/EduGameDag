using UnityEngine;

namespace AroundEnvironmentBehaivour
{
    public class EnvironmentMovement : MonoBehaviour
    {
        [SerializeField]
        private float scrollSpeed;

        [SerializeField]
        private int packCount =10;
    
        [SerializeField]
        private GameObject[] envPacks;
    
        private void Start()
        {
            float counter = 0;
            for (int i = 0; i < packCount; i++)
            {
                var envPrefab = Instantiate(envPacks[UnityEngine.Random.Range(0, envPacks.Length)],
                    new Vector3(-11,0,counter), Quaternion.Euler(-90, 0, 0));
                envPrefab.transform.parent = this.transform;
                counter += 21f;
            }
            counter = 0f;
            for (int i = 0; i < packCount; i++)
            {
                var envPrefab = Instantiate(envPacks[UnityEngine.Random.Range(0, envPacks.Length)],
                    new Vector3(17,0,counter),Quaternion.Euler(-90,0,0));
                envPrefab.transform.parent = this.transform;
                counter += 21f;
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.position -= new Vector3(0, 0, scrollSpeed * Time.deltaTime/10);
        }
    }
}
