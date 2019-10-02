using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plotter : PlotterBehaviour
{
    public virtual void Update()
    {

    }

    public override void PlotImage(ImageMessage imageMessage)
    {
        Debug.Log("Use image message to plot the image.");
    }

    public override void PlotHeightmap(HeightmapMessage heightmapMessage)
    {
        Debug.Log("Use heightmap message to plot the heightmap.");
            }
}
