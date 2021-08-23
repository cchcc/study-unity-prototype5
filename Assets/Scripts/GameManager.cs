using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI gameOverText;
    public Slider volumeSlider;
    public Button restartButton;
    public GameObject titleScreen;
    public bool isGameOver = false;
    public bool isPaused = false;
    public int life = 2;

    private AudioSource gameAudioSource;
    private int score;
    private float spawnRate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        gameAudioSource = GetComponent<AudioSource>();
        // volumeSlider.onValueChanged.AddListener(OnValueChangedVolumeSlider);
    }

    private void OnValueChangedVolumeSlider(float v)
    {
        gameAudioSource.volume = v;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
    
    

    IEnumerator SpawnTarget()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            var target = targets[Random.Range(0, targets.Count)];
            Instantiate(target);
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
        scoreText.text = $"Score: {this.score}";
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        titleScreen.gameObject.SetActive(false);
        StartCoroutine(SpawnTarget());
        score = 0;
        AddScore(0);
        lifeText.text = $"Life: {life}";
    }

    public void clickedBad()
    {
        --life;
        lifeText.text = $"Life: {life}";
        if (life == 0)
            GameOver();
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameOver = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
