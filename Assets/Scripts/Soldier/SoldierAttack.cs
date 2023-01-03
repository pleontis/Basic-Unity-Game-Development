using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class SoldierAttack : MonoBehaviour
{
    Camera myCam;
    public LayerMask enemy;
    private float distance;
    private GameObject enemyObject=null;
    public AudioClip battleClip;
    private AudioSource audioSource;
    private RaycastHit hit;
    private float cooldown=0f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.4f;
        myCam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (SoldierSelections.Instance.soldierSelected.Count > 0) {

            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit, Mathf.Infinity, enemy))
                {
                    enemyObject = hit.collider.gameObject;
                    distance = Vector3.Distance(this.gameObject.transform.position, enemyObject.transform.position);
                }
                if (enemyObject != null)
                {
                    if(distance<6)
                    StartCoroutine(DelayedAttack(enemyObject));
                }
            }
            if(enemyObject!=null)
                distance = Vector3.Distance(this.gameObject.transform.position, enemyObject.transform.position);
        }
    }
    IEnumerator DelayedAttack(GameObject enemyObject)
    {
        yield return new WaitUntil(() => distance < 6f);

        if (cooldown <=0)
        {
            if (enemyObject != null)
            {
                audioSource.PlayOneShot(battleClip);
                this.gameObject.transform.GetComponent<UnitStats>().Attack(enemyObject);
                cooldown = 1f;
            }   
        }
    }
}
