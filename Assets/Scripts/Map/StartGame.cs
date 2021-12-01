using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    public UnityEvent OnStartGame;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnStartGame?.Invoke();
    }
}
