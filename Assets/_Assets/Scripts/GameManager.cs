using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnStateChanged;
    public static GameManager Instance;
   [SerializeField] private bool isGamePaused = false;
    [SerializeField] GameObject GamePuseUIPanel;
    [SerializeField] Button resumeButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button restartButton;
    private enum State
    {
        waitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    } 
    private State state;
    private float waitingToStartTimer = 1f;
    private float countDownToStartTimer = 3f;
    private float gamePlayingTimer;
    [SerializeField] private float gamePlayingTimerMax = 25f;


    private void Awake()
    {
        Instance = this;
        resumeButton.onClick.AddListener(() =>
        {
            TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        restartButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            Loader.Load(Loader.Scene.GameScene);
        });
        state = State.waitingToStart;
    }
    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }
    private void Update()
    {
        switch (state)
        {
            case State.waitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0)
                {
                    state = State.CountDownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer < 0)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;

        }
        Debug.Log(state);
    }
  public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
  public bool IsCountDownStartToActive()
    {
        return state == State.CountDownToStart;
    }
   public float GetCountDownToStartTimer()
    {
        return countDownToStartTimer;
    }
   public bool IsGameOver()
    {
        return state == State.GameOver;
    }
   public float GetGamePlayingTimerNormalized()
    {
        return 1-gamePlayingTimer / gamePlayingTimerMax;
    }
    private void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if(isGamePaused)
        {
            Time.timeScale = 0f;
            GamePuseUIPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            GamePuseUIPanel.SetActive(false);
        }
        
    }
}
