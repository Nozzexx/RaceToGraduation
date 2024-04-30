using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
   public void resumeGame()
   {
        this.gameObject.SetActive(false);
        Time.timeScale = 1; 
   }

   public void quitGame()
   {
        Application.Quit();
   }
   
}
