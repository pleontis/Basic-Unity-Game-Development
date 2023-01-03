using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI rockText;
    public TextMeshProUGUI unitsText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI limitText;

    public int[] goldLimits;
    private int goldLimit;
    public int[] unitLimit ;
    public double coins;
    public int wood,rock, units;
    private static ResourcesManager _instance;
    public static ResourcesManager Instance { get { return _instance; } }

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
    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
        wood = 0;
        rock = 0;
        units = 0;
        UpdateUnitsLimit(0);
        goldLimit = goldLimits[0];

        StartCoroutine(CoinGenerator());
    }
    private void Update()
    {
        unitsText.text = units.ToString();
    }
    public void UpdateGoldLimit(int baselevel)
    {
        goldLimit = goldLimits[baselevel];
    }
    public IEnumerator CoinGenerator()
    {
        yield return new WaitForSecondsRealtime(2);
        double  multiplier = UnitSelections.Instance.unitList.Count*0.1;
        coins += multiplier;
        //Display only Integer Values
        if(coins%1==0)
            coinsText.text = coins.ToString();
        else
        {
            coinsText.text = ((int)coins).ToString();
        }
        yield return new WaitUntil(()=>coins < goldLimit);
        StartCoroutine(CoinGenerator());
    }
    public void UpdateGlobalWood(int amount)
    {
        wood += amount;
        woodText.text = wood.ToString();
        
    }
    public void UpdateGlobalRock(int amount)
    {
        rock += amount;
        rockText.text = rock.ToString();
    }
    public void UpdateGlobalCoins(int amount)
    {
        coins += amount;
        //Display only Integer Values
        if (coins % 1 == 0)
            coinsText.text = coins.ToString();
        else
        {
            coinsText.text = ((int)coins).ToString();
        }
    }

    public void UpdateGlobalUnits(int amount)
    {
        units += amount;
        unitsText.text = units.ToString();
        
    }
    public void UpdateUnitsLimit(int baselevel)
    {
        limitText.text = ("/" + unitLimit[baselevel]).ToString();
    }
}
