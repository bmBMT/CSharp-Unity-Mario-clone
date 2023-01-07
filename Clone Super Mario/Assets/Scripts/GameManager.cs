using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int worlds = 1;
    public int stages = 1;

    public int stage { get; private set; }
    public int lives { get; set; }
    public int world { get; set; }
    public int coins { get; private set; }
    public int score { get; private set; }

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    public void DestroyManager() {
        Object.Destroy(gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    } 

    private void Update()
    {
        if (PlayerPrefs.GetInt("score") <= score) {
            PlayerPrefs.SetInt("score", score);
        }
    }
    
    public void NewGame()
    {
        world = 1;
        lives = 3;
        coins = 0;
        score = 0;

        LoadLevel(1);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
      
        Invoke(nameof(NewGame), 3f);
    }

    public void LoadLevel(int stage)
    {
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel()
    {
        if (stage + 1 <= stages) {
            stage++;
            LoadLevel(stage);
        } else if (world + 1 <= worlds) {
            world++;
            stage = 1;
            LoadLevel(stage);
        } else {
            GameOver();
        }
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;

        if (lives > 0) {
            LoadLevel(stage);
        } else {
            GameOver();
        }
    }

    public void Exit() {
        Application.Quit();
    }

    public void AddCoin()
    {
        coins++;

        if (coins == 100) {
            AddLife();
            coins = 0;
        }

        score += 200;
    }

    public void AddScore(int value) {
        score += value;
    }

    public void AddLife() {
        lives++;
    }

    public int GetCoins() {
        return coins;
    }

    public string GetWorld() {
        return world.ToString() + "-" + stage.ToString();
    }
    
    public int GetLives() {
        return lives;
    }

    public int GetScore() {
        return score;
    }
}