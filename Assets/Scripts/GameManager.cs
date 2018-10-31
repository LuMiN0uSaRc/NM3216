using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager Instance; 
    public GameObject SheepPrefab;
    public int BallBounceCount = 0;
    public int NumOfSheeps = 1;
    public int numOfSoundPlaying = 0;
    public TextMeshProUGUI _numberOfBounces;
    public TextMeshProUGUI _speedOfCharacter;
    public bool ifGameStarted = false;

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
            else if (Input.GetKeyDown(KeyBindings.Instance._currentListOfKeys["2"]))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.TWO);
            }
            else if (Input.GetKeyDown(KeyBindings.Instance._currentListOfKeys["3"]))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.THREE);
            }
            else if (Input.GetKeyDown(KeyBindings.Instance._currentListOfKeys["4"]))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.FOUR);
            }
            else if (Input.GetKeyDown(KeyBindings.Instance._currentListOfKeys["5"]))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.SIX);
            }
            else if (Input.GetKeyDown(KeyBindings.Instance._currentListOfKeys["6"]))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.SEVEN);
            }
            else if (Input.GetKeyDown(KeyBindings.Instance._currentListOfKeys["7"]))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.EIGHT);
            }
            else if (Input.GetKeyDown(KeyBindings.Instance._currentListOfKeys["8"]))
            {
                playArea.TriggerGate(PlayAreaInputScript.NumPad.NINE);
            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
