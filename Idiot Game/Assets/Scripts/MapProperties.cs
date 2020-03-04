using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapProperties
{
    
    public Dictionary<string, int> prefabKeys = new Dictionary<string, int>(){{"Box", 0}, {"Player", 1}};
    public List<int> gameObjectTypes = new List<int>();
    public List<float> positions = new List<float>();
    
}
