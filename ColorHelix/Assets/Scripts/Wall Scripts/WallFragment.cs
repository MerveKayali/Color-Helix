using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFragment : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    
    void Start()
    {
        if(this.gameObject.tag=="Hit" )
        {
            if (PlayerPrefs.GetInt("Level") >= 3)
            {
                GameObject colorBump = GameObject.FindGameObjectWithTag("ColorBump");

                if (transform.position.z > colorBump.transform.position.z)
                {
                    GameController.instance.hitColor = colorBump.GetComponent<ColorBump>().GetColor();
                }
            }

            meshRenderer.material.color = GameController.instance.hitColor;
        }
        else
        {
            if(GameController.instance.failColor==GameController.instance.hitColor)
            {
                GameController.instance.failColor = GameController.instance.colors[Random.Range(0, GameController.instance.colors.Length)];
            }
            meshRenderer.material.color = GameController.instance.failColor;
        }
    }
}
