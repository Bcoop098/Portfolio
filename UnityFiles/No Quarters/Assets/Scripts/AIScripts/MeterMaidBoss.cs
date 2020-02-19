using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterMaidBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject doomBall;

    [SerializeField]
    private GameObject tentacle;

    [SerializeField]
    float[] possibleSpawnsX;

    [SerializeField]
    float[] possibleSpawnsY;

    bool canBeDamaged = false;

    [SerializeField]
    private int maidHealth = 100;

    [SerializeField]
    bool isEnraged = false;

    [SerializeField]
    bool isReallyMad = false;

    private bool alreadyPissed = false;

    private bool alreadyEnraged = false;

    float invincibilityFrame = 0f;

    [SerializeField]
    private GameObject[] tentacleArray;
    [SerializeField]
    private List<GameObject> activeTentacles;

    private List<float> xPositions;
    private List<float> yPositions;

    public GameObject[] shootyTentacles;

    // Start is called before the first frame update
    void Start()
    {
        doomBall = GameObject.FindGameObjectWithTag("DoomBall");
        shootyTentacles = GameObject.FindGameObjectsWithTag("ShootedTentacle");
        foreach (GameObject tentacle in shootyTentacles)
        {
            tentacle.GetComponent<ShootingTentacle>().disableShootTentacle();
        }
        doomBall.GetComponent<ShootDoomBall>().disableDoom();
        invincibilityFrame = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getInitTimer();
        InitXPositions();
        InitYPositions();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (maidHealth <= 0)
        {
            CancelInvoke();
            gameObject.SetActive(false);

            Toolbox.Instance.GetObject<PlayerData>("PlayerData").updateData();
            Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").CompleteLevel(Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").GetCurrentLevelString());
            Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").ReturnToMainMenu();
        }

        if (isEnraged)
        {
            if (alreadyEnraged == false)
            {
                alreadyEnraged = true;
                ReleaseTentacles();
            }
        }

        if (isReallyMad)
        {
            if (alreadyPissed == false)
            {
                alreadyPissed = true;
                canBeDamaged = true;
                InvokeRepeating("FireDoomBall", 5f, 7f);
                InvokeRepeating("ShootTentacle", 4f, 10f);
            }
        }
    }

    public void TakePlayerDamage(int damage)
    {
        if (canBeDamaged && isEnraged && isReallyMad)
        {
            SetDamagedStatus(false);
            maidHealth -= damage;
            StartCoroutine(InvincibilityFrame());
        }

    }

    void InitTentacles()
    {
        activeTentacles = new List<GameObject>();
        for (int i = 0; i < tentacleArray.Length; i++)
        {
            activeTentacles.Add(tentacleArray[i]);
        }
    }

    void InitXPositions()
    {
        xPositions = new List<float>();
        for (int i = 0; i < possibleSpawnsX.Length; i++)
        {
            xPositions.Add(possibleSpawnsX[i]);
        }
    }

    void InitYPositions()
    {
        yPositions = new List<float>();
        for (int i = 0; i < possibleSpawnsY.Length; i++)
        {
            yPositions.Add(possibleSpawnsY[i]);
        }
    }
    public void KillTentacle(GameObject tentacle)
    {
        for (int i = 0; i < activeTentacles.Count; i++)
        {
            if (activeTentacles[i] == tentacle)
            {
                activeTentacles.RemoveAt(i);
                break;
            }
        }
        if (activeTentacles.Count == 0)
        {
            makePissed();
        }
    }

    void FireDoomBall()
    {
        doomBall.GetComponent<ShootDoomBall>().enableDoom();
    }

    void ShootTentacle()
    {
        foreach (GameObject tentacle in shootyTentacles)
        {
            tentacle.GetComponent<ShootingTentacle>().enableShootTentacle();
        }
    }
    void ReleaseTentacles()
    {
        for (int i = 0; i < 4; i++)
        {
            int randXIndex = Random.Range(0, xPositions.Count);
            int randYIndex = Random.Range(0, yPositions.Count);
            Instantiate(tentacle, new Vector3(xPositions[randXIndex], yPositions[randYIndex], 0), Quaternion.identity);
            removeXSpawn(xPositions[randXIndex]);
            removeYSpawn(yPositions[randYIndex]);
        }
        tentacleArray = GameObject.FindGameObjectsWithTag("Tentacle");
        InitTentacles();
    }

    void removeXSpawn(float val)
    {
        for (int i = 0; i < xPositions.Count; i++)
        {
            if (xPositions[i] == val)
            {
                xPositions.RemoveAt(i);
                break;
            }
        }
    }

    void removeYSpawn(float val)
    {
        for (int i = 0; i < yPositions.Count; i++)
        {
            if (yPositions[i] == val)
            {
                yPositions.RemoveAt(i);
                break;
            }
        }
    }


    public bool checkEnrage4()
    {
        return isEnraged;
    }

    public bool checkIfPissed()
    {
        return isReallyMad;
    }
    public int returnHealth()
    {
        return maidHealth;
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

    public void setEnrageMaid()
    {
        isEnraged = true;
    }

    public void makePissed()
    {
        isReallyMad = true;
    }
}
