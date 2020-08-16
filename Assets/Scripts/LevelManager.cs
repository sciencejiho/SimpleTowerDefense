using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for creating levels for the game
public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update() {}

    //
    private void CreateLevel()
    {
        float tileSize = tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject newTile = Instantiate(tile);
                newTile.transform.position = new Vector3(tileSize * i, tileSize * j, 0);
            }
        }
    }
}