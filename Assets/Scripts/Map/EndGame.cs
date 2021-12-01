using UnityEngine;

public class EndGame : MonoBehaviour
{
    //Sets Char_Controller script variable
    private Char_Controller player;
    //Max amount of times the player maybe caught before the game ends
    [SerializeField] int catchMax = 3;
    //Itterator for how many times the player has been caught
    int currentCatch = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Defines variable for Char_Controller
        player = FindObjectOfType<Char_Controller>();
        //Event listener for when the OnCaught event is triggered
        player.OnCaught.AddListener(EndGameOnCaught);
    }

    /// <summary>
    /// Function to count when player is caught then end game when the max is reached
    /// </summary>
    void EndGameOnCaught()
    {
        //Itterates the variable
        currentCatch++;
        //Checks if max is reached
        if (currentCatch == catchMax)
        {
            //Triggers OnGameEnd event as max has been reached
            FindObjectOfType<Timer>().OnGameEnd?.Invoke();
        }
    }
}
