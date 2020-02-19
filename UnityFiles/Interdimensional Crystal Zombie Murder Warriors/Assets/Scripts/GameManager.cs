using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private int currentWave = 1;

    private int maxWaves = 0;

    [SerializeField]
    private Image nexusHealthBar;

    [SerializeField]
    private int maxNexusHealth = 100;

    private int nexusHealth = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetNexusHealth();
        SetNexusHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        SetNexusHealthBar();
        CheckNexusHealth();
    }

    public void UpdateWave()
    {
        if (currentWave == maxWaves)
        {
            SceneManager.LoadScene("YouWin");
        }
        else
        {
            currentWave++;
        }
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    public void SetMaxWaves(int _count)
    {
        maxWaves = _count;
        return;
    }

    public void DamageNexus(int _damage)
    {
        nexusHealth -= _damage;
        nexusHealth = Mathf.Max(0, nexusHealth);

        SetNexusHealthBar();

        return;
    }

    public void ResetNexusHealth()
    {
        nexusHealth = maxNexusHealth;
        return;
    }

    public void SetNexusHealthBar()
    {
        nexusHealthBar.fillAmount = (float)nexusHealth / (float)maxNexusHealth;
        return;
    }

    private void CheckNexusHealth()
    {
        if (nexusHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
