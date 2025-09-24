using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDeath : MonoBehaviour
{
    public GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            explosion = Instantiate(explosion, transform.position, Quaternion.identity);
            CarController carController = other.gameObject.GetComponent<CarController>();
            carController.OnDeath();

            PlayerInput playerInput = other.gameObject.GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                GameManager.Instance.ActivePlayers.Remove(playerInput);
                if (GameManager.Instance.ActivePlayers.Count == 0)
                {
                    Debug.Log("Everybody died");
                    GameManager.Instance.InvokeEvent(0);
                }
            }
            Destroy(explosion, 2f);
            Destroy(other.gameObject);
            Debug.Log("Write what is happening to the player, when they hit the a trap.");
        }
    }
}
