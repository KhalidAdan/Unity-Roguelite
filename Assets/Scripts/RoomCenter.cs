using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public bool openOnCleared;
    public Room room;

    // Start is called before the first frame update
    void Start()
    {
        if (openOnCleared)
        {
            room.closeOnEnter = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && room.roomActive && openOnCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
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
                room.OpenDoors();
            }
        }
    }
}
