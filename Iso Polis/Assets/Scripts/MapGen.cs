using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class MapGen : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    Tile[] tiles;
    public int islandDegeneration;
    public float treePerlinScale;
    public float treePerlinCutoff;
    public float rockPerlinScale;
    public float rockPerlinCutoff;

    const float yInterval = (9f/32f);
    const float xInterval = 0.5f;
    public int mapWidth;
    public int mapLength;

    // Start is called before the first frame update
    void Start()
    {
        mapWidth = Random.Range(40, 80);
        mapLength = Random.Range(40, 80);
        GenerateMap();
        DetermineTileNum();
        FormIsland();
        SpawnTiles();
    }

    public void GenerateMap() {
        tiles = new Tile[mapWidth * mapLength];
        Vector2 treeOffset = new Vector2(Random.Range(0, 1000), Random.Range(0, 1000));
        Vector2 rockOffset = new Vector2(Random.Range(0, 1000), Random.Range(0, 1000));
        for(int y = 0; y < mapLength; y++) {
            for(int x = 0; x < mapWidth; x++) {
                int tileType = 0;
                float treeVal = Mathf.PerlinNoise(x/treePerlinScale + treeOffset.x, y/treePerlinScale + treeOffset.y);
                if(treeVal < treePerlinCutoff) {
                    tileType = 1;
                } else {
                    float rockVal = Mathf.PerlinNoise(x/rockPerlinScale + rockOffset.x, y/rockPerlinScale + rockOffset.y);
                    if(rockVal > rockPerlinCutoff) tileType = 2;
                }
                tiles[y * mapWidth + x] = new Tile{ tileNum = 0, loc = new Vector2(x, y), tileType = tileType };
            }
        }
    }

    public void DetermineTileNum() {
        for(int y = 0; y < mapLength; y++) {
            for(int x = 0; x < mapWidth; x++) {
                var tile = tiles[y * mapWidth + x];
                if(tile.tileNum != -1) {
                    tiles[y * mapWidth + x].tileNum = GetSurroundingTiles(x, y);
                }
            }
        }
    }

    public void FormIsland() {
        for(int i = 0; i < islandDegeneration; i++) {
            var coastTiles = tiles.Where(t => t.tileNum != 15);
            foreach(var tile in coastTiles) {
                float xDist = Mathf.Abs(tile.loc.x/mapWidth - 0.5f);
                float yDist = Mathf.Abs(tile.loc.y/mapLength - 0.5f);
                bool keep = xDist < yDist ? Random.Range(0f, 0.5f) > xDist : Random.Range(0f, 0.5f) > yDist;

                if(!keep) {
                    tile.tileNum = -1;
                }
            }
            DetermineTileNum();
        }
    }

    public void SpawnTiles() {
        for(int y = 0; y < mapLength; y++) {
            float startingX = -y * xInterval;
            float startingY = y * yInterval;
            for(int x = 0; x < mapWidth; x++) {
               //x - 0.5 intervals
               //z - (9/32) intervals
               var tile = tiles[y * mapWidth + x];
                if(tile.tileNum != -1) {
                    float xLoc = startingX + (x * xInterval);
                    float yLoc = startingY + (x * yInterval);
                    var tileObj = (GameObject)Instantiate(tilePrefabs[tile.tileType], new Vector3(xLoc, yLoc, 0), gameObject.transform.rotation);
                    tileObj.transform.parent = transform;
                    tile.tileObj = tileObj;
                    tileObj.GetComponent<TileMono>().data = tile;
                }
            }
        }
    }

    public int GetSurroundingTiles(int x, int y) {
        //North
        int north = 1;
        if(y == mapLength - 1) {
            north = 0;
        } else {
            north = tiles[(y+1) * mapWidth + x].tileNum != -1 ? 1 : 0;
        }
        //East
        int east = 1;
        if(x == mapWidth - 1) {
            east = 0;
        } else {
            east = tiles[y * mapWidth + (x+1)].tileNum != -1 ? 1 : 0;
        }
        //South
        int south = 1;
        if(y == 0) {
            south = 0;
        } else {
            south = tiles[(y-1) * mapWidth + x].tileNum != -1 ? 1 : 0;
        }
        //West
        int west = 1;
        if(x == 0) {
            west = 0;
        } else {
            west = tiles[y * mapWidth + (x-1)].tileNum != -1 ? 1 : 0;
        }

        return (north * 1) + (east * 2) + (south * 4) + (west * 8);
   }
}
