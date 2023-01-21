using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectedBall : MonoBehaviour
{
   
    public int currentBallIndex = 0;
    public GameObject[] balls;

    private void Start()
    {
        currentBallIndex = PlayerPrefs.GetInt("SelectedBall", 0);
        foreach (GameObject ball in balls)
            ball.SetActive(false);

        balls[currentBallIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
