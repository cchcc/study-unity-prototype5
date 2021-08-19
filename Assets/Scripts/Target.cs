using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    public int scorePoint;
    public ParticleSystem explosionParticle;
    
    private GameManager gameManager;
    private Rigidbody rb;
    private float torqueRange = 10f;
    private float maxSpeed = 18f;
    private float minSpeed = 12f;
    private int xRange = 4;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        
        rb.AddForce(Vector3.up * Random.Range(minSpeed, maxSpeed), ForceMode.Impulse);
        rb.AddTorque(Random.Range(-torqueRange, torqueRange), Random.Range(-torqueRange, torqueRange), Random.Range(-torqueRange, torqueRange), ForceMode.Impulse);
        transform.position = new Vector3(Random.Range(-xRange, xRange), -6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameOver)
            return;
        
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gameManager.AddScore(scorePoint);
        Destroy(gameObject);
        
        if (gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    private void OnMouseUp()
    {
        
    }
}
