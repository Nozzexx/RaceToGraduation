using System.Collections;
using System.Collections.Generic;
using SUPERCharacter;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
     public int pickUpCount = 0;
     public int pickUpTotal = 10;
     public bool diplomaFound = false;
     private bool displayEnd = false;
     public SurvivalStats stats;
     [SerializeField] public TMP_Text displayText;

     public GameObject UIOFF;
     public GameObject Exit;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(stats.Health.ToString());
        Exit = GameObject.FindGameObjectWithTag("Exit").transform.GetChild(1).gameObject;

      
            UIOFF = GameObject.FindGameObjectWithTag("MainCamera").transform.GetChild(0).gameObject.transform.GetChild(3).gameObject;

            UIOFF.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (pickUpCount == pickUpTotal && displayEnd == false)
        {
            StartCoroutine(StartFunction());
            displayEnd = true;
        }
         if(displayText == null)
        {
            displayText = GameObject.Find("Text (TMP)").GetComponent<TMP_Text>();
            displayText.enabled = false;
            Debug.Log("Text Found");
        }
        if(stats == null)
        {
            stats = gameObject.GetComponent<SurvivalStats>();
        }

        if(pickUpCount == pickUpTotal)
        {
            Exit.SetActive(true);
        }

        if(UIOFF.activeInHierarchy)
        {
            UIOFF.SetActive(false);
        }
        
    }

    private IEnumerator StartFunction()
    {
        Debug.Log("I Got Here.");
        //Wait for 5 sec.

        //Turn My game object that is set to false(off) to True(on).
       displayText.enabled = true;
       displayText.text = "The pieces have been collected. Claim your Diploma to escape...";

       yield return new WaitForSeconds(10);

       displayText.enabled = false;

    }

    private IEnumerator DeadFunction()
    {
        Debug.Log("I Got Here.");
        //Wait for 5 sec.
        
        //Turn My game object that is set to false(off) to True(on).
       displayText.enabled = true;
       displayText.text = "TEMOC has claimed your soul... Try again.";

       yield return new WaitForSeconds(3);
        
       displayText.enabled = false;

       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

     public void TakeDamage(int damage)
    {
        stats.Health -= damage;

        Debug.Log(stats.Health);

        if (stats.Health <= 0) 
        {
            
            Debug.Log("YOU DIED.");
            StartCoroutine(DeadFunction());
            
        }
    }

    public int setPickUpCount()
    {
        pickUpCount++;
        return pickUpCount;
    }

    public int getPickUpCount()
    {
        return pickUpCount;
    }

    public int getPickUpTotal()
    {
        return pickUpTotal;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy")
        {
            TakeDamage(250);
            //Destroy(other.gameObject);
        }
        else
        {
            //Do NOTHING
        }
    }
}
