using UnityEngine;

public class PlankSpawning : MonoBehaviour
{
    public GameObject [] plankTypes;
    private int sinceLastTry = 0;

    public void TrySpawnPlank()
    {
        if (sinceLastTry > 30)
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
        Instantiate(plankTypes[Random.Range(0,plankTypes.Length)], new Vector3(Random.Range(-8f, 8f), 7+transform.position.y, 0), Quaternion.identity);
    }
}
