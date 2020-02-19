using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 20;
    [SerializeField]
    public int health = 0;
    [SerializeField]
    private Transform nexusLocation;
    [SerializeField]
    private GameObject bloodSplat;

    [SerializeField]
    private int healAmount = 1;

    [SerializeField]
    private int healDelay = 3;

    [SerializeField]
    private Image healthBar;

    private float bloodLife = 0.75f;

    private bool dead = false;

    private bool healing = true;

    private bool isOnNexus = true;

    private bool isLowHealth = false;

    private int lowHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.fillAmount = (float)health / (float)maxHealth;
        lowHealth = (int)(maxHealth * 0.3);
    }

    private void Update()
    {
        CheckHealth();
        Vector3 nexusPosition = nexusLocation.position;
        nexusPosition.z = transform.position.z;

        if (transform.position == nexusPosition && !healing && health < maxHealth && isOnNexus)
        {
            AudioManager.instance.PlayHealing(gameObject.name);

            healing = true;
            StartCoroutine("Heal");

        }
        else if (transform.position != nexusPosition)
        {
            healing = false;
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void DamageHealth(int _amount)
    {
        health -= _amount;

        health = Mathf.Max(health, 0);

        healthBar.fillAmount = (float)health / (float)maxHealth;

        Vector3 bloodPosition = transform.position;

        bloodPosition.z = -1;

        GameObject blood = Instantiate(bloodSplat, bloodPosition, Quaternion.identity);

        Destroy(blood, bloodLife);

        return;
    }

    public void HealHealth(int _amount)
    {
        health += _amount;

        health = Mathf.Min(health, maxHealth);

        healthBar.fillAmount = (float)health / (float)maxHealth;

        return;
    }

    private void CheckHealth()
    {
        if (health <= 0 && !dead)
        {
            dead = true;

            Vector3 nexusPosition = nexusLocation.position;
            nexusPosition.z = transform.position.z;
            transform.position = nexusPosition;

            isOnNexus = true;
        }

        if (health <= lowHealth && !isLowHealth)
        {
            isLowHealth = true;
            AudioManager.instance.PlayLowHealth(gameObject.name);
        }

        // todo: add in a new if statement that plays audio cue when the player is at low health
    }

    private IEnumerator Heal()
    {
        while (dead || healing)
        {
            HealHealth(healAmount);

            if (health == maxHealth)
            {
                AudioManager.instance.PlayFullHealth(gameObject.name);
                healing = false;
                dead = false;
                break;
            }
            else if (health > lowHealth)
            {
                isLowHealth = false;
            }

            yield return new WaitForSeconds(healDelay);

            StartCoroutine("Heal");
        }
    }

    public bool IsDead()
    {
        return dead;
    }

    public void SetIsOnNexus(bool _onNexus)
    {
        isOnNexus = _onNexus;

        if (!isOnNexus)
        {
            StopCoroutine("Heal");
        }
        return;
    }
}