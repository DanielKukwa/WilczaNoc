
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // Health
    public int maxHealth = 100;
    public int currentHealth; //{ get; private set; }
    private Healthbar _healthbar;
    //public Stat damage;
    //public Stat damage2;
    

    [Header("Damage")]
    public int damageValue;
    public int damageValue2;
    public int damageModifier;
    // Set current health to max health
    // when starting the game.
    void Awake()
    {
        currentHealth = maxHealth;
        damageModifier = 1;
    }

    private void Start()
    {
        _healthbar = GetComponentInChildren<Healthbar>();
        _healthbar.SetSliderMaxValue(maxHealth);
        

    }


    public int GetValue()
    {
        return damageValue * damageModifier; 
    }

    public int GetValue2()
    {
        return damageValue2 * damageModifier;
    }

    // Damage the character
    public void TakeDamage(int damage)
    {
        // Subtract the armor value

        // Damage the character
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        _healthbar.UpdateHealth(currentHealth);
        //IncreaseDamage();    UNCOMMENT IF NEEDED

        if (this.tag != "Player")
        {
            CameraShake.Instance.ShakeCamera();
            AudioManager.Instance.PlayWolfHit();
            Debug.Log("WOWOWF");
        }
        // If health reaches zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage2(int damage2)
    {
        // Subtract the armor value


        // Damage the character
        currentHealth -= damage2;
        Debug.Log(transform.name + " takes " + damage2 + " damage.");
        _healthbar.UpdateHealth(currentHealth);
        //IncreaseDamage();  UNCOMMENT IF NEEDED

        if (this.tag != "Player")
        {
            CameraShake.Instance.ShakeCamera();
            AudioManager.Instance.PlayWolfHit();
            Debug.Log("WOWOWF");
        }
        // If health reaches zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal()
    {
        if (currentHealth <= maxHealth / 2)
        {
            currentHealth += (maxHealth / 2);
        }
        else
        {
            currentHealth += (maxHealth - currentHealth);
        }
        Debug.Log("HEALED!");
        _healthbar.UpdateHealth(currentHealth);
    }

    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }

    public void IncreaseDamage()
    {

        //if (this.tag == "Player")
        //{
            if (currentHealth <= 0.75 * maxHealth && currentHealth > 0.5 * maxHealth)
            {
                Debug.Log("DAMAGE1");
                damageModifier = 1 + 1/2;
                //damageValue = damageValue * damageModifier;
                
            }
            else if (currentHealth <= 0.5 * maxHealth && currentHealth > 0.25 * maxHealth)
            {
                Debug.Log("DAMAGE2");
                damageModifier = 2;
                //damageValue = damageValue * damageModifier;
            }
            else if (currentHealth < 0.25 * maxHealth)
            {
                Debug.Log("DAMAGE3");
                damageModifier = 4;
                //damageValue = damageValue * damageModifier;
            }
            else
            {
                damageModifier = 1;
            }


        //}

    }
}
