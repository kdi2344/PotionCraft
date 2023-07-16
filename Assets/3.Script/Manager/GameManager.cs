using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int DayCount = 1; //ù���� day 1

    public bool isFirstStart = true; //�ƿ� ù �����̶��
    public bool isFirstCustomer = true; //ù �մ��̶��
    public bool CanMakePotion = false; //���ǿ� ��������� Ȱ��ȭ
    public bool CanSavePotion = false; //���� ���� ���� ���� ������ ���¶�� Ȱ��ȭ
    public InvenItemManager.Potion currentPotionEffect = InvenItemManager.Potion.None; //���� ���� �� �ִ� ���� ȿ��

    [SerializeField] GameObject whole;
    [SerializeField] GameObject potion;
    public int Coin = 100;
    public int Success = 0;
    public int PopulLevel = 0;
    public int Karma = 0;
    enum Room { Main, Customer, Garden, Bed, Machine}
    [SerializeField] Room currentRoom;
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject up;
    [SerializeField] private GameObject down;

    [SerializeField] private Text textDay;
    [SerializeField] private Text textCoin;
    [SerializeField] private Text textPoPul;
    [SerializeField] private Text textKarma;
    [SerializeField] private GameObject populIcon;
    [SerializeField] private GameObject karmaIcon;

    [SerializeField] Vector3 movePos;
    Vector3 speed = Vector3.zero;

    [SerializeField] int[] PopulLevels;
    [SerializeField] Sprite[] PopulIcons;
    [SerializeField] Sprite[] KarmaIcons;
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

        if (Success >= PopulLevels[PopulLevel])
        {
            Success = 0;
            PopulLevel++;
            populIcon.GetComponent<Image>().sprite = PopulIcons[PopulLevel];
        }
        else if (Success < 0)
        {
            if (PopulLevel > 0)
            {
                PopulLevel--;
                Success = PopulLevels[PopulLevel] - 1;
            }
            else
            {
                Success = 0;
            }
            
        }
        textDay.text = DayCount.ToString();
        textPoPul.text = Success.ToString() + "/" + PopulLevels[PopulLevel];
        textKarma.text = Karma.ToString();
        SetKarma();
    }
    private void SetKarma()
    {
        if (Karma <= -15) //Evil 6 
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[0];
        }
        else if (Karma <= -13 && Karma > -15) //Evil 5 
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[1];
        }
        else if (Karma <= -10 && Karma > -13) //Evil 4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[2];
        }
        else if (Karma <= -8 && Karma > -10) //Evil 3 
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[3];
        }
        else if (Karma <= -5 && Karma > -8) //Evil 2 
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[4];
        }
        else if (Karma <= -2 && Karma > -5) //Evil 1  -2 -3 -4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[5];
        }
        else if (Karma <= 1 && Karma > -2) //Neutral -1 0 1
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[6];
        }
        else if (Karma <= 3 && Karma > 1) //Kind 1  -2 -3 -4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[7];
        }
        else if (Karma <= 5 && Karma > 3) //Kind 2  -2 -3 -4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[8];
        }
        else if (Karma <= 8 && Karma > 5) //Kind 3  -2 -3 -4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[9];
        }
        else if (Karma <= 11 && Karma > 8) //Kind 4  -2 -3 -4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[10];
        }
        else if (Karma <= 14 && Karma > 11) //Kind 5  -2 -3 -4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[11];
        }
        else if (Karma > 14) //Kind 1  -2 -3 -4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[12];
        }
    }
    public void CustomerSet() //�մ��� �Ϸ翡 5�� 2��մ� 1�� ���� 2��մ�
    {
        for (int i =0; i < MadeCustomers.Count; i++)
        {
            if (MadeCustomers[i].activeSelf) Destroy(MadeCustomers[i]);
        }
        MadeCustomers.Clear();
        for (int i =0; i < customerDetails.Length; i++)
        {
            if (i != 2)
            {
                int j = Random.Range(0, 4); //4���� �Ϲ� ���߿� �̱�
                if (MadeCustomers.Count != 5) MadeCustomers.Add(Instantiate(CustomerPrefabs[j], CustomerPosition.transform.position, Quaternion.identity , CustomerParent));
                else MadeCustomers[i] =(Instantiate(CustomerPrefabs[j], CustomerPosition.transform.position, Quaternion.identity, CustomerParent));
                int k = Random.Range(1, 6);
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
    public void GardenReset()
    {
        int plantNum = Random.Range(4, 7); //4~6���� 
        for (int i = 0;  i < plantNum; i++)
        {
            GardenPlants[i].SetActive(true);
        }
        for (int i =0; i < PotionQuantity.Length; i++)
        {
            PotionQuantity[i] = 0;
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
            if (isFirstCustomer)
            {
                CustomerManager.instance.GoFirstCustomer();
                isFirstCustomer = false;
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
