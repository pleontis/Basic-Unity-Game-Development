using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMinimapController : MonoBehaviour
{
    public Transform mainCam;
   

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = mainCam.position;
        newPos.y = transform.position.y;
        newPos.z = mainCam.position.z + 15;
        transform.position = newPos;
    }
}
