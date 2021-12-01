using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public GameObject player;

    void Update()
    {
        // Camera follows the player
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
