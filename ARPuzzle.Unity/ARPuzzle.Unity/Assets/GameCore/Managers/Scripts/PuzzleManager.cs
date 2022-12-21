using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject _part;
    [SerializeField, Range(1, 10)] private float _size;
    [SerializeField] List<GameObject> _puzzleParts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (float x = 0; x < _size; x++)
        {
            for (float z = 0; z < _size; z++)
            {
                GameObject newPart = Instantiate(_part, transform);

                var startPos = new Vector3((x - 2f) * _size * 2 - (_size / 2), 1, (z - 2) * _size * 2 - (_size / 2));
                newPart.transform.DOMove(startPos, x + z);
                ;
                /*    = new Vector3
                        (Random.Range(-_size * 2, _size * 2),
                        0,
                        Random.Range(-_size * 2, _size * 2));
                */
                newPart.GetComponentInChildren<Renderer>().material.SetFloat("_size", _size);
                newPart.GetComponentInChildren<Renderer>().material.SetFloat("_x", x);
                newPart.GetComponentInChildren<Renderer>().material.SetFloat("_y", z);

                _puzzleParts.Add(newPart);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
