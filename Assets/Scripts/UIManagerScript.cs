using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerScript : MonoBehaviour {
    [SerializeField] TextMeshProUGUI _timerTextField;

    private float _timeLeft = 3.0f;

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
}
