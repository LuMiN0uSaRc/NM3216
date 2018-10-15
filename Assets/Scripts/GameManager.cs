using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance; 
    public GameObject SheepPrefab;
    public int BallBounceCount = 0;

    [SerializeField] PlayAreaInputScript playArea;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update () {
        if (!UIManagerScript._gameOverCheck && Time.timeScale == 1)
        {
            if (Input.GetKeyDown(KeyBindings.Instance._currentListOfKeys["1"]))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.ONE);
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.TWO);
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.THREE);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.FOUR);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.SIX);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.SEVEN);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.EIGHT);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.NINE);
            }
        }
    }
}
