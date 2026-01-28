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
            while (delay > 0)
            {
                delay -= Time.deltaTime;
            }
        }
    }
}
