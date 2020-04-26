using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool gameIsActive;
    public float bottomLimit = -30f;
    [SerializeField] GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        gameIsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIsActive && !gameOverScreen.gameObject.activeInHierarchy) gameOverScreen.gameObject.SetActive(true);
    }
}
