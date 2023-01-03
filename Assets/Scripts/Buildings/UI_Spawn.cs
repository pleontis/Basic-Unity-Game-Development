using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Spawn : MonoBehaviour
{
    public GameObject unitPrefab;
    public GameObject resourcesManager;
    public GameObject limitImage;
    public Transform spawner;
    public List<GameObject> iconLoading = new List<GameObject>();

    public bool isBuilding,limitIsOpen;
    public int buildingCounter;
    private Vector3 tempCoordinates;
    public int unitCounter = 0;
    private int baseLevel,maxUnits;

    // Start is called before the first frame update
    void Start()
    {
        tempCoordinates = spawner.position;
        isBuilding = false;
    }
    private void Update()
    {
        baseLevel = gameObject.transform.GetComponent<UI_BuildingMenu>().baseLevel;
        maxUnits = resourcesManager.transform.GetComponent<ResourcesManager>().unitLimit[baseLevel];
    }

    public void Button_Spawner()
    {
        limitImage.SetActive(false);
        if (unitCounter <maxUnits )
        {

            if (!isBuilding)
            {
                unitCounter++;
                buildingCounter = 0;

                iconLoading[buildingCounter].SetActive(!iconLoading[buildingCounter].activeSelf);

                StartCoroutine("CalculateTime");
                isBuilding = true;
            }
            else
            {
                if (buildingCounter <= 3)
                {
                    unitCounter++;
                    buildingCounter++;
                    iconLoading[buildingCounter].SetActive(!iconLoading[buildingCounter].activeSelf);
                }
            }
        }
        else //Cant spawn more units
        {
            limitImage.SetActive(true);
        }

    }
    public void ActiveBuilding()
    {
        if (buildingCounter >= 0)
        {
            tempCoordinates = tempCoordinates + new Vector3(1, 0, 1);
            StartCoroutine("CalculateTime");
        }
        else
        {
            isBuilding = false;
        }
    }

    public IEnumerator CalculateTime()
    {
        for (int i = 0; i < iconLoading.Count; i++)
        {
            yield return new WaitForSeconds(1);
            iconLoading[0].GetComponentInChildren<Slider>().value++;
        }

        if (unitCounter <= maxUnits)
        {
            resourcesManager.transform.GetComponent<ResourcesManager>().UpdateGlobalUnits(1);
            Instantiate(unitPrefab, tempCoordinates, Quaternion.identity);

            iconLoading[buildingCounter].SetActive(!iconLoading[buildingCounter].activeSelf);
            buildingCounter--;
            iconLoading[0].GetComponentInChildren<Slider>().value = 0;

            ActiveBuilding();
        }
    }
}
