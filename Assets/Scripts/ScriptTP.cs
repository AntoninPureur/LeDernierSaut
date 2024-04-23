using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class ScriptTP : NetworkBehaviour
{
    // R�f�rence au composant MeshRenderer
    private MeshRenderer meshRenderer;
    public int etat;


    // Start is called before the first frame update
    void Start()
    {
        // R�cup�rer le composant MeshRenderer au d�marrage
        meshRenderer = GetComponent<MeshRenderer>();
        etat = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (etat == 0)
        {
            // V�rifier si le script est ex�cut� sur le serveur
            // Changer la position de l'objet pour tous les joueurs
            other.transform.position = new Vector3(0, 400, 0);

            // Activer la texture pour tous les joueurs
            RpcActivateTextureServerRpc();
        }

        if (etat == 1)
        {

            WinScreenServerRpc();
        }

        etat++;
    }

    // M�thode pour activer la texture sur tous les clients via un RPC
    [ServerRpc(RequireOwnership = false)]
    void RpcActivateTextureServerRpc()
    {
        // Appeler une m�thode sur tous les clients pour activer la texture
        RpcActivateTextureClientRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    void WinScreenServerRpc()
    {
        // Appeler une m�thode sur tous les clients pour activer la texture
        WinScreenClientRpc();
    }


    // M�thode RPC client pour activer la texture sur tous les clients
    [ClientRpc]
    void RpcActivateTextureClientRpc()
    {
        // Activer le composant MeshRenderer
        meshRenderer.enabled = true;
    }

    [ClientRpc]
    void WinScreenClientRpc()
    {

    }

}