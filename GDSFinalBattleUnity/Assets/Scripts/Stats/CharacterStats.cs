
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // Health
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    private Healthbar _healthbar;
    public Stat damage;


    // Set current health to max health
    // when starting the game.
    void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        _healthbar = GetComponentInChildren<Healthbar>();
        _healthbar.SetSliderMaxValue(maxHealth);
    }
    // Damage the character
    public void TakeDamage(int damage)
    {
        // Subtract the armor value


        // Damage the character
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        _healthbar.UpdateHealth(currentHealth);

        if(this.tag != "Player")
        {
            CameraShake.Instance.ShakeCamera();
        }
        // If health reaches zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal()
    {
        currentHealth += (maxHealth - currentHealth);
        Debug.Log("HEALED!");
        _healthbar.UpdateHealth(currentHealth);
    }

    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }

}
