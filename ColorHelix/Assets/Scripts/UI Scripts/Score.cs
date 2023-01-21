using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI BestscoreText;
    //ben
    void Awake()
    {
        scoreText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        BestscoreText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Ball.GetZ() == -5f)
        {
            BestscoreText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(false);
        }
        else
        {
            BestscoreText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
        }

        scoreText.text = GameController.instance.score.ToString();

        if (GameController.instance.score > PlayerPrefs.GetInt("HighScore", 0))
            PlayerPrefs.SetInt("HighScore", GameController.instance.score);

        BestscoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
