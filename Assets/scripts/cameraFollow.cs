using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform objectfollow;

    public float offset,yoffset;

    Vector3 temp;
    public static bool checkcam = true;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (this.transform.position.x >= objectfollow.transform.position.x)
        {
            checkcam = true;
        }
        if (checkcam)
        {
            temp = transform.position;
            temp.x = objectfollow.position.x;
            temp.x += offset;

            transform.position = temp;

        }
        



    }
}
