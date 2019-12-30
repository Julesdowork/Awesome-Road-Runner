using UnityEngine;

public class OffScreen : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (!GeometryUtility.TestPlanesAABB(planes, spriteRenderer.bounds))
        {
            if (transform.position.x - Camera.main.transform.position.x < 0f)
            {
                CheckTile();
            }
        }
    }

    private void CheckTile()
    {
        if (CompareTag(MyTags.ROAD))
        {
            Change(ref MapGenerator.instance.lastPosOfRoad,
                new Vector3(1.5f, 0, 0),
                ref MapGenerator.instance.lastOrderOfRoad);
        }
        else if (CompareTag(MyTags.TOP_NEAR_GRASS))
        {
            Change(ref MapGenerator.instance.lastPosOfTopNearGrass,
                new Vector3(1.2f, 0, 0),
                ref MapGenerator.instance.lastOrderOfTopNearGrass);
        }
        else if (CompareTag(MyTags.TOP_FAR_GRASS))
        {
            Change(ref MapGenerator.instance.lastPosOfTopFarGrass,
                new Vector3(4.8f, 0, 0),
                ref MapGenerator.instance.lastOrderOfTopFarGrass);
        }
        else if (CompareTag(MyTags.BOTTOM_NEAR_GRASS))
        {
            Change(ref MapGenerator.instance.lastPosOfBottomNearGrass,
                new Vector3(1.2f, 0, 0),
                ref MapGenerator.instance.lastOrderOfBottomNearGrass);
        }
        else if (CompareTag(MyTags.BOTTOM_FAR_LAND_1))
        {
            Change(ref MapGenerator.instance.lastPosOfBottomFarLandF1,
                new Vector3(1.6f, 0, 0),
                ref MapGenerator.instance.lastOrderOfBottomFarLandF1);
        }
        else if (CompareTag(MyTags.BOTTOM_FAR_LAND_2))
        {
            Change(ref MapGenerator.instance.lastPosOfBottomFarLandF2,
                new Vector3(1.6f, 0, 0),
                ref MapGenerator.instance.lastOrderOfBottomFarLandF2);
        }
        else if (CompareTag(MyTags.BOTTOM_FAR_LAND_3))
        {
            Change(ref MapGenerator.instance.lastPosOfBottomFarLandF3,
                new Vector3(1.6f, 0, 0),
                ref MapGenerator.instance.lastOrderOfBottomFarLandF3);
        }
        else if (CompareTag(MyTags.BOTTOM_FAR_LAND_4))
        {
            Change(ref MapGenerator.instance.lastPosOfBottomFarLandF4,
                new Vector3(1.6f, 0, 0),
                ref MapGenerator.instance.lastOrderOfBottomFarLandF4);
        }
        else if (CompareTag(MyTags.BOTTOM_FAR_LAND_5))
        {
            Change(ref MapGenerator.instance.lastPosOfBottomFarLandF5,
                new Vector3(1.6f, 0, 0),
                ref MapGenerator.instance.lastOrderOfBottomFarLandF5);
        }
    }

    private void Change(ref Vector3 pos, Vector3 offset, ref int orderLayer)
    {
        transform.position = pos;
        pos += offset;

        spriteRenderer.sortingOrder = orderLayer;
        
        orderLayer++;
    }
}
