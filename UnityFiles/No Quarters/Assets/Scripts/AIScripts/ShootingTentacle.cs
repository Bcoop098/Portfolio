using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTentacle : MonoBehaviour
{
    [SerializeField]
    private int shootTenacleDamage = 5;

    [SerializeField]
    private float speed = 300.0f;

    private float yPos;
    float maxY = 10f;

    [SerializeField]
    GameObject shadow;
    GameObject myShadow;

    bool isVisible;

    // Start is called before the first frame update
    void Start()
    {
        isVisible = true;
        //GetComponent<Rigidbody2D>().AddForce(transform.forward * speed);
        yPos = Random.Range(1.5f, -3);
        myShadow = Instantiate(shadow, new Vector2(transform.position.x, yPos), Quaternion.identity);
        StartCoroutine(WaitToShow(3f));
        gameObject.transform.position = new Vector3(transform.position.x, -20, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isVisible)
        {
            //gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
            isVisible = true;
        }
        if(transform.position.y >= maxY)
        {
            gameObject.SetActive(false);
            myShadow.SetActive(false);
        }
        
    }

    private IEnumerator WaitToShow(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.transform.position = new Vector3(transform.position.x, myShadow.transform.position.y, 0f);
        isVisible = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerControllerV2>().TakeDamage(shootTenacleDamage);
        }
    }

    public void enableShootTentacle()
    {
        isVisible = true;
        gameObject.SetActive(true);
        myShadow.SetActive(true);
        //GetComponent<Rigidbody2D>().AddForce(transform.forward * speed);
        float randX = Random.Range(-7, 8);
        float randY = Random.Range(-3f, 3f);
        transform.position = new Vector3(randX, -20f, 0f);
        myShadow.transform.position = new Vector3(randX, randY, 0f);
        StartCoroutine(WaitToShow(3f));
    }

    public void disableShootTentacle()
    {
        gameObject.SetActive(false);
        myShadow.SetActive(false);
    }
}
