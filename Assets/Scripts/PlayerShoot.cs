
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    public PlayerWeapon weapon;

    private const string PLAYER_TAG = "Player";

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced");
            this.enabled = false;
        }
            
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           Shoot();
        }  
    }

    //Local Method [Client]
    [Client]
    void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            // Hit something
            //Debug.Log("We hit " + _hit.collider.name);
            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, weapon.damage);
            }
        }
    }
    //Called only on the server
    [Command]
    void CmdPlayerShot (string _playerId, int _damage)
    {
        Debug.Log(_playerId + " has been shot");
        Player _player = GameManager.GetPlayer(_playerId);
        _player.TakeDamage(_damage);
        
    }
}
