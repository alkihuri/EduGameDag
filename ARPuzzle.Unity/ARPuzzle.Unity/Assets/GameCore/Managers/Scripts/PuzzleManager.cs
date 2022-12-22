using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject _part;
    [SerializeField, Range(1, 10)] private float _size;
    [SerializeField] List<PartController> _puzzleParts = new List<PartController>();
    [SerializeField] private float _ratio;

    public float Size { get => _size; set => _size = value; }
    public List<PartController> PuzzleParts { get => _puzzleParts; set => _puzzleParts = value; }
    public float Ratio { get => _ratio; set => _ratio = value; }

    private void Awake()
    {
        Cashing();
        Settings();
    }

    private void Settings()
    {

        Ratio = 16 / 9;
        InnitPuzzlePool();
    }

    private void Cashing()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        ShowRight();
    }


    private void InnitPuzzlePool()
    {
        for (float x = 0; x < Size; x++)
        {
            for (float z = 0; z < Size; z++)
            {
                GameObject newPart = Instantiate(_part, transform);
                PartController partController = newPart.GetComponent<PartController>();
                partController.ID = (int)((x + 1) * (z + 1));
                partController.X = (int)((x) * 11);
                partController.Y = (int)((z) * 11);
                partController.Ratio = Ratio;
                ShaderSettings(x, z, newPart);
                PuzzleParts.Add(partController);
            }
        }
    }

    private void ShaderSettings(float x, float z, GameObject newPart)
    {
        var newPartRenderer = newPart.GetComponentInChildren<Renderer>();
        newPartRenderer.material.SetFloat("_size", Size);
        newPartRenderer.material.SetFloat("_ratio", Ratio);
        newPartRenderer.material.SetFloat("_x", x);
        newPartRenderer.material.SetFloat("_y", z);
    }

    public void ShowRight()
    {
        StartCoroutine(RightPOstionDelayed());
    }
    private IEnumerator RightPOstionDelayed()
    {
        foreach (PartController part in _puzzleParts)
        {
            yield return new WaitForSeconds(0.1f);
            part.DoCorrectPostion(0.1f);
        }

        yield return new WaitForSeconds(2);
        StartCoroutine(RandomPostionDelay());
    }
    private IEnumerator RandomPostionDelay()
    {
        foreach (PartController part in _puzzleParts)
        {
            yield return new WaitForSeconds(0.1f);
            part.DoRandomPosition(Size*10,0.1f);
        }
    }
}
