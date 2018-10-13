using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerScript : MonoBehaviour {
    [SerializeField] TextMeshProUGUI _timerTextField;
    [SerializeField] GameObject _pauseScreenPrefab;
    [SerializeField] GameObject _tutorialPrefab;

    public static bool _gameOverCheck = false;

    private float _timeLeft = 3.0f;

    private void Start()
    {
        Time.timeScale = 1;
    }

    void Update () {
        if (_timerTextField != null)
        {
            _timeLeft -= Time.deltaTime;
            Debug.Log(_timeLeft);
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
}
