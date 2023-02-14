using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SphereControl : MonoBehaviour
{
    bool countingDown = false;
    float timeCount = 0;
    float speed = 20f;
    [SerializeField]
    public int score;
    [SerializeField]
    public TMP_Text scoreText,
        pauseText;

    // Start is called before the first frame update
    void Start()
    {
        timeCount = 0;
        countingDown = false;
        pauseText.gameObject.SetActive(false);
        score = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.W))
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
        if (Input.GetKey(KeyCode.S))
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
        if (Input.GetKey(KeyCode.A))
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        if (Input.GetKey(KeyCode.D))
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        */

        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        


        //print(xMove + " " + zMove);

        //Vector3 movement = new Vector3(xMove, 0, zMove) * speed;
        //transform.position += movement;

        Vector3 movement = new Vector3(xMove, 0, zMove).normalized * speed * Time.deltaTime;
        print(movement.magnitude);
        transform.position = new Vector3(Mathf.Clamp((movement.x + transform.position.x), -9, 9), 
                                        0, 
                                        Mathf.Clamp((movement.z + transform.position.z), -3, 3));

        if (Input.GetKeyDown(KeyCode.Space)){
            if (Time.timeScale == 0){
                pauseText.gameObject.SetActive(false);
                Time.timeScale = 1;
                return;
            }
            pauseText.gameObject.SetActive(true); 
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.Q))
            countingDown = true;
        

        if (countingDown){
            timeCount += Time.deltaTime;
            if (timeCount > 3){
                timeCount = 0;
                // to end in editor
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        float newX = Random.Range(-9, 9);
        float newZ = Random.Range(-3, 3);
        other.transform.position = new Vector3(newX, 0, newZ);
        score++;
        scoreText.text = string.Format("Score : {0}", score);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Still in the trigger");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited the trigger");
    }
}
