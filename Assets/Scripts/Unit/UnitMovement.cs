using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class UnitMovement : MonoBehaviour
{
    Camera myCam;
    NavMeshAgent agent;
    public LayerMask ground;
    public LayerMask resource;


    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        //Right click is pressed
        if (Input.GetMouseButtonDown(1))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                agent.SetDestination(hit.point);
            }
        }
        //Left click is pressed on a resource tree for mining
        if (Input.GetMouseButton(0)) {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, resource))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
