using UnityEngine;
using System.Collections;

public class AsteroidsSpawner : MonoBehaviour
{
    // Tiempo entre intervalos de generación de asteroides
    [SerializeField] float interval; 
    
    // Tiempo de espera antes de comezar a generar asteroides
    [SerializeField] float delay; 

    // Prefab de la nave enemiga
    [SerializeField] GameObject AsteroidSmall; 

    // Coordenadas mínima y máxima en el eje X
    const float MIN_X = -4.5f; 
    const float MAX_X = 4.5f; 

    void Start()
    {
        StartCoroutine("AsteroidSpawn");
    }

    IEnumerator AsteroidSpawn()
    {
        // Retraso antes de empezar a generar asteroides
        yield return new WaitForSeconds(delay);

        // Generación infinita de asteroides
        while(true)
        {
            // Generar una posición aleatoria en el eje X dentro del rango establecido
            Vector3 position = new Vector3(Random.Range(MIN_X, MAX_X), transform.position.y, 0);
       
        Debug.Log("Xerando asteroide en: " + position);

            // Instanciar un nuevo asteroide en la posición aleatoria
            Instantiate(AsteroidSmall, position, Quaternion.identity);

            // Esperar antes de generar el siguiente asteroide
            yield return new WaitForSeconds(interval);
        }
    }
}