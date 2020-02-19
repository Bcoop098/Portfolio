﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private int damage = 5;

    [SerializeField]
    private float timeToDeath = 6f;

    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        StartCoroutine(Destroy(timeToDeath));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerControllerV2>().TakeDamage(damage);
            gameObject.SetActive(false);
            StopAllCoroutines();
        }
    }

    public void setWaterPos(Vector3 pos)
    {
        initialPos = pos;
    }

    private IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
        StopAllCoroutines();
    }

    public void enableWater()
    {
        gameObject.SetActive(true);
        transform.position = initialPos;
        StartCoroutine(Destroy(timeToDeath));
    }

    public void disableWater()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        
    }

    //public void stopWater()
    //{
    //    Destroy(gameObject);
    //}
}