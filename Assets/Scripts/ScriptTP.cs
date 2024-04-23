using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class ScriptTP : NetworkBehaviour
{
    // R�f�rence au composant MeshRenderer
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // R�cup�rer le composant MeshRenderer au d�marrage
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // V�rifier si le script est ex�cut� sur le serveur
        // Changer la position de l'objet pour tous les joueurs
        other.transform.position = new Vector3(0, 400, 0);

        // Activer la texture pour tous les joueurs
        RpcActivateTextureServerRpc();
    }

    // M�thode pour activer la texture sur tous les clients via un RPC
    [ServerRpc]
    void RpcActivateTextureServerRpc()
    {
        // Appeler une m�thode sur tous les clients pour activer la texture
        RpcActivateTextureClientRpc();
    }

    // M�thode RPC client pour activer la texture sur tous les clients
    [ClientRpc]
    void RpcActivateTextureClientRpc()
    {
        // Activer le composant MeshRenderer
        meshRenderer.enabled = true;
    }
}