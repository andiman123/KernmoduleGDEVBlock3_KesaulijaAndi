using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    public bool DrawGrid;
    public LayerMask unwakableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;


    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid = null;
            StartCoroutine(CreateGrid());
            
        }
    }
    public IEnumerator CreateGrid()
    {
        yield return new WaitForSeconds(0.1f);
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 wordlBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for (int x = 0; x<gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = wordlBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y*nodeDiameter+nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius,unwakableMask));
                grid[x, y] = new Node(walkable, worldPoint,x,y);
            }
        }



        yield return new WaitForSeconds(0);
    }

    public List <Node> GetNeighbours(Node node)
    {
        List<Node> Neighbours = new List<Node>();

        for(int x = -1; x < 1; x++)
        {
            for (int y = -1; y < 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    Neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return Neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public List<Node> path;
    void OnDrawGizmos()
    {
        if(DrawGrid == true)
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

            if (grid != null)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;
                    if(path!= null)
                    {
                        if (path.Contains(n))
                        {
                            Gizmos.color = Color.blue;
                        }
                    }
                    Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - 0.1f));
                }
            }
        }
        
    }
}
