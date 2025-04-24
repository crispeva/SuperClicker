using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GosthAgent : Agent
{
    // Start is called before the first frame update
    #region Properties
    float leftLimit; // L�mite izquierdo
    float rightLimit;
    private float speed = 20f; // Velocidad del movimiento
    private int direction = 1;
    #endregion

    #region Fields;
    #endregion
    private void Awake()
    {
        leftLimit = transform.position.x - 200f;
        rightLimit = transform.position.x + 200f;
    }
    #region Unity Callbacks
    protected override void Start()
    {
        base.Start();
        //Debug.Log("�Game es null?: " + (game == null));
        StartCoroutine(ComboSpeed());


    }
    private void Update()
    {
        // Si el objeto est� en movimiento, llama a la funci�n de movimiento
        if (gameObject.activeSelf)
        {
            Fly();
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void Fly()
    {
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        // Cambia la direcci�n si alcanza los l�mites
        if (transform.position.x >= rightLimit)
        {
            direction = -1; // Cambia a moverse hacia la izquierda
        }
        else if (transform.position.x <= leftLimit)
        {
            direction = 1; // Cambia a moverse hacia la derecha
        }
    }
    protected override void Click()
    {
        //No se hace nada en este agente
    }
    #endregion


}
