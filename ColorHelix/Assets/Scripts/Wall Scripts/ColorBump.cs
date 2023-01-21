using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBump : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Color color;

   
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

   
    void Start()
    {
        transform.parent = null;//helix objesi altýna oluþmamasý için 
        transform.rotation = Quaternion.Euler(Vector3.zero);

        color = GameController.instance.colors[Random.Range(0, GameController.instance.colors.Length)];
        while (color == GameController.instance.hitColor)//ayný denk gelirse deðiþtir
            color = GameController.instance.colors[Random.Range(0, GameController.instance.colors.Length)];
        meshRenderer.material.color = color;
    }

    public Color GetColor()
    {
        return this.color;
    }
}
