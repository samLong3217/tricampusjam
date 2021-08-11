using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private static MoneyManager _instance;
    private int _money;

    private void Awake()
    {
        Destroy(_instance);
        _instance = this;

        _money = Bootstrapper.Instance.MapData.StartMoney;
    }

    public static bool Spend(int amount)
    {
        if (_instance._money < amount) return false;
        _instance._money -= amount;
        return true;
    }

    public static void Earn(int amount)
    {
        _instance._money += amount;
    }

    public static int GetMoney()
    {
        return _instance._money;
    }
}
