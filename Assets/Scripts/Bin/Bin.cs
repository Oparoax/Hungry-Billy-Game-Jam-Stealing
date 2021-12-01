using UnityEngine;

public class Bin : MonoBehaviour, IInteraction
{

    // Sets Char_Controller script variable
    private Char_Controller player;

    void Start()
    {
        //Defines variable for Char_Controller
        player = FindObjectOfType<Char_Controller>();
    }

    /// <summary>
    /// Interact method triggered when IInteraction is identified
    /// </summary>
    public void Interact()
    {
        //Adds players wallet score to the bank score
        player.playerBank += player.playerWallet;
        //Resets players wallet
        player.playerWallet = 0;
        
        Debug.Log("Stored Money");
        //Triggers OnMoneyChanged event
        player.OnMoneyChanged?.Invoke();
    }
}
