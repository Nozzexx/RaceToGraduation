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
    // Start is called before the first frame update
    void Start()
    {
        pickUpText = GameObject.Find("PickupText").GetComponent<TMP_Text>();
        //newPlayer = GameObject.Find("Player");
        //playerPickUpCount = GameObject.Find("Player").GetComponent<Player>().pickUpCount;
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

        if(playerPickUpCount == playerPickUpTotal)
        {
            this.gameObject.SetActive(false); 
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        this.gameObject.SetActive(false);
        playerPickUpCount = newPlayer.setPickUpCount();
        pickUpText.text = (playerPickUpCount.ToString() + " / " + playerPickUpTotal.ToString());
    }
}
