using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SaveData
{
    [System.Serializable]
    public class SavePropertis
    {


        public class VideoSettings
        {
            public int ScreenResolution;

            public int ScreenModed;

            public int Quality;

            public int Framerate;

            public VideoSettings()
            {
                ScreenResolution = 2;

                Quality = 2;

                ScreenModed = 0;

                Framerate = 0;
            }
        }
        public class InputSettings
        {
            public string Forward;

            public string Backward;

            public string Left;

            public string Right;

            public string Attack;

            public string ActiveItem;

            public InputSettings()
            {
                Forward = "w";

                Backward = "s";

                Left = "a";

                Right = "d";

                Attack = "f";

                ActiveItem = "space";
            }
        }

            public class AudioSettings
            {
                public float MusicVolume;
                public float SoundVolume;
                public AudioSettings()
                {
                    MusicVolume = 0;
                    SoundVolume = 0;
                }
          
        }
    }
}
  