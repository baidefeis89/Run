using System;
using System.Collections.Generic;
using Tao.Sdl;

namespace Run
{
    class Audio
    {
        private List<IntPtr> audios;


        public Audio()
        {
            SdlMixer.Mix_OpenAudio(8000, (short)SdlMixer.MIX_DEFAULT_FORMAT, 2, 4096);
            audios = new List<IntPtr>();
            audios.Add(SdlMixer.Mix_LoadMUS("musica.wav"));
        }

        /**
         * Reproduce la canción de fondo del juego
         * */
        public void PlayMusica()
        {
            SdlMixer.Mix_PlayMusic(audios[0], -1);
        }

        /**
         * Pausa la música de fondo
         * */
        public void StopMusica()
        {
            SdlMixer.Mix_PauseMusic();
        }
    }
}
