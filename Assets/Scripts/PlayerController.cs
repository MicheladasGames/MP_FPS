using UnityEngine;
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 1f;

    [SerializeField]
    private float thrusterForce = 1000f;

    /*
     * Esta parte está obsoleta, la he omitido
    [SerializeField]
    private ConfigurableJoint confJoint;
    */
    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();        
    }

    void Update()
    {
        //Calculate movement velocity as a 3D vector
        float xMov = Input.GetAxisRaw("Horizontal"); //-1 to 1
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;
        motor.Move(velocity);

        //Calculate rotation - Vertical is only for camera (Model would rotate, that's weird)
        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f,_yRot, 0f) * lookSensitivity;

        motor.Rotate(_rotation);

        //Calculate rotation - Vertical is only for camera (Model would rotate, that's weird)
        float _xRot = Input.GetAxisRaw("Mouse Y");
        //Old Rotational calculation for camera
        //Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;
        float _cameraRotation = _xRot * lookSensitivity;

        motor.RotateCamera(-_cameraRotation);

        //Setup Thruster
        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
        }

        //apply Thruster force
        motor.ApplyThruster(_thrusterForce);
    }


}
