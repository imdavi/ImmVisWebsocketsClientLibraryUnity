


using UnityEngine;

public abstract class PlotterBehaviour : MonoBehaviour {

    public abstract void PlotImage(ImageMessage imageMessage);

    public abstract void PlotHeightmap(HeightmapMessage heightmapMessage);

}