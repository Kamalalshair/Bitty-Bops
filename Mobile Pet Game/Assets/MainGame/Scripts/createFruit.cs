using UnityEngine;
using
using System.Collections;

public class createFruit : MonoBehaviour
{
    public GameObject myPrefab;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(myPrefab, worldPosition, Quaternion.identity);
        }
    }
}