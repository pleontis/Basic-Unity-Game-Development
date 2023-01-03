using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BuildingMenu : MonoBehaviour
{
    public struct Requirements
    {
        public int wood, rock;
    }

    public TextMeshProUGUI woodRequirementsText, rockRequirementsText, title,upgradeText;
    public GameObject resourcesManager, menuImage, limitImage, upgradeImage, mainMenu, blockImage,upgradeMenuButton,enemiesObject;
    public GameObject upgradeButton;
    public Requirements[] reqs = new Requirements[2];
    public int[] requirements;
    private bool menuIsActive = false;
    public int baseLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        reqs[0].wood = requirements[0];
        reqs[0].rock = requirements[1];
        woodRequirementsText.text = reqs[0].wood.ToString();
        woodRequirementsText.color = new Color(0, 255, 0);

        rockRequirementsText.text = (reqs[0].rock).ToString();
        rockRequirementsText.color = new Color(0, 255, 0);

        title.text = this.gameObject.name + " Lv." + (baseLevel + 1);
    }
    private void Update()
    {
        UpdateUpgradeRequirements();
        menuIsActive = mainMenu.activeSelf;
        upgradeText.text = "Upgrade Lv." + (baseLevel + 2);
        title.text = this.gameObject.name + " Lv." + (baseLevel + 1);
        if (limitImage.activeInHierarchy)
        {
            StartCoroutine(Waiter());   
        }
    }
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(1.5f);
        CloseMenuImage();
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if (!menuIsActive)
                StartCoroutine(OpenBuildingMenu());
        }
    }
    IEnumerator OpenBuildingMenu()
    {
        yield return new WaitForEndOfFrame();
        menuImage.SetActive(true);
        blockImage.SetActive(true);
    }

    public void UpgradeBuilding()
    {
        baseLevel++;
        resourcesManager.transform.GetComponent<ResourcesManager>().UpdateGlobalWood(-reqs[0].wood);
        resourcesManager.transform.GetComponent<ResourcesManager>().UpdateGlobalRock(-reqs[0].rock);
        if (this.gameObject.transform.name.Equals("Town Hall")) 
        {
            resourcesManager.transform.GetComponent<ResourcesManager>().UpdateUnitsLimit(baseLevel);
            resourcesManager.transform.GetComponent<ResourcesManager>().UpdateGoldLimit(baseLevel);
        }
        if (this.gameObject.transform.name.Equals("Barracks")) 
        {
            enemiesObject.SetActive(true);
        }

        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        upgradeMenuButton.SetActive(false);
        CloseUpgradeImage();
    }
    public void OpenUpgradeImage()
    {
        upgradeImage.SetActive(true);
    }
    public void CloseUpgradeImage()
    {
        upgradeImage.SetActive(false);
    }
    public void CloseMenuImage()
    {
        menuImage.SetActive(false);
        limitImage.SetActive(false);
        blockImage.SetActive(false);
    }
    public void UpdateUpgradeRequirements()
    {

        int rock = (int)resourcesManager.transform.GetComponent<ResourcesManager>().rock;
        int wood = resourcesManager.transform.GetComponent<ResourcesManager>().wood;

        //Check for wood requirements
        if (wood >= reqs[0].wood)
        {
            woodRequirementsText.color = new Color(0, 100, 0);
        }
        else
        {
            woodRequirementsText.color = new Color(255, 0, 0);
            upgradeButton.gameObject.transform.GetComponent<Button>().interactable = false;
        }
        //Check for rock requirements
        if (rock >= reqs[0].rock)
        {
            rockRequirementsText.color = new Color(0, 100, 0);
        }
        else
        {
            rockRequirementsText.color = new Color(255, 0, 0);
            upgradeButton.gameObject.transform.GetComponent<Button>().interactable = false;
        }

        if (wood >= reqs[0].wood && rock >= reqs[0].rock)
        {
            upgradeButton.gameObject.transform.GetComponent<Button>().interactable = true;
        }
    }
}

