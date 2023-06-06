using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StepMove : MonoBehaviour
{
    // Update is called once per frame
    public Rigidbody2D otherRigidbody;
    public GameObject grid;
    private GameObject nearestChildGameObject;
    private string tagname = "";
    public Text text;
    public Text gameOverText;
    private int moves = 100;

    void Start()
    {
        //grid.transform.GetChild(7).gameObject.SetActive(false);
        //grid.transform.GetChild(2).gameObject.SetActive(false);
        //grid.transform.GetChild(12).gameObject.SetActive(false);
        //grid.transform.GetChild(19).gameObject.SetActive(false);
        text.text = moves.ToString();
    }

    void Update()
    {
        
        
        Collider[] allGameObjects = Physics.OverlapSphere(transform.position, 10f);
        int nearestGridIndex = GetNearestChild(grid);
        string direction = CheckDirection();

        if (nearestChildGameObject.gameObject.name == "Cell (20)")
        {
            gameOverText.text = "Game Won";
            return;
        }
        
        if (moves == 0)
        {
            gameOverText.text = "Game Over";
            return;
        }
        
        if (!CanMove(tagname) && Input.GetKeyDown(KeyCode.Space) == false)
        {
            
            return;
        }
        
        if (direction == "Up")
        {
            MoveUp();
            moves--;
        }
        else if (direction == "Down")
        {
            MoveDown();
            moves--;
        }
        else if (direction == "Left")
        {
            MoveLeft();
            moves--;
        }
        else if (direction == "Right")
        {
            MoveRight();
            moves--;
        }
        
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Rotate the GameObject by 90 degrees on the Y axis
            //transform.RotateAround(transform.position, Vector3.right, 90f);
            
            transform.Rotate(0f, 0f, 90f);
            
            transform.GetComponent<Collider2D>().enabled = false;
            
        }
        
        if(transform.childCount > 0)
            transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        
        Collider2D[] colliders = Physics2D.OverlapPointAll(new Vector2(transform.position.x, transform.position.y));
        //Debug.Log(colliders.Length);
        
        text.text = moves.ToString();
        
        
    }

    private string CheckDirection()
    {
        string ans = "";
        if (transform.rotation.z == 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                ans = "Up";
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                ans = "Down";
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                ans = "Left";
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                ans = "Right";

            tagname = ans;

        }
        else if (Mathf.Approximately(transform.rotation.eulerAngles.z,90))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ans = "Right";
                tagname = "Up";
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ans = "Left";
                tagname = "Down";
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ans = "Up";
                tagname = "Left";
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ans = "Down";
                tagname = "Right";
            }
        }
        else if (Mathf.Approximately(transform.rotation.eulerAngles.z,180))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ans = "Down";
                tagname = "Up";
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ans = "Up";
                tagname = "Down";
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ans = "Right";
                tagname = "Left";
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ans = "Left";
                tagname = "Right";
            }
        }
        else if (Mathf.Approximately(transform.rotation.eulerAngles.z,270))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ans = "Left";
                tagname = "Up";
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ans = "Right";
                tagname = "Down";
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ans = "Down";
                tagname = "Left";
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ans = "Up";
                tagname = "Right";
            }
        }
        
        return ans;

    }

    private void MoveUp()
    {
        float y = 1.75f * 4;
        transform.Translate(0, y, 0);
        transform.GetComponent<Collider2D>().enabled = true;
    }
    
    private void MoveDown()
    {
        float y = -1.75f * 4;
        transform.Translate(0, y, 0);
        transform.GetComponent<Collider2D>().enabled = true;
    }
    
    private void MoveLeft()
    {
        float x = -1.75f * 4;
        transform.Translate(x, 0, 0);
        transform.GetComponent<Collider2D>().enabled = true;
    }
    
    private void MoveRight()
    {
        float x = 1.75f * 4;
        transform.Translate(x, 0, 0);
        transform.GetComponent<Collider2D>().enabled = true;
    }

    private bool CanMove(string direction)
    {
        if (direction.Length == 0)
            return false;
        
        GameObject nearestDoor = null;

        //if (nearestChildGameObject.transform.childCount > 0 && transform.childCount == 0)
        //   return false;
        
        for (int i = 0; i < nearestChildGameObject.transform.childCount; i++)
        {
            GameObject child = nearestChildGameObject.transform.GetChild(i).gameObject;
            if (child.CompareTag(direction))
                nearestDoor = nearestChildGameObject.transform.GetChild(i).gameObject;
        }

        if (nearestDoor != null && transform.childCount == 0)
            return false;
                
        if(transform.childCount == 1 && nearestDoor != null)
        {
            if (Vector3.Distance(transform.position, nearestDoor.transform.position) <
                Vector3.Distance(transform.GetChild(0).position, nearestDoor.transform.position))
                return false;

            if (Vector3.Distance(transform.position, nearestDoor.transform.position) >
                Vector3.Distance(transform.GetChild(0).position, nearestDoor.transform.position) &&
                ProcessName(transform.GetChild(0).name) != ProcessName(nearestDoor.transform.name))
                return false;
        }

        return true;
    }

    private string ProcessName(string name)
    {
        return name.Split(" ")[0];
    }

    private int GetNearestChild(GameObject obj)
    {
        // Initialize the minimum distance
        float minimumDistance = float.MaxValue;
        int index = -1;

        // For each child GameObject
        for (int i=0; i<obj.transform.childCount; i++) {
            // Get the distance between the child GameObject and the parent GameObject
            float distance = Vector3.Distance(obj.transform.GetChild(i).transform.position, transform.position);

            // If the distance is less than the minimum distance
            if (distance < minimumDistance) {
                // Set the nearest child GameObject to the child GameObject
                nearestChildGameObject = obj.transform.GetChild(i).gameObject;

                // Set the minimum distance to the distance
                minimumDistance = distance;
                index = i;
            }
        }

        return index;
    }
}
