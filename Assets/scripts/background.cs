using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    [SerializeField]
    Vector2  parallaxeffectmultiplier;
    private Transform camtransform;
    private Vector3 lastcamposition;

    private float textureunit;
    // Start is called before the first frame update
    void Start()
    {
        camtransform = Camera.main.transform;
        lastcamposition = camtransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureunit = texture.width / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = camtransform.position - lastcamposition;
        transform.position += new Vector3(deltaMovement.x * parallaxeffectmultiplier.x,deltaMovement.y*parallaxeffectmultiplier.y)  ;
        lastcamposition = camtransform.position;

        if (camtransform.position.x - transform.position.x >= textureunit)
        {
            // float offset = (camtransform.position.x - transform.position.x) % textureunit;
            transform.position = new Vector3(camtransform.position.x/*+offset*/, transform.position.y);
        }
    }
}
