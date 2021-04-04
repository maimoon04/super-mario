using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(desball());
    }

    
    IEnumerator desball()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
