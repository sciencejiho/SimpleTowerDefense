using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Responsible for creating levels for the game
public class LevelManager : MonoBehaviour {
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

    public float TileSize {
        get {
            return tilePrefabs[0].GetComponent<SpriteRenderer> ().sprite.bounds.size.x;
        }
    }

    // Start is called before the first frame update
    void Start () {
        CreateLevel ();
    }

    // Update is called once per frame
    void Update () { }

    private void CreateLevel () {
        string[] mapData = ReadLevelText();

        // Default: 13
        int mapXSize = mapData[0].ToCharArray().Length;
        // Default: 8
        int mapYSize = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        Debug.Log(mapXSize);
        Debug.Log(mapYSize);

        // Get the top left corner of the map
        Vector3 worldStart = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height));
        
        // Y position of the map
        for (int j = 0; j < mapYSize; j++) 
        {
            // Gets all the tiles that we need to place
            char[] newTiles = mapData[j].ToCharArray ();

            // X position of the map
            for (int i = 0; i < mapXSize; i++) 
            {
                maxTile = PlaceTile (newTiles[i].ToString(), i, j, worldStart);
            }
        }

        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
    }

    private Vector3 PlaceTile (string tileType, int x, int y, Vector3 worldStart) {
        // Parse string value into int type
        int tileIndex = int.Parse (tileType);

        // Creates a new tile and makes a reference to that tile in the newTile variable
        GameObject newTile = Instantiate (tilePrefabs[tileIndex]);

        // Uses the newTile variable to change the position of the tile
        newTile.transform.position = new Vector3 (worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);
    
        return newTile.transform.position;
    }

    private string[] ReadLevelText () 
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string tempData = bindData.text.Replace(Environment.NewLine, string.Empty);
    
        return tempData.Split('-');
    }
}