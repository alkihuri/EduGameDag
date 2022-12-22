using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PartController : MonoBehaviour
{
    [SerializeField] private int _iD;
    [SerializeField] private int _x;
    [SerializeField] private int _y;
    [SerializeField] private float _ratio;
    [SerializeField] private GameObject _mesh;
    [SerializeField] private bool _isCorrectPostion;


    public UnityEvent<PartController> OnPartBinded = new UnityEvent<PartController>();


    private int _yStartLevel;
    private List<PartController> _closeParts = new List<PartController>();
    public int ID { get => _iD; set => _iD = value; }
    public int X { get => _x; set => _x = value; }
    public int Y { get => _y; set => _y = value; }
    public int YStartLevel { get => _yStartLevel; set => _yStartLevel = value; }
    public float Ratio
    {
        get
        {
            return _ratio;
        }
        set
        {
            _ratio = value;
            DoMeshScale();
        }
    }

    public bool IsCorrectPostion { get => _isCorrectPostion; set => _isCorrectPostion = value; }
    public float OBJECT_SIZE = 10;

    private void DoMeshScale()
    {
        if (_mesh != null)
        {
            _mesh.transform.DOScaleX(Ratio, 1);
        }
    }

    private void Awake()
    {
        Settigns();
        Cashing();
        EventSettings();
    }

    private void EventSettings()
    { 

    }

    private void Cashing()
    {
    }

    private void Settigns()
    {
        if (YStartLevel == null)
        {
            YStartLevel = 1;
        }
    }

    public void DoCorrectPostion(float delay = 1)
    {
        transform.DOLocalMove(new Vector3(X, YStartLevel, Y), delay);
        IsCorrectPostion = true;
    }
    public void DoRandomPosition(float size, float delay = 1)
    {
        var xRand = Random.Range(-size, size);
        var yRand = Random.Range(-size, size);

        var x = xRand; 
        var z = yRand;

        x -= (x % OBJECT_SIZE); 
        z -= (z % OBJECT_SIZE);
         

        Vector3 calculatedPostion = new Vector3(x, YStartLevel, z);
        transform.DOLocalMove(calculatedPostion, delay);
        IsCorrectPostion = false;
    }
}
