
namespace Svnvav.Samples
{
    public class Plant : Creature
    {
        
        private Effect[] _effects;

        private void OnEnable()
        {
            IsAlive = true;
            _effects = GetComponents<Effect>();
        }

        public override void GameUpdate()
        {
            
        }

        public void PassEffects(Animal animal)
        {
            foreach (var effect in _effects)
            {
                animal.TakeEffect(effect);
            }
        }
    }
}