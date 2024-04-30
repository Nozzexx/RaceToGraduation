using UnityEngine;
using System.Collections;


public class PauseGame : MonoBehaviour {
    public GameObject canvas;
 
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            quitGame();
        }

        if(canvas == null)
        {
           canvas = GameObject.Find("PauseMenu");
           canvas.gameObject.SetActive(false);
        }
	}
    public void Pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;       
        }
    }

     public void quitGame()
   {
        Debug.Log("Game Quit.");
        Application.Quit();
   }
}