using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExitPlayArea : MonoBehaviour {
    [SerializeField] TextMeshProUGUI _gameOverText;
    [SerializeField] AudioSource _bgm;

    public AudioClip GameOverBgm;

    private void OnTriggerExit2D(Collider2D collision)
    {
        Time.timeScale = 0;
        _gameOverText.gameObject.SetActive(true);
        UIManagerScript._gameOverCheck = true;
        _bgm.GetComponent<AudioSource>().clip = GameOverBgm;
        _bgm.GetComponent<AudioSource>().Play();
    }
}
