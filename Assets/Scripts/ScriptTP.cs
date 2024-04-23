using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

// Script appliqué aux blocs aux fond de la piscine permettant de d'afficher sa texture et teleporter le joueur sur le plongeoir après collision

public class ScriptTP : NetworkBehaviour
{
    // Référence au composant MeshRenderer
    private MeshRenderer meshRenderer;
    private Component etat;


    // Start is called before the first frame update
    void Start()
    {
        // Récupérer le composant MeshRenderer au démarrage
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
            // Teleporte le joueur ayant effectué la collision
            other.transform.position = new Vector3(0, 400, 0);

            // Activer la texture pour tous les joueurs
            RpcActivateTextureServerRpc();
        }
    }

    // Méthode pour activer la texture sur tous les clients via un RPC
    [ServerRpc(RequireOwnership = false)]
    void RpcActivateTextureServerRpc()
    {
        // Appeler une méthode sur tous les clients pour activer la texture
        RpcActivateTextureClientRpc();
    }

    
    // Méthode RPC client pour activer la texture sur tous les clients
    [ClientRpc]
    void RpcActivateTextureClientRpc()
    {
        // Activer le composant MeshRenderer
        meshRenderer.enabled = true;
    }
}