using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResourceSrcUI : MonoBehaviour
{
    public GameObject menuCanvas, mineButton,closeMenuButton;
    public TextMeshProUGUI resourceText,costText;
    public ResourceSrc resource;
    private bool exit=false;

    private void Start()
    {
        costText.text = (this.gameObject.GetComponent<ResourceSrc>().cost).ToString();
    }
    void Update()
    {
        updateRequirements();
    }
    public bool GetExit()
    {
        return exit;
    }
    public void SetExit(bool value)
    {
        exit = value;
    }
    public void OnResourceChange()
    {
        resourceText.text = resource.quant.ToString();
    }
    public void ExitPressed()
    {
        exit = true;
        CloseMenu(); 
    }
    public void OpenMenu()
    {
        //Units reached
        if (!exit)
        {
            StartCoroutine(PulseEffect()); 
        }
    }
    public IEnumerator PulseEffect()
    {
        for(int i=0; i<5; i++)
        {
            this.gameObject.transform.Find("Sphere").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            this.gameObject.transform.Find("Sphere").gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
        menuCanvas.SetActive(true);
        this.gameObject.transform.Find("Sphere").gameObject.SetActive(false);
        exit = true;
    }
    public void CloseMenu()
    {
        menuCanvas.SetActive(false);
    }
    void updateRequirements()
    {
        int coins = (int)ResourcesManager.Instance.coins;
        if(coins>= this.gameObject.GetComponent<ResourceSrc>().cost)
        {
            if(!this.gameObject.transform.GetComponent<ResourceSrc>().isGathering)
                mineButton.transform.GetComponent<Button>().interactable = true;
            costText.color = new Color(255, 255, 255);
        }
        else
        {
            mineButton.transform.GetComponent<Button>().interactable = false;
            costText.color = new Color(255, 0, 0);
        }
    }
}
