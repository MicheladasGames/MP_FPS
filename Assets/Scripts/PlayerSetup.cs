using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] disabledComponents;
    [SerializeField]
    string remoteLayerName = "RemotePlayer";
    Camera sceneCamera;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        } else
        {
            sceneCamera = Camera.main;
            if (sceneCamera!=null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
            
        }

        
        RegisterPlayer();
    }

    void RegisterPlayer()
    {
        string _Id = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = _Id;
    }

    private void AssignRemoteLayer ()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    private void DisableComponents()
    {
        for (int i = 0; i < disabledComponents.Length; i++)
        {
            disabledComponents[i].enabled = false;
        }
    }
    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}