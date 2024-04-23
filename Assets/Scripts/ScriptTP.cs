using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class ScriptTP : NetworkBehaviour
{
    // Référence au composant MeshRenderer
    private MeshRenderer meshRenderer;
    public int etat;


    // Start is called before the first frame update
    void Start()
    {
        // Récupérer le composant MeshRenderer au démarrage
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
            // Vérifier si le script est exécuté sur le serveur
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

    // Méthode pour activer la texture sur tous les clients via un RPC
    [ServerRpc(RequireOwnership = false)]
    void RpcActivateTextureServerRpc()
    {
        // Appeler une méthode sur tous les clients pour activer la texture
        RpcActivateTextureClientRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    void WinScreenServerRpc()
    {
        // Appeler une méthode sur tous les clients pour activer la texture
        WinScreenClientRpc();
    }


    // Méthode RPC client pour activer la texture sur tous les clients
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