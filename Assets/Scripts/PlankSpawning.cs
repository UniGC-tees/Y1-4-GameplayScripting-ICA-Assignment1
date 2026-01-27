using UnityEngine;

public class PlankSpawning : MonoBehaviour
{
    public GameObject Plank;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Plank, new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0), Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
