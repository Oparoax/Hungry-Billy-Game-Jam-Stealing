using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    //Text objects we are using to display score
    public TextMeshProUGUI walletText, bankText, scoreText;
    //Sets Char_Controller script variable
    private Char_Controller player;
    //GameObject we are displaying when game ends
    public GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        //Sets endScreen inactive just in case it was left on
        endScreen.SetActive(false);
        //Defines variable for Char_Controller
        player = FindObjectOfType<Char_Controller>();
        //Adds listener onto OnMoneyChanged event to update text objects with new score
        player.OnMoneyChanged.AddListener(updateText);
        //Adds listener onto OnGameEnd event to trigger EndGame
        GetComponent<Timer>().OnGameEnd.AddListener(EndGame);
    }

    /// <summary>
    /// Updates text with new values after event trigger
    /// </summary>
    private void updateText()
    {
        //Sets wallet text to new wallet text value
        walletText.text = $"Wallet: ${player.playerWallet}";
        //Sets bank text to new bank text value
        bankText.text = $"Bank: ${player.playerBank}";
    }

    //String array to store fail messages
    [SerializeField] private string[] failMessages = new string[] { 
    ":(", "You'll get em next time champ", "HeadAss", "Can we talk for a second how you said you relate the word for this game jam to the applicant and you said 'Stealing'???"
    }; 

    /// <summary>
    /// Method used to display end screen and display the players score and an appropriate message
    /// </summary>
    void EndGame()
    {
        //Enables endScreen object, displays end screen
        endScreen.SetActive(true);
        //If player has acheived no score
        if (player.playerBank == 0 && player.playerWallet == 0)
        {
            //Sets score text to random fail message from array
            scoreText.text = failMessages[UnityEngine.Random.Range(0, failMessages.Length-1)];
        }
        //If player has got score in their wallet but not banked it
        else if (player.playerBank == 0)
        {
            //Sets score text to new text
            scoreText.text = "You have to bank money for me to get it later, cmon man!";
        }
        //If player has got score in the bank but still score left in wallet
        else if (player.playerWallet != 0)
        {
            //Sets score text to new text
            scoreText.text = $"Your bank is ${player.playerBank}, unfortunatley you still had ${player.playerWallet} in your wallet";
        }
        //If player has banked all their score
        else
        {
            //Sets score text to new text
            scoreText.text = $"Your bank is ${player.playerBank}, and you managed to bank it all! Well Done!";
        }
        
    }

    /// <summary>
    /// Function for try again button
    /// </summary>
    public void TryAgain()
    {
        //Reloads current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
