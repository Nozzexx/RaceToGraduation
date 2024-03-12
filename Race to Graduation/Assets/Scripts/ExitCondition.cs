using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ExitCondition : MonoBehaviour
{

    [SerializeField] private BoxCollider exit;
    [SerializeField] public TMP_Text displayText;

    [SerializeField] public Player newPlayer;
    [SerializeField] public int playerPickUpTotal;
    [SerializeField] public int playerPickUpCount;
    public void Start()
    {
       displayText = GameObject.Find("Text (TMP)").GetComponent<TMP_Text>();
       if(displayText != null)
       {
            displayText.enabled = false;
       }
       
    }

    // Update is called once per frame
    void Update()
    {
        if(displayText == null)
        {
            displayText = GameObject.Find("Text (TMP)").GetComponent<TMP_Text>();
            displayText.enabled = false;
            Debug.Log("Text Found");
        }

        if(newPlayer == null)
        {
            newPlayer = GameObject.Find("Player").GetComponent<Player>();
            playerPickUpTotal = GameObject.Find("Player").GetComponent<Player>().pickUpTotal;
        }

    }

    private bool checkPlayerPickUps()
    {
        playerPickUpTotal = newPlayer.getPickUpTotal();
        playerPickUpCount = newPlayer.getPickUpCount();
        if(playerPickUpCount == playerPickUpTotal)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    private IEnumerator ExitFunction()
    {
        Debug.Log("I Got Here.");
        //Wait for 5 sec.
        
        //Turn My game object that is set to false(off) to True(on).
       displayText.enabled = true;
       displayText.text ="You have escaped.... for now.";

       yield return new WaitForSeconds(3);
        
       displayText.enabled = false;

       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

     private IEnumerator DenyFunction()
    {
        Debug.Log("I Got Here.");
        //Wait for 5 sec.
        
        //Turn My game object that is set to false(off) to True(on).
       displayText.enabled = true;
       displayText.text ="You do not have enough comet shards to escape...";

       yield return new WaitForSeconds(3);
        
       displayText.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(checkPlayerPickUps())
        {
            StartCoroutine(ExitFunction());
        }
        else
        {
            StartCoroutine(DenyFunction());
        }
        
    }
}
