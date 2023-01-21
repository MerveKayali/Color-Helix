using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helix : MonoBehaviour
{
    private bool movable = true;

    private float angle;
    private float lastDeltaAngle, lastTouchX;


    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))//ekrana týklandýðýnda rotate yapmamasý için
        {
            lastTouchX = GetMouseX();            
        }           
        if (movable && Touch.ISPressing())
        {
            float mouseX = this.GetMouseX();
            lastDeltaAngle = lastTouchX - mouseX;
            angle += lastDeltaAngle * 360 * 1.7f;
            lastTouchX = mouseX;
        }
        else if (lastDeltaAngle != 0)
        {
            lastDeltaAngle -= lastDeltaAngle * 5 * Time.deltaTime;
            angle += lastDeltaAngle * 360 * 1.7f;
        }

        transform.eulerAngles = new Vector3(0, 0, angle);

    }

    private float GetMouseX()
    {
        return Input.mousePosition.x / (float)Screen.width;
    }
}
