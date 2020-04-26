using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTarget : MonoBehaviour
{
    private GameObject thePet;
    private Transform[] x = new Transform[3];
    private int y = 0;
    public void Start()
    {
        thePet = GameObject.Find("ThePet");
        x[0] = GameObject.Find("TestFruit1").GetComponent<Transform>();
        x[1] = GameObject.Find("TestFruit2").GetComponent<Transform>();
        x[2] = GameObject.Find("TestFruit3").GetComponent<Transform>();
    }

    public void OnButtonPress()
    {
        thePet.GetComponent<SeekArriveAvoid>().target = x[y];
        y++;
        if (y > 2) y = 0;
    }
    
}
