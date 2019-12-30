using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHolder : MonoBehaviour
{
    public GameObject[] children;
    public Vector3 firstPos, secondPos;

    public float limitAxisX;
    
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-GameplayController.instance.moveSpeed * Time.deltaTime, 0, 0);

        if (transform.localPosition.x <= limitAxisX)
        {
            // INFORM THE GAMEPLAY CONTROLLER THAT THE OBSTACLE IS NOT ACTIVE
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetActive(true);
        }

        if (Random.value <= 0.5f)
        {
            transform.localPosition = firstPos;
        }
        else
        {
            transform.localPosition = secondPos;
        }
    }
}
