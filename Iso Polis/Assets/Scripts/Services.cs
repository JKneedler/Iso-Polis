using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Services : MonoBehaviour
{
    public MasterManager masterService;
    public BuildMode buildService;
    public MapGen mapService;

    public void Start() {
        masterService.services = this;
        buildService.services = this;
        mapService.services = this;
    }
}
