using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] bool isRaceFinished;

    //[SerializeField] private GameObject p1ScoreBoard;
    //[SerializeField] private GameObject p2ScoreBoard;
    //[SerializeField] private TextMeshProUGUI p1Score;
    //[SerializeField] private TextMeshProUGUI p2Score;
    //[SerializeField] private GameObject everyoneFinishished;
    //[SerializeField] private GameObject noOneFinished;
    //[SerializeField] private TextMeshProUGUI descriptionText;

    //[HideInInspector] public int pointsP1;
    //[HideInInspector] public int pointsP2;

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

   

    public void OnFinish()
    {
        int activePlayers = GameManager.Instance.ActivePlayers.Count; 
        int finishedPlayers = GameManager.Instance.FinishedPlayers.Count;

        if (activePlayers == 2 && finishedPlayers == 2)
        {
            Debug.Log("Everybody finished, nobody wins");
            //p1ScoreBoard.SetActive(true);
            //p2ScoreBoard.SetActive(true);
            //p1Score.text = pointsP1.ToString();
            //p2Score.text = pointsP2.ToString();
            //everyoneFinishished.SetActive(true);
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
        //yield return new WaitForSeconds(7);
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
