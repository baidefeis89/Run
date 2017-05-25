using Tao.Sdl;

namespace Run
{
    class Fondo:Sprite
    {

        public Fondo()
        {
            ancho = 2000;
            alto = 600;
            imagen = SdlImage.IMG_Load("img/BG.png");
            x = -600;
            y = 0;
        }

        

        
    }
}
