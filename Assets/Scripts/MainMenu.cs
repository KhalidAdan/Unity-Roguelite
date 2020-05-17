using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void ExitGame()
    {
        // close game - ONCE IT IS BUILT, NOTHING HAPPENS IN EDITOR
        Application.Quit();
    }

}
