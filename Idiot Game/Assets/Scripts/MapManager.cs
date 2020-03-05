using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class MapManager
{
    
      public static T KeyByValue<T, W>(Dictionary<T, W> dict, W val)
    {
        T key = default;
        foreach (KeyValuePair<T, W> pair in dict)
        {
            if (EqualityComparer<W>.Default.Equals(pair.Value, val))
            {
                key = pair.Key;
                break;
            }
        }
        return key;
    }
    
    private MapProperties mapProperties(){
        MapProperties mapP = new MapProperties();
        Dictionary<string, int> keys = mapP.prefabKeys;
        foreach(GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>()){
            if(go.activeInHierarchy){
                foreach(var item in keys){
                    if(item.Key == go.tag){
                        mapP.gameObjectTypes.Add(keys[go.tag]);
                        mapP.positions.Add(go.transform.position.x);
                        mapP.positions.Add(go.transform.position.y);
                        mapP.positions.Add(go.transform.position.z);
                    }
                }
            }
        }

        return mapP;
    }

    public void ClearGameObjects(){
        MapProperties mapP = new MapProperties();
        Dictionary<string, int> keys = mapP.prefabKeys;
        foreach(GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>()){
            if(go.activeInHierarchy){
                foreach(var item in keys){
                    if(item.Key == go.tag){
                        UnityEngine.Object.Destroy(go);
                    }
                }
            }
        }
    }

    public void SaveMap(string mapName)
    {
    
        MapProperties save = mapProperties();
 
        BinaryFormatter bf = new BinaryFormatter();
        if(!File.Exists(Application.dataPath + "/Maps/" + mapName + ".igm")){
            FileStream file = File.Create(Application.dataPath + "/Maps/" + mapName + ".igm");
            bf.Serialize(file, save);
            file.Close();
            
            Debug.Log("Game Saved");
        }
        else{
            Debug.Log("Warning! Did not save! Map name already exists!");
        }
        
    }

    public void LoadMap(string mapName)
    { 
  // 1
        GameObject prefab;
        string s;
        MapProperties mapP = new MapProperties();
        Dictionary<string, int> keys = mapP.prefabKeys;
        
        if (File.Exists(Application.dataPath + "/Maps/" + mapName + ".igm"))
        {
            ClearGameObjects();

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/Maps/" + mapName + ".igm", FileMode.Open);
            MapProperties save = (MapProperties)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < save.positions.Count; i += 3)
            {
                s = KeyByValue(mapP.prefabKeys, save.gameObjectTypes[i/3]);
                prefab = Resources.Load("Prefabs/" + s) as GameObject;
                float positionx = save.positions[i];
                float positiony = save.positions[i+1];
                float positionz = save.positions[i+2];
                UnityEngine.Object.Instantiate(prefab, new Vector3(positionx, positiony, positionz), Quaternion.identity);
                //Debug.Log("Game object " + s + " has positions x: " + positionx + " y: " + positiony + " z: " + positionz);
                
                
            }

            
            Debug.Log("Map " + mapName + " loaded!");

        }
        else
        {
            Debug.Log("Map name does not exist in Assets/Maps. Make sure not to include .igm at the end of \"Map Name\"");
        }
    }
}
