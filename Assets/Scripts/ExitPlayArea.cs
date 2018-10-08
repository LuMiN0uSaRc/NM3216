using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExitPlayArea : MonoBehaviour {
    [SerializeField] TextMeshProUGUI _gameOverText;

    private void OnTriggerExit2D(Collider2D collision)
    {
        Time.timeScale = 0;
        _gameOverText.gameObject.SetActive(true);
        UIManagerScript._gameOverCheck = true;
    }
}
