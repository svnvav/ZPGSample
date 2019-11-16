namespace Svnvav.Samples
{
    [System.Serializable]
        public struct AnimalSpawnConfiguration
        {
            public CreatureFactory factory;
            public float searchFoodRadius;
            public float moveSpeed;
            public float maxAge;
            public float maxHunger;
            public float maxHealth;
        }
}