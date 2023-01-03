using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject myCamera;
    public Vector3[] rotSettings, posSettings;
    public Vector2 panLimit;
    private bool changeAngles = false;
    float camera_speed = 10f;
    public float panBorderThickness = 7.5f;
    public float scroll_speed = 20f;
    public float minY, maxY,minYPerspective,maxYPerspective;
    Vector3 pos; 
    void Start()
    {
        myCamera.transform.position = posSettings[0];
        myCamera.transform.rotation = Quaternion.Euler(rotSettings[0]);
    }
    
    void Update()
    {
        pos = transform.position;
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height-panBorderThickness)
            pos.z += camera_speed * Time.deltaTime;

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            pos.z -= camera_speed * Time.deltaTime;
        
        if (Input.GetKey("a") || Input.mousePosition.x <=panBorderThickness)
            pos.x -= camera_speed * Time.deltaTime;

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            pos.x += camera_speed * Time.deltaTime;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scroll_speed * 100f * Time.deltaTime;
        if (changeAngles)
        {
            pos.y = Mathf.Clamp(pos.y, minYPerspective, maxYPerspective);
        }
        else
        {
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

        }
        pos.x = Mathf.Clamp(pos.x,-panLimit.x,panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);
        transform.position = pos;
    }
    public void ChangeAngle()
    {
        if (!changeAngles)
        {
            pos = posSettings[1];
            myCamera.transform.rotation= Quaternion.Euler(rotSettings[1]);
            changeAngles = true;
        }
        else
        {
            pos = posSettings[0];
            myCamera.transform.rotation = Quaternion.Euler(rotSettings[0]);
            changeAngles = false;
        }
    }
}