using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    public GameObject slider;
    private float cooldown = 0f;
    public GameObject gameLogic;
    // Start is called before the first frame update
    void Start()
    {   
        agent = GetComponent<NavMeshAgent>();
        slider.transform.GetComponent<Slider>().maxValue = this.gameObject.transform.GetComponent<UnitStats>().maxHealth;
        slider.transform.GetComponent<Slider>().value = this.gameObject.transform.GetComponent<UnitStats>().maxHealth;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        slider.transform.GetComponent<Slider>().value= this.gameObject.transform.GetComponent<UnitStats>().currentHealth;
        foreach (GameObject soldier in SoldierSelections.Instance.soldierList)
        {
            target = soldier.transform;
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);
                if (distance <= 6f)
                {
                    if (soldier != null)
                    StartCoroutine(DelayedAttack(soldier));
                    break;
                }
            }
        }
    }
    IEnumerator DelayedAttack(GameObject soldier)
    {
        yield return new WaitUntil(() => true);
        if (cooldown <= 0)
        {
            
            this.gameObject.transform.GetComponent<UnitStats>().Attack(soldier);
            cooldown = 0.5f;
        }
    }
    private void OnDestroy()
    {
        gameLogic.transform.GetComponent<GameLogic>().defeated = true;
    }
}
