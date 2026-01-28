using UnityEngine;
namespace Auxiliares
{
    public class WaiterCommand : ICommand
    {
        float delay;
        public WaiterCommand(float delay)
        {
            this.delay = delay;
        }
        public void Execute()
        {
            Debug.LogWarning($"WaiterCommand created to wait for {delay} seconds.");
            while (delay > 0)
            {
                delay -= Time.deltaTime;
            }
        }
    }
}
