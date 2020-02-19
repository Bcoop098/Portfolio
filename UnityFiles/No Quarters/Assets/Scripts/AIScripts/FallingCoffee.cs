using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCoffee : MonoBehaviour
{
    [SerializeField]
    private int coffeeDamage = 5;

    [SerializeField]
    private float fallSpeed = 300.0f;

    private float yPos;

    [SerializeField]
    GameObject shadow;
    GameObject myShadow;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(-transform.up * fallSpeed);
        yPos = Random.Range(1.5f, -3);
        myShadow = Instantiate(shadow, new Vector2(transform.position.x, yPos), Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y <= yPos)
        {
            gameObject.SetActive(false);
            myShadow.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerControllerV2>().TakeDamage(coffeeDamage);
        }
    }

    public void enableCoffee()
    {
        gameObject.SetActive(true);
        myShadow.SetActive(true);
        GetComponent<Rigidbody2D>().AddForce(-transform.up * fallSpeed);
        float randX = Random.Range(-8, 8);
        float randY = Random.Range(10f, 15f);
        transform.position = new Vector3(randX, randY, 0f);
        myShadow.transform.position = new Vector3(randX, yPos, 0f);
    }

    public void disableCoffee()
    {
        gameObject.SetActive(false);
        myShadow.SetActive(false);
    }
}
