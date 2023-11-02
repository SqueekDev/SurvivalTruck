using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public static class PlayerPrefsKeys
{
    public const string LevelNumberName = "Level";
    public const string PlayerDamage = "PlayerDamage";
    public const string CurrentCoinsCountName = "CurrentCoins";
    public const string TotalEarnedCoinsName = "TotalEarnedCoins";

}
public enum Savings
{
    [Description(PlayerPrefsKeys.LevelNumberName)]
    LevelNumberName,

    [Description(PlayerPrefsKeys.PlayerDamage)]
    PlayerDamage,

    [Description(PlayerPrefsKeys.CurrentCoinsCountName)]
    CurrentCoinsCountName,

    [Description(PlayerPrefsKeys.TotalEarnedCoinsName)]
    TotalEarnedCoinsName
}
