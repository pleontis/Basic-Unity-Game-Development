using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth { get; private set; }
    public Stat damage;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth<=0)
        {
            SoldierSelections.Instance.soldierSelected.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
    public void Attack(GameObject enemy)
    {
        if(enemy!=null)
        enemy.transform.GetComponent<UnitStats>().TakeDamage(this.damage.GetValue());
    }
}
