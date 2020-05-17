using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;

    private GameObject endRoom;
    private List<GameObject> layoutRoomObjects = new List<GameObject>();

    public Color startColor, endColor;

    public int distanceToEnd;

    public Transform generatorPoint;

    public enum Direction
    {
        up,
        right, 
        down, 
        left
    };
    public Direction selectedDirection;

    public float xOffset = 18f;
    public float yOffset = 10f;

    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i <= distanceToEnd; i++)
        {
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);
            layoutRoomObjects.Add(newRoom);
            if (i == 0)
            {
                // create first room
                newRoom
                    .GetComponent<SpriteRenderer>().color = startColor;
            } else if (i == distanceToEnd - 1)
            {
                // create last room
                newRoom
                     .GetComponent<SpriteRenderer>().color = endColor;
                layoutRoomObjects.RemoveAt(layoutRoomObjects.Count - 1);

                endRoom = newRoom;
            }
            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();

            while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, layerMask))
            {
                // stop overlap of rooms by adding box collider and small physics circle to check location
                MoveGenerationPoint();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void MoveGenerationPoint()
    {
        switch (selectedDirection)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0f, yOffset, 0f);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0f, -yOffset, 0f);
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(xOffset, 0f, 0f);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(-xOffset, 0f, 0f);
                break;
        }
    }
}
