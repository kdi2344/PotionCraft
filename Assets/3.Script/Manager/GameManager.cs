using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    [SerializeField] GameObject whole;
    [SerializeField] GameObject potion;

    enum Room { Main, Customer, Garden, Bed, Machine}
    Room currentRoom;
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject up;
    [SerializeField] private GameObject down;

    [SerializeField] Vector3 movePos;
    Vector3 speed = Vector3.zero;

    [SerializeField] GameObject[] GardenPlants; 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        currentRoom = Room.Main;
        movePos = Camera.main.transform.position;
        IgnoreCollision();
        GardenReset();
    }
    private void Update()
    {
        if (movePos.x != Camera.main.transform.position.x || movePos.y != Camera.main.transform.position.y)
        {
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, movePos, ref speed, 0.5f);
        }
    }
    private void GardenReset()
    {
        int plantNum = Random.Range(4, 7); //4~6Á¾·ù 
        for (int i = 0;  i < plantNum; i++)
        {
            GardenPlants[i].SetActive(true);
        }
        
    }
    private void IgnoreCollision()
    {
        Physics2D.IgnoreCollision(whole.GetComponent<BoxCollider2D>(), potion.GetComponent<CircleCollider2D>());
        
    }
    public void ArrowRight()
    {
        if (currentRoom == Room.Main) currentRoom = Room.Garden;
        else if (currentRoom == Room.Customer) currentRoom = Room.Main;
        movePos = movePos + new Vector3(17.8f, 0, 0);
        ChangeBtn();
    }
    public void ArrowLeft()
    {
        if (currentRoom == Room.Main) currentRoom = Room.Customer;
        else if (currentRoom == Room.Garden) currentRoom = Room.Main;
        movePos = movePos - new Vector3(17.8f, 0, 0);
        ChangeBtn();
    }
    public void ArrowUp()
    {
        if (currentRoom == Room.Main) currentRoom = Room.Bed;
        else if (currentRoom == Room.Machine) currentRoom = Room.Main;
        movePos = movePos + new Vector3(0, 10, 0);
        ChangeBtn();
    }
    public void ArrowDown()
    {
        if (currentRoom == Room.Main) currentRoom = Room.Machine;
        else if (currentRoom == Room.Bed) currentRoom = Room.Main;
        movePos -= new Vector3(0, 10, 0);
        ChangeBtn();
    }

    private void ChangeBtn()
    {
        if (currentRoom == Room.Main)
        {
            left.transform.GetChild(0).gameObject.SetActive(true);
            left.transform.GetChild(1).gameObject.SetActive(false);
            right.transform.GetChild(0).gameObject.SetActive(true);
            right.transform.GetChild(1).gameObject.SetActive(false);
            up.transform.GetChild(0).gameObject.SetActive(true);
            up.transform.GetChild(1).gameObject.SetActive(false);
            down.transform.GetChild(0).gameObject.SetActive(true);
            down.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (currentRoom == Room.Customer)
        {
            left.transform.GetChild(0).gameObject.SetActive(false);
            left.transform.GetChild(1).gameObject.SetActive(false);
            right.transform.GetChild(0).gameObject.SetActive(false);
            right.transform.GetChild(1).gameObject.SetActive(true);
            up.transform.GetChild(0).gameObject.SetActive(false);
            up.transform.GetChild(1).gameObject.SetActive(false);
            down.transform.GetChild(0).gameObject.SetActive(false);
            down.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (currentRoom == Room.Garden)
        {
            left.transform.GetChild(0).gameObject.SetActive(false);
            left.transform.GetChild(1).gameObject.SetActive(true);
            right.transform.GetChild(0).gameObject.SetActive(false);
            right.transform.GetChild(1).gameObject.SetActive(false);
            up.transform.GetChild(0).gameObject.SetActive(false);
            up.transform.GetChild(1).gameObject.SetActive(false);
            down.transform.GetChild(0).gameObject.SetActive(false);
            down.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (currentRoom == Room.Bed)
        {
            left.transform.GetChild(0).gameObject.SetActive(false);
            left.transform.GetChild(1).gameObject.SetActive(false);
            right.transform.GetChild(0).gameObject.SetActive(false);
            right.transform.GetChild(1).gameObject.SetActive(false);
            up.transform.GetChild(0).gameObject.SetActive(false);
            up.transform.GetChild(1).gameObject.SetActive(false);
            down.transform.GetChild(0).gameObject.SetActive(false);
            down.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (currentRoom == Room.Machine)
        {
            left.transform.GetChild(0).gameObject.SetActive(false);
            left.transform.GetChild(1).gameObject.SetActive(false);
            right.transform.GetChild(0).gameObject.SetActive(false);
            right.transform.GetChild(1).gameObject.SetActive(false);
            up.transform.GetChild(0).gameObject.SetActive(false);
            up.transform.GetChild(1).gameObject.SetActive(true);
            down.transform.GetChild(0).gameObject.SetActive(false);
            down.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
