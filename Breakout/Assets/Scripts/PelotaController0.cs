using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaController0 : MonoBehaviour
{


    Rigidbody2D rb; 

    AudioSource sfx;
    [SerializeField] AudioClip sfxPaddle;
    [SerializeField] AudioClip sfxBrick;
    [SerializeField] AudioClip sfxWall;

  
    //Será serializable para poder acceder a través de Unity
    [SerializeField] float force;

    [SerializeField] float delay;
    Dictionary<string,int> ladrillos = new Dictionary<string, int>(){
        {"Ladrillo-Amarillo", 10},
        {"Ladrillo-Verde", 15},
        {"Ladrillo-Naranja", 20},
        {"Ladrillo-Rojo", 25},
    };

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("LanzarPelota", delay);
        //Inicializamos la variable RigidBody

        sfx = GetComponent<AudioSource>();
    }



    private void LanzarPelota(){

        //Reseteamos posición y velocidad
        transform.position = Vector3.zero; //Accedemos directamente a la posición de la pelota y la reseteamos a la posición (0,0,0) 
        //Para resetear la velocidad debemos acceder al componente RigidBody 
        rb.linearVelocity = Vector2.zero;

        //Obtener una dirección aleatoria en el eje X 
        float dirX, dirY = -1; //En el eje y siempre se desplazará hacia abajo 
        //Queremos un ángulo 45º por lo que tiene que ser el mismo valor en eje X y en eje Y. Como en Y es -1 
        dirX = Random.Range(0,2) == 0 ? -1 : 1;  //Como son enteros son exclusivos 
        Vector2 dir = new Vector2(dirX, dirY);
        dir.Normalize(); //Podemos normalizarlo (no es necesario)

        //Aplicamos una fuerza determinando la dirección y una fuerza 
        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {

        //Almacenamos la etiqueta que tiene el objto con el que estamos colisionando
        string tag = other.gameObject.tag;

     
        //Comprobamos si estamos chocando con una pared
        if(tag == "ParedDerecha" || tag == "ParedIzquierda" || tag == "ParedSuperior" ){

            sfx.clip = sfxWall; 
            sfx.Play();
        }

        //Comprobamos si la etiqueta es un ladrillo 
        if(ladrillos.ContainsKey(tag)){
            sfx.clip = sfxBrick; 
            sfx.Play();
        }

        else if(tag == "Pala"){

            sfx.clip = sfxPaddle; 
            sfx.Play();
        }
    }


}