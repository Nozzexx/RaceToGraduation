using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour
{

     [SerializeField] public TMP_Text pickUpText;
     [SerializeField] public Player newPlayer;
     [SerializeField] public int playerPickUpCount = 0;
    [SerializeField] public int playerPickUpTotal;

    public AudioClip starPickUpClip;

    public AudioSource source;

    // Start is called before the first frame update

    void Start()
    {
        pickUpText = GameObject.Find("PickupText").GetComponent<TMP_Text>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pickUpText == null)
        {
            pickUpText = GameObject.Find("PickupText").GetComponent<TMP_Text>();
            Debug.Log("Text Found");
        }

        if(newPlayer == null)
        {   newPlayer = GameObject.Find("Player").GetComponent<Player>();
            playerPickUpTotal = GameObject.Find("Player").GetComponent<Player>().pickUpTotal;
        }

    }

     private IEnumerator pickupSound()
    {
        Debug.Log("I Got Here.");
        //Wait for 5 sec.
        
      source.PlayOneShot(starPickUpClip, 1f);

      this.gameObject.GetComponent<SphereCollider>().enabled = false;

      playerPickUpCount = newPlayer.setPickUpCount();
      pickUpText.text = (playerPickUpCount.ToString() + " / " + playerPickUpTotal.ToString());

       yield return new WaitForSeconds(0.5f);
        
    }

    private IEnumerator StarFunction()
    {
        Debug.Log("I Got Here.");
        //Wait for 5 sec.

       yield return new WaitForSeconds(1);
        
       this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) 
    {
        StartCoroutine(pickupSound());
        StartCoroutine(StarFunction());

    }
}
