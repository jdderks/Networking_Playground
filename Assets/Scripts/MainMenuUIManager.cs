using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    private List<GameObject> uiPanels = new List<GameObject>();
    private int activePanelIndex = 0;


    private void Awake()
    {
        foreach (Transform transform in transform)
        {
            uiPanels.Add(transform.gameObject);
            ChangePanel(activePanelIndex);
        }
    }

    private void ChangePanel(int panelIndex)
    {
        for (int i = 0; i < uiPanels.Count; i++)
        {
            if (i != panelIndex)
            {
                uiPanels[i].SetActive(false);
            }
            else
            {
                activePanelIndex = i;
                uiPanels[i].SetActive(true);
            }
        }
    }
}
