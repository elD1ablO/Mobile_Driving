using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Car : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float speedGain = 0.2f;
    [SerializeField] float turnSpeed = 200f;

    int steerValue;
    void Update()
    {
        speed += speedGain * Time.deltaTime;

        transform.Rotate(0, steerValue * turnSpeed * Time.deltaTime, 0);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            SceneManager.LoadScene(0); 
#endif
            
        }
    }
    public void Steer(int value)
    {
        steerValue = value;
    }
}
