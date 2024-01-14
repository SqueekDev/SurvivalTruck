namespace Truck
{
    public class MiddleBlock : WoodBlock
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            Obstacle.MiddleBlockDestroyed += OnBlockDestroyed;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Obstacle.MiddleBlockDestroyed -= OnBlockDestroyed;
        }
    }
}
