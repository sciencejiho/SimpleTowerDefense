using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Responsible for creating levels for the game
public class LevelManager : MonoBehaviour {
    [SerializeField]
    private GameObject[] tilePrefabs;

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

        Debug.Log(mapXSize);
        Debug.Log(mapYSize);

        // Get the top left corner of the map
        Vector3 worldStart = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height));
        for (int j = 0; j < mapYSize; j++) 
        {
            char[] newTiles = mapData[j].ToCharArray ();

            for (int i = 0; i < mapXSize; i++) 
            {
                PlaceTile (newTiles[i].ToString (), i, j, worldStart);
            }
        }
    }

    private void PlaceTile (string tileType, int x, int y, Vector3 worldStart) {
        // Parse string value into int type
        int tileIndex = int.Parse (tileType);

        GameObject newTile = Instantiate (tilePrefabs[tileIndex]);
        newTile.transform.position = new Vector3 (worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);
    }

    private string[] ReadLevelText () 
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string tempData = bindData.text.Replace(Environment.NewLine, string.Empty);
    
        return tempData.Split('-');
    }
}