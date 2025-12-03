using Auxiliares;
using UnityEngine;

public class DeleteDeck : ICommand
{
    Deck delete;
    public DeleteDeck(Deck deck)
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
