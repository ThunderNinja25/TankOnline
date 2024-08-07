using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class DespawnAfterTime : NetworkBehaviour
{
    public float timeToDespawn;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsServer)
        {
            Invoke("DelayDespawn", timeToDespawn);
        }
    }

    private void DelayDespawn()
    {
        NetworkObject.Despawn();
    }
}
