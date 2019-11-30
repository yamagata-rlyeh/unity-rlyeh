using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RlyehTestcamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(rotatincamera());
        }
    }
    
     IEnumerator rotatincamera()
    {
        float yyy = 90f;
        float change = transform.rotation.y + yyy;
        int count = 0;
        while (count<90)
        {
            transform.Rotate(0,1,0);
            count++;
            yield return 0.02f;
        }


        yield return null;
    }
}
