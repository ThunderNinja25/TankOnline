using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using UnityEngine;
using TMPro;
using Unity.Services.Core;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameInput;
    [SerializeField] private GameObject multiplayerMenu;
    [SerializeField] private TextMeshProUGUI scoreboardText;
    [SerializeField] private TMP_InputField joinCodeInput;

    private async Task ConnectToHost()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(5);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
        var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        joinCodeInput.text = joinCode.ToString();
        joinCodeInput.interactable = false;
        NetworkManager.Singleton.StartHost();
    }

    public async Task StartClientWithRelay(string joinCode)
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCode);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
        //return !string.IsNullOrEmpty(joinCode) && NetworkManager.Singleton.StartClient();
    }

    public void HostButtonClick()
    {
        ConnectToHost();
        multiplayerMenu.SetActive(false);
    }

    public void ClientButtonClick()
    {
        
        multiplayerMenu.SetActive(false);
    }

    public string GetLocalNickname()
    {
        return nicknameInput.text;
    }

    public void SetScoreboardText(string scoreboard)
    {
        scoreboardText.text = scoreboard;
    }

    public void ServerButtonClick()
    {
        NetworkManager.Singleton.StartServer();
        multiplayerMenu.SetActive(false);
    }
}
