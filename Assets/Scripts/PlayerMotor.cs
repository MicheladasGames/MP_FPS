using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float cameraRotLim = 85f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 thrusterforce = Vector3.zero;

    private Rigidbody rb;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        
	}
	
	// Update is called once per frame
	public void Move (Vector3 _velocity) {
        velocity = _velocity;
	}

    void FixedUpdate()
    {
        PerformMov();
        PerformRot();
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    void PerformRot()
    {
        if (rotation != Vector3.zero)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
            if (cam != null)
            {
                /* Old Rotational Code
                cam.transform.Rotate(cameraRotationX);
                */
                // Set rotation and clamp it
                currentCameraRotationX -= cameraRotationX;
                currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotLim, cameraRotLim);
                // Set rotation to the rotation of our camera
                cam.transform.localEulerAngles = new Vector3(-currentCameraRotationX, 0, 0);
            }
        }
    }

    void PerformMov()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        if (thrusterforce != Vector3.zero)
        {
            rb.AddForce(thrusterforce * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    // Get force vector for Thruster
    public void ApplyThruster (Vector3 _thusterForce)
    {
        thrusterforce = _thusterForce;
    }
}
