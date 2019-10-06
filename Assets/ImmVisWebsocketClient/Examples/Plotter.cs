using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plotter : MessageListener
{
    public virtual void Update()
    {

    }

    public override void PlotImage(ImageMessage imageMessage)
    {
        Debug.Log("Use image message to plot the image.");

        var sprite = gameObject.GetComponent<Sprite>();

        var texture = new Texture2D(2, 2);
        texture.LoadImage(imageMessage.Bytes);

        Instantiate(gameObject, gameObject.transform.position * 2, gameObject.transform.rotation);
    }

    public override void PlotHeightmap(HeightmapMessage heightmapMessage)
    {
        Debug.Log("Use heightmap message to plot the heightmap.");


    }
}
