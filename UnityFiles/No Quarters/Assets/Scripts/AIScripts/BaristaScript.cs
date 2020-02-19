using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BaristaScript : MonoBehaviour
{
    /*
    [SerializeField]
    private GameObject croissantShoot;

    private Transform CroissantSpawn;

    // Start is called before the first frame update
    void Start()
    {
        CroissantSpawn = transform.Find("CroissantSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating
    }*/

    bool canBeDamaged = true;

    //static public BaristaScript barista;

    [SerializeField]
    private GameObject croissantShoot = null;

    private Transform CroissantSpawn;

    [SerializeField]
    private int baristaHealth = 100;

    [SerializeField]
    private int meleeDamage = 5;

    [SerializeField]
    private int tablesDestroyed = 3;

    private float coffeeRespawnY = 10f;


    [SerializeField]
    GameObject Croissant;

    [SerializeField]
    GameObject Barista;

    public GameObject[] tables;
    public List<GameObject> activeTableList;

    public GameObject[] coffee;

    [SerializeField]
    private bool isEnraged = false;

    private bool alreadyEnraged = false;


    float invincibilityFrames = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Croissant = GameObject.FindGameObjectWithTag("Boomerrang");
        coffee = GameObject.FindGameObjectsWithTag("Coffee");
        croissantShoot = GameObject.FindGameObjectWithTag("Croissant");
        StartCoroutine("WaitForSceneLoad", 3);
        foreach (GameObject coffeeBean in coffee)
        {
            coffeeBean.GetComponent<FallingCoffee>().disableCoffee();
        }
        Croissant.GetComponent<Boomerang>().disableBoomerrang();
        croissantShoot.GetComponent<CroissantShot>().disableShot();

        Toolbox.Instance.GetObject<AudioManager>("AudioManager").SetAudioSource(GameObject.FindObjectOfType<AudioSource>());
        Toolbox.Instance.GetObject<AudioManager>("AudioManager").SetAudioClip("bossfights");
        Toolbox.Instance.GetObject<AudioManager>("AudioManager").PlayAudio();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (baristaHealth <= 0)
        {
            CancelInvoke();
            gameObject.SetActive(false);

            Toolbox.Instance.GetObject<PlayerData>("PlayerData").updateData();
            Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").CompleteLevel(Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").GetCurrentLevelString());
            Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").AddSecretShopCoins(3);
            //Unlock Level 2
            Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").ActivateLevel(4);
            //Unlock Secret Shop
            Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").ActivateLevel(10);
            //return to main menu
            Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").ReturnToMainMenu();
        }

        if (isEnraged)
        {
            if (alreadyEnraged == false)
            {
                alreadyEnraged = true;
                BaristaAnim.baristaAnimInstance.SetBarCurrentAnimations(0);
                CancelInvoke();
                InvokeRepeating("StraightShot", 8.0f, 8.0f);
            }
        }
    }

    IEnumerator WaitForSceneLoad(int sceneNum)
    {
        while (SceneManager.GetActiveScene().buildIndex != sceneNum)
        {
            yield return null;
        }
        if (SceneManager.GetActiveScene().buildIndex == sceneNum)
        {
            //coffee = Resources.Load("Coffee") as GameObject;
            //Croissant = Resources.Load("Croissant") as GameObject;
            //croissantShoot = Resources.Load("CroissantShoot") as GameObject;
            InvokeRepeating("DropTheCoffee", 7.0f, 7.0f);
            InvokeRepeating("CROISSANT", 10.0f, 12.0f);
            CroissantSpawn = transform.Find("CroissantSpawn");
            BaristaAnim.baristaAnimInstance.SetBarCurrentAnimations(2);
            tables = GameObject.FindGameObjectsWithTag("Table");
            invincibilityFrames = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getInitTimer();
            Barista = GameObject.FindGameObjectWithTag("Boss");
            InitTables();
        }
    }

    void InitTables()
    {
        activeTableList = new List<GameObject>();
        for (int i = 0; i < tables.Length; i++)
        {
            activeTableList.Add(tables[i]);
        }
    }

    void DropTheCoffee()
    {
        foreach (GameObject coffeeBean in coffee)
        {
            coffeeBean.GetComponent<FallingCoffee>().enableCoffee();
        }
    }

    void CROISSANT()
    {
        Croissant.GetComponent<Boomerang>().activateBoomerrang();
    }

    public void KillTable(GameObject table)
    {
        for (int i = 0; i < activeTableList.Count; i++)
        {
            if (activeTableList[i] == table)
            {
                activeTableList.RemoveAt(i);
                break;
            }
        }
        if (activeTableList.Count == tablesDestroyed)
        {
            isEnraged = true;
            
        }
    }

    void StraightShot()
    {
        croissantShoot.GetComponent<CroissantShot>().enableShot();
    }

    public void TakePlayerDamage(int damage)
    {
        if (canBeDamaged && isEnraged)
        {
            SetDamagedStatus(false);
            baristaHealth -= damage;
            StartCoroutine(InvincibilityFrame());
        }
        
    }

    public bool checkEnrage()
    {
        return isEnraged;
    }

    public int returnHealth()
    {
        return baristaHealth;
    }
    void SetDamagedStatus(bool canHit)
    {
        canBeDamaged = canHit;
    }
    IEnumerator InvincibilityFrame()
    {
        yield return new WaitForSeconds(invincibilityFrames);
        SetDamagedStatus(true);
    }
}
