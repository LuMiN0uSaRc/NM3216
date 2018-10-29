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
    public TextMeshProUGUI ReminderText;
    public AudioClip BloopSound;
    public AudioClip ClickSound;

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
    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene" && PlayerPrefs.GetString("FirstTimeOpeningGame") != "false")
        {
            PlayerPrefs.SetString("FirstTimeOpeningGame", "false");
            Time.timeScale = 0;
            _tutorialPrefab.SetActive(true);
        } else
        {
            Time.timeScale = 1;
        }
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
        bool toClosePanel = true;
        if (inGameObject.name == "KeyRebind")
        {
            for (int i = 0; i < 8; i ++)
            {
                if (KeyBindings.Instance.ListOfKeysText[i].text == "None")
                {
                    toClosePanel = false;
                    ReminderText.gameObject.SetActive(true);
                    ReminderText.color = new Color(ReminderText.color.r, ReminderText.color.g, ReminderText.color.b, 1);
                    StartCoroutine(FadeTextAndMoveUp());
                }
            }
        }
        else
        {
            Time.timeScale = 1;
        }
        if (toClosePanel) inGameObject.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("StartScreen");
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
            _keyBindings.SetActive(true);

            //if (Time.timeScale == 1)
            //{
            //    Time.timeScale = 0;
            //    _keyBindingsCloseButton.SetActive(true);
            //    for (int i = 0; i < 8; i++)
            //    {
            //        KeyBindings.Instance.ListOfKeysText[i].gameObject.SetActive(false);
            //    }
            //}
            //else
            //{
            //    Time.timeScale = 1;
            //    _keyBindings.SetActive(false);
            //    _keyBindingsCloseButton.SetActive(false);
            //    if (KeyBindings.Instance._currentKey != null)
            //    {
            //        KeyBindings.Instance._currentKey.GetComponent<Button>().interactable = true;
            //    }
            //    for (int i = 0; i < 8; i++)
            //    {
            //        KeyBindings.Instance.ListOfKeysText[i].gameObject.SetActive(true);
            //    }
            //}
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

    IEnumerator FadeTextAndMoveUp()
    {
        float alpha = ReminderText.color.a;
        float final = -1;
        float rate = 1.0f / 0.75f;
        float progress = 0.0f;
        Color tempColor = ReminderText.color;

        while (progress < 1.0f)
        {
            final = 0;
            ReminderText.color = new Color(tempColor.r, tempColor.g, tempColor.b, Mathf.Lerp(alpha, final, progress));

            progress += rate * 0.02f;
            yield return null;
        }
        ReminderText.color = new Color(tempColor.r, tempColor.g, tempColor.b, final);
        ReminderText.gameObject.SetActive(false);
        progress = 0.0f;
    }

    public void PlayClickSound(GameObject inGameObject)
    {
        AudioSource audioSource = inGameObject.GetComponent<AudioSource>();
        audioSource.clip = ClickSound;
        audioSource.Play();
    }

    public void PlayHoverSound(GameObject inGameObject)
    {
        AudioSource audioSource = inGameObject.GetComponent<AudioSource>();
        audioSource.clip = BloopSound;
        audioSource.Play();
    }
}
