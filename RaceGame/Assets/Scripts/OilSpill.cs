using System.Collections.Generic;
using UnityEngine;

public class OilSpill : MonoBehaviour
{
    [System.Serializable]
    public class PlayerState
    {
        public GameObject player;
        public bool spinInAction = false;
        public bool isAligning = false;
        public float time = 0f;
    }

    public List<GameObject> players = new List<GameObject>();
    public Vector3 spinAxis = new Vector3(0f, 1f, 0f);
    public float spinSpeed = 90f;
    public float spinDuration = 1f;
    public float alignSpeed = 5f;

    public List<PlayerState> playerStates = new List<PlayerState>();

    private void Awake()
    {
        players.Add(GameObject.Find("BlueCar(Clone)"));
        players.Add(GameObject.Find("RedCar(Clone)"));

        foreach (var player in players)
        {
            if (player != null)
            {
                playerStates.Add(new PlayerState { player = player });
            }
        }
    }

    private void Update()
    {
        foreach (var playerState in playerStates)
        {
            if (playerState.spinInAction)
            {
                playerState.time += Time.deltaTime;
                playerState.player.transform.Rotate(spinAxis, spinSpeed * Time.deltaTime);

                if (playerState.time > spinDuration)
                {
                    playerState.spinInAction = false;
                    playerState.isAligning = true;
                    playerState.time = 0f;
                }
            }

            if (playerState.isAligning)
            {
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);

                playerState.player.transform.rotation = Quaternion.RotateTowards(playerState.player.transform.rotation, targetRotation, alignSpeed * Time.deltaTime * 100f);

                if (Quaternion.Angle(playerState.player.transform.rotation, targetRotation) < 0.1f)
                {
                    playerState.player.transform.rotation = targetRotation;
                    playerState.isAligning = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var ps = playerStates.Find(p => p.player == other.gameObject);

            if (ps != null)
            {
                ps.spinInAction = true;
                ps.time = 0f;
                Debug.Log("Write what is happening to the player, when they go throught the oil spill aka lack of traction.");
            }
        }
    }
}
