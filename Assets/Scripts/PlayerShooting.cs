using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerShooting : NetworkBehaviour
{
    [SerializeField] private float shootingStrength;
    [SerializeField] private NetworkBullet bullet;
    [SerializeField] private Transform shootingPoint;

    void Update()
    {
        if (IsOwner)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootBulletRpc();
            }
        } 
    }
    [Rpc(SendTo.Server)]
    private void ShootBulletRpc(RpcParams param = default)
    {
        NetworkBullet bulletClone = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
        bulletClone.NetworkObject.Spawn();
        bulletClone.ShootBullet(shootingStrength, param.Receive.SenderClientId);

        //bulletClone.GetComponent<Rigidbody>().AddForce(bulletClone.transform.forward * shootingStrength, ForceMode.VelocityChange);
        //Destroy(bulletClone, 3f);
    }
}
