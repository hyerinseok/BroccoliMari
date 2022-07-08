using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    public GameObject[] myObjects; //broccoli and obstacle prefabs
    float elapsed = 0f;

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f) //every 1 second a random prefab is spawned
        {
            elapsed = elapsed % 1f;

            int randomIndex = Random.Range(0, myObjects.Length);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), Random.Range(-5, 6), transform.position.z);

            GameObject go = Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
            go.transform.parent = transform;
        }
    }
}
