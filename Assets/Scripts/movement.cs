using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    public int mode;
    public float cameraDistance;

    GameObject camera;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Pellet")
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Ghost")
        {
            Destroy(gameObject);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    public float speed = 8;

    void Update()
    {
        // Get user input
        float z = Input.GetAxis("Vertical");
        //float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Mouse X");
        float z_keyboard = Input.GetAxis("Horizontal");

        transform.Rotate(0, x * speed * 10 * Time.deltaTime, 0);
        

        Vector3 move = transform.forward * z;
        Vector3 keyboard_mvmnt = transform.right * z_keyboard;
        gameObject.GetComponent<CharacterController>().Move(move * Time.deltaTime * speed);
        
        gameObject.GetComponent<CharacterController>().Move(keyboard_mvmnt * speed * Time.deltaTime);



        


        if (mode == 3)
        {
            camera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z);
            Vector3 camLocalEulerAngles = transform.localEulerAngles;
            camLocalEulerAngles.x = 45;
            camera.transform.localPosition -= transform.forward * 2;
            camera.transform.localEulerAngles = camLocalEulerAngles;
        } else if( mode == 1)
        {
            camera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.1f, gameObject.transform.position.z);
            camera.transform.rotation = transform.rotation;
            camera.transform.localPosition += transform.forward * 0.1f;
        }
        



        

        


    }

}
