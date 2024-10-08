using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager_color : MonoBehaviour {

    public GameObject panel_loading;

    public void LoadLevel() {
      panel_loading.SetActive(true);
       SceneManager.LoadSceneAsync(0 , LoadSceneMode.Single);
    }
 }
