using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Loading : MonoBehaviour
{
    private AsyncOperation async;//pourcentage de scene chargée

    
    [SerializeField] private Image progressbar;
    [SerializeField] private Text txtPourcent;

    [SerializeField] private bool waitForUserInput = false;//attendre le joueur appuie sur une touche
    private bool ready = false;//toutes les conditions doivent etre remplis avant de passer à la scene suivante

    [SerializeField] private float delay = 1;//delay pour passer à la scene suivante

    [SerializeField] private int sceneToLoad = -1;//si = -1 alors passe a la scene suivante sinon charge la scene correspondante


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;//reset le time scale à default
        Input.ResetInputAxes();//vas reinitialiser les entrées durant 1 frame
        //jette les trucs inutiles sur la ram
        System.GC.Collect();//garbage collector vide la ram des trucs inutiles
        //chargement de scene incremental
        Scene currentscene = SceneManager.GetActiveScene();//memorise la scene actuelle

        if (sceneToLoad < 0)
        {//si la valeur negative
            async = SceneManager.LoadSceneAsync(currentscene.buildIndex + 1);//charge la scene suivante
        }
        else//si la avleur positive
        {
            async = SceneManager.LoadSceneAsync(sceneToLoad);
        }
        async.allowSceneActivation = false;//atttendree avant de passer a la scene suivante
        //il blocque a 90 donc on le turn true dans update

        if (waitForUserInput == false)
        {//invoke appel une fonction apres un delais
            Invoke("Activate", delay); //acrive la rpochaine scene apres un delais
        }
    }

    public void Activate()//appel cette fonction pour passer a la  scene suivante
    {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForUserInput && Input.anyKey)
        {
            ready = true;

        }
        if (progressbar)
        {
            progressbar.fillAmount = async.progress + 0.1f;//fill la barre verte avec un début de 10 pour compenser 
        }
        if (txtPourcent)
        {
            txtPourcent.text = ((async.progress + 0.1f) * 100).ToString("F2") + " %";//F2 = 2 deciamls ex 88.88 %
        }
        //massurer charger tout avant de changer scene et tout le slogos afficher
        if (async.progress > 0.89f && SplashScreen.isFinished && ready == true)
        {
            async.allowSceneActivation = true;
        }
    }
}
