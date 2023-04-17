using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle_script : MonoBehaviour
{
    [SerializeField]
    int speedRotation;
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 0, speedRotation*Time.deltaTime);

    }
}
