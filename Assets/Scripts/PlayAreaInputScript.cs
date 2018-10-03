using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayAreaInputScript : MonoBehaviour {

    public enum NumPad
    {
        ONE,
        TWO,
        THREE,
        FOUR,
        SIX,
        SEVEN,
        EIGHT,
        NINE
    }

    [SerializeField] GameObject Wall;
    [SerializeField] GameObject[] SpawnPoints;

    List<GameObject> wallPool;
    private int _numOfWalls;
    private int[] _wallListPriority = new int[3];

    // Use this for initialization
    void Start () {
        wallPool = new List<GameObject>();
        _numOfWalls = 0;
        for (int i = 0; i < 3; i ++)
        {
            GameObject obj = Instantiate(Wall);
            obj.SetActive(false);
            wallPool.Add(obj);
            obj.transform.SetParent(gameObject.transform, false);

            //Initialize the list to -1
            _wallListPriority[i] = -1;
        }
    }

    private GameObject GetFromPool()
    {
        for (int i = 0; i < wallPool.Count; i++)
        {
            if (!wallPool[i].activeInHierarchy)
            {
                UpdateWallsPriorityList(i);
                return wallPool[i];
            }
        }

        return null;
    }

    private void SpawnWall(Vector3 inPosition, Quaternion inRotation)
    {
        GameObject obj = GetFromPool();

        if (obj == null) return;

        obj.transform.position = inPosition;
        obj.transform.rotation = inRotation;
        obj.SetActive(true);
        _numOfWalls++;
    }

    private void DeactiveWall()
    {
        if (_numOfWalls == 3)
        {
            wallPool[_wallListPriority[0]].SetActive(false);
            _numOfWalls--;
            UpdateWallsPriorityList();
        }
    }

    private void UpdateWallsPriorityList()
    {
        //Set second element to be first element
        _wallListPriority[0] = _wallListPriority[1];
        //set third element to be second element 
        _wallListPriority[1] = _wallListPriority[2];
        //set third element to be -1
        _wallListPriority[2] = -1;
    }

    //Takes in last priority number to be added. The first number will only be removed if all 3 elements are full.
    private void UpdateWallsPriorityList(int inLastPriority)
    {
        if (_wallListPriority[0] == -1) _wallListPriority[0] = inLastPriority;
        else if (_wallListPriority[1] == -1) _wallListPriority[1] = inLastPriority;
        else if (_wallListPriority[2] == -1) _wallListPriority[2] = inLastPriority;
    }

    public void TriggerGate(NumPad inKeyCode)
    {
        DeactiveWall();
        switch (inKeyCode)
        {
            case NumPad.ONE:
                SpawnWall(SpawnPoints[0].transform.position, SpawnPoints[0].transform.rotation);
                break;
            case NumPad.TWO:
                SpawnWall(SpawnPoints[1].transform.position, SpawnPoints[1].transform.rotation);
                break;
            case NumPad.THREE:
                SpawnWall(SpawnPoints[2].transform.position, SpawnPoints[2].transform.rotation);
                break;
            case NumPad.FOUR:
                SpawnWall(SpawnPoints[3].transform.position, SpawnPoints[3].transform.rotation);
                break;
            case NumPad.SIX:
                SpawnWall(SpawnPoints[4].transform.position, SpawnPoints[4].transform.rotation);
                break;
            case NumPad.SEVEN:
                SpawnWall(SpawnPoints[5].transform.position, SpawnPoints[5].transform.rotation);
                break;
            case NumPad.EIGHT:
                SpawnWall(SpawnPoints[6].transform.position, SpawnPoints[6].transform.rotation);
                break;
            case NumPad.NINE:
                SpawnWall(SpawnPoints[7].transform.position, SpawnPoints[7].transform.rotation);
                break;
            default:
                break;
        }
    }
}
