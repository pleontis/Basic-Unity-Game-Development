using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BuildingLootMenu : MonoBehaviour
{
    public int woodLoot, rockLoot;
    public TextMeshProUGUI titleText,reqsText,rockText,woodText;
    public GameObject menuImage,blockImage,resourcesManager,buttonLoot,slider,messageImage;
    public int reqs;
    // Start is called before the first frame update
    void Start()
    {
        titleText.text = "Loot Tower";
        reqsText.color = new Color(0, 255, 0);
        reqsText.text = reqs.ToString();

        rockText.text = rockLoot.ToString();
        woodText.text = woodLoot.ToString();   
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRequirements();
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if(SoldierSelections.Instance.soldierSelected.Count>0)
                StartCoroutine(OpenBuildingMenu());
        }
    }
    IEnumerator OpenBuildingMenu()
    {
        yield return new WaitForSeconds(3f);
        menuImage.SetActive(true);
        blockImage.SetActive(true);
    }
    public void LootBuilding()
    {
        StartCoroutine(Loot());
    }
    IEnumerator Loot()
    {
        slider.SetActive(true);
        buttonLoot.transform.GetComponent<Button>().interactable = false;
        resourcesManager.transform.GetComponent<ResourcesManager>().UpdateGlobalCoins(-reqs);
        for(int i = 0; i < 15; i++)
        {
            this.transform.Find("Sphere").gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            slider.transform.GetComponent<Slider>().value++;
            this.transform.Find("Sphere").gameObject.SetActive(false);

        }
        resourcesManager.transform.GetComponent<ResourcesManager>().UpdateGlobalWood(woodLoot);
        resourcesManager.transform.GetComponent<ResourcesManager>().UpdateGlobalRock(rockLoot);
        buttonLoot.transform.GetComponent<Button>().interactable = false;
        CloseMenu();
        messageImage.SetActive(true);
        yield return new WaitForSeconds(2f);
        messageImage.SetActive(false);
        Destroy(gameObject);
       
    }
    void UpdateRequirements()
    {
        int coins =(int) resourcesManager.transform.GetComponent<ResourcesManager>().coins;
        if (coins >= reqs)
        {
            buttonLoot.transform.GetComponent<Button>().interactable = true;
            reqsText.color = new Color(0, 100, 0);
        }
        else
        {
            buttonLoot.transform.GetComponent<Button>().interactable = false;
            reqsText.color = new Color(255, 0, 0);
        }
    }
    public void CloseMenu()
    {
        menuImage.SetActive(false);
        blockImage.SetActive(false);
    }
}
