using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenericMovement {
    public bool left, right;
    public int speed;
    // Start is called before the first frame updat


    public void checkbool()
    {
        if (left)
        {

            left = false;
            right = true;
        }
        else if (right)
        {

            left = true;
            right = false;
        }
    }
}