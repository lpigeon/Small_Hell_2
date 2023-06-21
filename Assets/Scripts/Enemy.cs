using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private float moveSpeed = 5f;

    private float minY = -7f;

    [SerializeField]
    private float hp = 1f;

    public void SetMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Weapon") {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;
            if (hp <= 0) {
                Destroy(gameObject);
                if (gameObject.tag == "LastBoss"){
                    GameManager.instance.SetGameOver();
                }
                else if (gameObject.tag == "Boss"){
                    for (int i = 0; i < 10; i++){
                        Instantiate(coin, transform.position, Quaternion.identity);
                    }
                }
                else {
                    Instantiate(coin, transform.position, Quaternion.identity);
                }
            }
            Destroy(other.gameObject);
        }
    }
}
