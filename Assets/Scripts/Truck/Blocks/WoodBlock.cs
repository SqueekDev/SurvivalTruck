using System.Collections.Generic;
using UnityEngine;

namespace Truck
{
    public class WoodBlock : MonoBehaviour
    {
        [SerializeField] private Obstacle _obstacle;
        [SerializeField] private Brick _brick;
        [SerializeField] private List<BrokenBrick> _brokenBricks;

        protected Obstacle Obstacle => _obstacle;

        protected virtual void OnEnable()
        {
            _obstacle.BlocksRepaired += OnBlocksRepaired;
        }

        protected virtual void OnDisable()
        {
            _obstacle.BlocksRepaired -= OnBlocksRepaired;
        }

        public void Destroy()
        {
            _brick.gameObject.SetActive(false);
            int brokenBrickNumber = Random.Range(0, _brokenBricks.Count);
            _brokenBricks[brokenBrickNumber].gameObject.SetActive(true);
        }

        private void OnBlocksRepaired()
        {
            foreach (var brokenBrick in _brokenBricks)
            {
                brokenBrick.gameObject.SetActive(false);
            }

            _brick.gameObject.SetActive(true);
        }
    }
}