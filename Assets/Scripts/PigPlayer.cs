using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigPlayer : MonoBehaviour
{
    private SpriteRenderer pigLookSideRenderer;
    private const int rightSpriteIndex = 0;
    private const int leftSpriteIndex = 1;
    private const int bottomSpriteIndex = 2;
    private const int topSpriteIndex = 3;
    private const int stoneLayer = 3;
    private const string bombName = "bomb";
    private Rigidbody2D pigBody;
    public bool hasBombs;
 

    public Sprite[] sprites;
    public MainLevelLoader LevelController;
    void Start()
    {
        pigLookSideRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        pigBody = gameObject.GetComponent<Rigidbody2D>();
        hasBombs = false;
    }
    void Update()
    {

    }

    public void MooveThisPig(Vector3 directionVector)
    {

        float angle = Vector3.Angle(directionVector, new Vector3(1, 0, 0));
        if (angle >= 0 & angle <= 45)
        {
            pigLookSideRenderer.sprite = sprites[rightSpriteIndex];
            pigLookSideRenderer.transform.up = Vector3.Cross(directionVector, Vector3.back); // поворачиваем спрайт вверх (перпендикулярно) из-за направления картинки
        }
        else if (angle > 45 & angle <= 135)
        {
            if (directionVector.y > 0)
            {
                pigLookSideRenderer.sprite = sprites[topSpriteIndex];
                pigLookSideRenderer.transform.up = directionVector;
            }    
            else
            {
                pigLookSideRenderer.sprite = sprites[bottomSpriteIndex];
                pigLookSideRenderer.transform.up = -directionVector;
            }    
        }
        else if (angle > 135 & angle <= 180)
        {
            pigLookSideRenderer.sprite = sprites[leftSpriteIndex];
            pigLookSideRenderer.transform.up = Vector3.Cross(directionVector, Vector3.forward);
        }
        Vector3 deltaPosition = directionVector * Time.deltaTime * 0.09f;
        //flag = pigBody.IsTouchingLayers(stoneLayer);
        if (Mathf.Abs(transform.position.y + deltaPosition.y) >= 6) 
        {
            return;
        }
        else
        {
            transform.Translate(deltaPosition);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            LevelController.GameOver(isWin: false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BombScript bombScript))
        {
            hasBombs = true;
        }
    }
}
