using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    public Button mainmenu;
    public Button retry;

    // Use this for initialization
    void Start () {
        mainmenu.onClick.AddListener(delegate { mainmenuCallback(); });
        retry.onClick.AddListener(delegate { retryCallback(); });
    }

    void mainmenuCallback()
    {
        Application.LoadLevel("Menu");
    }

    void retryCallback()
    {
        Application.LoadLevel("Astar_test");
    }

    // Update is called once per frame
    void Update () {
		
	}


}
