using System;
using System.Collections.Generic;
using Tao.Sdl;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    class Audio
    {
        List<IntPtr> audios;


        public Audio()
        {

            SdlMixer.Mix_OpenAudio(44100, (short)SdlMixer.MIX_DEFAULT_FORMAT, 2, 4096);
            audios = new List<IntPtr>();
            audios.Add(SdlMixer.Mix_LoadMUS("musica.wav"));
            audios.Add(SdlMixer.Mix_LoadMUS("sound.wav"));
        }

        public void PlayMusica()
        {
            SdlMixer.Mix_PlayMusic(audios[0], -1);
        }

        public void StopMusica()
        {
            SdlMixer.Mix_PauseMusic();
        }
    }
}
