using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool hasPowerUp = false;
    private float powerUpStrength = 15f;
    public float powerUpDuration;
    public GameObject powerUpIndicator;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    private void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput);
        powerUpIndicator.transform.position = transform.position - new Vector3(0, .5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            hasPowerUp = true;
            StartCoroutine(DeactivePowerUp());
        }
    }

    IEnumerator DeactivePowerUp()
    {
        yield return new WaitForSeconds(powerUpDuration);
        powerUpIndicator.gameObject.SetActive(false);
        hasPowerUp = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 propulseDir = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(propulseDir * powerUpStrength, ForceMode.Impulse);
        }
    }
}
