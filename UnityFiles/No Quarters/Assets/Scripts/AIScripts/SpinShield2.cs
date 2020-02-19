using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinShield2 : MonoBehaviour
{
    [SerializeField]
    private float RotateSpeed = 5f;
    [SerializeField]
    private float Radius = 0.1f;
    [SerializeField]
    private GameObject cleaner;

    [SerializeField]
    private int damage = 5;

    private Vector2 center;
    private float angle;

    /*[SerializeField]
    private float timeToDeath = 5f;*/

    void Start()
    {
        //StartCoroutine(Destroy(timeToDeath));
        center = cleaner.transform.position;
        //center = CleanerScript.cleaner.transform.position;
    }

    void FixedUpdate()
    {
        center = cleaner.transform.position;
        //center = CleanerScript.cleaner.transform.position;
        angle -= RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        transform.position = center + offset;
    }

    /*private IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerControllerV2>().TakeDamage(damage);
        }
    }
}
