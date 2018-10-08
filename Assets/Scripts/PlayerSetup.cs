using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]

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
        GetComponent<Player>().Setup();
        
        //RegisterPlayer(); 
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        string _netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.RegisterPlayer(_netId, _player);
    }

    //Managed by GameManager.cs
    /*void RegisterPlayer()
    {
        string _Id = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = _Id;
    }
    */

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
        GameManager.UnregisterPlayer(transform.name);
    }
}