using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_CreateBuilding : MonoBehaviour
{
    public struct Requirements
    {
        public int wood, rock;
    }
    public TextMeshProUGUI woodRequirementsText,rockRequirementsText;
    public GameObject createBuildingMenu,blockerImage,resourcesManager,buildingMenuUI,createBuildingMenuUI,newMessageSpawnImage;
    public Button barracksButton;
    public Requirements[] reqs = new Requirements[2];
    private bool isBuilding = false;
    public AudioSource buildSound;

    // Start is called before the first frame update
    void Start()
    {
        reqs[0].wood = 25;
        reqs[0].rock = 10;
        woodRequirementsText.text = reqs[0].wood.ToString();
        woodRequirementsText.color = new Color(0, 255, 0);

        rockRequirementsText.text = (reqs[0].rock).ToString();
        rockRequirementsText.color = new Color(0, 255, 0);

    }
    void Awake()
    {
        barracksButton.onClick.AddListener(delegate { CreateBuilding("Barracks");});
    }
    // Update is called once per frame
    void Update()
    {
        UpdateUpgradeRequirements();
    }
    private void OnMouseOver()
    {
        
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if(UnitSelections.Instance.unitsSelected.Count>0)
                OpenCreateBuildingMenu();
        }
    }
    private void OpenCreateBuildingMenu()
    { 
        createBuildingMenu.SetActive(true);
        blockerImage.SetActive(true);
    }
    public void CloseCreateBuildingMenu()
    {
        blockerImage.SetActive(false);
        createBuildingMenu.SetActive(false);  
    }
    void CreateBuilding(string buildingTitle)
    {
        StartCoroutine(NewBuilding(buildingTitle));
    }
    IEnumerator NewBuilding(string title)
    {
        isBuilding = true;
        buildSound.PlayOneShot(buildSound.clip);
        barracksButton.transform.GetComponent<Button>().interactable = false;
        barracksButton.transform.Find("Slider").gameObject.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1);
           barracksButton.GetComponentInChildren<Slider>().value++;
        }
        barracksButton.GetComponentInChildren<Slider>().value=0;
        isBuilding = false;
        resourcesManager.transform.GetComponent<ResourcesManager>().UpdateGlobalWood(-reqs[0].wood);
        resourcesManager.transform.GetComponent<ResourcesManager>().UpdateGlobalRock(-reqs[0].rock);
        
        //Deactivate canvas for new building menu and activate
        CloseCreateBuildingMenu();

        this.gameObject.transform.Find("newBuilding").gameObject.SetActive(false);
        //Inform User
        newMessageSpawnImage.SetActive(true);
        StartCoroutine(waiter());

        this.gameObject.transform.Find("barracklevel0").gameObject.SetActive(true);
       

        this.gameObject.transform.GetComponent<UI_BuildingMenu>().gameObject.SetActive(true);
        buildingMenuUI.SetActive(true);
        this.gameObject.transform.GetComponent<UI_BuildingMenu>().CloseMenuImage();
        buildSound.Stop();
    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(2f);
        newMessageSpawnImage.SetActive(false);
        createBuildingMenuUI.SetActive(false);
    }
    void UpdateUpgradeRequirements()
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
            barracksButton.gameObject.transform.GetComponent<Button>().interactable = false;
        }
        //Check for rock requirements
        if (rock >= reqs[0].rock)
        {
            rockRequirementsText.color = new Color(0, 100, 0);
        }
        else
        {
            rockRequirementsText.color = new Color(255, 0, 0);
            barracksButton.gameObject.transform.GetComponent<Button>().interactable = false;
        }

        if (wood >= reqs[0].wood && rock >= reqs[0].rock)
        {
            if(!isBuilding)
                barracksButton.gameObject.transform.GetComponent<Button>().interactable=true;
        }
    }
}
