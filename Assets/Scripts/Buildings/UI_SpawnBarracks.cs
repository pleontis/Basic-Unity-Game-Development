using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SpawnBarracks : MonoBehaviour
{
    public struct Requirements
    {
        public int rock, wood;
    }
    private Vector3 tempCoordinates;
    public Transform spawner;
    public TextMeshProUGUI woodRequirementsText, rockRequirementsText;
    public GameObject soldierPrefab, soldierLv2Prefab, limitImage,spawnMenuImage,resourcesManager,spawnSoldierButton,slider,menuImage;
    public bool isBuilding, limitIsOpen;
    private int  maxUnits;
    public int unitCounter = 0, buildingCounter = 0;
    public Requirements[] reqs = new Requirements[2];
    public int[] requirements;
    public Sprite lv2Soldier;
    // Start is called before the first frame update
    void Start()
    {
        tempCoordinates = spawner.position;

        reqs[0].wood = requirements[0];
        reqs[0].rock = requirements[1];

        woodRequirementsText.text = reqs[0].wood.ToString();
        woodRequirementsText.color = new Color(0, 255, 0);

        rockRequirementsText.text = (reqs[0].rock).ToString();
        rockRequirementsText.color = new Color(0, 255, 0);

        tempCoordinates = spawner.position;
        isBuilding = false;
    }
    void Awake()
    {
        spawnSoldierButton.transform.GetComponent<Button>().onClick.AddListener(delegate { Button_Spawner(); });
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawnRequirements();
        maxUnits = UnitSelections.Instance.unitList.Count;
        unitCounter = SoldierSelections.Instance.soldierList.Count;
        if (limitImage.activeInHierarchy)
        {
            StartCoroutine(Waiter());
        }
    }
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(1.5f);
        CloseSpawnMenu();
    }

    public void Button_Spawner()
    {
        limitImage.SetActive(false);
        if (unitCounter < maxUnits && maxUnits>0)
        {
            if (!isBuilding)
            {
                unitCounter++;
                buildingCounter = 0;

                StartCoroutine("SpawnSoldier");
                isBuilding = true;
            }
            else
            {
                if (buildingCounter <= 3)
                {
                    unitCounter++; buildingCounter++;
                }
            }
        }
        else //Cant Spawn more units
        {
            limitImage.SetActive(true);
        }
    }
    public void OpenSpawnMenu()
    {
        spawnMenuImage.SetActive(true);
    }
    public void CloseSpawnMenu()
    {
        spawnMenuImage.SetActive(false);
    }
    public IEnumerator SpawnSoldier()
    {
        spawnSoldierButton.transform.GetComponent<Button>().interactable = false;
        slider.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1);
            slider.transform.GetComponent<Slider>().value++;
        }
        slider.transform.GetComponent<Slider>().value = 0;
        if (unitCounter <= maxUnits)
        {
            resourcesManager.transform.GetComponent<ResourcesManager>().UpdateGlobalRock(-reqs[0].rock);
            resourcesManager.transform.GetComponent<ResourcesManager>().UpdateGlobalWood(-reqs[0].wood);
            if (this.gameObject.transform.GetComponent<UI_BuildingMenu>().baseLevel==0)
            {
                Instantiate(soldierPrefab, tempCoordinates, Quaternion.identity);
            }
            else
            {
                Instantiate(soldierLv2Prefab, tempCoordinates, Quaternion.identity);
            }
            
        }
        isBuilding = false;
    }
    public void UpdateSpawnLevel()
    {
        spawnMenuImage.transform.Find("SpawnSoldierButton").transform.GetComponent<Image>().sprite = lv2Soldier;
    }
    public void UpdateSpawnRequirements()
    {
        int wood = resourcesManager.transform.GetComponent<ResourcesManager>().wood;
        int rock = resourcesManager.transform.GetComponent<ResourcesManager>().rock;
        //Check for wood requirements
        if (wood >= reqs[0].wood)
        {
            woodRequirementsText.color = new Color(0, 100, 0);
        }
        else
        {
            woodRequirementsText.color = new Color(255, 0, 0);
            spawnSoldierButton.gameObject.transform.GetComponent<Button>().interactable = false;
        }
        if (rock >= reqs[0].rock)
        {
            rockRequirementsText.color = new Color(0, 100, 0);
        }
        else
        {
            rockRequirementsText.color = new Color(255, 0, 0);
            spawnSoldierButton.gameObject.transform.GetComponent<Button>().interactable = false;
        }

        if (wood >= reqs[0].wood && rock>=reqs[0].rock && !isBuilding)
        {
            spawnSoldierButton.gameObject.transform.GetComponent<Button>().interactable = true;
        }
    }
}
