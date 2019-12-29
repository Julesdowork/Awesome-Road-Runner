using System.Collections;
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

            }
            else if (clone.tag == MyTags.BOTTOM_NEAR_GRASS)
            {

            }
            else if (clone.tag == MyTags.BOTTOM_FAR_LAND_2)
            {

            }
            else if (clone.tag == MyTags.TOP_FAR_GRASS)
            {

            }

            clone.transform.SetParent(holder.transform);
            listTile.Add(clone);

            orderInLayer += 1;
            lastOrder = orderInLayer;

            lastPos += offset;
        }
    }

}
