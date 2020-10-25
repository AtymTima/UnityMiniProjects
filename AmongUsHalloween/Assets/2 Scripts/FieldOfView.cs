using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("Cashed References")]
    [SerializeField] private MeshFilter meshFilter;

    [Header("FOV Parameters")]
    [SerializeField] private float maxFieldOfView = 90f;
    [SerializeField] private float maxDistanceFOV = 10f;
    [SerializeField] private int numberOfRays = 2;
    private float currentAngle;
    private float angleIncrease;
    private Vector3 origin = Vector3.zero;
    Mesh fieldMesh;

    public string sortingLayerName = string.Empty;
    public int orderInLayer = 0;
    public Renderer MyRenderer;

    private void Start()
    {
        fieldMesh = new Mesh();
        meshFilter.mesh = fieldMesh;
        SetSortingLayer();

    }

    private void Update()
    {
        angleIncrease = maxFieldOfView / numberOfRays;
        currentAngle = 0;

        Vector3[] fieldVertices = new Vector3[numberOfRays + 2];
        Vector2[] fieldUV = new Vector2[fieldVertices.Length];
        int[] fieldTriangles = new int[numberOfRays * 3];

        int vertexIndex = 1;
        int triangleIndex = 0;

        fieldVertices[0] = origin;
        for (int i=0; i <= numberOfRays; i++)
        {
            Vector3 currentVertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetAngleFromFloat(currentAngle), maxDistanceFOV);
            if (raycastHit2D.collider == null)
            {
                currentVertex = origin + GetAngleFromFloat(currentAngle) * maxDistanceFOV;
                Debug.Log("1");
            }
            else if (raycastHit2D.collider != null && !raycastHit2D.collider.CompareTag("Player"))
            {
                currentVertex = raycastHit2D.point;
                Debug.Log("2");
            }
            else
            {
                currentVertex = origin + GetAngleFromFloat(currentAngle) * maxDistanceFOV;
                Debug.Log("3");
            }

            fieldVertices[vertexIndex] = currentVertex;

            if (i > 0)
            {
                fieldTriangles[triangleIndex] = 0;
                fieldTriangles[triangleIndex + 1] = vertexIndex - 1;
                fieldTriangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;
            currentAngle -= angleIncrease;
        }

        fieldMesh.vertices = fieldVertices;
        fieldMesh.uv = fieldUV;
        fieldMesh.triangles = fieldTriangles;
    }

    void SetSortingLayer()
    {
        if (sortingLayerName != string.Empty)
        {
            MyRenderer.sortingLayerName = sortingLayerName;
            MyRenderer.sortingOrder = orderInLayer;
        }
    }

    private Vector3 GetAngleFromFloat(float angle)
    {
        float angleRad = angle * Mathf.PI / 180;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
