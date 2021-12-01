using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    //Variable for how long the game is in seconds
    public float gameTime = 90;
    //Boolean for when timer is running
    private bool timerIsRunning = false;
    //Text object we are using to display time
    public TextMeshProUGUI timerText;
    //Event for to trigger Game End
    public UnityEvent OnGameEnd;

    // Start is called before the first frame update
    void Start()
    {
        //Sets timer text to max value
        timerText.text = $"Time left: {gameTime}s";
        //Adds listener to OnStartGame event to start timer
        FindObjectOfType<StartGame>().OnStartGame.AddListener(delegate { timerIsRunning = true; });
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if timer has been started
        if (timerIsRunning)
        {
            //Checks if there is still time
            if (gameTime > 0)
            {
                //Decrements time variable by real time
                gameTime -= Time.deltaTime;
                //Sets timer text to amount of time left after decrement
                timerText.text = $"Time left: {(int)gameTime}s";
            }
            else
            {
                Debug.Log("Time Is Up");
                //Sets gameTime to zero
                gameTime = 0;
                //Stops timer running
                timerIsRunning = false;
                //Triggers OnGameEnd Event
                OnGameEnd?.Invoke();
            }
        }
    }
}
