using UnityEngine;

public static class Utils
{
    // This function returns the horizontal input of the player
    public static float GetHorizontal(string character_name)
    {
        if (character_name == "DogPolyart 1")
        {
            return Input.GetAxis("Horizontal Player 1");
        }
        else
        {
            return Input.GetAxis("Horizontal Player 2");
        }
    }

    // This function returns the vertical input of the player
    public static float GetVertical(string character_name)
    {
        if (character_name == "DogPolyart 1")
        {
            return Input.GetAxis("Vertical Player 1");
        }
        else
        {
            return Input.GetAxis("Vertical Player 2");
        }
    }

    // This function returns the  TriggerAttack input of the player
    public static bool GetTriggerAttack(string character_name)
    {
        if (character_name == "DogPolyart 1")
        {
            return Input.GetKeyDown(KeyCode.J);
        }
        else
        {
            // q key for player 2
            return Input.GetMouseButtonDown(0);

        }
    }
}
