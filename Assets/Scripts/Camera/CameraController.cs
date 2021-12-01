using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Defines what the camera is following
    [SerializeField] public GameObject player;

    void Update()
    {
        // Camera centres on player object
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
