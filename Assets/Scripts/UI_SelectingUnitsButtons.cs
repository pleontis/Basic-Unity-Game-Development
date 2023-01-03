using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SelectingUnitsButtons : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.transform.Find("Image").gameObject.transform.Find("Button Select All Units").gameObject.SetActive(false);
        this.gameObject.transform.Find("Image").gameObject.transform.Find("Toggle Soldiers").gameObject.SetActive(false);
    }
    public void ActivateForUnits()
    {
        this.gameObject.transform.Find("Image").gameObject.transform.Find("Button Select All Units").gameObject.SetActive(true);
    }   
    public void ActivateForSoldiers()
    {
        this.gameObject.transform.Find("Image").gameObject.transform.Find("Toggle Soldiers").gameObject.SetActive(true);
    }
}
