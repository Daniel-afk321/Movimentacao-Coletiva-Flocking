using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlockManager : MonoBehaviour
{
    //Pegando o prefab do peixe e definindo a quantidade e dist�ncia entre eles
    public GameObject fishPrefab;
    public int numFish = 20;
    public GameObject[] allFish;
    public Vector3 swinLimits = new Vector3(5, 5, 5);
    public Vector3 goalPos;


    //Configura��es para escolher a velocidade m�nima e m�xima entre os peixes
    [Header("Configura��es do Cardume")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;
    void Start()
    {
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {
            //Definindo a dire��o em que os prefab ir�o se movimentar
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x,
            swinLimits.x),
            Random.Range(-swinLimits.y,
            swinLimits.y),
            Random.Range(-swinLimits.z,
            swinLimits.z));
            //Inst�nciando os outros peixes na cena
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
        }
        goalPos = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        //Esse codigo define a posi��o do peixe para um objeto com base em sua posi��o atual e o peixe pode receber um deslocamento aleat�rio caso o IF verifique que deu um numero entre 0 e 100 que � menor que 10
        goalPos = this.transform.position;
        if (Random.Range(0, 100) < 10)
            goalPos = this.transform.position + new Vector3(Random.Range(-swinLimits.x,
            swinLimits.x),
            Random.Range(-swinLimits.y,
            swinLimits.y),
            Random.Range(-swinLimits.z,
            swinLimits.z));
    }

}

