using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    public TileMono hoverTile;

    void Start()
    {
        
    }

    void Update()
    {
        SetMousePosition();
    }

    void SetMousePosition(){
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero , 200f);

        if(hit) {
            if(hit.collider.gameObject.GetComponent<TileMono>()) {
                hoverTile = hit.collider.gameObject.GetComponent<TileMono>();
            }
        }

        // if(Physics.Raycast(ray, out hit, 100f, mouseLocLayerMask)) {
        //     mousePos.x = hit.point.x;
        //     mousePos.y = hit.point.y;
        // }
    }
}
