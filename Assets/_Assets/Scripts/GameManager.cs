using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnStateChanged;
    public static GameManager Instance;
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
        state = State.waitingToStart;
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
}
