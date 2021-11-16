using UnityEngine;

namespace AroundEnvironmentBehaivour
{
    public class EnvironmentMovement : MonoBehaviour
    {
        [SerializeField]
        private float scrollSpeed;

        [SerializeField]
        private int packCount = 10;

        [SerializeField]
        private GameObject[] envPacks;

        private bool flag_counter = false;
        float counter = 0;

        private void Start()
        {
            for (int i = 0; i < packCount; i++)
            {
                var envPrefab = Instantiate(envPacks[UnityEngine.Random.Range(0, envPacks.Length)]);
                envPrefab.transform.parent = this.transform;
                envPrefab.transform.localPosition = new Vector3(-11, 0, counter);
                envPrefab.transform.rotation = Quaternion.Euler(-90,0,0);
                counter += 21f;
            }
            
            counter = 0f;
            for (int i = 0; i < packCount; i++)
            {
                var envPrefab = Instantiate(envPacks[UnityEngine.Random.Range(0, envPacks.Length)]);
                envPrefab.transform.parent = this.transform;
                envPrefab.transform.localPosition = new Vector3(17, 0, counter);
                envPrefab.transform.rotation = Quaternion.Euler(-90,0,0);
                counter += 21f;
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.position -= new Vector3(0, 0, scrollSpeed * Time.deltaTime / 10);
        }

        public void RecreateEnvironment(Transform packTransform)
        {
            Debug.Log("recreated");
            if (flag_counter == false)
            {
                packTransform.localPosition = new Vector3(packTransform.localPosition.x, packTransform.localPosition.y, counter);
                flag_counter = true;
                Debug.Log("recreated by false");
            }
            else
            {
                flag_counter = false;
                packTransform.localPosition = new Vector3(packTransform.localPosition.x, packTransform.localPosition.y, counter);
                counter += 21f;
                Debug.Log("recreated by true");
            }
        }
    }
}
