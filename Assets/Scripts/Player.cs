using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SerializeField]
    private int maxHealth = 100;
    //Sync variable in order to send this to the other clients
    [SyncVar]
    private int currentHealth;

    private void Awake()
    {
        SetDefaults();
    }
    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage (int _amount)
    {
        currentHealth -= _amount;
        Debug.Log(transform.name + "HP = " + currentHealth);
    }
}
