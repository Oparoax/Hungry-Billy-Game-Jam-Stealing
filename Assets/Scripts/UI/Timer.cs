using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public float gameTime = 90;

    private bool timerIsRunning = false;

    public TextMeshProUGUI timerText;

    public UnityEvent OnGameEnd;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = $"Time left: {gameTime}s";
        FindObjectOfType<StartGame>().OnStartGame.AddListener(delegate { timerIsRunning = true; });
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (gameTime > 0)
            {
                gameTime -= Time.deltaTime;
                timerText.text = $"Time left: {(int)gameTime}s";
            }
            else
            {
                Debug.Log("Time Is Up");
                gameTime = 0;
                timerIsRunning = false;
                OnGameEnd?.Invoke();
            }
        }
    }
}
