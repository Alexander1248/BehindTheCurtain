using UnityEngine;

namespace NPCs
{
    public class CitySpawnNPC : SlowmoNPC
    {
        public GameObject house;
        public GameObject monsters;
        public GameObject fire;
        public AudioSource scream;
        
        protected override void OnTextLine(int line)
        {
            base.OnTextLine(line);
            switch (line)
            {
                case 2:
                    house.SetActive(true);
                    fire.SetActive(true);
                    scream.Play();
                    break;
                case 3:
                    monsters.SetActive(true);
                    break;
            }
            
        }
    }
}