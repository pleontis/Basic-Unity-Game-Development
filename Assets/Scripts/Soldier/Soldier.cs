using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : MonoBehaviour
{
    public GameObject slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.transform.GetComponent<Slider>().maxValue= this.gameObject.transform.GetComponent<UnitStats>().maxHealth;
        SoldierSelections.Instance.soldierList.Add(this.gameObject);
    }
    private void Update()
    {
        slider.transform.GetComponent<Slider>().value = this.gameObject.transform.GetComponent<UnitStats>().currentHealth;
    }
    private void OnDestroy()
    {
        SoldierSelections.Instance.soldierList.Remove(this.gameObject);
    }
}
