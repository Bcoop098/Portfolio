using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankerScript : MonoBehaviour
{
    //static public BankerScript banker;

    [SerializeField]
    private GameObject coin;

    bool canBeDamaged = true;

    [SerializeField]
    private int bankerHealth = 100;

    [SerializeField]
    bool isEnraged = false;

    private bool alreadyEnraged = false;

    float invincibilityFrame = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //banker = this;
        coin = GameObject.FindGameObjectWithTag("BankerCoin");
        coin.GetComponent<ShootCoin2>().disableCoin2();
        invincibilityFrame = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getInitTimer();
        Toolbox.Instance.GetObject<AudioManager>("AudioManager").SetAudioSource(GameObject.FindObjectOfType<AudioSource>());
        Toolbox.Instance.GetObject<AudioManager>("AudioManager").SetAudioClip("bossfights");
        Toolbox.Instance.GetObject<AudioManager>("AudioManager").PlayAudio();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bankerHealth <= 0)
        {
            CancelInvoke();
            gameObject.SetActive(false);

            //Complete Boss
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").updateData();
            Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").CompleteLevel(Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").GetCurrentLevelString());
            Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").AddSecretShopCoins(3);
            Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").ActivateLevel(8);
            Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").ReturnToMainMenu();
        }

        if (isEnraged)
        {
            if (alreadyEnraged == false)
            {
                alreadyEnraged = true;
                InvokeRepeating("ThrowBar", 5f, 10f);
            }
        }
    }

    void ThrowBar() 
    {
        coin.GetComponent<ShootCoin2>().enableCoin2();
    }

    public void TakePlayerDamage(int damage)
    {
        if (canBeDamaged && isEnraged)
        {
            SetDamagedStatus(false);
            bankerHealth -= damage;
            StartCoroutine(InvincibilityFrame());
        }

    }

    public bool checkEnrage3()
    {
        return isEnraged;
    }

    public int returnHealth()
    {
        return bankerHealth;
    }
    void SetDamagedStatus(bool canHit)
    {
        canBeDamaged = canHit;
    }
    IEnumerator InvincibilityFrame()
    {
        yield return new WaitForSeconds(invincibilityFrame);
        SetDamagedStatus(true);
    }

    public void setEnrage()
    {
        isEnraged = true;
    }
}
