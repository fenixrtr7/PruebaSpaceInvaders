using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager sharedInstance;
    public static bool gameManagerCreated;
    // Nivel
    int level = 5;
    public int currentLevel;
    public Text textLevel, textScore;
    public int score;

    public GameObject canvasGameOver, canvasStartGame;
    public Text textInstructions;
    bool instructionsActive = false, inGame = false;

    public GameObject playerShip;

    private void Awake()
    {
        // Singleton
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (!gameManagerCreated)
        {
            gameManagerCreated = true;
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentLevel = 1;
        textLevel.text = "Level: " + currentLevel;
        score = 0;
        textScore.text = "Score: " + score;
        canvasGameOver.SetActive(false);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.A) && !instructionsActive && !inGame)
        {
            instructionsActive = true;
            textInstructions.enabled = true;
            Debug.Log("A 1");
        }
        else if (Input.GetKeyDown(KeyCode.A) && instructionsActive == true)
        {
            inGame = true;

            instructionsActive = false;
            textInstructions.enabled = false;
            canvasStartGame.SetActive(false);

            Debug.Log("A 2");

            Time.timeScale = 1;
        }

    }

    // Revisamos enemigos con vida
    public void CheckEnemysWithLife()
    {
        GameObject[] arrayEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(arrayEnemies.Length);

        if (arrayEnemies.Length <= 0)
        {
            Debug.Log("No hay enemigos");
            // Subimos de nivel
            LevelUp();
        }
        else
        {
            Debug.Log("Hay enemigos");
        }
    }

    // Subir de nivel
    public void LevelUp()
    {
        currentLevel++;
        textLevel.text = "Level: " + currentLevel;

        if (currentLevel > 5)
        {
            currentLevel = 5;
            textLevel.text = "Level: " + currentLevel;

            GameManager.sharedInstance.GameOver();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Suma score
    public void AddScore(int addScore)
    {
        score += addScore;
        textScore.text = "Score: " + score;
    }

    public void GameOver()
    {

        canvasGameOver.SetActive(true);
        Time.timeScale = 0;
        inGame = false;

        SceneManager.LoadScene(0);
    }

    public void ButtonMenu()
    {
        ResetPropierties();
        canvasGameOver.SetActive(false);
        canvasStartGame.SetActive(true);
    }

    // Reset a propidades del juego
    public void ResetPropierties()
    {
        playerShip.SetActive(true);
        
        currentLevel = 0;
        textLevel.text = "Level: " + currentLevel;
        score = 0;
        textScore.text = "Score: " + score;
        
        playerShip.GetComponent<Health>().ResetLife();
    }
}
