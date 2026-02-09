using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    // Tiempo entre intervalos de generación de naves enemigas
    [SerializeField] float interval; 
    
    // Tiempo de espera antes de comenzar a generar naves enemigas
    [SerializeField] float delay; 

    // Prefab de la nave enemiga
    [SerializeField] GameObject enemy; 

    // Coordenadas mínima y máxima en el eje X
    const float MIN_X = -3.5f; 
    const float MAX_X = 3.5f; 

    void Start()
    {
        StartCoroutine("EnemySpawn");
    }

    IEnumerator EnemySpawn()
    {
        // Retraso antes de comenzar a generar naves enemigas
        yield return new WaitForSeconds(delay);

        // Generación infinita de naves enemigas
        while(true)
        {
            // Generar una posición aleatoria en el eje X dentro del rango establecido
            Vector3 position = new Vector3(Random.Range(MIN_X, MAX_X), transform.position.y, 0);
            
            // Instanciar una nueva nave enemiga en la posición aleatoria
            Instantiate(enemy, position, Quaternion.identity);

            // Esperar antes de generar la siguiente nave enemiga
            yield return new WaitForSeconds(interval);
        }
    }
}