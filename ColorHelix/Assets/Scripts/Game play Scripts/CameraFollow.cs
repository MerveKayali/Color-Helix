using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float cameraZ;

    private Animator anim;

    private float time, speed = 2;

    void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(time<1)
        {
            time += Time.deltaTime;
            cameraZ = Mathf.Lerp(transform.position.z, -2.95f, time);
        }
        else
        {
            cameraZ = Ball.GetZ() - 2.95f;
        }
        
        transform.position = new Vector3(0, 2.2f, cameraZ);
    }

    public void Flash()
    {
        anim.SetTrigger("Flash");
        cameraZ = 0;
        time = 0;
    }
}
