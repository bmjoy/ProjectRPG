using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class StatStrings
{
    private static Dictionary<StatTypes, string> statStrings = new Dictionary<StatTypes, string>()
    {
        {StatTypes.Armor, "Decreases damage taken" },
        {StatTypes.CritChance, "Chance to deal double damage!" },
        {StatTypes.Damage, "Increase damage dealt" },
        {StatTypes.Dexterity, "Increases crit chance" },
        {StatTypes.Haste, "Determines the turn order for fights" },
        {StatTypes.Health, "The amount of health you have" },
        {StatTypes.Intelligence, "Increases Haste" },
        {StatTypes.Stamina, "Increases total health" },
        {StatTypes.Strength, "Increases physical damage dealt" },
    };

    public static string GetStatTooltip(StatTypes statType)
    {
        return statStrings[statType];
    }
}
