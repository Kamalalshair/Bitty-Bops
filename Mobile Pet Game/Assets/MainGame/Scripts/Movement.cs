using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // Start is called before the first frame update
    public void movementAlter()
    {
        var movement = GameObject.Find("ThePet").GetComponent<Mouse_Control>();
        if (movement.enabled) movement.enabled = false;
        else movement.enabled = true;
    }
}
