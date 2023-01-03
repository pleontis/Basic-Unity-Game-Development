using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSelections : MonoBehaviour
{
    public GameObject UnitSelect;
    public List<GameObject> soldierList = new List<GameObject>();
    public List<GameObject> soldierSelected = new List<GameObject>();

    private static SoldierSelections _instance;
    public static SoldierSelections Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void ShiftClickSelectSoldier(GameObject soldierToAdd)
    {
        if (!soldierSelected.Contains(soldierToAdd))
        {
            soldierSelected.Add(soldierToAdd);
            soldierToAdd.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            soldierToAdd.transform.GetChild(0).gameObject.SetActive(false);
            soldierSelected.Remove(soldierToAdd);
        }
    }
    public void SelectAllSoldiers(bool isPressed)
    {
        if (isPressed)
        {
            foreach (GameObject soldier in soldierList)
            {
                ShiftClickSelectSoldier(soldier);
                UnitSelections.Instance.DeselectAll();
            }
        }
        else
        {
            DeselectAllSoldiers();
            UnitSelect.transform.GetComponent<UnitSelections>().gameObject.SetActive(true);
        }
    }
    public void DeselectAllSoldiers()
    {
        foreach (GameObject soldier in soldierSelected)
        {
            soldier.transform.GetChild(0).gameObject.SetActive(false);
        }
        soldierSelected.Clear();
    }
}
