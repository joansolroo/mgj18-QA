using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    new Camera camera;
    private Vector3 targetPosition;

    void Start()
    {
        camera = GetComponent<Camera>();
        Cursor.visible = false;
    }
    void Update()
    {
        Vector3 rotationDelta;
        Vector3 currentRotation = transform.localEulerAngles;
        {
            rotationDelta = Vector3.zero;
            if (axes == RotationAxes.MouseXAndY)
            {
                rotationDelta.x = Input.GetAxis("Mouse X") * sensitivityX;
                //rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

                rotationDelta.y = Input.GetAxis("Mouse Y") * sensitivityY;
                //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            }


            float rotationX = ((currentRotation.y + 180) % 360 - 180) + rotationDelta.x;
            float rotationY = ((currentRotation.x + 180) % 360 - 180) - rotationDelta.y;

            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);
        }
        Ray ray = camera.ScreenPointToRay(camera.ViewportToScreenPoint(Vector3.one * 0.5f));
    }

}