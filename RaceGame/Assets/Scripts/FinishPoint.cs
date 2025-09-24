using UnityEngine;
using UnityEngine.InputSystem;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishTrigger"))
        {
            PlayerInput player = other.GetComponentInParent<PlayerInput>(); 

            GameManager.Instance.PlayerFinished(player);

            other.gameObject.SetActive(false);

            OnFinish();
        }
    }

    void OnFinish()
    {
        int activePlayers = GameManager.Instance.ActivePlayers.Count; 
        int finishedPlayers = GameManager.Instance.FinishedPlayers.Count;

        if (activePlayers == finishedPlayers)
        {
            Debug.Log("Everybody finished, nobody wins");
        }
        else if (activePlayers == 2 && finishedPlayers == 1)
        {
            Debug.Log("Round continues");
        }
        else if (activePlayers == 1 && finishedPlayers == 1)
        {
            Debug.Log($"{GameManager.Instance.ActivePlayers[0]} won!");
        }
    }
}
