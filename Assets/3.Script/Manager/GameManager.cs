using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    //public int DayCount = 1; //첫날이 day 1

    //public bool isFirstStart = true; //아예 첫 시작이라면
    public bool isFirstCustomer = true; //첫 손님이라면
    public bool isNextDay = false; //침대에서 저장하고 다음날 아침일 경우
    public bool CanMakePotion = false; //포션에 닿아있으면 활성화
    public bool CanSavePotion = false; //포션 만들어서 지금 저장 가능한 상태라면 활성화
    public InvenItemManager.Potion currentPotionEffect = InvenItemManager.Potion.None; //지금 만들 수 있는 포션 효과

    [SerializeField] GameObject whole;
    [SerializeField] GameObject potion;
    // public int Coin = 100;
    // public int Success = 0;
    // public int PopulLevel = 0;
    // public int Karma = 0;
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
    // public int[] IngreQuantity = { 2, 2, 0, 0, 0, 0, 0, 0, 0 };
    // public int[] PotionQuantity = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public PotionDetail[] PotionDetails;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        currentRoom = Room.Main;
        movePos = Camera.main.transform.position;
        IgnoreCollision();

        if (DataManager.instance.nowData.DayCount == 1)
        {
            GardenReset();
            CustomerSet();
            for (int i =0; i < PotionDetails.Length; i++)
            {
                PotionDetails[i].ingredients.Clear();
                PotionDetails[i].effect = InvenItemManager.Potion.None;
                DataManager.instance.nowData.PotionDetails[i] = PotionDetails[i];
            }
        }
        else
        {
            GardenReset();
            CustomerSet();
        }
    }
    private void Update()
    {
        if (movePos.x != Camera.main.transform.position.x || movePos.y != Camera.main.transform.position.y)
        {
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, movePos, ref speed, 0.5f);
        }
        textCoin.text = DataManager.instance.nowData.Coin.ToString();

        if (DataManager.instance.nowData.Success >= PopulLevels[DataManager.instance.nowData.PopulLevel])
        {
            DataManager.instance.nowData.Success = 0;
            DataManager.instance.nowData.PopulLevel++;
            populIcon.GetComponent<Image>().sprite = PopulIcons[DataManager.instance.nowData.PopulLevel];
        }
        else if (DataManager.instance.nowData.Success < 0)
        {
            if (DataManager.instance.nowData.PopulLevel > 0)
            {
                DataManager.instance.nowData.PopulLevel--;
                DataManager.instance.nowData.Success = PopulLevels[DataManager.instance.nowData.PopulLevel] - 1;
            }
            else
            {
                DataManager.instance.nowData.Success = 0;
            }
            
        }
        textDay.text = DataManager.instance.nowData.DayCount.ToString();
        textPoPul.text = DataManager.instance.nowData.Success.ToString() + "/" + PopulLevels[DataManager.instance.nowData.PopulLevel];
        textKarma.text = DataManager.instance.nowData.Karma.ToString();
        SetKarma();
    }
    private void SetKarma()
    {
        if (DataManager.instance.nowData.Karma <= -15) //Evil 6 
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[0];
        }
        else if (DataManager.instance.nowData.Karma <= -13 && DataManager.instance.nowData.Karma > -15) //Evil 5 
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[1];
        }
        else if (DataManager.instance.nowData.Karma <= -10 && DataManager.instance.nowData.Karma > -13) //Evil 4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[2];
        }
        else if (DataManager.instance.nowData.Karma <= -8 && DataManager.instance.nowData.Karma > -10) //Evil 3 
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[3];
        }
        else if (DataManager.instance.nowData.Karma <= -5 && DataManager.instance.nowData.Karma > -8) //Evil 2 
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[4];
        }
        else if (DataManager.instance.nowData.Karma <= -2 && DataManager.instance.nowData.Karma > -5) //Evil 1  -2 -3 -4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[5];
        }
        else if (DataManager.instance.nowData.Karma <= 1 && DataManager.instance.nowData.Karma > -2) //Neutral -1 0 1
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[6];
        }
        else if (DataManager.instance.nowData.Karma <= 3 && DataManager.instance.nowData.Karma > 1) //Kind 1  -2 -3 -4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[7];
        }
        else if (DataManager.instance.nowData.Karma <= 5 && DataManager.instance.nowData.Karma > 3) //Kind 2  -2 -3 -4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[8];
        }
        else if (DataManager.instance.nowData.Karma <= 8 && DataManager.instance.nowData.Karma > 5) //Kind 3  -2 -3 -4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[9];
        }
        else if (DataManager.instance.nowData.Karma <= 11 && DataManager.instance.nowData.Karma > 8) //Kind 4  -2 -3 -4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[10];
        }
        else if (DataManager.instance.nowData.Karma <= 14 && DataManager.instance.nowData.Karma > 11) //Kind 5  -2 -3 -4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[11];
        }
        else if (DataManager.instance.nowData.Karma > 14) //Kind 1  -2 -3 -4
        {
            karmaIcon.GetComponent<Image>().sprite = KarmaIcons[12];
        }
    }
    public void CustomerSet() //손님은 하루에 5명 2명손님 1명 상인 2명손님
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
                int j = Random.Range(0, 4); //4명의 일반 고객중에 뽑기
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
        int plantNum = Random.Range(4, 7); //4~6종류 
        for (int i = 0;  i < 9; i++)
        {
            if (i < plantNum)
            {
                GardenPlants[i].SetActive(true);
                GardenPlants[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                GardenPlants[i].SetActive(false);
            }
        }
    }
    private void IgnoreCollision()
    {
        Physics2D.IgnoreCollision(whole.GetComponent<BoxCollider2D>(), potion.GetComponent<CircleCollider2D>());
    }
    public void ArrowRight()
    {
        SoundManager.instance.PlayEffect("btn");
        if (currentRoom == Room.Main) currentRoom = Room.Garden;
        else if (currentRoom == Room.Customer) currentRoom = Room.Main;
        movePos = movePos + new Vector3(17.8f, 0, 0);
        ChangeBtn();
    }
    public void ArrowLeft()
    {
        SoundManager.instance.PlayEffect("btn");
        if (currentRoom == Room.Main) currentRoom = Room.Customer;
        else if (currentRoom == Room.Garden) currentRoom = Room.Main;
        movePos = movePos - new Vector3(17.8f, 0, 0);
        ChangeBtn();
    }
    public void ArrowUp()
    {
        SoundManager.instance.PlayEffect("btn");
        if (currentRoom == Room.Main) currentRoom = Room.Bed;
        else if (currentRoom == Room.Machine) currentRoom = Room.Main;
        movePos = movePos + new Vector3(0, 10, 0);
        ChangeBtn();
    }
    public void ArrowDown()
    {
        SoundManager.instance.PlayEffect("btn");
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
