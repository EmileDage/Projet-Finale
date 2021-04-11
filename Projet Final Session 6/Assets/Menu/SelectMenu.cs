using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private Selectable[] defaultButtons;//selectable est une clase commune de tout les objects UI

    void Start()
    {
        Invoke("PanelToggle", 0.01f);      
    }

    public void PanelToggle()
    {
        PanelToggle(0);
    }

    public void PanelToggle(int position)
    {
        Input.ResetInputAxes();//pendant 1 frame desactive si on a appuyer sur un boutton

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(position == i);

            if (position == i)
            {
                defaultButtons[i].Select();
            }
        }

    }

    public void SavePrefs()
    {
        PlayerPrefs.Save();
    }
}
