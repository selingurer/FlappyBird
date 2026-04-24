using Event;
using Event.Boosters;
using ScriptableObjects;
using Service;
using UnityEngine;
using VContainer;

namespace Booster
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] SpriteRenderer pickupImage;
        public BoosterType BoosterType ;

        [Inject] private IBoosterService _boosterService;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bird"))
            {
                _boosterService.Boost(BoosterType);
                EventBus<PickupActive>.Publish(new PickupActive
                {
                    Pickup = this
                });
            }
        }

        public void Initialize(BoosterType type, Sprite pickupSprite)
        {
           BoosterType = type;
           pickupImage.sprite = pickupSprite;
        }
    }
}