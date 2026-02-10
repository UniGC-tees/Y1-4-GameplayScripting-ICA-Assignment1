using UnityEngine;

public class PlankSpawning : MonoBehaviour
{
    public GameObject [] plankTypes;
    private int sinceLastTry = 0;
    private float plankScreenRange = 9f;

    public void TrySpawnPlank()
    {
        if (sinceLastTry > 20)
        {
            SpawnPlank();
            sinceLastTry = 0;
            return;
        }
        else
        {
            sinceLastTry++;
        }
    }

    public void SpawnPlank()
    {
        Instantiate(plankTypes[Random.Range(0,plankTypes.Length)], new Vector3(Random.Range(-plankScreenRange, plankScreenRange), 7+transform.position.y, 0), Quaternion.identity);
    }
}
