using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 5.0f;
    Rigidbody playerRb;
    GameObject focalPoint;
    public bool hasPowerUp = false;
    float powerUpStrength = 15f;
    public float powerUpDuration;
    public GameObject powerUpIndicator;
    float bottomLimit;
    GameManager gameManager;
    [SerializeField] ParticleSystem powerUpParticles;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        bottomLimit = gameManager.bottomLimit;
    }

    private void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput);
        powerUpIndicator.transform.position = transform.position - new Vector3(0, .5f, 0);

        if (transform.position.y < bottomLimit)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            powerUpParticles.transform.position = other.transform.position;
            powerUpParticles.Play();
            //GameObject particlesCopy = Instantiate(powerUpParticles, other.transform.position, other.transform.rotation) as GameObject;
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
            GameObject.Find("Main Camera").GetComponent<CameraShake>().enabled = true;
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 propulseDir = collision.gameObject.transform.position - transform.position;

            enemyRb.AddForce(propulseDir * powerUpStrength, ForceMode.Impulse);
        }
    }

    void GameOver()
    {
        Destroy(transform.gameObject);
        gameManager.gameIsActive = false;
    }
}
