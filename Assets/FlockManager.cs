using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlockManager : MonoBehaviour
{
    //Pegando o prefab do peixe e definindo a quantidade e distância entre eles
    public GameObject fishPrefab;
    public int numFish = 20;
    public GameObject[] allFish;
    public Vector3 swinLimits = new Vector3(5, 5, 5);
    public Vector3 goalPos;


    //Configurações para escolher a velocidade mínima e máxima entre os peixes
    [Header("Configurações do Cardume")]
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
            //Definindo a direção em que os prefab irão se movimentar
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x,
            swinLimits.x),
            Random.Range(-swinLimits.y,
            swinLimits.y),
            Random.Range(-swinLimits.z,
            swinLimits.z));
            //Instânciando os outros peixes na cena
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
        }
        goalPos = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        //Esse codigo define a posição do peixe para um objeto com base em sua posição atual e o peixe pode receber um deslocamento aleatório caso o IF verifique que deu um numero entre 0 e 100 que é menor que 10
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

