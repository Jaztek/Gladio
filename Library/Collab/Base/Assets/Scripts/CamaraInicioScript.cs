using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraInicioScript : MonoBehaviour {

    private Vector3 vectorObjetivo;

    public Vector3 HABILIDADES = new Vector3(0, -2f, -5);

    public Vector3 INICIO = new Vector3(0, 0, -10);

    void Start () {
        StartCoroutine("goToVector");
        vectorObjetivo = INICIO;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void goToHabilidades()
    {
        vectorObjetivo = HABILIDADES;
    }

    public void goToInicio()
    {
        vectorObjetivo = INICIO;
    }

    private void moveCameraTo(Vector3 vector)
    {
        vectorObjetivo = vector;
    }


    IEnumerator goToVector()
    {

        for (; ; )
        {
            if (vectorObjetivo != null)
            {
                print(vectorObjetivo);
                float x = transform.position.x;

                //float obj = vectorObjetivo.z / Mathf.Abs(vectorObjetivo.y);
                // 
                if (transform.position.x != vectorObjetivo.x && (Mathf.Abs((transform.position.x - vectorObjetivo.x)) > 0.1))
                {
                    x = transform.position.x < vectorObjetivo.x ? transform.position.x + 0.1f : transform.position.x - 0.1f;
                }
                float y = transform.position.y;

                //
                if (transform.position.y != vectorObjetivo.y && (Mathf.Abs((transform.position.y - vectorObjetivo.y)) > 0.1))
                {
                    y = transform.position.y < vectorObjetivo.y ? transform.position.y + 0.05f : transform.position.y - 0.05f;
                }
                float z = transform.position.z;

                //
                if (transform.position.z != vectorObjetivo.z && (Mathf.Abs((transform.position.z - vectorObjetivo.z)) > 0.1))
                //&& transform.position.z + 0.1f < vectorObjetivo.z && transform.position.z - 0.1f > vectorObjetivo.z)
                {
                    z = transform.position.z < vectorObjetivo.z ? transform.position.z + 0.1f : transform.position.z - 0.1f;
                }

                Vector3 vectorTemporal = new Vector3(x, y, z);

                transform.position = vectorTemporal;
            }

            //este yield indica  cada cuando se va a llamar a la corrutina, en este caso cada 0.1 segundos.
            yield return new WaitForSeconds(.01f);
        }

    }
}
