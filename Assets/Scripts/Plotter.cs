using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plotter : MonoBehaviour
{

    public void Plot(ImageMessage imageMessage)
    {
        Terrain map = GetComponent<Terrain>();

        map.terrainData = GenerateTerrainDataFromImage(imageMessage);
    }

    private TerrainData GenerateTerrainDataFromImage(ImageMessage imageMessage)
    {
        TerrainData terrainData = new TerrainData();
        terrainData.size = new Vector3(imageMessage.Width, 20f, imageMessage.Height);

        return terrainData;
    }

    public virtual void Update()
    {

    }
}
