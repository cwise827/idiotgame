using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoadSave : MonoBehaviour
{
    MapManager mMan = new MapManager();
    bool loadedOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!loadedOnce){
            //mMan.SaveMap();
            mMan.LoadGame();
            Debug.Log("started");
        }
        else{
            loadedOnce = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
