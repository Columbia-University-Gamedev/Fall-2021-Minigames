using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController
{
    private static int tickets = 1000;

    // Start is called before the first frame update

    public static void SetTickets(int amount) {
        tickets = amount;
    }

    public static int GetTickets() {
        return tickets;
    }

    public static bool AddTickets(int amount) {
        if (amount < 0) return false;
        tickets += amount;
        return true;
    }

    public static bool SpendTickets(int amount) {
        if (amount > tickets) return false;
        tickets -= amount;
        return true;
    }

    public static bool CheckTickets(int amount) {
        return tickets >= amount;
    }

    // void OnPrintTickets() {
    //     Debug.Log(tickets);
    // }

    // void OnAddTickets() {
    //     AddTickets(10);
    // }

    // void OnRemoveTickets() {
    //     SpendTickets(3);
    // }
}
