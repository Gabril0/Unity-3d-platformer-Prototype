using UnityEngine;
using UnityEngine.UI;

public class DeathTrigger : MonoBehaviour
{

    private bool playerTouched = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerTouched)
        {
            Debug.Log("You died");
        }
    }
}
