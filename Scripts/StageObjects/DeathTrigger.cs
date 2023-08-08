using UnityEngine;
using UnityEngine.UI;

public class DeathTrigger : MonoBehaviour
{

    private bool playerTouched = false;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerTouched)
        {
            playerMovement.setIsAlive(false);
        }
    }
}
