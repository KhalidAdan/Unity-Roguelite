using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeOnEnter, openOnCleared;
    public GameObject[] doors;

    public List<GameObject> enemies = new List<GameObject>();

    private bool roomActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && roomActive && openOnCleared)
        {
            for(int i = 0; i < enemies.Count; i ++)
            {
                if (enemies[i] == null)
                {
                    // removes items and sorts the list so we need to remove one iteration from 
                    // our loop
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            if (enemies.Count == 0)
            {
                // unlock door
                foreach (GameObject door in doors)
                {
                    door.SetActive(false);
                    closeOnEnter = false;
                }
            }
        }  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.instance.ChangeTarget(transform);
            if (closeOnEnter)
            {
                foreach(GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }
            roomActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            roomActive = false;
        }
    }
}
