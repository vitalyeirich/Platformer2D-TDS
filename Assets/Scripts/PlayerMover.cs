using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]

// Значение гравитации уже выставлено на 0 при старте

public class TopDownPlayer : MonoBehaviour
{
    // обычная скорость
    public float moveSpeed = 5f;

    // скорость рывка
    public float dashSpeed = 15f;

    // длительность рывка в секундах
    public float dashDuration = 0.2f;

    private Vector2 moveDirection;
    private bool isDashing = false;
    private float dashTime;

    void Update()
    {
        // Функция для обработки ввода с клавиатуры
        ProcessInputs();
    }

    void FixedUpdate()
    {
        // Функция, которая отвечает за передвижение объекта на основе направления и скорости
        Move();
    }

    void ProcessInputs()
    {
        // Считываем горизонтальное направление
        float moveX = Input.GetAxisRaw("Horizontal");

        // Считываем вертикальное направление
        float moveY = Input.GetAxisRaw("Vertical");
        
        // Делаем длину вектора равной 1, чтобы движение по диагонали не было быстрее, чем по прямой
        moveDirection = new Vector2(moveX, moveY).normalized;

        // Если пробел нажат + есть направление
        if (Input.GetKeyDown(KeyCode.Space) && moveDirection != Vector2.zero)
        {
            isDashing = true;
            dashTime = dashDuration;
        }
    }

    void Move()
    {
        // Если условия рывка выполнены:
        if (isDashing)
        {
            // Ускоряем Player'a в направлении рывка
            GetComponent<Rigidbody2D>().velocity = moveDirection * dashSpeed;

            dashTime -= Time.fixedDeltaTime;
            if (dashTime <= 0f)
            {
                isDashing = false;
            }
        }
        // Во всех остальных случаях:
        else
        {
            // Меняем свойство velocity в компоненте RigidBody нашего Player'а
            GetComponent<Rigidbody2D>().velocity = moveDirection * moveSpeed;
        }
    }
}