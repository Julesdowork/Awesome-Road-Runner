using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator instance;

    public GameObject
        roadPrefab,
        grassPrefab,
        groundPrefab1,
        groundPrefab2,
        groundPrefab3,
        groundPrefab4,
        grassBottomPrefab,
        landPrefab1,
        landPrefab2,
        landPrefab3,
        landPrefab4,
        landPrefab5,
        bigGrassPrefab,
        bigGrassBottomPrefab,
        treePrefab1,
        treePrefab2,
        treePrefab3,
        bigTreePrefab;

    public GameObject
        roadHolder,
        topNearSidewalkHolder,
        topFarSidewalkHolder,
        bottomNearSidewalkHolder,
        bottomFarSidewalkHolder;

    public List<GameObject>
        roadTiles,
        topNearGrassTiles,
        topFarGrassTiles,
        bottomNearGrassTiles,
        bottomFarLandF1Tiles,
        bottomFarLandF2Tiles,
        bottomFarLandF3Tiles,
        bottomFarLandF4Tiles,
        bottomFarLandF5Tiles;
    [HideInInspector]
    public Vector3
        lastPosOfRoad,
        lastPosOfTopNearGrass,
        lastPosOfTopFarGrass,
        lastPosOfBottomNearGrass,
        lastPosOfBottomFarLandF1,
        lastPosOfBottomFarLandF2,
        lastPosOfBottomFarLandF3,
        lastPosOfBottomFarLandF4,
        lastPosOfBottomFarLandF5;

    public int
        startRoadTile,      // initialization: number of "road" tiles
        startGrassTile,     // initialization: number of "grass" tiles
        startGround3Tile,   // initialization: number of "ground3" tiles
        startLandTile;      // initialization: number of "land" tiles

    [Header("Positions to Ground Tile")]
    public int[] posForTopGround1;      // positions for ground1 on top from 0 to startGround3Tile
    public int[] posForTopGround2;      // positions for ground2 on top from 0 to startGround3Tile
    public int[] posForTopGround4;      // positions for ground4 on top from 0 to startGround3Tile

    [Header("Positions to Grass Tile")]
    public int[] posForTopBigGrass;     // positions for big grass with tree on top near grass from 0 to startGrassTile
    public int[] posForTopTree1;        // positions for tree1 on top near grass from 0 to startGrassTile
    public int[] posForTopTree2;        // positions for tree1 on top near grass from 0 to startGrassTile
    public int[] posForTopTree3;        // positions for tree1 on top near grass from 0 to startGrassTile
    public int[] posForBottomBigGrass;       // positions for big grass with tree on bottom near grass from 0 to startGrassTile
    public int[] posForBottomTree1;     // positions for tree1 on bottom near grass from 0 to startGrassTile
    public int[] posForBottomTree2;     // positions for tree2 on bottom near grass from 0 to startGrassTile
    public int[] posForBottomTree3;     // positions for tree3 on bottom near grass from 0 to startGrassTile

    [Header("Positions to Road Tile")]
    public int posForRoadTile1;       // position for road tile on road from 0 to startRoadTile
    public int posForRoadTile2;       // position for road tile on road from 0 to startRoadTile
    public int posForRoadTile3;       // position for road tile on road from 0 to startRoadTile
    [HideInInspector]
    public int
        lastOrderOfRoad,
        lastOrderOfTopNearGrass,
        lastOrderOfTopFarGrass,
        lastOrderOfBottomNearGrass,
        lastOrderOfBottomFarLandF1,
        lastOrderOfBottomFarLandF2,
        lastOrderOfBottomFarLandF3,
        lastOrderOfBottomFarLandF4,
        lastOrderOfBottomFarLandF5;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        InitializePlatform(roadPrefab, ref lastPosOfRoad, roadPrefab.transform.position,
            startRoadTile, roadHolder, ref roadTiles, ref lastOrderOfRoad,
            new Vector3(1.5f, 0, 0));

        InitializePlatform(grassPrefab, ref lastPosOfTopNearGrass, grassPrefab.transform.position,
            startGrassTile, topNearSidewalkHolder, ref topNearGrassTiles,
            ref lastOrderOfTopNearGrass, new Vector3(1.2f, 0, 0));
        InitializePlatform(groundPrefab3, ref lastPosOfTopFarGrass, groundPrefab3.transform.position,
            startGround3Tile, topFarSidewalkHolder, ref topFarGrassTiles,
            ref lastOrderOfTopFarGrass, new Vector3(4.8f, 0, 0));
        InitializePlatform(grassBottomPrefab, ref lastPosOfBottomNearGrass,
            new Vector3(2f, grassBottomPrefab.transform.position.y, 0),
            startGrassTile, bottomNearSidewalkHolder, ref bottomNearGrassTiles,
            ref lastOrderOfBottomNearGrass, new Vector3(1.2f, 0, 0));

        InitializeBottomFarLand();
    }

    private void InitializePlatform(GameObject prefab, ref Vector3 lastPos, Vector3 lastPosOfTile,
        int amountTile, GameObject holder, ref List<GameObject> listTile, ref int lastOrder, Vector3 offset)
    {
        int orderInLayer = 0;
        lastPos = lastPosOfTile;

        for (int i = 0; i < amountTile; i++)
        {
            GameObject clone = Instantiate(prefab, lastPos, prefab.transform.rotation);
            clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;

            if (clone.tag == MyTags.TOP_NEAR_GRASS)
            {
                SetNearScene(bigGrassPrefab, ref clone, ref orderInLayer,
                    posForTopBigGrass, posForTopTree1, posForTopTree2, posForTopTree3);
            }
            else if (clone.tag == MyTags.BOTTOM_NEAR_GRASS)
            {
                SetNearScene(bigGrassBottomPrefab, ref clone, ref orderInLayer,
                    posForBottomBigGrass, posForBottomTree1, posForBottomTree2, posForBottomTree3);
            }
            else if (clone.tag == MyTags.BOTTOM_FAR_LAND_2)
            {
                if (orderInLayer == 5)
                {
                    CreateTreeOrGround(bigTreePrefab, ref clone, new Vector3(-0.57f, -1.34f, 0));
                }
            }
            else if (clone.tag == MyTags.TOP_FAR_GRASS)
            {
                CreateGround(ref clone, ref orderInLayer);
            }

            clone.transform.SetParent(holder.transform);
            listTile.Add(clone);

            orderInLayer += 1;
            lastOrder = orderInLayer;

            lastPos += offset;
        }
    }

    private void CreateScene(GameObject bigGrass, ref GameObject tileClone, int orderInLayer)
    {
        GameObject clone = Instantiate(bigGrassPrefab,
            tileClone.transform.position, bigGrassPrefab.transform.rotation);
        clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        clone.transform.SetParent(tileClone.transform);
        clone.transform.localPosition = new Vector3(-0.183f, 0.106f, 0);

        CreateTreeOrGround(treePrefab1, ref clone, new Vector3(0, 1.52f, 0));

        // Turn off parent tile to show child tile
        tileClone.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void CreateTreeOrGround(GameObject prefab, ref GameObject tileClone, Vector3 localPos)
    {
        GameObject clone = Instantiate(prefab, tileClone.transform.position, prefab.transform.rotation);

        SpriteRenderer tileCloneRenderer = tileClone.GetComponent<SpriteRenderer>();
        SpriteRenderer cloneRenderer = clone.GetComponent<SpriteRenderer>();

        cloneRenderer.sortingOrder = tileCloneRenderer.sortingOrder;
        clone.transform.SetParent(tileClone.transform);
        clone.transform.localPosition = localPos;

        if (prefab == groundPrefab1 || prefab == groundPrefab2 || prefab == groundPrefab4)
        {
            tileCloneRenderer.enabled = false;
        }
    }

    private void CreateGround(ref GameObject clone, ref int orderInLayer)
    {
        for (int i = 0; i < posForTopGround1.Length; i++)
        {
            if (orderInLayer == posForTopGround1[i])
            {
                CreateTreeOrGround(groundPrefab1, ref clone, Vector3.zero);
                break;
            }
        }

        for (int i = 0; i < posForTopGround2.Length; i++)
        {
            if (orderInLayer == posForTopGround2[i])
            {
                CreateTreeOrGround(groundPrefab2, ref clone, Vector3.zero);
                break;
            }
        }

        for (int i = 0; i < posForTopGround4.Length; i++)
        {
            if (orderInLayer == posForTopGround4[i])
            {
                CreateTreeOrGround(groundPrefab4, ref clone, Vector3.zero);
                break;
            }
        }
    }

    void SetNearScene(GameObject bigGrass, ref GameObject clone, ref int orderInLayer,
        int[] posForBigGrass, int[] posForTree1, int[] posForTree2, int[] posForTree3)
    {
        for (int i = 0; i < posForBigGrass.Length; i++)
        {
            if (orderInLayer == posForBigGrass[i])
            {
                CreateScene(bigGrassPrefab, ref clone, orderInLayer);
                break;
            }
        }

        for (int i = 0; i < posForTree1.Length; i++)
        {
            if (orderInLayer == posForTree1[i])
            {
                CreateTreeOrGround(treePrefab1, ref clone, new Vector3(0, 1.15f, 0));
                break;
            }
        }

        for (int i = 0; i < posForTree2.Length; i++)
        {
            if (orderInLayer == posForTree2[i])
            {
                CreateTreeOrGround(treePrefab2, ref clone, new Vector3(0, 1.15f, 0));
                break;
            }
        }

        for (int i = 0; i < posForTree3.Length; i++)
        {
            if (orderInLayer == posForTree3[i])
            {
                CreateTreeOrGround(treePrefab3, ref clone, new Vector3(0, 1.15f, 0));
                break;
            }
        }
    }

    private void InitializeBottomFarLand()
    {
        InitializePlatform(landPrefab1, ref lastPosOfBottomFarLandF1, landPrefab1.transform.position,
            startLandTile, bottomFarSidewalkHolder, ref bottomFarLandF1Tiles,
            ref lastOrderOfBottomFarLandF1, new Vector3(1.6f, 0, 0));

        InitializePlatform(landPrefab2, ref lastPosOfBottomFarLandF2, landPrefab2.transform.position,
            startLandTile - 3, bottomFarSidewalkHolder, ref bottomFarLandF2Tiles,
            ref lastOrderOfBottomFarLandF2, new Vector3(1.6f, 0, 0));

        InitializePlatform(landPrefab3, ref lastPosOfBottomFarLandF3, landPrefab3.transform.position,
            startLandTile - 4, bottomFarSidewalkHolder, ref bottomFarLandF3Tiles,
            ref lastOrderOfBottomFarLandF3, new Vector3(1.6f, 0, 0));

        InitializePlatform(landPrefab4, ref lastPosOfBottomFarLandF4, landPrefab4.transform.position,
            startLandTile - 7, bottomFarSidewalkHolder, ref bottomFarLandF4Tiles,
            ref lastOrderOfBottomFarLandF4, new Vector3(1.6f, 0, 0));

        InitializePlatform(landPrefab5, ref lastPosOfBottomFarLandF5, landPrefab5.transform.position,
            startLandTile - 10, bottomFarSidewalkHolder, ref bottomFarLandF5Tiles,
            ref lastOrderOfBottomFarLandF5, new Vector3(1.6f, 0, 0));
    }
}
