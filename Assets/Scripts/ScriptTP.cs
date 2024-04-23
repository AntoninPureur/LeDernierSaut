using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

// Script appliqu� aux blocs aux fond de la piscine permettant de d'afficher sa texture et teleporter le joueur sur le plongeoir apr�s collision

public class ScriptTP : NetworkBehaviour
{
    // R�f�rence au composant MeshRenderer
    private MeshRenderer meshRenderer;
    private Component etat;


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

        if (meshRenderer.enabled == true)
        {
            other.transform.position = new Vector3(63, 20, 500);
        }

        else
        {
            // Teleporte le joueur ayant effectu� la collision
            other.transform.position = new Vector3(0, 400, 0);

            // Activer la texture pour tous les joueurs
            RpcActivateTextureServerRpc();
        }
    }

    // M�thode pour activer la texture sur tous les clients via un RPC
    [ServerRpc(RequireOwnership = false)]
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