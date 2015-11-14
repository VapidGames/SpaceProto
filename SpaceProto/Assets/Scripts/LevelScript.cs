using UnityEngine;
using System.Collections;

public class LevelScript
{
    public bool shipBattle;

    public int mediumAsteroids;
    public int largeAsteroids;
    public int enemyShips;

    [Range(100, 500)]
    public int levelLength;

    public Vector3[] backgroundPlanets;
    public Vector3[] backgroundPlanetsSize;
    public Material[] backgroundPlanetsMaterials;

}
