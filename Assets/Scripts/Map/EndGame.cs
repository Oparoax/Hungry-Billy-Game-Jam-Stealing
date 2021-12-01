using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private Char_Controller player;
    [SerializeField] int catchMax = 3;
    int currentCatch = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Char_Controller>();
        player.OnCaught.AddListener(EndGameOnCaught);
    }

    void EndGameOnCaught()
    {
        currentCatch++;
        if (currentCatch == catchMax)
        {
            FindObjectOfType<Timer>().OnGameEnd?.Invoke();
        }
    }
}
