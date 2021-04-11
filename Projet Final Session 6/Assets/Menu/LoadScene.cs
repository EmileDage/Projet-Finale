using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private AsyncOperation async;

    public void BtnLoadScene(int i)
    {
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(i);
            async.allowSceneActivation = true;//false= charge dans le background
        }
    }
    public void BtnLoadScene(string s)
    {
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(s);
            async.allowSceneActivation = true;
        }
    }
}
