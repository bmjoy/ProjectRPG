using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TurnOrder
{
    private CombatController combatController;
    private List<Character> allCharacters = new List<Character>();

    public TurnOrder(List<Character> chars, CombatController combatController)
    {
        this.combatController = combatController;
        foreach (var character in chars.OrderByDescending(ch => ch.Stats.Haste))
        {
            allCharacters.Add(character);
        }
    }

    public void NextTurn()
    {
        var character = allCharacters[0];
        allCharacters.RemoveAt(0);
        allCharacters.Add(character);

        combatController.OnTurnOrderChanged?.Invoke(character);
    }

    public void Remove(Character character)
    {
        if (allCharacters.Contains(character))
            allCharacters.Remove(character);
    }
}
