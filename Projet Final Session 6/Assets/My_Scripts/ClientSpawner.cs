using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{

    [SerializeField] private float max_client_time;
    private float time_left;
    Hasard rnd = Hasard.Get_Instance();
    [SerializeField] GameObject client_prefab;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (time_left <= 0)
        {
            Spawn();
        }
        else {
            time_left -= Time.deltaTime;
        }
    }

    public void Spawn() {
        Instantiate(client_prefab);
        max_client_time = rnd.Next(40, 120);
        time_left = max_client_time;
    }

}
