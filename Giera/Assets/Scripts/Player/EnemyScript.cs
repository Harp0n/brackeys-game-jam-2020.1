﻿using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;
    public float speed;
    public float stealAmount;
    public float nodeProximity = 0.01f;
    public AudioClip[] hurtSounds;
    public AudioClip[] deathSounds;
    public AudioSource hurtSource;

    private Animator animator;
    private Collider2D col;
    private Transform path;
    private Vector2[] nodes;
    private float startingScaleX;
    private int currentNode;
    private bool canMove;
    private bool faceLeft = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("sword"))
        {
            GetDamage();
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        path = GameObject.FindGameObjectWithTag("enemyPath").transform;
        nodes = new Vector2[path.childCount];
        for (int i = 0; i < path.childCount; i++) nodes[i] = path.GetChild(i).position;
    }

    private void OnEnable()
    {
        startingScaleX = transform.localScale.x;
        currentNode = 0;
        transform.position = nodes[currentNode++];
        col.enabled = true;
        canMove = true;
    }

    private void Update()
    {
        if (canMove)
        {
            UpdateNode();
            Move();
        }
    }

    private void UpdateNode()
    {
        Vector2 diff = nodes[currentNode] - new Vector2(transform.position.x, transform.position.y);
        if (diff.SqrMagnitude() < nodeProximity)
        {
            if (++currentNode >= nodes.Length)
                Escape();
            faceLeft = nodes[currentNode].x > nodes[currentNode - 1].x;
        }
    }

    private void Move()
    {
        Vector2 moveVector = nodes[currentNode] - new Vector2(transform.position.x, transform.position.y);
        moveVector.Normalize();
        transform.Translate(moveVector * Time.deltaTime * speed);
        if (faceLeft) transform.localScale = new Vector2(startingScaleX, transform.localScale.y);
        else transform.localScale = new Vector2(-startingScaleX, transform.localScale.y);
    }

    public void GetDamage()
    {
        animator.SetTrigger("damage");
        hurtSource.clip = hurtSounds[Random.Range(0, hurtSounds.Length)];
        hurtSource.Play();
        if (--health <= 0) Die();
    }

    private void Die()
    {
        canMove = false;
        col.enabled = false;
        hurtSource.clip = deathSounds[Random.Range(0, deathSounds.Length)];
        hurtSource.Play();
        animator.SetTrigger("die");
    }

    private void Escape()
    {
        canMove = false;
        col.enabled = false;
        animator.SetTrigger("escape");
        //tutaj odjąć towar z łodzi
        AfterAnim();//powinno sie z animacji wezwac, ale zepsul mi sie Animator xd
    }

    public void AfterAnim()
    {
        gameObject.SetActive(false);
    }
}
