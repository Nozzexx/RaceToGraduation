using System.Collections;
using System.Collections.Generic;
using SUPERCharacter;
using TMPro;
using UnityEngine;

public class CandyPickup : MonoBehaviour
{

     [SerializeField] public TMP_Text pickUpText;
     [SerializeField] public Player newPlayer;

    void Start()
    {
        pickUpText = GameObject.Find("Text (TMP)").GetComponent<TMP_Text>();
        //newPlayer = GameObject.Find("Player");
        //playerPickUpCount = GameObject.Find("Player").GetComponent<Player>().pickUpCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(pickUpText == null)
        {
            pickUpText = GameObject.Find("Text (TMP)").GetComponent<TMP_Text>();
            Debug.Log("Text Found");
        }

        if(newPlayer == null)
        {   newPlayer = GameObject.Find("Player").GetComponent<Player>();
        }

    }

    private void OnTriggerEnter(Collider other) 
    {
        

        newPlayer.GetComponent<SUPERCharacterAIO>().walkingSpeed += 25;
        newPlayer.GetComponent<SUPERCharacterAIO>().sprintingSpeed += 25;
        newPlayer.GetComponent<SUPERCharacterAIO>().s_regenerationSpeed += .5f;

        StartCoroutine(CandyFunction());
        
    }

     private IEnumerator CandyFunction()
    {
        Debug.Log("I Got Here.");
        //Wait for 5 sec.
        
        //Turn My game object that is set to false(off) to True(on).
       pickUpText.enabled = true;
       pickUpText.text = "You have found Candy, your speed has improved.";

       yield return new WaitForSeconds(2);
        
       pickUpText.enabled = false;
       this.gameObject.SetActive(false);
    }
}
