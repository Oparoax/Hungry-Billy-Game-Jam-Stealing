using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour, IInteraction
{
    private Char_Controller player;

    void Start()
    {
        player = FindObjectOfType<Char_Controller>();
    }

    public void Interact()
    {
        player.playerBank += player.playerWallet;
        player.playerWallet = 0;
        Debug.Log("Stored Money");
        player.OnMoneyChanged?.Invoke();
    }
}
