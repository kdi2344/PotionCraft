using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int DayCount = 1; //첫날이 day 1

    public bool isFirstStart = true; //아예 첫 시작이라면
    public bool isFirstCustomer = true; //첫 손님이라면
    public bool CanMakePotion = false; //포션에 닿아있으면 활성화
    public bool CanSavePotion = false; //포션 만들어서 지금 저장 가능한 상태라면 활성화
    public InvenItemManager.Potion currentPotionEffect = InvenItemManager.Potion.None; //지금 만들 수 있는 포션 효과

    [SerializeField] GameObject whole;
    [SerializeField] GameObject potion;
    public int Coin;
    enum Room { Main, Customer, Garden, Bed, Machine}
    [SerializeField] Room currentRoom;
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject up;
    [SerializeField] private GameObject down;

    [SerializeField] private Text textCoin;

    [SerializeField] Vector3 movePos;
    Vector3 speed = Vector3.zero;

    [SerializeField] GameObject[] GardenPlants;
    [SerializeField] GameObject[] CustomerPrefabs;
    public CustomerDetail[] customerDetails;
    public List<GameObject> MadeCustomers = new List<GameObject>();
    [SerializeField] GameObject CustomerPosition;
    [SerializeField] Transform CustomerParent;

    public Color[] PotionColors;
    public int[] IngreQuantity = { 2, 2, 0, 0, 0, 0, 0, 0, 0 };
    public int[] PotionQuantity = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public PotionDetail[] PotionDetails;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        currentRoom = Room.Main;
        movePos = Camera.main.transform.position;
        IgnoreCollision();
        GardenReset();
        CustomerSet();
        if (isFirstStart)
        {
            for(int i =0; i < PotionDetails.Length; i++)
            {
                PotionDetails[i].ingredients.Clear();
                PotionDetails[i].effect.Clear();
            }
        }
    }
    private void Update()
    {
        if (movePos.x != Camera.main.transform.position.x || movePos.y != Camera.main.transform.position.y)
        {
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, movePos, ref speed, 0.5f);
        }
        textCoin.text = Coin.ToString();

    }
    private void CustomerSet() //손님은 하루에 5명 2명손님 1명 상인 2명손님
    {
        for (int i =0; i < customerDetails.Length; i++)
        {
            if (i != 2)
            {
                int j = Random.Range(0, 4); //4명의 일반 고객중에 뽑기
                if (MadeCustomers.Count != 5) MadeCustomers.Add(Instantiate(CustomerPrefabs[j], CustomerPosition.transform.position, Quaternion.identity , CustomerParent));
                else MadeCustomers[i] =(Instantiate(CustomerPrefabs[j], CustomerPosition.transform.position, Quaternion.identity, CustomerParent));
                int k = Random.Range(0, 6);
                customerDetails[i].needPotion = (InvenItemManager.Potion)k;
                int m = Random.Range(0, 3);
                customerDetails[i].RandomMent = m;
                customerDetails[i].type = CustomerDetail.CustomerType.Normal;
                MadeCustomers[i].GetComponent<CustomerInfo>().detail = customerDetails[i];
            }
            else
            {
                customerDetails[i].type = CustomerDetail.CustomerType.Shop;
                if (MadeCustomers.Count != 5) MadeCustomers.Add(Instantiate(CustomerPrefabs[4], CustomerPosition.transform.position, Quaternion.identity, CustomerParent));
                else MadeCustomers[i] = (Instantiate(CustomerPrefabs[4], CustomerPosition.transform.position, Quaternion.identity, CustomerParent));
                MadeCustomers[i].GetComponent<CustomerInfo>().detail = customerDetails[i];
            }

        }
    }
    private void GardenReset()
    {
        int plantNum = Random.Range(4, 7); //4~6종류 
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
            if (isFirstStart)
            {
                CustomerManager.instance.GoFirstCustomer();
                isFirstStart = false;
            }
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
