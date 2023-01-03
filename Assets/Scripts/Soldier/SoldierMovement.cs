using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class SoldierMovement : MonoBehaviour
{
    public GameObject groundMark;
    Camera myCam;
    NavMeshAgent agent;
    public LayerMask ground;
    public LayerMask loot;
    public LayerMask enemy;
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SoldierSelections.Instance.soldierSelected.Count > 0)
        {
           
            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                RaycastHit hit;
                Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray,out hit, Mathf.Infinity, loot))
                {
                    agent.SetDestination(hit.point);
                }
                if(Physics.Raycast(ray, out hit, Mathf.Infinity, enemy))
                {
                    agent.SetDestination(hit.point);
                }
            }
            if (Input.GetMouseButton(1))
            {
                groundMark.SetActive(false);
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                groundMark.SetActive(true);

                RaycastHit hit;
                Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
                {
                    agent.SetDestination(hit.point);
                    StartCoroutine(Waiter());
                }
            }
        }
    }
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(4f);
        groundMark.SetActive(false);
    }
}
