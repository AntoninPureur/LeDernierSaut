using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class ScriptTP : NetworkBehaviour
{
    // Référence au composant MeshRenderer
    private MeshRenderer meshRenderer;
    public bool block;
    public ulong id = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Récupérer le composant MeshRenderer au démarrage
        meshRenderer = GetComponent<MeshRenderer>();
        block = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (!block)
        {
            // Vérifier si le script est exécuté sur le serveur
            // Changer la position de l'objet pour tous les joueurs
            other.transform.position = new Vector3(0, 400, 0);

            // Activer la texture pour tous les joueurs
            ActivateTextureServerRpc();
            UpdateBlockServerRpc();
        }

        if (block)
        {
            id = other.GetComponent<NetworkPlayer>().playerId;
            Debug.LogError(id);
        }

        Debug.LogError(block);
    }

    // Méthode pour activer la texture sur tous les clients via un RPC
    [ServerRpc(RequireOwnership = false)]
    void ActivateTextureServerRpc()
    {
        // Appeler une méthode sur tous les clients pour activer la texture
        ActivateTextureClientRpc();
    }
    [ServerRpc(RequireOwnership = false)]
    void UpdateBlockServerRpc()
    {
        // Mettre à jour le booléen "block" pour tous les joueurs
        UpdateBlockClientRpc();
    }

    // Méthode RPC client pour activer la texture sur tous les clients
    [ClientRpc]
    void ActivateTextureClientRpc()
    {
        // Activer le composant MeshRenderer
        meshRenderer.enabled = true;
    }
    [ClientRpc]
    void UpdateBlockClientRpc()
    {
        // Mettre à jour le booléen "block" pour tous les joueurs
        block = true;
    }

}