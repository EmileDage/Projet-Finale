using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chairs : MonoBehaviour
{
    public Transform seat;
    private bool seated = false;
    private Client karen;
    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
        GM.AddChair(this.gameObject.transform.position);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Client" && !seated)
        {
            Debug.Log("Client is touching his seat");

            karen = other.GetComponent<Client>();
            if (karen != null)
            {
                karen.SetDestination(other.gameObject.transform.position);
                Debug.Log("Detected the script client");
                GM.RemoveChair(this.gameObject.transform.position);
                other.transform.position = seat.position;
                other.transform.rotation = seat.rotation;
                seated = true;
                karen.ChangeMood(Client.ClientMood.ordering);
                karen.Ordering();
            }
            else {
                Debug.Log("We got a problem Houston we didnt detect the script client");
               
            }
           

        }
    }

    public void Leaving() {

        seated = false;
        GM.AddChair(this.gameObject.transform.position);
    }
}
