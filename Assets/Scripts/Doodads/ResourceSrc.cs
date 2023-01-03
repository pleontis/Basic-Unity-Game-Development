using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum ResourceType
{
    Wood,
    Rock
}
public class ResourceSrc : MonoBehaviour
{
    public GameObject slider,mineButton;
    public ResourceType type;
    public int quant;
    //Event happens
    public UnityEvent onQuantChange;
    public int amountToGive;
    public int cost;
    public bool isGathering=false;

    public void GatherResource(int amount)
    {
        StartCoroutine(Waiter(amount));
    }
    IEnumerator Waiter(int amount)
    {
        isGathering = true;
        mineButton.transform.GetComponent<Button>().interactable=false;
        quant -= amount;
        amountToGive = amount;
        if (quant < 0)
        {
            amountToGive = amount + quant;
        }
        if (onQuantChange != null)
        {
            ResourcesManager.Instance.UpdateGlobalCoins(-cost);

            for (int i = 0; i < 5; i++)
            {
                transform.Find("Sphere").gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                slider.transform.GetComponent<Slider>().value++;
            }
            slider.transform.GetComponent<Slider>().value = 0;
            transform.Find("Sphere").gameObject.SetActive(false);

            if (type.Equals(ResourceType.Wood))
            {
                ResourcesManager.Instance.UpdateGlobalWood(amountToGive);
            }

            if (type.Equals(ResourceType.Rock))
            {
                ResourcesManager.Instance.UpdateGlobalRock(amountToGive);
            }
        }
        if (quant <= 0)
        {
            Destroy(gameObject);
        }
        
        onQuantChange.Invoke();
        isGathering = false;
    }
}
