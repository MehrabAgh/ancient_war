using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using Unity.Netcode.Transports.UTP;

public class UISys_MainMenu : MonoBehaviour
{
    [SerializeField] private Button startHost, startServer, startClient;
    [SerializeField] private TMP_InputField field;
    private UnityTransport transport;
    [SerializeField] private getIP GetIP;
    void Start()
    {
        startHost.onClick.AddListener(()=>NetworkManager.Singleton.StartHost());
        startServer.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
        startClient.onClick.AddListener(() => NetworkManager.Singleton.StartClient());

        transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.ConnectionData.Address = GetIP.GetLocalIPAddress();

    }

    public void ChangeIP()
    {
        transport.ConnectionData.Address = field.text;
    }
}
 