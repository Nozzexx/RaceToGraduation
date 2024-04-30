using System.Collections;
using System.Collections.Generic;
using SUPERCharacter;
using TMPro;
using UnityEngine;

public class DiplomaPickup : MonoBehaviour
{

     [SerializeField] public TMP_Text pickUpText;
     [SerializeField] public Player newPlayer;

     public AudioClip diplomaPickUpClip;

    public AudioSource source;

    void Start()
    {
        pickUpText = GameObject.Find("Text (TMP)").GetComponent<TMP_Text>();
        source = GetComponent<AudioSource>();
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
      private IEnumerator diplomaPickupSound()
    {
        Debug.Log("I Got Here.");
        //Wait for 5 sec.
        
       source.PlayOneShot(diplomaPickUpClip, 1f);

       newPlayer.diplomaFound = true;

       yield return new WaitForSeconds(0.5f);
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        StartCoroutine(diplomaPickupSound());

        StartCoroutine(DiplomaFunction());
    }

     private IEnumerator DiplomaFunction()
    {
        Debug.Log("I Got Here.");
        //Wait for 5 sec.
        
        //Turn My game object that is set to false(off) to True(on).
       pickUpText.enabled = true;
       pickUpText.text = "You have graduated! Escape while there is still time...";

       yield return new WaitForSeconds(2);
        
       pickUpText.enabled = false;
       this.gameObject.SetActive(false);
    }
}
