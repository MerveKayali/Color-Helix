using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderHelix : MonoBehaviour
{
    private GameObject helix;
    void Awake()
    {
        helix = GameObject.Find("Helix");
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, helix.gameObject.transform.eulerAngles.z /5);//silindir tam olmadýðý için alt tarafýna tamamen dönmemesini saðlýyoruz yani360 derece dönmeyecek
    }
}
