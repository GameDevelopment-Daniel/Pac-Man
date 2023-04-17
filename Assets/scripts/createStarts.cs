using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createStarts : MonoBehaviour
{
    float xgap = 1.3f;
    float ygap = 1.35f;

    void Start()
    {
        if (gameObject.name == "stars")
        {
            GameObject star = GameObject.Find("star");
            for (int j = 0; j < 14; j++)
            {
                for (int i = 0; i < 25; i++)
                {
                    if (i == 0 && j == 0) 
                        continue;
                    Instantiate(star, new Vector3(star.transform.position.x + i * xgap, star.transform.position.y - j * ygap, 0), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
