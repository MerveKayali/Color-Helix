using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{ //benim
    public Color[] colors;
    [HideInInspector]
    public Color hitColor, failColor;

    public GameObject finishLine;
    public static GameController instance;
    private int spawnWallsNumber = 11, wallsCount=0;

    private GameObject[] walls2,walls1;

    public int score;
    private bool colorBump;

    

    private float z=7;
    void Awake()
    {
        GenerateColor();
        instance = this;
        PlayerPrefs.GetInt("Level", 1);

    }

    void Start()
    {
        //Screen.SetResolution(Screen.currentResolution.width / 3, Screen.currentResolution.height / 3, true);
        GenerateLevel();
    }

     void Update()
    {
        SumUpWalls();
        
    }

    public float GetFinishLineDistance()
    {
        return finishLine.transform.position.z;
    }

    void SumUpWalls()
    {
        walls1 = GameObject.FindGameObjectsWithTag("Hit");

        if (walls1.Length > wallsCount)
            wallsCount = walls1.Length ;//counta duvar sayýsýný attýk

        if(wallsCount>walls1.Length)//top sürekli duvara çarpýp yok edeceði için bu ifade saðlanacak ve score artacak
        { 
            wallsCount = walls1.Length;
            
            if (GameObject.Find("Ball").GetComponent<Ball>().perfectStar)
            {
                
                GameObject.Find("Ball").GetComponent<Ball>().perfectStar = false;
                score += PlayerPrefs.GetInt("Level",1) * 2;//yýldýza deðerse 2 katý puan

            }
            else
            {                
                score += PlayerPrefs.GetInt("Level",1);
            }
            //print("puan"+score);

        }
            

    }
    public void GenerateLevel()
    {
        GenerateColor();

        if(PlayerPrefs.GetInt("Level")>=1 && PlayerPrefs.GetInt("Level")<=4)
        {
            spawnWallsNumber = 12;
        }
        else if(PlayerPrefs.GetInt("Level") >= 5 && PlayerPrefs.GetInt("Level") <= 10)
        {
            spawnWallsNumber = 13;
        }
        else if(PlayerPrefs.GetInt("Level") >= 6 && PlayerPrefs.GetInt("Level") <= 14)
        {
            spawnWallsNumber = 14;
        }
        else if(PlayerPrefs.GetInt("Level") >= 15 && PlayerPrefs.GetInt("Level") <= 24)
        {
            spawnWallsNumber = 16;
        }
        else
        {
            spawnWallsNumber = 20;
        }
        
        z = 7;
        DeleteWalls();
        colorBump = false;
        SpawnWalls();
       

    }

   void DeleteWalls()
    {
        walls2 = GameObject.FindGameObjectsWithTag("Fail");

        if(walls2.Length>1)
        {
            for(int i=0;i<walls2.Length;i++)
            {
                Destroy(walls2[i].transform.parent.gameObject);
            }
        }

        Destroy(GameObject.FindGameObjectWithTag("ColorBump"));
    }

    void GenerateColor()
    {        
        hitColor = colors[Random.Range(0, colors.Length)];
        failColor = colors[Random.Range(0, colors.Length)];

        while (failColor == hitColor)//ayný denk gelirse deðiþtir
            failColor = colors[Random.Range(0, colors.Length)];

        Ball.SetColor(hitColor);
    }

   


    void SpawnWalls()
    {
        for(int i=0;i<spawnWallsNumber;i++)
        {
            GameObject wall;

            if(Random.value<=0.4 && !colorBump && PlayerPrefs.GetInt("Level")>=3)
            {
                colorBump = true;
                wall = Instantiate(Resources.Load("ColorBump") as GameObject, transform.position, Quaternion.identity);
                
            }
            else if(Random.value<=0.2 && PlayerPrefs.GetInt("Level") >= 9)
            {
                wall = Instantiate(Resources.Load("Wall") as GameObject, transform.position, Quaternion.identity);
                z -= 3f;
                
            }
            else if(i>=spawnWallsNumber-1 && !colorBump && PlayerPrefs.GetInt("Level") >= 3)
            {
                colorBump = true;
                wall = Instantiate(Resources.Load("ColorBump") as GameObject, transform.position, Quaternion.identity);
                
            }
            else
            {
                wall = Instantiate(Resources.Load("Wall") as GameObject, transform.position, Quaternion.identity);
            }

           
            wall.transform.SetParent(GameObject.Find("Helix").transform);
            wall.transform.localPosition = new Vector3(0, 0, z);
            float randomRotation = Random.Range(0, 360);
            wall.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, randomRotation));
            z += 9;

            if(i<=spawnWallsNumber)
            {
                finishLine.transform.position = new Vector3(0, 0.03f, z);
                
            }
        }
    }
}
