using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;
    private GameObject enemy;
    private Vector3 offset;
    // Use this for initialization

    Vector3 vectorObjetivo;
    void Start () {

     
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        StartCoroutine("goToVector");


    }
	
	// Update is called once per frame
	void Update () {
 

        Vector3 distance = player.transform.position - enemy.transform.position;
        
        float proximity = Mathf.Abs(distance.x);

        float centroCamara = player.transform.position.x + (proximity / 2);

        float comtesalga = proximity+10;
        //proximity * 0.2f;

        if (proximity < 4)
        {
            comtesalga = 0;
            proximity = -10;

        }
        else if (proximity < 10)
        {
            proximity = proximity * -2.5f;
        }
        else if (proximity > 10 && proximity < 20)
        {
            proximity = proximity * -2f;
 
        }
        else if (proximity > 20)
        { 
            proximity = proximity * -3f;
        }
        
        float y = (comtesalga / 10) - 1;

        vectorObjetivo = new Vector3(centroCamara,y, proximity);
    }

    IEnumerator goToVector()
    {

        for (; ; )
        {
            if(vectorObjetivo != null)
            {

                float x = transform.position.x;

                //float obj = vectorObjetivo.z / Mathf.Abs(vectorObjetivo.y);
                // && (Mathf.Abs((transform.position.x - vectorObjetivo.x)) > 0.5)
                if (transform.position.x != vectorObjetivo.x)
                {
                    x = transform.position.x < vectorObjetivo.x ? transform.position.x + 0.01f : transform.position.x - 0.01f;
                }
                float y = transform.position.y;

                // && (Mathf.Abs((transform.position.y - vectorObjetivo.y)) > 1)
                if (transform.position.y != vectorObjetivo.y)
                {
                    y = transform.position.y < vectorObjetivo.y ? transform.position.y + 0.01f  : transform.position.y  - 0.01f;
                }
                float z = transform.position.z;

                // && (Mathf.Abs((transform.position.z - vectorObjetivo.z)) > 1)
                if (transform.position.z != vectorObjetivo.z)
                    //&& transform.position.z + 0.1f < vectorObjetivo.z && transform.position.z - 0.1f > vectorObjetivo.z)
                { 
                    z = transform.position.z < vectorObjetivo.z ? transform.position.z + 0.01f : transform.position.z - 0.01f;
                }

                Vector3 vectorTemporal = new Vector3(x, y, z);

                transform.position = vectorTemporal;
            }

          //este yield indica  cada cuando se va a llamar a la corrutina, en este caso cada 0.1 segundos.
          yield return new WaitForSeconds(.01f);
        }

    }
}
