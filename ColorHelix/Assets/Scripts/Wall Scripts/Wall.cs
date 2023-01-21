using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private GameObject wallFragment;
    private GameObject wall1, wall2;
    private GameObject perfectStar;

    public static Wall instance;
    

    private bool smallWall;

    private float rotationZ;
    private float rotationZMax = 180;

    private Rigidbody rb;

    void Awake()
    {
        wallFragment = Resources.Load("WallFragment") as GameObject;
        perfectStar = Resources.Load("PerfectStar") as GameObject;
    }

    // Update is called once per frame
    void Start()
    {
        SpawnWallFragment();
    }

    void SpawnWallFragment()
    {
        wall1 = new GameObject();
        wall2 = new GameObject();

        

        wall1.name = "Wall1";
        wall2.name = "Wall2";

        wall1.gameObject.tag = "Hit";
        wall2.gameObject.tag = "Fail";

        wall1.transform.SetParent(transform);
        wall2.transform.SetParent(transform);

        wall2.AddComponent<BoxCollider>();
        wall2.GetComponent<BoxCollider>().size = new Vector3(0.9f, 1.85f, 0.2f);
        wall2.GetComponent<BoxCollider>().center = new Vector3(0.46f, 0, 0);

        if (Random.value <= 0.2 && PlayerPrefs.GetInt("Level") >= 6) smallWall = true;

        if(smallWall)
        {
            rotationZMax = 90;
        }
        else
        {
            //wall1.AddComponent<BoxCollider>();
            //wall1.GetComponent<BoxCollider>().size = new Vector3(0.9f, 1.85f, 0.2f);
            //wall1.GetComponent<BoxCollider>().center = new Vector3(-0.5017059f, -5.149306e-09f, 0);
            rotationZMax = 180;
        } 

        for(int i=0;i<100;i++)
        {
            GameObject Wallf = Instantiate(wallFragment, Vector3.zero, Quaternion.Euler(0, 0, rotationZ));
            rotationZ += 3.6f;//eklenen duvarýn direk bitiþiðine ekliyor
           
            if (rotationZ <= rotationZMax)
            {//ilk yarýsýný oluþturuyor bu yüzden 180le karþýlaþtýrýyor yarýsý olunca diðer yarýsýný wall2 altýna ekliyor.
                Wallf.transform.SetParent(wall1.transform);
                Wallf.gameObject.tag = "Hit";
            }
            else
                Wallf.transform.SetParent(wall2.transform);
        }
        wall1.transform.localPosition = Vector3.zero;
        wall2.transform.localPosition = Vector3.zero;

        wall1.transform.localRotation = Quaternion.Euler(Vector3.zero);
        wall2.transform.localRotation = Quaternion.Euler(Vector3.zero);

        if(!smallWall)
        {
            GameObject wallFragmentChild = wall1.transform.GetChild(25).gameObject;
            AddStar(wallFragmentChild);
        }
        else
        {
            GameObject wallFragmentChild = wall1.transform.GetChild(12).gameObject;
            AddStar(wallFragmentChild);
        }


        
    }
    void AddStar(GameObject wallFragmentChild)
    {
        GameObject star = Instantiate(perfectStar, transform.position, Quaternion.identity);
        star.transform.SetParent(wallFragmentChild.transform);
        star.transform.localPosition = new Vector3(0.05f, 0.75f, -0.06f);
       
    }
}
