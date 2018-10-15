using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyBindings : MonoBehaviour {
    public static KeyBindings Instance;
    public GameObject _currentKey;
    public TextMeshProUGUI[] ListOfKeysText = new TextMeshProUGUI[8];
    public Dictionary<string, KeyCode> _currentListOfKeys = new Dictionary<string, KeyCode>();

    private Dictionary<string, KeyCode> _originalListOfKeys = new Dictionary<string, KeyCode>();

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        //initialize the keys to the default value
        _originalListOfKeys.Add("1", KeyCode.Z);
        _originalListOfKeys.Add("2", KeyCode.X);
        _originalListOfKeys.Add("3", KeyCode.C);
        _originalListOfKeys.Add("4", KeyCode.A);
        _originalListOfKeys.Add("5", KeyCode.D);
        _originalListOfKeys.Add("6", KeyCode.Q);
        _originalListOfKeys.Add("7", KeyCode.W);
        _originalListOfKeys.Add("8", KeyCode.E);

        _currentListOfKeys = _originalListOfKeys;

        for (int i = 0; i < 8; i++)
        {
            int stringToGet = i + 1;
            ListOfKeysText[i].text = _originalListOfKeys[stringToGet.ToString()].ToString();
        }
	}

    private void OnGUI()
    {
        if (_currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                if (!_currentListOfKeys.ContainsValue(e.keyCode) && _currentListOfKeys[_currentKey.name] != e.keyCode 
                    && e.keyCode != KeyCode.None)
                { 
                    _currentListOfKeys[_currentKey.name] = e.keyCode;
                    _currentKey.transform.Find("Letter").GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                    _currentListOfKeys[_currentKey.name] = e.keyCode;
                    ListOfKeysText[System.Convert.ToInt32(_currentKey.name)-1].text = e.keyCode.ToString();
                    _currentKey.GetComponent<Button>().interactable = true;
                    _currentKey = null;
                } 
                else
                {
                    Debug.Log("NO");
                }
                
            }
        }
    }

    public void UpdateKeyBindings(GameObject inClickedKey)
    {
        _currentKey = inClickedKey;
        _currentKey.GetComponent<Button>().interactable = false;
    }
}
