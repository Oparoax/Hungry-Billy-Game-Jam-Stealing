using UnityEngine;

public class Enemy : MonoBehaviour, IInteraction
{
    //Integer for money on enemies
    int Money = 300;

    //Sets Char_Controller script variable
    private Char_Controller player;

    /// <summary>
    /// Interact script for when interaction occurs
    /// </summary>
    public void Interact()
    {
        //Checks for if enemy has money
        if (Money != 0)
        {
            //Removes money from enemy
            Money -= player.stealAmount;
            //Adds removed money into players wallet
            player.playerWallet += player.stealAmount;

            Debug.Log("Stolen Money");
            //Trigger OnMoneyChanged event
            player.OnMoneyChanged?.Invoke();
        }
        else
        {
            //Triggers OnCaught event
            player.OnCaught?.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Defines variable for Char_Controller 
        player = FindObjectOfType<Char_Controller>();
    }
}
