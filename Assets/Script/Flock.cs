using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Flock : MonoBehaviour
{
    //Pegando o outro c�digo e uma vari�vel para a velocidade
    public FlockManager myManager;
    public float speed;
    bool turning = false;
    // Start is called before the first frame update
    void Start()
    {
        //Escolhendo a velocidade do peixe entre o m�nimo e o m�ximo, que est� no FlockManager
        speed = Random.Range(myManager.minSpeed,
        myManager.maxSpeed);
    }
    void Update()
    {
        //cria uma regi�o delimitada para os peixes 
        Bounds b = new Bounds(myManager.transform.position, myManager.swinLimits * 2);
        RaycastHit hit = new RaycastHit();
        Vector3 direction = myManager.transform.position - transform.position;
        //Esse codigo determina se o peixe precisa girar ou mudar de dire��o com base em sua posi��o
        if (!b.Contains(transform.position))
        {
            turning = true;
            direction = myManager.transform.position - transform.position;
        }
        else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit))
        {
            turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        else
            turning = false;
        //Esse c�digo controla a rota��o e a velocidade do Peixe
        if (turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(direction),
            myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 100) < 10)
                speed = Random.Range(myManager.minSpeed,
                myManager.maxSpeed);
            if (Random.Range(0, 100) < 20)
                ApplyRules();
        }
        //esse c�digo move o Peixe ao longo do eixo Z com uma velocidade determinada
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
    void ApplyRules()
    {
        //Esse codigo esta declarando algumas vari�veis 
        GameObject[] gos;
        gos = myManager.allFish;
        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                //Esse codigo � respons�vel por calcular se os peixes estao se mantendo pr�ximos uns dos outros e de agirem de forma coordenada, e de evitar a sobreposi��o dos peixes
                if (nDistance <= myManager.neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;
                    if (nDistance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }
        //Esse c�digo finaliza o c�lculo da posi��o m�dia, velocidade m�dia e dire��o do grupo de peixes
        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
            speed = gSpeed / groupSize;
            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction),
                myManager.rotationSpeed * Time.deltaTime);
        }
    }
}