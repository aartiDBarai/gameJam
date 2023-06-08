using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StepMove : MonoBehaviour
{
    // Update is called once per frame
    public Rigidbody2D otherRigidbody;
    public GameObject grid;
    private GameObject nearestChildGameObject;
    private string tagname = "";
    public Text text;
    public Text gameOverText;
    public int moves;
    // The name of the scene to navigate to
    public string levelChange;
    public string lastCellName;
    // The number of seconds to wait before changing scenes
    private float waitTime = 2f;


    void Start()
    {
        //grid.transform.GetChild(7).gameObject.SetActive(false);
        //grid.transform.GetChild(2).gameObject.SetActive(false);
        //grid.transform.GetChild(12).gameObject.SetActive(false);
        //grid.transform.GetChild(19).gameObject.SetActive(false);
        text.text = moves.ToString();
        waitTime = Time.time;
    }

    void Update()
    {
        
        
        Collider[] allGameObjects = Physics.OverlapSphere(transform.position, 10f);
        int nearestGridIndex = GetNearestChild(grid);
        string direction = CheckDirection();

        

        if (nearestChildGameObject.gameObject.name == lastCellName)
        {
            gameOverText.text = "Game Won";
            
            // Pause execution for 2 seconds
            if(levelChange.Length > 0 && Time.time >= waitTime + 6f)
            {
                waitTime = Time.time;
                SceneManager.LoadScene(levelChange);
                
            }
            return;
        }
        
        if (moves == 0)
        {
            gameOverText.text = "Game Over";
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) == false && !CanMove(tagname))
        {
            
            return;
        }
        
        if (direction == "Up")
        {
            MoveUp(nearestGridIndex);
            
        }
        else if (direction == "Down")
        {
            MoveDown(nearestGridIndex);
            
        }
        else if (direction == "Left")
        {
            MoveLeft(nearestGridIndex);
            
        }
        else if (direction == "Right")
        {
            MoveRight(nearestGridIndex);
            
        }
        
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Rotate the GameObject by 90 degrees on the Y axis
            //transform.RotateAround(transform.position, Vector3.right, 90f);
            
            Debug.Log("old " + transform.position.x + " " + transform.position.y);
            transform.Rotate(0f, 0f, 90f);
            Debug.Log("new " + transform.position.x + " " + transform.position.y);
            
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

    private void MoveUp(int nearestGridIndex)
    {
        float y = 1.75f * 20;
        if (!CheckBounds(grid, nearestGridIndex, 0, 1.75f))
            return;
        transform.Translate(0, y, 0);
        transform.GetComponent<Collider2D>().enabled = true;
        moves--;
    }
    
    private void MoveDown(int nearestGridIndex)
    {
        float y = -1.75f * 20;
        if (!CheckBounds(grid, nearestGridIndex, 0, -1.75f))
        {
            Debug.Log("not down");
            return;
        }
        transform.Translate(0, y, 0);
        transform.GetComponent<Collider2D>().enabled = true;
        moves--;
    }
    
    private void MoveLeft(int nearestGridIndex)
    {
        float x = -1.75f * 20;
        if (!CheckBounds(grid, nearestGridIndex, -1.75f, 0))
            return;
        transform.Translate(x, 0, 0);
        transform.GetComponent<Collider2D>().enabled = true;
        moves--;
    }
    
    private void MoveRight(int nearestGridIndex)
    {
        float x = 1.75f * 20;
        if (!CheckBounds(grid, nearestGridIndex, 1.75f, 0))
            return;
        transform.Translate(x, 0, 0);
        transform.GetComponent<Collider2D>().enabled = true;
        moves--;
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
            
            //Destroy(transform.GetChild(0).gameObject);
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
    
    private bool CheckBounds(GameObject obj, int nearestGridIndex, float x, float y)
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            x = 0;
            y = 1.75f;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            x = 0;
            y = -1.75f;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            x = -1.75f;
            y = 0;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            x = 1.75f;
            y = 0;
        }
        
        Vector3 newPos = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z);
        
        // For each child GameObject
        for (int i=0; i<obj.transform.childCount; i++) {
            
            //Bounds bounds = obj.transform.GetChild(i).GetComponent<Renderer>().bounds;
            // Extract the X and Y components of the bounds
            //float minX = bounds.min.x;
            //float maxX = bounds.max.x;
            //float minY = bounds.min.y;
            //float maxY = bounds.max.y;

            float minX = obj.transform.GetChild(i).localPosition.x - 0.875f;
            float maxX = obj.transform.GetChild(i).localPosition.x + 0.875f;
            
            float minY = obj.transform.GetChild(i).localPosition.y - 0.875f;
            float maxY = obj.transform.GetChild(i).localPosition.y + 0.875f;
            
            Debug.Log(obj.transform.GetChild(i).name + " " + minX + " " + maxX + " " + minY + " " + maxY);
            Debug.Log(newPos.x + " " + newPos.y);
            
            if (newPos.x >= minX && newPos.x <= maxX &&
                newPos.y >= minY && newPos.y <= maxY)
            {
                return true;
            }
            
        }
        
        return false;
    }
}
