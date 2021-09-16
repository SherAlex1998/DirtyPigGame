using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float speed;
    public float delay;

    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;

    private const int rightSpriteIndex = 0;
    private const int leftSpriteIndex = 1;
    private const int bottomSpriteIndex = 2;
    private const int topSpriteIndex = 3;

    private int currentSpriteIndex = 0;

    private bool isMoving;
    private bool isBombed;

    void Start()
    {       
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        isMoving = false;
        isBombed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving & !isBombed)
            StartCoroutine(Patrolling(startPoint, endPoint, delay));
    }

    IEnumerator Patrolling(Vector3 from, Vector3 to, float delay)
    {
        Vector3 directionVector = (to - transform.position).normalized;

        if (!isMoving)
        {
            float angle = Vector3.Angle(directionVector, new Vector3(1, 0, 0));
            if (angle >= 0 & angle <= 45)
            {
                spriteRenderer.sprite = sprites[rightSpriteIndex];
                currentSpriteIndex = rightSpriteIndex;
                spriteRenderer.transform.up = Vector3.Cross(directionVector, Vector3.back); // поворачиваем спрайт вверх (перпендикулярно) из-за направления картинки
            }
            else if (angle > 45 & angle <= 135)
            {
                if (directionVector.y > 0)
                {
                    spriteRenderer.sprite = sprites[topSpriteIndex];
                    currentSpriteIndex = topSpriteIndex;
                    spriteRenderer.transform.up = directionVector;
                }
                else
                {
                    spriteRenderer.sprite = sprites[bottomSpriteIndex];
                    currentSpriteIndex = bottomSpriteIndex;
                    spriteRenderer.transform.up = -directionVector;
                }
            }
            else if (angle > 135 & angle <= 180)
            {
                spriteRenderer.sprite = sprites[leftSpriteIndex];
                currentSpriteIndex = leftSpriteIndex;
                spriteRenderer.transform.up = Vector3.Cross(directionVector, Vector3.forward);
            }
        }

        isMoving = true;

        while (Mathf.Abs(to.sqrMagnitude - transform.position.sqrMagnitude) / speed >= 0.01f)
        {
            transform.Translate(directionVector * Time.deltaTime * speed / 10);
            yield return null;
        }

        yield return new WaitForSeconds(delay);
        Vector3 temp = startPoint;
        startPoint = endPoint;
        endPoint = temp;

        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BombScript bombScript))
        {
            bombScript.BoomThisBomb();
            spriteRenderer.sprite = sprites[currentSpriteIndex + 4];
            isBombed = true;
            MainLevelLoader.instance.AddScore();
        }
    }
}
