using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    public float speed = 3.0f;

    private float bottomLimit = -30f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 enemyDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(enemyDirection * speed);

        if(transform.position.y < bottomLimit)
        {
            Destroy(transform.gameObject);
        }
    }
}
