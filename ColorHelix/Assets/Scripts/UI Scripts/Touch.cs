using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IPointerUpHandler,IPointerDownHandler
{

    private static bool pressing;

    public static bool ISPressing()
    {
        if (pressing)
            print("we are pressing");
        else
            print("not pressing");
        return pressing;
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        pressing= true;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        pressing = false;
    }
}
