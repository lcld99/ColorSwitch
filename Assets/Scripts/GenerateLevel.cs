using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour {

    public GameObject[] obstacles;
    public GameObject star, colorChanger;
    private int counter, i;
    private float space = 9f;

	void Start () {
        StartLevel();
        counter = 0;
	}
	
	void Update () {
        if (Player_color.score > counter) {
            MakeNewObstacle();
        }
	}
    //Make new obsctacle whenever a new point is scored
    private void MakeNewObstacle() {
        int index = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[index], new Vector3(obstacles[index].transform.position.x, (2.5f + ((i + counter) * space)), obstacles[index].transform.position.z), Quaternion.identity);
        Instantiate(star, new Vector3(transform.position.x, (2.5f + ((i + counter) * space)), transform.position.z), Quaternion.identity);
        Instantiate(colorChanger, new Vector3(transform.position.x, (6f + ((i + counter) * space)), transform.position.z), Quaternion.identity);
        counter++;
    }
    //Start level by creating two random obstacles
    private void StartLevel() {
        for (i = 0; i < 2; i++) {
            int index = Random.Range(0, obstacles.Length);
            Instantiate(obstacles[index], new Vector3(obstacles[index].transform.position.x, (2.5f + (i* space)), obstacles[index].transform.position.z), Quaternion.identity);
            Instantiate(star, new Vector3(transform.position.x, (2.5f + (i * space)), transform.position.z), Quaternion.identity);
            Instantiate(colorChanger, new Vector3(transform.position.x, (6f + (i * space)), transform.position.z), Quaternion.identity);
        }
    }
}
