using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator 
{

    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve, bool useFlatShading){
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width-1)/-2f;
        float topLeftz = (height-1)/2f;

        MeshData meshData = new MeshData (width, height, useFlatShading);
        int vertexIndex = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x,y])*heightMultiplier,topLeftz - y);
                meshData.uvs[vertexIndex] = new Vector2(x/(float)width, y/(float)height);

                if(x<width-1 && y<height-1){
                    meshData.Addtriangle(vertexIndex, vertexIndex+width+1, vertexIndex+width);
                    meshData.Addtriangle(vertexIndex+width+1, vertexIndex, vertexIndex+1);
                }

                vertexIndex++;
            }
        }

        meshData.Finalize();

        return meshData;
    }

}

public class MeshData {
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;
    bool useFlatShading;

    public MeshData(int meshWidth, int meshHeight, bool useFlatShading){
        this.useFlatShading = useFlatShading;
        vertices = new Vector3[meshWidth*meshHeight];
        uvs = new Vector2[meshWidth*meshHeight];
        triangles = new int[(meshWidth-1)*(meshHeight-1)*6];
    }

    public void Addtriangle(int a, int b, int c){
        triangles [triangleIndex] =a;
        triangles [triangleIndex+1] =b;
        triangles [triangleIndex+2] =c;
        triangleIndex +=3;
    }

    public void Finalize(){
        if(useFlatShading){
            FlatShading();
        }
    }

    void FlatShading(){
        Vector3[] flatShadedVertices = new Vector3[triangles.Length];
        Vector2[] flatShadedUvs = new Vector2[triangles.Length];
        for (int i = 0; i < triangles.Length; i++)
        {
            flatShadedVertices[i] = vertices[triangles[i]];
            flatShadedUvs[i] = uvs[triangles[i]];
            triangles[i]= i;
        }

        vertices = flatShadedVertices;
        uvs = flatShadedUvs;
    }

    public Mesh CreateMesh(){
        Mesh mesh = new Mesh();
        Debug.Log(vertices.Length);
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        if(useFlatShading){
            mesh.RecalculateNormals();
        }
        mesh.RecalculateNormals();
        return mesh;
    }
}
