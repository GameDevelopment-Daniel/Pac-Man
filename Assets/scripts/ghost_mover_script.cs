using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost_mover_script : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int speedX;
    [SerializeField]
    int speedY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speedX, speedY, 0) * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "wall")
        {
            transform.localScale=new Vector3(transform.localScale.x*-1, transform.localScale.y,transform.localScale.z);
            speedY *= -1;
            speedX *= -1;
        }
    }
}
