using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ScriptForVideo : MonoBehaviour
{
    public bool davai= false;
 
    public List<GameObject> list = new List<GameObject>();
    public GameObject everyoneWins;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (davai)
        {
            foreach (var item in list)
            {
                item.gameObject.SetActive(true);
                StartCoroutine(WaitForSpawn());
            }
        }
    }
    public IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(2);
        everyoneWins.SetActive(true);
    }
}
