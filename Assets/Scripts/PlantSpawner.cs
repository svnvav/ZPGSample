using UnityEngine;

namespace Svnvav.Samples
{
    public class PlantSpawner : Spawner
    {
        [SerializeField] private CreatureFactory factory;

        [SerializeField] private int CreatureIndex;

        [SerializeField] private Terrain _terrain;
        
        public override void Spawn()
        {
            Plant plant = factory.Get<Plant>(CreatureIndex);
            
            var terrainWidth = _terrain.terrainData.size.x;
            var terrainLength = _terrain.terrainData.size.z;
            var terrainPosX = _terrain.transform.position.x;
            var terrainPosZ = _terrain.transform.position.z;
            
            var posx = Random.Range(terrainPosX, terrainPosX + terrainWidth);
            var posz = Random.Range(terrainPosZ, terrainPosZ + terrainLength);
            var posy = _terrain.SampleHeight(new Vector3(posx, 0, posz));

            plant.transform.position = new Vector3(posx, posy, posz);
            
        }
    }
}