using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{

    public TMP_InputField playerName;


    public void StartGame()
    {
        if (playerName.text == "")
            PlayerPrefs.SetString("PlayerName", "Player");
        else
            PlayerPrefs.SetString("PlayerName", playerName.text);

        SceneManager.LoadScene(0);
    }
}
