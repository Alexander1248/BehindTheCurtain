using UnityEngine;

namespace NPCs
{
    public class CitySpawnNPC : NPC
    {
        public GameObject house;
        public GameObject monsters;
        public GameObject fire;
        public AudioSource scream;
        
        protected override void OnTextLine(int line)
        {
            switch (line)
            {
                case 0:
                    Time.timeScale = 0;
                    break;
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

        protected override void OnDialogEnd()
        {
            Time.timeScale = 1;
        }
    }
}