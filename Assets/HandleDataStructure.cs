using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HandleDataStructure : MonoBehaviour
{
	Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        DataStructureManager.getInstance().initializeWithTilemap(tilemap);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
