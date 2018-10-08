using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerScript : MonoBehaviour {
    [SerializeField] TextMeshProUGUI _timerTextField;

    private float _timeLeft = 3.0f;

    private void Start()
    {
        Time.timeScale = 1;
    }

    void Update () {
        if (_timerTextField != null)
        {
            _timeLeft -= Time.deltaTime;
            _timerTextField.text = (_timeLeft).ToString("0");
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            //game is playing
            Time.timeScale = 0;
        } 
        else
        {
            ResumeGame();
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
}
