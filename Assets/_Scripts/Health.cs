using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int lifes = 100;

    //[HideInInspector]
    public int currentLifes;
    public Text textLife;

    SpriteRenderer sprite;
    public bool enemy;

    [Header("Hurt")]
    public int hurt = 1;

    Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        currentLifes = lifes;
        UpdateText();
        sprite = GetComponent<SpriteRenderer>();
        // Color original
        originalColor = sprite.color;
    }

    public void QuitLife(int quitPoints)
    {
        currentLifes-= quitPoints;
        UpdateText();
        
        sprite.color = Color.red;

        StartCoroutine(RedShip());

        // Si no tienes vida...
        if (currentLifes <= 0)
        {
            currentLifes = 0;

            if (!enemy)
            {
                GameManager.sharedInstance.GameOver();
            }

            this.gameObject.SetActive(false);
        }
    }

    // Parpadeo daño a nave
    IEnumerator RedShip()
    {
        yield return new WaitForSeconds(0.3f);
        sprite.color = originalColor;
    }

    void UpdateText()
    {
        if(!enemy)
        {
            textLife.text = currentLifes + "/100";
        }
    }

    public void ResetLife()
    {
        currentLifes = lifes;
        UpdateText();
    }
}
