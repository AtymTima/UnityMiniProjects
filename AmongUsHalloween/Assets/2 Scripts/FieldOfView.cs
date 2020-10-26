using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("Cashed References")]
    [SerializeField] private MeshFilter meshFilter;

    [Header("FOV Parameters")]
    [SerializeField] private float maxFieldOfView = 90f;
    [SerializeField] private float maxDistanceFOV = 10f;
    [SerializeField] private float minFieldOfView = 30f;
    [SerializeField] private float minDistanceFOV = 30f;
    [SerializeField] private int numberOfRays = 2;
    [SerializeField] private LayerMask colliderLayer;
    private float currentFieldOfView;
    private float currentDistanceFOV;
    private float currentAngle;
    private float startingAngle;
    private float angleIncrease;
    private bool isShortRange = true;
    private Vector3 origin = Vector3.zero;

    Mesh fieldMesh;
   
    public string sortingLayerName = string.Empty;
    public int orderInLayer = 0;
    public Renderer MyRenderer;

    private void Start()
    {
        currentDistanceFOV = maxDistanceFOV;
        currentFieldOfView = maxFieldOfView;
        fieldMesh = new Mesh();
        meshFilter.mesh = fieldMesh;
        origin = Vector3.zero;

        SetSortingLayer();
    }

    private void LateUpdate()
    {
        angleIncrease = currentFieldOfView / numberOfRays;
        currentAngle = startingAngle;

        Vector3[] fieldVertices = new Vector3[numberOfRays + 2];
        Vector2[] fieldUV = new Vector2[fieldVertices.Length];
        int[] fieldTriangles = new int[numberOfRays * 3];

        int vertexIndex = 1;
        int triangleIndex = 0;

        fieldVertices[0] = origin;
        for (int i=0; i <= numberOfRays; i++)
        {
            Vector3 currentVertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetAngleFromFloat(currentAngle), 
            currentDistanceFOV, colliderLayer);
            if (!raycastHit2D.collider)
            {
                currentVertex = origin + GetAngleFromFloat(currentAngle) * currentDistanceFOV;
            }
            else
            {
                currentVertex = raycastHit2D.point;
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

        fieldMesh.RecalculateBounds();
    }
     
    void SetSortingLayer()
    {
        if (sortingLayerName != string.Empty)
        {
            MyRenderer.sortingLayerName = sortingLayerName;
            MyRenderer.sortingOrder = orderInLayer;
        }
    }

    public void SetOriginPos(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetFieldOfViewParams(bool isCustom = false, float currentDistanceFOV = 0, float currentFieldOfView = 0)
    {
        if (isCustom)
        {
            this.currentDistanceFOV = currentDistanceFOV;
            this.currentFieldOfView = currentFieldOfView;
            return;
        }

        switch (isShortRange)
        {
            case true:
                this.currentFieldOfView = minFieldOfView;
                this.currentDistanceFOV = minDistanceFOV;
                break;
            case false:
                this.currentFieldOfView = maxFieldOfView;
                this.currentDistanceFOV = maxDistanceFOV;
                break;
        }
        isShortRange = !isShortRange;
    }

    public void SetAimingAngle(Vector3 aimingAngle)
    {
        startingAngle = GetAngleFloatFromVector(aimingAngle) + currentFieldOfView / 2;
    }

    private Vector3 GetAngleFromFloat(float angle)
    {
        float angleRad = angle * Mathf.PI / 180;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private float GetAngleFloatFromVector(Vector3 aimDirection)
    {
        aimDirection = aimDirection.normalized;
        float angleDeg = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        if (angleDeg < 0) { angleDeg += 360; }

        return angleDeg;
    }
}
