using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class ChatSystem : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI serverMessage;
    [SerializeField] private TMP_InputField inputField;

    public void SendToChat()
    {
        if (IsServer && !IsHost)
        {
            ShowServerMessageRpc(inputField.text, NetworkManager.ConnectedClients[OwnerClientId].PlayerObject.GetComponent<PlayerInput>().playerNickname.Value.ToString());
        }
        else
        {
            RequestMessageSentRpc(inputField.text);
        }
    }
    [Rpc(SendTo.Server)]
    public void RequestMessageSentRpc(string msg, RpcParams param = default)
    {
        if (msg.Contains("badword"))
        {
            msg = "I am a sore loser";
        }

        ShowServerMessageRpc(msg, NetworkManager.ConnectedClients[param.Receive.SenderClientId].PlayerObject.GetComponent<PlayerInput>().playerNickname.Value.ToString());
    }

    [Rpc(SendTo.Everyone)]
    public void ShowServerMessageRpc(string msg, string senderName)
    {
        serverMessage.text += "\n" + senderName + ":" + msg;
    }
}
