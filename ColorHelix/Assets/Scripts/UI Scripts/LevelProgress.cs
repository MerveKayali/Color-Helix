using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelProgress : MonoBehaviour
{
    private Image currentTickBoxImage;
    private Image endLevel;
    private Image progression;

    private Image[] alwaysColoredImages = new Image[3];

    private TextMeshProUGUI endLevelText,startLevelText;
    private TextMeshProUGUI currentTickBoxText;
    [SerializeField]
    private TextMeshProUGUI levelCompleteMessage;

    private RectTransform currentTickBox;

    private Color color;
    void Awake()
    {
        alwaysColoredImages[0] = base.transform.GetChild(0).GetComponent<Image>();
        alwaysColoredImages[1] = base.transform.GetChild(1).GetComponent<Image>();
        alwaysColoredImages[2] = base.transform.GetChild(3).GetComponent<Image>();
        endLevel = base.transform.GetChild(4).GetComponent<Image>();

        endLevelText = endLevel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        startLevelText = base.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();

        progression = base.transform.GetChild(2).GetChild(0).GetComponent<Image>();
        currentTickBox = base.transform.GetChild(2).GetChild(1).GetComponent<RectTransform>();
        currentTickBoxImage = currentTickBox.GetComponent<Image>();
        currentTickBoxText = currentTickBox.GetChild(0).GetComponent<TextMeshProUGUI>();


    }

     void Update()
    {
        
        //print(Ball.GetZ());
        if (progression.fillAmount != 1)
        {
            SetProgression((Ball.GetZ()+5f)/ GameController.instance.GetFinishLineDistance());

        }

        else if (progression.fillAmount >= 1 && Ball.GetZ() == -5f)
        {
            SetProgression(0);
        }
            

        UpdateColors();

        startLevelText.text = PlayerPrefs.GetInt("Level",1).ToString();
        endLevelText.text = (PlayerPrefs.GetInt("Level",1)+1).ToString();
    }


    private void SetProgression(float precentage)
    {
        progression.fillAmount = precentage;
        currentTickBox.anchorMin = new Vector2(precentage, 0);
        currentTickBox.anchorMax = currentTickBox.anchorMin;
        currentTickBoxText.text = Mathf.RoundToInt(precentage * 100) + " %";

    }

    private void UpdateColors()
    {
        color = Ball.GetColor();
        if (progression.fillAmount == 1)
        {
            endLevel.color = this.color;
            endLevelText.color = Color.white;

            levelCompleteMessage.gameObject.SetActive(true);
            levelCompleteMessage.text = "Level " + PlayerPrefs.GetInt("Level",1) + " Complete!";
        }
        else
        {
            endLevel.color = Color.white;
            endLevelText.color = color;
            levelCompleteMessage.gameObject.SetActive(false);
        }

        foreach (Image image in alwaysColoredImages)
        {
            image.color = color;
        }

        progression.color = color;
        currentTickBoxImage.color = color;

    }

}

    


