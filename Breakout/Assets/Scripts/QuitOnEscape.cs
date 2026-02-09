using UnityEngine;

public class QuitOnEscape : MonoBehaviour
{
    void Update()
    {
        // Detecta si la tecla Escape fue presionada en este frame
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Cierra la aplicación
            Application.Quit();

            // Nota: Esto solo funciona en el juego compilado (.exe, .app, etc.)
            // Para probarlo dentro del editor de Unity, usamos esta línea:
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
