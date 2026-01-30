using UnityEngine;

public class PlankSpawning : MonoBehaviour
{
    public GameObject Plank;
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
        Instantiate(Plank, new Vector3(Random.Range(-8f, 8f), 5+transform.position.y, 0), Quaternion.identity);
    }
}
