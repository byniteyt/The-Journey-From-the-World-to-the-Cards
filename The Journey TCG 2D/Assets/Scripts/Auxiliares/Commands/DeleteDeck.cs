using Auxiliares;
using UnityEngine;

public class DeleteDeck : ICommand
{
    BattleDeck delete;
    public DeleteDeck(BattleDeck deck)
    {
        delete = deck;
    }
    public void Execute()
    {
        if (delete != null)
        {
            Debug.Log($"Eliminando el mazo: {delete.name}");
            string detail = $"<b><color=red>Eliminar el mazo: {delete.name}</color></b>";
            Debug.Log(detail);
            EventManager.AddWarningDetails?.Invoke(this, detail);
        }
    }
}
