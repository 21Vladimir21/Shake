using System;

[Serializable]
public class PlayerData
{
    public float PushStrenght;
    public float MaxDistance;
    public bool NoAds;
    public float Money;
    public int CurrentLevel;
    public Skin PlayerSkin;
    public int MoneyProgresion;
    // public JsonDateTime TimerStartTime;
    public PlayerData(PlayerStats stats, Currensy currensy, int level, Skin playerSkin, DateTime timeToSave, int moneyProgresion = 0)
    {
        PushStrenght = stats.PushStrength;
        MaxDistance = stats.MaxDistance;
        Money = currensy.CoinAmount;
        NoAds = stats.NoAds;
        PlayerSkin = playerSkin;
        CurrentLevel = level;
        MoneyProgresion = moneyProgresion;
#if !UNITY_EDITOR
        TimerStartTime = (JsonDateTime)timeToSave;
#endif
    }

    public PlayerData()
    {
        PushStrenght = 69;
        MaxDistance = 6.8f;
        Money = 0;
        CurrentLevel = 0;
        NoAds = false;
        #if !UNITY_EDITOR
        TimerStartTime = (JsonDateTime)DateTime.Now;
#endif
    }
}
