using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ShopSystem : MonoBehaviour
{
    public float rotationspeed = 50;
    public int currentBallIndex=0;
    public GameObject[] ballModels;

    public TextMeshProUGUI ShopScore;

    public BallBlueprint[] balls;

    

    public Button buyButton;
    private void Start()
    {
        ShopScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        foreach (BallBlueprint ball in balls)
        {
            if (ball.price == 0)
                ball.isUnlocked = true;
            else
                ball.isUnlocked = PlayerPrefs.GetInt(ball.name, 0) == 0 ? false : true;
        }
        currentBallIndex = PlayerPrefs.GetInt("SelectedBall", 0);
        foreach (GameObject ball in ballModels)
            ball.SetActive(false);

        ballModels[currentBallIndex].SetActive(true);

        
    }

   
    private void Update()
    {
        transform.Rotate(0, rotationspeed * Time.deltaTime, 0);
        UpdateUI();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(0);//build index-> 0
    }

   

    public void ChangeNext()
    {
        ballModels[currentBallIndex].SetActive(false);
        currentBallIndex++;
        if (currentBallIndex == ballModels.Length)
            currentBallIndex = 0;

        ballModels[currentBallIndex].SetActive(true);
        BallBlueprint b = balls[currentBallIndex];

        if (!b.isUnlocked)
            return;
        PlayerPrefs.SetInt("SelectedBall", currentBallIndex);
    }

    public void ChangePrevious()
    {
        ballModels[currentBallIndex].SetActive(false);
        currentBallIndex--;
        if (currentBallIndex == -1)
            currentBallIndex = ballModels.Length-1;

        ballModels[currentBallIndex].SetActive(true);

        BallBlueprint b = balls[currentBallIndex];

        if (!b.isUnlocked)
            return;
        PlayerPrefs.SetInt("SelectedBall", currentBallIndex);
    }
    public void UnclockBall()
    {
        BallBlueprint b = balls[currentBallIndex];

        PlayerPrefs.SetInt(b.name, 1);
        PlayerPrefs.SetInt("SelectedBall", currentBallIndex);
        b.isUnlocked = true;
        PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("HighScore", 0) - b.price);
    }

    private void UpdateUI()
    {
        BallBlueprint b = balls[currentBallIndex];
        if(b.isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "BUY-" + b.price;
            if(b.price<PlayerPrefs.GetInt("HighScore",0))
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }

        }
    }
}
