using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaidController : MonoBehaviour
{
    public bool isGameOn;
    public bool isJump;
    [SerializeField, Range(0, 10)] float speedOftransition = 5 ; 
    [SerializeField, Range(0, 4)]  int currentRoad;
    [SerializeField] List<Transform> roads; // have to be serialized
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartGame");
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3);
        isGameOn = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(isGameOn)
           transform.position = Vector3.MoveTowards(transform.position, roads[currentRoad].position + new Vector3(0,1,0), speedOftransition / 10);
    }

    void OnMove(int direction)
    {
        if (currentRoad < 4 && currentRoad > 0)
            currentRoad += direction;
        else
            Debug.Log("Error!");
    }

    public void OnLeftMove()
    {
        OnMove(-1);
    }
    public void OnRightMove()
    {
        OnMove(1);
    }
}
