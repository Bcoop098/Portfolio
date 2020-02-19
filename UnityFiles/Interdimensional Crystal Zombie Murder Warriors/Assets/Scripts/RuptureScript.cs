using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class RuptureScript : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float maxRadius = 5f;

    private float originalRadius = 1f;

    [SerializeField]
    private int damage = 1;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        originalRadius = circleCollider.radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (circleCollider.radius < maxRadius)
        {
            circleCollider.radius += Time.deltaTime;

            Vector3 newScale = spriteRenderer.transform.localScale;

            newScale.x += Time.deltaTime * 2;
            newScale.y += Time.deltaTime * 2;

            spriteRenderer.transform.localScale = newScale;
        }
        else
        {
            circleCollider.radius = originalRadius;
            spriteRenderer.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerScript>().DamageHealth(damage);
        }
    }
}
