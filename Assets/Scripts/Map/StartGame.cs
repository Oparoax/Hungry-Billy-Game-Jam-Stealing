using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    //Event to start the game timer
    public UnityEvent OnStartGame;

    /// <summary>
    /// Starts game when collider is entered/triggered
    /// </summary>
    /// <param name="collision">Collider we are using as trigger</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Triggers OnStartGame Event
        OnStartGame?.Invoke();
    }
}
