using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform mario;
    public float xDelta;

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Max(mario.position.x + xDelta, transform.position.x);
        transform.position = pos;
    }
}
