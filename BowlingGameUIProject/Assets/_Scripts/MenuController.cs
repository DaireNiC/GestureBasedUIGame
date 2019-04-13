using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    // panel for start menu
    [SerializeField]
    private GameObject startMenu;
    // panel for help menu
    [SerializeField]
    private GameObject helpMenu;

    // Start is called before the first frame update
    void Start()
    {
    }


    // start the game
    public void Play()
    {
        // hide the menu panel
        this.startMenu.SetActive(false);
        this.helpMenu.SetActive(false);
    }

    // view the game tutorial
    public void Help()
    {

        //show the menu panel
        this.helpMenu.SetActive(true);

    }
    // exit the help menu
    public void Exit()
    {
        this.helpMenu.SetActive(false);

    }


    /* Start Menu shown at beginning of game
 Also contains game tutorial */
    public void StartMenu()
    {

       
    }


}
