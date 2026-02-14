using UnityEngine;
using System.Collections;

public class AsteroidsSpawner : MonoBehaviour
{
    [SerializeField] float minInterval = 1.5f; 
    [SerializeField] float maxInterval = 3.5f; 
    [SerializeField] float delay = 2.0f; 

    [SerializeField] GameObject AsteroidPrefab; 

    const float MIN_X = -4.5f; 
    const float MAX_X = 4.5f; 

    void Start()
    {
        StartCoroutine(AsteroidSpawnRoutine());
    }

    IEnumerator AsteroidSpawnRoutine()
    {
        yield return new WaitForSeconds(delay);

        while(true)
        {
            // Si el juego ha terminado, dejamos de generar
            if (GameManager.GetInstance() != null && GameManager.GetInstance().IsGameOverCheck())
                yield break;

            // Posición aleatoria
            Vector3 position = new Vector3(Random.Range(MIN_X, MAX_X), transform.position.y, 0);
            
            // Instanciar
            Instantiate(AsteroidPrefab, position, Quaternion.identity);

            // Esperar un tiempo aleatorio entre el rango definido
            float nextWait = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(nextWait);
        }
    }
}