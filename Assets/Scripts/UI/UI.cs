using System;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI walletText, bankText, scoreText;

    private Char_Controller player;

    public GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        endScreen.SetActive(false);
        player = FindObjectOfType<Char_Controller>();
        player.OnMoneyChanged.AddListener(updateText);
        GetComponent<Timer>().OnGameEnd.AddListener(EndGame);
    }

    private void updateText()
    {
        walletText.text = $"Wallet: ${player.playerWallet}";
        bankText.text = $"Bank: ${player.playerBank}";
    }

    [SerializeField] private string[] failMessages = new string[] { 
    ":(", "You'll get em next time champ", "HeadAss", "Can we talk for a second how you said you relate the word for this game jam to the applicant and you said 'Stealing'???"
    }; 

    void EndGame()
    {
        endScreen.SetActive(true);
        if (player.playerBank == 0 && player.playerWallet == 0)
        {
            scoreText.text = failMessages[UnityEngine.Random.Range(0, failMessages.Length-1)];
        }
        else if (player.playerBank == 0)
        {
            scoreText.text = "You have to bank money for me to get it later, cmon man!";
        }
        else if (player.playerWallet != 0)
        {
            scoreText.text = $"Your bank is ${player.playerBank}, unfortunatley you still had ${player.playerWallet} in your wallet";
        }
        else
        {
            scoreText.text = $"Your bank is ${player.playerBank}, and you managed to bank it all! Well Done!";
        }
        
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
