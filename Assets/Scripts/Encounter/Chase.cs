using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField] GameObject prey;
    [SerializeField] float move_speed = 1f;

    void Update()
    {
        Vector2 player_position = prey.transform.position;
        Vector2 enemy_position = gameObject.transform.position;
        float distance = Vector2.Distance(enemy_position, player_position);

        if (distance > 0.1f){
            Vector2 new_position = Vector2.MoveTowards(enemy_position, player_position, move_speed * Time.deltaTime);
            gameObject.transform.position = new_position;
        }
    }


}