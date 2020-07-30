using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        //TODO Create new save file and load world
    }

    public void LoadGame()
    {
        //TODO Show list of save files
    }

    public void Settings()
    {
        //TODO Show settings
        //Can probably just grab them from the game scene
    }

    public void Exit()
    {
        Application.Quit();
    }

}
