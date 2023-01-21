using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDisplay : MonoBehaviour
{
    private TextMesh textMesh;
    void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

   public void SetText(string text)
    {
        this.textMesh.text = text;
        textMesh.color = Color.white;
        
    }
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Ball.GetZ());
        Destroy(gameObject, 1.2f);
    }
}
