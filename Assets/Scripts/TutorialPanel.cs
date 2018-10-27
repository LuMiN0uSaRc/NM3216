using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialPanel : MonoBehaviour {
    public TextMeshProUGUI ClickToContinueText;

    private int _currentIndex = 0;
    [SerializeField] GameObject[] _tutorialPanels;

	// Use this for initialization
	void Start () {
        StartCoroutine("BlinkingText");
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            if (_currentIndex < 9)
            {
                OpenNextPanel();
            }
            else
            {
                gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
	}

    private void OpenNextPanel()
    {
        _tutorialPanels[_currentIndex].SetActive(false);
        _currentIndex++;
        _tutorialPanels[_currentIndex].SetActive(true);
    }

    IEnumerator BlinkingText()
    {
        float alpha = ClickToContinueText.color.a;
        float final = -1;
        float rate = 1.0f / 0.75f;
        float progress = 0.0f;
        Color tempColor = ClickToContinueText.color;

        while (progress < 1.5f)
        {
            if (alpha == 0)
            {
                final = 1;
                ClickToContinueText.color = new Color(tempColor.r, tempColor.g, tempColor.b, Mathf.Lerp(alpha, final, progress));
            }
            else if (alpha == 1)
            {
                final = 0;
                ClickToContinueText.color = new Color(tempColor.r, tempColor.g, tempColor.b, Mathf.Lerp(alpha, final, progress));
            }

            progress += rate * 0.02f;
            yield return null;
        }
        ClickToContinueText.color = new Color(tempColor.r, tempColor.g, tempColor.b, final);
        progress = 0.0f;
        StartCoroutine(BlinkingText());
    }
}
