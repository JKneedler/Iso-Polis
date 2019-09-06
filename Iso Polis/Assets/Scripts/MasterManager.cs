using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    [HideInInspector]
    public Services services;
    public TileMono hoverTile;
    public GameObject tileMarkerPrefab;
    GameObject tileMarkerInstance;
    Vector2 mousePos;
    BuildModes curMode;
    //Temp variable
    public GameObject dock;

    void Start()
    {
        curMode = BuildModes.Run;
    }

    void Update()
    {
        if(curMode == BuildModes.Run) {

        } else if(curMode == BuildModes.Build) {
            services.buildService.RunBuildMode();
        }


        SetMousePosition();
        if(hoverTile) {
            if(!tileMarkerInstance) {
                tileMarkerInstance = (GameObject)Instantiate(tileMarkerPrefab, hoverTile.transform.position, hoverTile.transform.rotation);
            } else {
                tileMarkerInstance.transform.position = hoverTile.transform.position;
            }
        } else {
           if(tileMarkerInstance) {
               Destroy(tileMarkerInstance);
           } 
        }
    }

    void SetMousePosition(){
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero , 200f, 1 << 8);
        //RaycastHit2D hitMouseLayer = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 200f, 1 << 9);

        if(hit) {
            if(hit.collider.gameObject.GetComponent<TileMono>()) {
                hoverTile = hit.collider.gameObject.GetComponent<TileMono>();
            }
        } else {
            hoverTile = null;
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


    }


    //Temp Method
    public void TestBuildDock() {
        GameObject obj = (GameObject)Instantiate(dock, new Vector3(0, 0, 0), dock.transform.rotation);
        services.buildService.StartBuildMode(obj);
        curMode = BuildModes.Build;
    }
}
