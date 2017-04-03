using UnityEngine;
using System.Collections;

public class ObjectGenerator : MonoBehaviour {

    // Use this for initialization

    public GameObject[] buildings;

    public static int mapSize = 40;

    private int startWidth;
    private int startHeight;
    private int endPosw;
    private int endPosh;
    private int streetCount;

    public GameObject[] Tiles;
    public static float[,] mapgrid;
    
    

	void Start () {

        



        //StartCoroutine(SetStreets());

        //SetStreetsin
        //int x = Random.RandomRange(0,mapWidth);
        //int maxStreetLength = Random.RandomRange(1, 5);
        //for (int n = 0; n < 2; n++)
        //{
        //    for (int h = 0; h < maxStreetLength; h++)
        //    {
        //        mapgrid[x, h] = -1;

        //    }
        //    x += Random.RandomRange(1, 3);
        //    if (x >= mapWidth)
        //    {
        //        break;
        //    }
        //}



        //Debug
        //    for (int w = 0; w < mapWidth; w++)
        //{
        //    for (int h = 0; h < mapHeight; h++)
        //    {
        //        Debug.Log(mapgrid[w, h]);
        //    }
        //}


        //SpawnObjects


    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {

            mapgrid = new float[mapSize, mapSize];
            Tiles = GameObject.FindGameObjectsWithTag("Tile");
            for (int i = 0; i < Tiles.Length; i++)
            {
                Destroy(Tiles[i].gameObject);
            }

            StartCoroutine(SetBuildings());
            //Reset&SetnewStreets
            streetCount = 0;
            StartCoroutine(SetStreets());
            StartCoroutine(SetBase());
            StartCoroutine(SetSpawner());

            //SpawnLEVEL
            StartCoroutine(SpawnObjects());




        }
        if (Input.GetMouseButtonDown(1))
        {

            

        }

    }

    private IEnumerator SetBuildings()
    {
        for (int w = 0; w < mapSize; w++)
        {
            for (int h = 0; h < mapSize; h++)
            {
                float seed = Random.Range(0, 100);

                mapgrid[w, h] = Mathf.PerlinNoise(w + 0.1f + seed, h + 0.1f + seed) * 10;

            }
        }

        yield return new WaitForSeconds(0);
    }

    private IEnumerator SetBase()
    {

        int startPos = mapSize/2;
        for(int x = 0; x < startPos; x++)
        {
            for (int y = 0; y < startPos; y++)
            {
                mapgrid[startPos-(startPos / 2) + x, startPos- (startPos / 2) + y] = -1;
            }
        }
        mapgrid[mapSize / 2, mapSize / 2] = -3;
        yield return new WaitForSeconds(0);
    }
    private IEnumerator SetStreets()
    {
        int direc = Random.RandomRange(0, 3);
        startWidth = Random.RandomRange(0, mapSize);
        startHeight = Random.RandomRange(0, mapSize);
        int maxStep = mapSize / 8;
      
        //Rechts
        if(direc == 0 && startWidth < mapSize - 1)
        {
            for (int c = 0; c < maxStep; c++)
            {
                mapgrid[startWidth + c, startHeight] = -1;
                endPosw += 1;
                if (startWidth + c + maxStep > mapSize)
                {
                    break;
                }
            }
        }

        if (direc == 1 && startWidth > 1)
        {
            for (int c = 0; c < maxStep; c++)
            {
                mapgrid[startWidth - c, startHeight] = -1;
                endPosw -= 1;


                if (startWidth - c - maxStep < mapSize)
                {
                    break;
                }
            }
        }

        if (direc == 2 && startHeight < mapSize - 1)
        {
            for (int c = 0; c < maxStep; c++)
            {
                mapgrid[startWidth, startHeight + c] = -1;
                endPosh += 1;

                if (startHeight + c + maxStep > mapSize)
                {
                    break;
                }
            }
        }

        if (direc == 3 && startHeight > 1)
        {
            for (int c = 0; c < maxStep; c++)
            {
                mapgrid[startWidth, startHeight - c] = -1;
                endPosh -= 1;

                if (startHeight - c - maxStep < mapSize)
                {
                    break;
                }
            }
        }

        startWidth = startWidth + endPosw;
        startHeight = startHeight + endPosh;

        endPosw = 0;
        endPosh = 0;
        streetCount = streetCount + 1;

        if (streetCount < mapSize * 10)
        {
            StartCoroutine(SetStreets());
        }
        yield return new WaitForSeconds(0);
    }
    private IEnumerator SetSpawner()
    {
        for (int x = 0; x < mapSize; x++)
        {
            if(mapgrid[x, 0] == -1)
            {
                mapgrid[x, 0] = -2;

            }

            if (mapgrid[0, x] == -1)
            {
                mapgrid[0, x] = -2;
            }

            if (mapgrid[x, mapSize-1] == -1)
            {
                mapgrid[x, mapSize-1] = -2;
            }

            if (mapgrid[mapSize-1, x] == -1)
            {
                mapgrid[mapSize-1, x] = -2;
            }
        }
        yield return new WaitForSeconds(0);
    }
    private IEnumerator SpawnObjects()
    {
        //SpawnObjects
        for (int w = 0; w < mapSize; w++)
        {
            for (int h = 0; h < mapSize; h++)
            {
                float result = mapgrid[w, h];

                Vector3 pos = new Vector3(w - (mapSize/2), 0, h - (mapSize/2));
                if (result < -2)
                {
                    Instantiate(buildings[5], pos + new Vector3(0, -0.99f, 0), Quaternion.identity);
                }
                if (result < -1)
                {
                    Instantiate(buildings[4], pos + new Vector3(0, -0.99f, 0), Quaternion.identity);
                }
                if (result < 0)
                {
                    Instantiate(buildings[3], pos + new Vector3(0, -0.99f, 0), Quaternion.identity);
                }
                else if (result < 4)
                {
                    Instantiate(buildings[0], pos, Quaternion.identity);
                }
                else if (result < 6)
                {
                    Instantiate(buildings[1], pos, Quaternion.identity);
                }
                else if (result < 10)
                {
                    Instantiate(buildings[2], pos, Quaternion.identity);
                }



            }
        }

        yield return new WaitForSeconds(0);
    }
}
