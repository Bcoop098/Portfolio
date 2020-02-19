using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class RingExpand : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float maxRadius = 5f;

    private float originalRadius = 1f;

    private bool canExpand;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        canExpand = false;
        originalRadius = circleCollider.radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (canExpand)
        {
            circleCollider.enabled = true;
            spriteRenderer.enabled = true;
            if (circleCollider.radius < maxRadius)
            {
                ExpandRing();
            }
            else
            {
                ResetRing();
            }
        }
        else
        {
            ResetRing();
            circleCollider.enabled = false;
            spriteRenderer.enabled = false;
        }
    }

    public void ChangeExpand(bool expand)
    {
        canExpand = expand;
    }

    private void ExpandRing()
    {
        circleCollider.radius += Time.deltaTime;

        Vector3 newScale = spriteRenderer.transform.localScale;

        newScale.x += Time.deltaTime * 2;
        newScale.y += Time.deltaTime * 2;

        spriteRenderer.transform.localScale = newScale;
    }

    private void ResetRing()
    {
        circleCollider.radius = originalRadius;
        spriteRenderer.transform.localScale = new Vector3(1, 1, 1);
    }
}
