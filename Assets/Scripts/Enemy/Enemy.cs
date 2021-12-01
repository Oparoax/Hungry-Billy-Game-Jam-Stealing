using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteraction
{
    int Money = 300;

    private Char_Controller player;
    public void Interact()
    {
        if (Money != 0)
        {
            Money -= player.stealAmount;
            player.playerWallet += player.stealAmount;
            Debug.Log("Stolen Money");
            player.OnMoneyChanged?.Invoke();
        }
        else
        {
            Debug.Log("Enemy has no money");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Char_Controller>();
    }
}
