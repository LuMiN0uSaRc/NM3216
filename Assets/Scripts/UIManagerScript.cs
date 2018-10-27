using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManagerScript : MonoBehaviour {
    public AudioClip GameOverBgm;
    public static UIManagerScript Instance;
    public Button CurrentDifficultyButton;

    [SerializeField] TextMeshProUGUI _timerTextField;
    [SerializeField] GameObject _pauseScreenPrefab;
    [SerializeField] GameObject _mainMenuPrefab;
    [SerializeField] GameObject _tutorialPrefab;
    [SerializeField] GameObject _selectModePrefab;
    [SerializeField] GameObject _startGameButton;
    [SerializeField] GameObject _keyBindings;
    [SerializeField] GameObject _keyBindingsCloseButton;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _creditsPanel;
    [SerializeField] GameObject _settingsPanel;
    [SerializeField] AudioSource _bgm;

    public static bool _gameOverCheck = false;

    private float _timeLeft = 3.0f;
    private GameObject _currentSelectedButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    void Update () {
        if (_timerTextField != null)
        {
            _timeLeft -= Time.deltaTime;
            _timerTextField.text = (_timeLeft >= 1) ? (_timeLeft).ToString("0") : "Start!";
            if (_timeLeft < 0)
            {
                _timerTextField.gameObject.SetActive(false);
            }
        }
    }
	
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        _gameOverCheck = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        if (!_gameOverCheck)
        {
            if (Time.timeScale == 1)
            {
                //game is playing
                Time.timeScale = 0;
                _pauseScreenPrefab.SetActive(true);
            }
            else
            {
                ResumeGame();
                _pauseScreenPrefab.SetActive(false);
            }
        }
    }

    public void ResumeGame()
    {
        if (Time.timeScale == 0)
        {
            //game is paused, need to resume game
            Time.timeScale = 1;
        }
    }

    public void OpenTutorialPanel()
    {
        _tutorialPrefab.SetActive(true);
    }

    public void CloseTutorialPanel()
    {
        _tutorialPrefab.SetActive(false);
    }

    public void OpenSetModePanel()
    {
        _mainMenuPrefab.SetActive(false);
        _selectModePrefab.SetActive(true);
    }

    public void OpenPanel(GameObject inGameObject)
    {
        Time.timeScale = 0;
        inGameObject.SetActive(true);
    }

    public void ClosePanel(GameObject inGameObject)
    {
        Time.timeScale = 1;
        inGameObject.SetActive(false);
    }

    public void UpdateCurrentSelectedButton(string inDifficulty)
    {
        PlayerPrefs.SetString("Difficulty", inDifficulty);
        _startGameButton.SetActive(true);
    }

    public void OpenCloseKeyBindingsPanel()
    {
        if (!_gameOverCheck)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                _keyBindings.SetActive(true);
                _keyBindingsCloseButton.SetActive(true);
                for (int i = 0; i < 8; i++)
                {
                    KeyBindings.Instance.ListOfKeysText[i].gameObject.SetActive(false);
                }
            }
            else
            {
                Time.timeScale = 1;
                _keyBindings.SetActive(false);
                _keyBindingsCloseButton.SetActive(false);
                if (KeyBindings.Instance._currentKey != null)
                {
                    KeyBindings.Instance._currentKey.GetComponent<Button>().interactable = true;
                }
                for (int i = 0; i < 8; i++)
                {
                    KeyBindings.Instance.ListOfKeysText[i].gameObject.SetActive(true);
                }
            }
        }
    }

    public void OpenGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
        _bgm.GetComponent<AudioSource>().clip = GameOverBgm;
        _bgm.GetComponent<AudioSource>().Play();
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SetCurrentDifficultyButton(Button inButton)
    {
        if (CurrentDifficultyButton != null)
        {
            CurrentDifficultyButton.interactable = true;
        }
        CurrentDifficultyButton = inButton;
        CurrentDifficultyButton.interactable = false;
    }
}
