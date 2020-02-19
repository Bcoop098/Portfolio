using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CleanerScript : MonoBehaviour
{
   // static public CleanerScript cleaner;

    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private GameObject shield1;

    [SerializeField]
    private GameObject shield2;

    /*[SerializeField]
    GameObject Cleaner;*/


    bool canBeDamaged = true;

    [SerializeField]
    private int CleanerHealth = 100;

    private int machinesBroken = 0;

    public GameObject[] dryer;
    public GameObject[] washer;
    public List<GameObject> activeMachines;

    [SerializeField]
    bool isEnraged = false;

    private bool alreadyEnraged = false;

    [SerializeField]
    private float spinTime = 5f;

    [SerializeField]
    private float coinTime = 4f;

    float invincibilityFrame = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

        //StartCoroutine("WaitForSceneLoad", 5);
        //cleaner = this;
        dryer = GameObject.FindGameObjectsWithTag("Dryer");
        washer = GameObject.FindGameObjectsWithTag("Washer");
        //Cleaner = GameObject.FindGameObjectWithTag("Boss2");
        InitMachines();
        coin = GameObject.FindGameObjectWithTag("CleanerCoin");
        InvokeRepeating("ShootCoin", coinTime, coinTime);
        shield1.SetActive(false);
        shield2.SetActive(false);
        coin.GetComponent<ShootCoin>().disableCoin();
        invincibilityFrame = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getInitTimer();

        Toolbox.Instance.GetObject<AudioManager>("AudioManager").SetAudioSource(GameObject.FindObjectOfType<AudioSource>());
        Toolbox.Instance.GetObject<AudioManager>("AudioManager").SetAudioClip("bossfights");
        Toolbox.Instance.GetObject<AudioManager>("AudioManager").PlayAudio();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CleanerHealth <= 0)
        {
            CancelInvoke();
            gameObject.SetActive(false);

            Toolbox.Instance.GetObject<PlayerData>("PlayerData").updateData();
            Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").CompleteLevel(Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").GetCurrentLevelString());
            Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").AddSecretShopCoins(3);
            Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").ActivateLevel(6);
            Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").ReturnToMainMenu();
        }

        if (isEnraged)
        {
            if (alreadyEnraged == false)
            {
                alreadyEnraged = true;
                CancelInvoke();          
                InvokeRepeating("Spin", 1.0f, spinTime);
            }
        }
    }

  

    void InitMachines()
    {
        activeMachines = new List<GameObject>();
        for (int i = 0; i < dryer.Length; i++)
        {
            activeMachines.Add(dryer[i]);
        }
        for (int i = 0; i < washer.Length; i++)
        {
            activeMachines.Add(washer[i]);
        }
        
    }


    public void KillMachine(GameObject machine)
    {
        for (int i = 0; i < activeMachines.Count; i++)
        {
            if (activeMachines[i] == machine)
            {
                activeMachines.RemoveAt(i);
                break;
            }
        }
        if (activeMachines.Count == machinesBroken)
        {
            isEnraged = true;

        }
    }

    public void TakePlayerDamage(int damage)
    {
        if (canBeDamaged && isEnraged)
        {
            SetDamagedStatus(false);
            CleanerHealth -= damage;
            StartCoroutine(InvincibilityFrame());
        }
        
    }

    public bool checkEnrage2()
    {
        return isEnraged;
    }

    public int returnCleanerHealth()
    {
        return CleanerHealth;
    }

    void ShieldSpin()
    {
        Instantiate(shield1, transform);
        Instantiate(shield2, transform);
    }

    void ShootCoin()
    {
        coin.GetComponent<ShootCoin>().enableCoin();
    }

    void Spin()
    {
        shield1.SetActive(!shield1.activeSelf);
        shield2.SetActive(!shield2.activeSelf);
    }

    IEnumerator Spin2()
    {
        yield return new WaitForSeconds(5);
        shield1.SetActive(!shield1.activeSelf);
        shield2.SetActive(!shield2.activeSelf);   
    }

    public Vector2 returnPosition()
    {
        return transform.position;
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


    /*IEnumerator WaitForSceneLoad(int sceneNum)
  {
      while (SceneManager.GetActiveScene().buildIndex != sceneNum)
      {
          yield return null;
      }
      if (SceneManager.GetActiveScene().buildIndex == sceneNum)
      {
          shield1 = Resources.Load("SpinShield") as GameObject;
          shield2 = Resources.Load("SpinShield2") as GameObject;
          coin = Resources.Load("ShootyCoin") as GameObject;

          machines = GameObject.FindGameObjectsWithTag("Machine");
          Cleaner = GameObject.FindGameObjectWithTag("Boss2");
          InitMachines();

          InvokeRepeating("ShootCoin", coinTime, coinTime);
          shield1.SetActive(false);
          shield2.SetActive(false);

          invincibilityFrame = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getInitTimer();
      }
  }*/
}
