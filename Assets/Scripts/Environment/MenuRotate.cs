using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotate : MonoBehaviour
{
    public float mRotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        mRotationSpeed = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * mRotationSpeed, Space.World);
    }
}
