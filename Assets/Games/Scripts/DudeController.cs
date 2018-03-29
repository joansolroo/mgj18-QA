using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DudeController : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 5;
    [SerializeField] float movementSpeed = 5;

    [SerializeField] AudioClip walk;
    AudioSource audio;
    [SerializeField] Transform visualization;

    Rigidbody rb;
    public static bool used = false;
    [SerializeField] float TimeUntilHelpIsShown = 15;
    [SerializeField] TriggerToggle help;
    bool helpShown = false;
    // Use this for initialization

    float startTime = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        startTime = Time.time;
    }


    [SerializeField] Vector2 speed = Vector2.zero;
    bool step = false;
    // Update is called once per frame
    void Update()
    {
        float dr = Input.GetAxis("Horizontal");
        float dx = Input.GetAxis("Vertical");
        if (dr != 0 || dx != 0)
        {
            if (!used)
            {
                help.Disable();
                used = true;
            }
            speed.x = dx; speed.y = dr;
            // Debug.Log("dr:" + dx + " dy:" + dx);
            Vector3 rotation = gameObject.transform.localEulerAngles;
            rotation.y += dr * rotationSpeed * Time.deltaTime;
            float sin = Mathf.Sin(Time.time * 10);
            if (!step && Mathf.Abs(dx) > 0 && Mathf.Abs(sin) >= 0.98f)
            {
                audio.clip = walk;
                audio.Play();
                step = true;
            }
            else if (Mathf.Abs(sin) < 0.3)
            {
                step = false;
                audio.Stop();
            }
            //rotation.z = Mathf.Lerp(0, sin * 35 / 2, Mathf.Abs(dx));
            visualization.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, sin * 35 / 2, Mathf.Abs(dx)));
            rb.velocity = transform.forward * -dx * movementSpeed;
            //gameObject.transform.position = gameObject.transform.TransformPoint();
            gameObject.transform.localEulerAngles = rotation;
        }
        if((!used || !CameraRotation.used)&& (Time.time - startTime)>TimeUntilHelpIsShown){
            helpShown = true;
            help.gameObject.active = true;
            help.Enable();
        }
    }
}
