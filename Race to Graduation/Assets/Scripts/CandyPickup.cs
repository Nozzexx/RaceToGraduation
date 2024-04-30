using System.Collections;
using System.Collections.Generic;
using SUPERCharacter;
using TMPro;
using UnityEngine;

public class CandyPickup : MonoBehaviour
{
     [SerializeField] public TMP_Text pickUpText;
     [SerializeField] public Player newPlayer;

     public AudioClip candyPickUpClip;

    public AudioSource source;

    void Start()
    {
        pickUpText = GameObject.Find("Text (TMP)").GetComponent<TMP_Text>();
        source = GetComponent<AudioSource>();
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
        
        StartCoroutine(CandyPickupSound());

        StartCoroutine(CandyFunction());
        
    }

     private IEnumerator CandyPickupSound()
    {
        Debug.Log("I Got Here.");
        //Wait for 5 sec.

      this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        
      source.PlayOneShot(candyPickUpClip, 1f);

      newPlayer.GetComponent<SUPERCharacterAIO>().walkingSpeed += 25;
      newPlayer.GetComponent<SUPERCharacterAIO>().sprintingSpeed += 25;
      newPlayer.GetComponent<SUPERCharacterAIO>().s_regenerationSpeed += .5f;

       yield return new WaitForSeconds(0.5f);
        
    }

     private IEnumerator CandyFunction()
    {
        Debug.Log("I Got Here.");
        //Wait for 5 sec.
        
        //Turn My game object that is set to false(off) to True(on).
       pickUpText.enabled = true;
       pickUpText.text = "You have found Candy, your speed and stamina have improved.";

       yield return new WaitForSeconds(1);
        
       pickUpText.enabled = false;
       this.gameObject.SetActive(false);
    }
}
