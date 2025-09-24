using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] bool isRaceFinished;

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

        if (activePlayers == 2 && finishedPlayers == 2)
        {
            Debug.Log("Everybody finished, nobody wins");
            isRaceFinished = true;
        }
        else if (activePlayers == 2 && finishedPlayers == 1)
        {
            Debug.Log("Round continues");
            isRaceFinished = false;
        }
        else if (activePlayers == 1 && finishedPlayers == 1)
        {
            Debug.Log($"{GameManager.Instance.ActivePlayers[0]} won!");
            isRaceFinished = true;
        }

        if (isRaceFinished)
        {
            StartCoroutine(HandleRaceEnd());
        }
    }

    private IEnumerator HandleRaceEnd()
    {
        // Destroy all active players
        foreach (PlayerInput player in GameManager.Instance.ActivePlayers)
        {
            Destroy(player.gameObject);
        }

        // Wait until the end of the frame for Unity to finalize destruction
        yield return new WaitForEndOfFrame();

        // Clear the lists
        GameManager.Instance.ActivePlayers.Clear();
        GameManager.Instance.FinishedPlayers.Clear();

        // Invoke the event to spawn new players
        GameManager.Instance.InvokeEvent(0);

        isRaceFinished = false;
    }
}
