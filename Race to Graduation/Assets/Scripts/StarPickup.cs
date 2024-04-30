using System.Collections;
using System.Collections.Generic;
using SUPERCharacter;
using TMPro;
using UnityEngine;

public class StarPickup : MonoBehaviour
{
     [SerializeField] public TMP_Text pickUpText;
     [SerializeField] public Player newPlayer;

    [SerializeField] public int playerPickUpCount = 0;
    [SerializeField] public int playerPickUpTotal;

     public AudioClip starPickUpClip;

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

     private IEnumerator StarPickupSound()
    {
        Debug.Log("I Got Here.");
        //Wait for 5 sec.
        
      source.PlayOneShot(starPickUpClip, 1f);

      playerPickUpCount = newPlayer.setPickUpCount();
      pickUpText.text = (playerPickUpCount.ToString() + " / " + playerPickUpTotal.ToString());

       yield return new WaitForSeconds(0.5f);
        
    }

     private IEnumerator StarFunction()
    {
        Debug.Log("I Got Here.");
        //Wait for 5 sec.
        
        //Turn My game object that is set to false(off) to True(on).
       pickUpText.enabled = true;
       pickUpText.text = "You have found a comet shard.";

       yield return new WaitForSeconds(1);
        
       pickUpText.enabled = false;
       this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) 
    {
        
        StartCoroutine(StarPickupSound());

        StartCoroutine(StarFunction());
        
    }
}
