using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    public Canvas pause;
    public float power;
    public Text Text;
    private bool isEnd = false;
    public Material secondMaterial;
    

    private Rigidbody Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody.AddForce(power, 0, 0); //move left
        }

        if (Input.GetKey(KeyCode.D))
        {
            Rigidbody.AddForce(-power, 0, 0); //move right
        }

        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody.AddForce(0, 0, -power); //move forward
        }

        if (Input.GetKey(KeyCode.S))
        {
            Rigidbody.AddForce(0, 0, power); //move back
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause.gameObject.active)
            {
                resume();
            }
            else
            {
                stop();
            }
        }
    }

    public void resume()
    {
        if(isEnd)
            return;
        Time.timeScale = 1;
        pause.gameObject.active = false;
    }

    public void stop()
    {
        pause.gameObject.active = true;
        Time.timeScale = 0;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    private void lose()
    {
        stop();
        Text.text = "you lose!";
        isEnd = true;
        try
        {
            GetComponent<AudioSource>().Play();
        }
        catch (Exception e)
        {
        }
    }

    private void win()
    {
        stop();
        isEnd = true;
        Text.text = "you win!";
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name.Equals("level1"))
        {
            Time.timeScale = 1;
            Invoke("loadLevel2",1f);
            Debug.Log("invoke");
        }
    }

    void loadLevel2()
    {
        Debug.Log("loadLevel2");
        SceneManager.LoadScene("level2");
    }
    private void OnCollisionEnter(Collision other)
    {
        string enemy = null;
        if (tag.Equals("Red"))
        {
            enemy = "Blue";
        }
        if(tag.Equals("Blue"))
        {
            enemy = "Red";
        }

        if (other.gameObject.tag.Equals(enemy))
        {
            lose();
        }

        if (other.gameObject.tag.Equals("finish"))
        {
            win();            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<MeshRenderer>().material = secondMaterial;
        this.tag = "Blue";
    }
}