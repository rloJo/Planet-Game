using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public float horizontalInput;
    private float speed = 7.0f;
    private Vector3 pos;
    private float xRange = 3.3f;
    public GameObject[] planets;
    float timer = 0f;
    float delay = 0.5f;
    // Start is called before the first frame update

    private void Spawn() 
    { 
        Vector3 spawnPosition = transform.position;
        int rand = Random.Range(1, 101);
        int idx = 0;

        if (rand <= 28) idx = 0;
        else if (rand <= 58) idx = 1;
        else if (rand <= 85) idx = 2;
        else idx = 3;

        GameObject SelectedPlanet = planets[idx];
        GameObject planet = Instantiate(SelectedPlanet, spawnPosition, Quaternion.identity);
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if(transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, 0);
        }
        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, 0);
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right *Time.deltaTime * speed * horizontalInput);
        }

        if (Input.GetMouseButtonUp(0) && timer > delay)
        {
            timer = 0;
            Spawn();
        }
        
    }
}
