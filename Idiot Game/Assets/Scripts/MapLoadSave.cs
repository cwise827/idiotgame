using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoadSave : MonoBehaviour
{
    MapManager mMan = new MapManager();
    public bool saveMap, loadMap;
    public string mapName = "Default";
    // Start is called before the first frame update
    void Start()
    {
        if(saveMap){
            mMan.SaveMap(mapName);
        }
        if(loadMap){
            mMan.LoadMap(mapName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
