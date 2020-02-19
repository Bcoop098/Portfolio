using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerV2 : MonoBehaviour
{
    /*
        Animation Reference Numbers
        0 = Idle
        1 = Walk
        2 = Attack
        3 = Jump
    */
    public static PlayerControllerV2 playerInstance;

    float startingYPos;

    float endXPos;
    float endYPos;

    float moveSpd = 1f;
    float moveSpdNoBuff = 1f;

    [SerializeField]
    int playerHealth = 100;
    [SerializeField]
    Text health;

    bool isJumping = false;

    public int controllerID;
    private string horz;
    private string vert;
    private string attack;
    private string jump;

    public GameObject player;

    GameObject weapon;
    [SerializeField]
    float coolDown = 1.0f;

    bool flip = false;
    bool canAttack = true;

    public Vector2 playerVelocity;

    [SerializeField]
    private float speedBoostTime = 3f;

    private void Awake()
    {
        player = this.gameObject;
        if (player)
        {
            weapon = player.transform.Find("Weapon").gameObject;
        }

        if(gameObject.gameObject.name == "PlayerUnitP1(Clone)")
        {
            playerHealth = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getPlayerHealth(0);
            moveSpd = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getMoveSpd(0);
            moveSpdNoBuff = moveSpd;
            coolDown = weapon.GetComponent<WeaponScript>().coolDown;
        }
        else if (gameObject.gameObject.name == "PlayerUnitP2(Clone)")
        {
            playerHealth = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getPlayerHealth(1);
            moveSpd = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getMoveSpd(1);
            moveSpdNoBuff = moveSpd;
            coolDown = weapon.GetComponent<WeaponScript>().coolDown;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        horz = "HorizontalP" + controllerID.ToString();
        vert = "VerticalP" + controllerID.ToString();
        attack = "XBut" + controllerID.ToString();
        playerInstance = this;

        string healthName = "HUD/Health P" + controllerID;

        health = GameObject.Find(healthName).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        UpdateHealth();
    }

    public void SpeedChange()
    {
        moveSpd += 1.5f;
        StartCoroutine(SpeedBoost(speedBoostTime));
    }

    private IEnumerator SpeedBoost(float time)
    {
        yield return new WaitForSeconds(time);
        moveSpd = moveSpdNoBuff;
    }

    void CheckInput()
    {        

        if (Mathf.Abs(Input.GetAxis(horz)) > 0.1f || Mathf.Abs(Input.GetAxis(vert)) > 0.1f)
        {
            PlayerAnim.playerAnimInstance.SetCurrentAnimations(1);
            if (Input.GetAxis(horz) > 0.1f)
            {
                player.GetComponent<SpriteRenderer>().flipX = false;
                flip = false;
            }
            if (Input.GetAxis(horz) < -0.1f)
            {
                player.GetComponent<SpriteRenderer>().flipX = true;
                flip = true;
            }

            playerVelocity = new Vector2(Input.GetAxis(horz), Input.GetAxis(vert)).normalized;

            endYPos = player.transform.position.y + playerVelocity.y * (moveSpd * Time.deltaTime);
            endXPos = player.transform.position.x + playerVelocity.x * (moveSpd * Time.deltaTime);
            player.transform.position = new Vector3(endXPos, endYPos, player.transform.position.z);
        }
        else
        {
            PlayerAnim.playerAnimInstance.SetCurrentAnimations(4);
        }
        if (Input.GetButton(attack) && canAttack)
        {
            Slash();
            PlayerAnim.playerAnimInstance.SetCurrentAnimations(5);
            StartCoroutine(AttackCoolDown());
        }
    }

    public void Slash()
    {
            //this.gameObject.GetComponent<PlayerAttackV2>().Attack(1f);

        if(canAttack)
        {
            if (flip)
            {
                this.gameObject.GetComponent<PlayerAttackV2>().Attack(-1f);
            }
            else
            {
                this.gameObject.GetComponent<PlayerAttackV2>().Attack(1f);
            }
        }
        StartCoroutine(AttackCoolDown());

    }

    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
    }

    void UpdateHealth()
    {
        if (playerHealth <= 25)
        {
            health.color = Color.red;
            if(playerHealth <= 0)
            {
                Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").Death(Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").GetCurrentLevelString());
                Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").LoadByName("Lose");
            }
        }
        else if (playerHealth <= 50)
        {
            health.color = Color.yellow;
        }
        else
        {
            health.color = Color.green;
        }
        health.text = "HP:" + playerHealth;
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
    }

    public int getCurrentHealth() { return playerHealth; }
    public int getCurrentWeaponDmg() { return weapon.GetComponent<WeaponScript>().weaponDamage; }
    public float getCurrentSpeed() { return moveSpd; }
}
