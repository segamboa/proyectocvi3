using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public enum DrawMode {NoiseMap, ColourMap, Mesh ,FalloffMap};
    public DrawMode drawMode;

    public GameObject pfb1;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public bool useFlatShading;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool useFalloff;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;

    float[,] falloffMap;

    private int aux = 4;

    public void GenerateMap(){
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[mapWidth*mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {

                if(useFalloff){
                    noiseMap[x,y] = Mathf.Clamp01(noiseMap[x,y]-falloffMap[x,y]);
                }

                float currentHeight = noiseMap[x,y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if(currentHeight<= regions[i].height){
                        colourMap[y * mapWidth + x] = regions[i].colour;
                        break;
                    }
                    if(currentHeight <= 0.4 && aux >0){
                        //Instantiate(pfb1, new Vector3(x, currentHeight, y), Quaternion.identity);
                        aux--;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawMode == DrawMode.NoiseMap){
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }else if(drawMode == DrawMode.ColourMap){
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap,mapWidth,mapHeight));
        }else if(drawMode == DrawMode.Mesh){
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, useFlatShading), TextureGenerator.TextureFromColourMap(colourMap,mapWidth,mapHeight));
        }else if(drawMode == DrawMode.FalloffMap){
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapWidth)));
        }

    }

    private void Start() {
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth);
        GenerateMap();
        Instantiate(pfb1, new Vector3(-212, 0, -225), Quaternion.Euler(0,-39,0));
        Instantiate(pfb1, new Vector3(-247, 0, 15), Quaternion.identity);
        Instantiate(pfb1, new Vector3(225, 0, -125), Quaternion.identity);
        Instantiate(pfb1, new Vector3(143, 0, 180), Quaternion.Euler(0,-43,0));
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.F)){
            useFlatShading=!useFlatShading;
            GenerateMap();
        }
        if(Input.GetKeyDown(KeyCode.G)){
            seed++;
            GenerateMap();
        }
    }

    private void OnValidate() {
        if(mapWidth<1){
            mapWidth=1;
        }
        if(mapHeight<1){
            mapHeight=1;
        }
        if(lacunarity<1){
            lacunarity=1;
        }
        if(octaves<0){
            octaves=0;
        }

        falloffMap = FalloffGenerator.GenerateFalloffMap(mapWidth);
    }
}

[System.Serializable]
public  struct TerrainType {
    public float height;
    public string name;
    public Color colour;
}
