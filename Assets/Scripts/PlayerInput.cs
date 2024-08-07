using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using TMPro;

public class PlayerInput : NetworkBehaviour
{
    [SerializeField] private TextMeshPro nicknameDisplay;
    public NetworkVariable<FixedString32Bytes> playerNickname = new NetworkVariable<FixedString32Bytes>("NO_NAME", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> killCount = new NetworkVariable<int>();

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsLocalPlayer)
        {
            GameManager.Singleton.OnLocalPlayerJoined(this);
        }
        else
        {
            GameManager.Singleton.OnPlayerJoined(this);
        }

        nicknameDisplay.text = playerNickname.Value.ToString();

        playerNickname.OnValueChanged += UpdateNickname;
    }

    private void UpdateNickname(FixedString32Bytes oldValue, FixedString32Bytes newValue)
    {
        if(IsServer)
        {
            //if against the rules
            //ban the player
        }

        nicknameDisplay.text = newValue.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
