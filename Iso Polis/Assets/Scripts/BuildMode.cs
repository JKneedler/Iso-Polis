using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMode : MonoBehaviour
{
    [HideInInspector]
    public Services services;
    BuildableTile curBuildTile;
    GameObject curBuildTileObj;

    public void StartBuildMode(GameObject newBuildTile) {
        curBuildTileObj = newBuildTile;
        curBuildTile = newBuildTile.GetComponent<BuildableTile>();

    }

    public void RunBuildMode() {
        if(services.masterService.hoverTile) {
            curBuildTileObj.SetActive(true);
            curBuildTileObj.transform.position = services.masterService.hoverTile.transform.position;
        } else {
            curBuildTileObj.SetActive(false);
        }
    }
}
