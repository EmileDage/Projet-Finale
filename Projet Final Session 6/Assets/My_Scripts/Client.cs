using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Client : MonoBehaviour, IInteractible
{
    public enum ClientMood {
        lookingSeat,
        ordering,
        waiting,
        angry,
        eating,
        paying
    }


    [SerializeField] private float patienceMax;
    private float patience_left;
    [SerializeField] private float eatingTime;
    private float eating_left;
    [SerializeField] private ClientMood mood = ClientMood.lookingSeat;
    [SerializeField] private Image patienceBar;
    [SerializeField] private Canvas Canvas;
    [SerializeField] private Image food_bubble;
    public NavMeshAgent agent;
    int food;
    GameManager GM;
    Hasard rnd = Hasard.Get_Instance();
    private Repas MyFood;


    // Start is called before the first frame update
    void Start()
    {

        GM = GameManager.Instance;
        Canvas.gameObject.SetActive(false);
        patienceMax =rnd.Next(15,30);
        patience_left = patienceMax;
        eatingTime = rnd.Next(20, 50);
        eating_left = eatingTime;
        mood = ClientMood.lookingSeat;
        food = rnd.Next(1, 5);
        SetDestination(GM.GetChair());
        MyFood = null;
    }

    // Update is called once per frame
    void Update()
    {
        //le problème ici c'est que tu va caller la fonction chaque frame
        if(ClientMood.waiting == mood) {
            Waiting();
        }else if (ClientMood.eating == mood)
        {
            Eating();
        }
    }

    private int Tipping() {
        if (mood == ClientMood.angry)
        {
            return 0;
        }
        else {
            return (int)(patienceMax/ patience_left)*10;
        }
    }

    public void Ordering() {
        if (mood == ClientMood.ordering) {
            MyFood = GM.ChooseWhatEat();
            Debug.Log("Le client veut manger " + MyFood);
            food_bubble.gameObject.GetComponent<Image>().sprite = GM.Food();
            Debug.Log("Le client montre un food bubble");
            ChangeMood(ClientMood.waiting);
        }
    }

    private void Waiting() {
        if (!Canvas.gameObject.activeSelf)
        {
            Canvas.gameObject.SetActive(true); ;
        }

        patience_left -= Time.deltaTime;
        patienceBar.fillAmount = patience_left / patienceMax;
        if (patience_left <= 0)
        {
            mood = ClientMood.angry;
        }
    }

    public void Eating() {
        if (!Canvas.gameObject.activeSelf)
        {
            Canvas.gameObject.SetActive(true); ;
        }
        eating_left -= Time.deltaTime;
        patienceBar.fillAmount = eating_left / eatingTime;
        if (eating_left <= 0)
        {
            Debug.Log("Client a finit de manger");
            GM.payForFuud(Tipping());
            //SetDestination(Exit);

        }
    }

    public void SetDestination(Vector3 destination) {
        agent.SetDestination(destination);
    }

    public void ChangeMood(ClientMood muud) {
        mood = muud;
        Debug.Log("Changing client mood "+ mood);
    }

    public void Interact(Player player)
    {
         if (player.HeldItem != null && mood == ClientMood.waiting) {
            if (MyFood == player.HeldItem) {
                ChangeMood(ClientMood.eating);
                Debug.Log("Client be eating dat shit");
            }
            else {
                Debug.Log("Wrong food item");
            }
         }
        else
        {
            Debug.Log("Player have nothing in his hands or client isnt waiting for food");
        }
    }
}
