using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private static float z;

    private float height = 0.58f;
    [HideInInspector]
    public float speed = 7f;
    
    public static Ball instance;
    private bool  isRising, gameOver;
    [HideInInspector]
    public bool move;
    private static Color currentColor;
    private float lerpAmount;

    public bool perfectStar, displayed;
    private SpriteRenderer splash;
    private MeshRenderer meshRenderer;
    private MeshRenderer[] childMesh;
    private AudioSource hitSound, failSound, LevelComplateSound;

    private Rigidbody rb;
    private bool smash;

    private GameObject touchImg,tapTxt;
    PlayfabManager playfabManager;



    void Awake()
    {
        hitSound = GameObject.Find("HitSound").GetComponent<AudioSource>();
        failSound = GameObject.Find("FailSound").GetComponent<AudioSource>();
        LevelComplateSound = GameObject.Find("LevelComplateSound").GetComponent<AudioSource>();
        Ball.z = -5f;       
        meshRenderer = GetComponent<MeshRenderer>();
        splash = transform.GetChild(0).GetComponent<SpriteRenderer>();
        
        rb = GetComponent<Rigidbody>();

        PlayfabManager playfabManager = GameObject.Find("PlayfabManager").GetComponent<PlayfabManager>();

    }

    void Start()
    {
        move = false;
        SetColor(GameController.instance.hitColor);
        tapTxt = GameObject.Find("TapText");
        touchImg = GameObject.Find("TouchImage");
        Application.targetFrameRate = 60;
    }


    void Update()
    {
        
        print(PlayerPrefs.GetInt("Level", 1));
        if (Touch.ISPressing() && !gameOver)
        {
            move = true;
            GetComponent<SphereCollider>().enabled = true;
            tapTxt.SetActive(false);
            touchImg.SetActive(false);           
        }

        if(move)
        {
           
            speed += Time.deltaTime/2;
            Ball.z += speed * 0.025f;
            
        }
               
        transform.position = new Vector3(0, height, Ball.z);
        displayed = false;
        UpdateColor();
        
    }

    
    public static float GetZ()
    {
        return Ball.z;
    }
    
    void UpdateColor()
    {
        meshRenderer.sharedMaterial.color = currentColor;
        if(isRising)
        {
            //ilk renk son renk ve süre
            currentColor = Color.Lerp(meshRenderer.material.color, GameObject.FindGameObjectWithTag("ColorBump").GetComponent<ColorBump>().GetColor(), lerpAmount);
            lerpAmount += Time.deltaTime;
        
        }
        if(lerpAmount>=1)
        {
            isRising = false;
        }
    }

    public static Color SetColor(Color color)
    {
        return currentColor = color;
    }

    public static Color GetColor()
    {
        return currentColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Hit")
        {
            
            if(perfectStar && !displayed)
            {              
                displayed = true;
                GameObject pointDisplay = Instantiate(Resources.Load("PointDisplay"), transform.position, Quaternion.identity) as GameObject;
                pointDisplay.GetComponent<PointDisplay>().SetText("PERFECT +" + PlayerPrefs.GetInt("Level",1)*2);
            }
            else if (!perfectStar && !displayed)
            {
                displayed = true;
                GameObject pointDisplay = Instantiate(Resources.Load("PointDisplay"), transform.position, Quaternion.identity) as GameObject;
                pointDisplay.GetComponent<PointDisplay>().SetText("+" + PlayerPrefs.GetInt("Level",1));
            }
            hitSound.Play();           
            Destroy(other.transform.parent.gameObject);            
        }
        if(other.tag=="ColorBump")
        {
            lerpAmount = 0;
            isRising = true;
        }
        if(other.tag=="Fail")
        {
            StartCoroutine(GameOver());
            speed = 7f;
            playfabManager.SendLeaderBoard(GameController.instance.score);                                 
        }
        if(other.tag=="FinishLine")
        {
            speed = 7f;
            StartCoroutine(PlayNewLevel());
            
        }

        if(other.tag=="Star")
        {
            
            perfectStar = true;
        }
    }

    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Hit")
        {

            if (perfectStar && !displayed)
            {
                print("girdi");
                displayed = true;
                GameObject pointDisplay = Instantiate(Resources.Load("PointDisplay"), transform.position, Quaternion.identity) as GameObject;
                pointDisplay.GetComponent<PointDisplay>().SetText("PERFECT +" + PlayerPrefs.GetInt("Level", 1) * 2);
            }
            else if (!perfectStar && !displayed)
            {
                displayed = true;
                GameObject pointDisplay = Instantiate(Resources.Load("PointDisplay"), transform.position, Quaternion.identity) as GameObject;
                pointDisplay.GetComponent<PointDisplay>().SetText("+" + PlayerPrefs.GetInt("Level"));
            }
            hitSound.Play();
            Destroy(other.transform.parent.gameObject);
        }
        if (other.gameObject.tag == "ColorBump")
        {
            lerpAmount = 0;
            isRising = true;
        }
        if (other.gameObject.tag == "Fail")
        {
            StartCoroutine(GameOver());
            GameController.instance.score = 0;

        }
        if (other.gameObject.tag == "FinishLine")
        {
            StartCoroutine(PlayNewLevel());
        }

        if (other.gameObject.tag == "Star")
        {
            perfectStar = true;
        }
    }*/

    IEnumerator PlayNewLevel()
    {
        LevelComplateSound.Play();
        Camera.main.GetComponent<CameraFollow>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        move = false;
        Camera.main.GetComponent<CameraFollow>().Flash();
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level",1) + 1);
        Camera.main.GetComponent<CameraFollow>().enabled = true;
        Ball.z = -5f;
        GameController.instance.GenerateLevel();
       
    }

    IEnumerator GameOver()
    {

        gameOver = true;
        
        failSound.Play();
        splash.color = currentColor;
        splash.transform.position = new Vector3(0, 0.7f, Ball.z - 0.05f);
        splash.transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
        splash.enabled = true;

        meshRenderer.enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        move = false;
        

        yield return new WaitForSeconds(1.5f);
        Camera.main.GetComponent<CameraFollow>().Flash();
        gameOver = false;
        Ball.z = -5f;
        GameController.instance.GenerateLevel();
        splash.enabled = false;
        meshRenderer.enabled = true;
        
        

    }


}
