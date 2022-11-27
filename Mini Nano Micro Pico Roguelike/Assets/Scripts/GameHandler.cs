using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public Enemy boss;
    public Player player;

    public AudioSource musicAudio;
    public AudioClip musicClip;

    public GameObject gameOverPanel;
    public Button gameOverButton;
    public GameObject gameOverTimeText;

    public GameObject startPanel;
    public Button startButton;

    public GameObject winPanel;
    public Button winButton;
    public GameObject winTimeText;

    public GameObject infoPanel;

    public string gameStatus;

    public float playTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        boss = GameObject.Find("Boss").GetComponent<Enemy>();
        
        gameStatus = "start";

        startPanel.SetActive(true);
        startButton.onClick.AddListener(StartGame);
        
        gameOverPanel.SetActive(false);
        gameOverButton.onClick.AddListener(RestartGame);

        winPanel.SetActive(false);
        winButton.onClick.AddListener(RestartGame);

        infoPanel.SetActive(false);

        musicAudio.clip = musicClip;
        musicAudio.Play();
    }

    void StartGame()
    {
        startPanel.SetActive(false);
        infoPanel.SetActive(true);

        player.UpdateHealth(player.maxHealth);
        player.UpdateEnergy(player.maxEnergy);

        gameStatus = "play";
        playTime = 0;
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.health <= 0)
            {
                gameOverPanel.SetActive(true);

                TimeSpan t = TimeSpan.FromSeconds(playTime);

                string finalTime = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);

                gameOverTimeText.GetComponent<TextMeshProUGUI>().SetText("You were alive for\n" + finalTime);
                gameStatus = "gameover";
                Destroy(player.gameObject);
            }
        }

        if (boss != null)
        {
            if (boss.health <= 0)
            {
                winPanel.SetActive(true);

                TimeSpan t = TimeSpan.FromSeconds(playTime);

                string finalTime = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);

                winTimeText.GetComponent<TextMeshProUGUI>().SetText("Completed in\n" + finalTime);
            }
        }

        if (gameStatus == "play")
        {
            playTime += Time.deltaTime;
        }
    }

}
