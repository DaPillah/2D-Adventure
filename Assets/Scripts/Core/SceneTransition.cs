using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Tooltip("[REQUIRED] The name of scene to load without quote.")]
    public string sceneToLoad;

    [Tooltip("Exiting Point: set x and y\nExiting East&West: set x to 0, y to exit on other scene y\nExiting: North&South: set y to 0, x to exit on other scene")]
    public Vector2 playerDestinationPosition;

    [Tooltip("How far from the edge should the player spawn.")]
    public float offset = 0.1f;

    [Tooltip("What side is the player existing from.")]
    public ExitDirection exitSide;

    [Tooltip("(Don't change) Link to the Scriptable Object of Player Position.")]
    public ExitVectorValue playerLocationStorage;

    //enums are kinda like ints, but you use their name instead
    public enum ExitDirection
    {
        Point,  //0
        East,   //1
        North,  //2
        West,   //3
        South   //4
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //Calculate the relative position of the player to the exit collider
            Vector2 exitCenter = transform.position;
            Vector2 playerCenter = other.transform.position;
            Vector2 difference = playerCenter - exitCenter;

            if (exitSide == ExitDirection.South || exitSide == ExitDirection.North)
                difference.y = 0;
            if (exitSide == ExitDirection.East || exitSide == ExitDirection.West)
                difference.x = 0;

            //Calculate an offset based on the side of the player's collider
            Vector2 playerColliderOffset = new Vector2(0, 0);
            if (exitSide == ExitDirection.South)
                playerColliderOffset.y = -offset + other.offset.y + other.bounds.size.y * 0.6f;
            if (exitSide == ExitDirection.North)
                playerColliderOffset.y = offset + -other.offset.y + other.bounds.size.y * 0.6f;

            if (exitSide == ExitDirection.East)
                playerColliderOffset.x = offset + other.bounds.size.x * 0.6f;
            if (exitSide == ExitDirection.West)
                playerColliderOffset.x = -offset - other.bounds.size.x * 0.6f;


            //Store this script's target location
            //Store the exit side
            playerLocationStorage.exit = exitSide;

            if (exitSide == ExitDirection.Point)
                playerLocationStorage.spawningPosition = playerDestinationPosition;
            else
                playerLocationStorage.spawningPosition = playerDestinationPosition + difference + playerColliderOffset;

            //Load the new scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
