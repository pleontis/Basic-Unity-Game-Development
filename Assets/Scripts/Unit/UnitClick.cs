using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.EventSystems;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;
    public GameObject groundMark;

    public LayerMask clickable;
    public LayerMask ground;
    public LayerMask resource;
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                UnitSelections.Instance.DeselectAll();
            }
        }
        //Left Click is pressed for selecting units or resources object
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity,clickable)) 
            {

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else
                {
                    UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                }
            }
            //If a resource tree is hit activate shade
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, resource))
            {
                groundMark.SetActive(false);
                
                //If there are selected units to move to resource tree
                if (UnitSelections.Instance.unitsSelected.Count != 0)
                {
                    GameObject temp = hit.collider.gameObject;
                    temp.transform.Find("Sphere").gameObject.SetActive(true);
                    
                    //Units reached resource object
                    if (temp!= null)
                    {
                        temp.transform.GetComponent<ResourceSrcUI>().SetExit(false);
                        temp.transform.GetComponent<ResourceSrcUI>().OpenMenu(); 
                    }
                }              
            }
            else
            {
                groundMark.SetActive(false);
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.DeselectAll();
                }
            }
        }
        
        //Right Click is pressed for moving into map
        if (Input.GetMouseButtonDown(1))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
           
                groundMark.transform.position = hit.point;
                groundMark.SetActive(false);
                groundMark.SetActive(true);
            }
        }
        //One of units selected is reaching target
        foreach (GameObject unit in UnitSelections.Instance.unitsSelected)
        {
            if (Vector3.Distance(unit.transform.position, groundMark.transform.position)<3.5f)
            {
                groundMark.SetActive(false);
                break;
            }
        }
    }
}