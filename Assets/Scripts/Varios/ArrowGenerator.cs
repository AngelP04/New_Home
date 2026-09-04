using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
public class ArrowGenerator : MonoBehaviour
{
    public float stemLength;
    public float stemWidth;
    public float tipLength;
    public float tipWidth;

    [System.NonSerialized] public List<Vector3> verticesList;
    [System.NonSerialized] public List<int> trianglesList;

    Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        GenerateArrow();
    }

    void GenerateArrow()
    {
        verticesList = new List<Vector3>();
        trianglesList = new List<int>();
        
        Vector3 stemOrigin = Vector3.zero;
        float stemHalfWidth = stemWidth/2f;

        verticesList.Add(stemOrigin + (stemHalfWidth * Vector3.down));
        verticesList.Add(stemOrigin + (stemHalfWidth * Vector3.up));
        /*
          Doble flecha
          verticesList.Add(stemOrigin + (stemHalfWidth * Vector3.right));
          verticesList.Add(stemOrigin + (stemHalfWidth * Vector3.right));
        */
        verticesList.Add(verticesList[0]+(stemLength*Vector3.right));
        verticesList.Add(verticesList[1]+(stemLength * Vector3.right));


        trianglesList.Add(0);
        trianglesList.Add(1);
        trianglesList.Add(3);
        trianglesList.Add(0);
        trianglesList.Add(3);
        trianglesList.Add(2);

        Vector3 tipOrigin = stemLength * Vector3.right;
        float tipHalfWidth = tipWidth / 2;

        verticesList.Add(tipOrigin + (tipHalfWidth * Vector3.up));
        verticesList.Add(tipOrigin + (tipHalfWidth * Vector3.down));
        verticesList.Add(tipOrigin + (tipHalfWidth* Vector3.right));

        trianglesList.Add(4);
        trianglesList.Add(6);
        trianglesList.Add(5);

        mesh.vertices = verticesList.ToArray();
        mesh.triangles = trianglesList.ToArray();
    }
}
