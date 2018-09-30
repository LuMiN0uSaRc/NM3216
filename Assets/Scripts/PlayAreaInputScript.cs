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
        }
    }

    private GameObject GetFromPool()
    {
        for (int i = 0; i < wallPool.Count; i++)
        {
            if (!wallPool[i].activeInHierarchy)
            {
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

    private int CheckAvailableWalls()
    {
        for (int i = 0; i < wallPool.Count; i++)
        {
            if(wallPool[i].activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }

    private void DeactiveWall()
    {
        if (_numOfWalls == 3)
        {
            int wallNumber = CheckAvailableWalls();


            if (wallNumber != -1)
            {
                wallPool[wallNumber].SetActive(false);
                _numOfWalls--;
            }
        }
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
