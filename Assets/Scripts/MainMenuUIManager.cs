using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    private List<GameObject> uiPanels = new List<GameObject>();
    private int activePanelIndex = 0;

    [SerializeField] private TMP_InputField ipInputField;
    [SerializeField] private TextMeshProUGUI amountOfPlayersConnectedText;

    public Server server;
    public Client client;

    private void Awake()
    {
        foreach (Transform transform in transform)
        {
            uiPanels.Add(transform.gameObject);
            ChangePanel(activePanelIndex);
        }
    }

    public void ChangePanel(int panelIndex)
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

    public void HostOnlineButton()
    {
        server.Init(8007);
        client.Init("127.0.0.1",8007);
    }

    public void OnOnlineConnect()
    {
        client.Init(ipInputField.text, 8007);
    }

    public void Disconnect()
    {
        server.Shutdown();
        client.Shutdown();
    }
}
