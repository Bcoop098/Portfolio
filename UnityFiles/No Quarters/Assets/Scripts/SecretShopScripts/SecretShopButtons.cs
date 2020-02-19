using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SecretShopButtons : MonoBehaviour
{
    [SerializeField]
    Text Muns;

    [SerializeField]
    int boostDamage = 50;
    [SerializeField]
    float boostSpeed = 3f;
    [SerializeField]
    int boostHealth = 125;

    static bool PoisionGot = false;
    static bool BreadGot = false;
    static bool SteerGot = false;
    static bool TicketGot = false;

    [SerializeField]
    Button poisionButton;
    [SerializeField]
    Button breadButton;
    [SerializeField]
    Button steerButton;
    [SerializeField]
    Button ticketButton;
    [SerializeField]
    Button leaveButton;

    EventSystem eventObj;

    public void Start()
    {
        eventObj = GameObject.FindObjectOfType<EventSystem>();
        Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").EnterTheShop();
        poisionButton.interactable = !PoisionGot;
        breadButton.interactable = !BreadGot;
        steerButton.interactable = !SteerGot;
        CheckTicket();
    }

    private void Update()
    {
        Muns.text = "Secret Coins: " + Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").GetSecretCoinTotal();
    }

    private void CheckTicket()
    {
        if (!TicketGot && PoisionGot && BreadGot && SteerGot)
            ticketButton.interactable = true;
        else
            ticketButton.interactable = false;
    }

    public void LeathalPoision()
    {
        if (!PoisionGot && Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").GetSecretCoinTotal() >= 2)
        {
            Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").ItemGot("Lethal Poision");
            Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").RemoveSecretShopCoins(2);
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setWeaponDamage(boostDamage, 0);
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setWeaponDamage(boostDamage, 1);
            PoisionGot = true;
            poisionButton.interactable = false;
            CheckTicket();
            ChangeButton();
        }
        //Increase DMG data
    }

    public void HeartyBread()
    {
        if (!BreadGot && Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").GetSecretCoinTotal() >= 2)
        {
            Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").ItemGot("Hearty Bread");
            Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").RemoveSecretShopCoins(2);
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setPlayerHealth(boostHealth, 0);
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setPlayerHealth(boostHealth, 1);
            BreadGot = true;
            breadButton.interactable = false;
            CheckTicket();
            ChangeButton();
        }
        //Increase HP data
    }

    public void CrimsonSteerPotion()
    {
        if (!SteerGot && Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").GetSecretCoinTotal() >= 2)
        {
            Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").ItemGot("Crimson Steer Potion");
            Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").RemoveSecretShopCoins(2);
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setMoveSpd(boostSpeed, 0);
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setMoveSpd(boostSpeed, 1);
            SteerGot = true;
            steerButton.interactable = false;
            CheckTicket();
            ChangeButton();
        }
        //Increase Speed Data
    }

    public void ParkingTicket()
    {
        if (!TicketGot && Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").GetSecretCoinTotal() >= 3)
        {
            Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").ItemGot("Parking Ticket");
            Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").RemoveSecretShopCoins(3);
            Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").ActivateLevel(9);
            TicketGot = true;
            ticketButton.interactable = false;
            ChangeButton();
        }
        //Unlock Final Level
    }

    public void LeaveShop()
    {
        Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").ExitTheShop();
        Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").ReturnToMainMenu();

    }

    void ChangeButton()
    {
        eventObj.SetSelectedGameObject(leaveButton.gameObject);
    }
}
