using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class bbd : PhysicsGame
{
    //class inventory : Widget;
        //Inventory inventory = new Inventory();
        

    PlatformCharacter2 pelaaja1;

    Image pelaajanKuva = LoadImage("norsu");
    Image tahtiKuva = LoadImage("norsu2");
    Image kivihakku = LoadImage("kivihakku");

    SoundEffect maaliAani = LoadSoundEffect("maali");
    DoubleMeter pelaaja1Elama;

    public override void Begin()
    {
        MultiSelectWindow valikko = new MultiSelectWindow("Game Menu", "Single player", "Multiplayer", "Options", "Quit");
        valikko.AddItemHandler(0, aloita);
        valikko.AddItemHandler(1, multiplayer);
        valikko.AddItemHandler(3, Exit);
        Add(valikko);
        //LisaaNappaimet();
        
        
        
       

    }

    void LuoKentta()
    {
        ColorTileMap kentta1 = ColorTileMap.FromLevelAsset("kentta1");
        kentta1.SetTileMethod(Color.Black, LisaaTaso);
        kentta1.SetTileMethod(Color.Red, LisaaPelaaja);
        kentta1.SetTileMethod(Color.Yellow, luovihollinen);
        kentta1.Optimize();
        kentta1.Execute(40,40);

        //Level.CreateBorders();

        Level.Background.CreateGradient(Color.SkyBlue, Color.SkyBlue);
    }

    void LisaaTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Position = paikka;
        taso.Color = Color.Brown;
        Add(taso);
    }

    

    void LisaaPelaaja(Vector paikka, double leveys, double korkeus)
    {
        pelaaja1 = new PlatformCharacter2(90,90);
        pelaaja1.Position = paikka;
        pelaaja1.Mass = 2.0;
        pelaaja1.Image = pelaajanKuva;
        Add(pelaaja1);
        pelaaja1Elama = new DoubleMeter(100);
        pelaaja1Elama.MaxValue = 100;
        BarGauge pelaaja1ElamaPalkki = new BarGauge(20, Screen.Width / 3);
        pelaaja1ElamaPalkki.X = Screen.Left + Screen.Width / 4;
        pelaaja1ElamaPalkki.Y = Screen.Top - 40;
        pelaaja1ElamaPalkki.Angle = Angle.FromDegrees(90);
        pelaaja1ElamaPalkki.BindTo(pelaaja1Elama);
        pelaaja1ElamaPalkki.Color = Color.Red;
        pelaaja1ElamaPalkki.BarColor = Color.Green;
        Add(pelaaja1ElamaPalkki);
        

        

    }

    void LisaaNappaimet()
    {
        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Näytä ohjeet");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        Keyboard.Listen(Key.Left, ButtonState.Down, Liikuta, "Liikkuu vasemmalle", pelaaja1, Direction.Left);
        Keyboard.Listen(Key.Right, ButtonState.Down, Liikuta, "Liikkuu vasemmalle", pelaaja1, Direction.Right);
        Keyboard.Listen(Key.Up, ButtonState.Down, Hyppaa, "Pelaaja hyppää", pelaaja1, 950.0);

        ControllerOne.Listen(Button.Back, ButtonState.Pressed, Exit, "Poistu pelistä");

        ControllerOne.Listen(Button.DPadLeft, ButtonState.Down, Liikuta, "Pelaaja liikkuu vasemmalle", pelaaja1, Direction.Left);
        ControllerOne.Listen(Button.DPadRight, ButtonState.Down, Liikuta, "Pelaaja liikkuu oikealle", pelaaja1, Direction.Right);
        ControllerOne.Listen(Button.A, ButtonState.Pressed, Hyppaa, "Pelaaja hyppää", pelaaja1, 350.0);

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
    }

    void Liikuta(PlatformCharacter2 pelaaja19, Direction nopeus)
    {
        pelaaja19.Walk(nopeus);
    }

    void Hyppaa(PlatformCharacter2 pelaaja1,Double nopeus)
    {
        pelaaja1.Jump(nopeus);
    }
    void aloita()
    {
        MultiSelectWindow tasovalikko = new MultiSelectWindow("World Menu", "Create world", "Load world", "Options","Back");
        tasovalikko.AddItemHandler(0, luomaailma);
        tasovalikko.AddItemHandler(1, taso1);
        Add(tasovalikko);
         
        
    }
    void taso1()
    {
        Gravity = new Vector(0, -900);

        LuoKentta();
        LisaaNappaimet();
        Inventory inventory = new Inventory();
        Add(inventory);
        foreach (PhysicsObject esine in esineet())
        {
            inventory.AddItem(esine, kivihakku);
            break;
        }
        

        Camera.Zoom(1.5);
        //Camera.ZoomToLevel();
        Camera.Follow(pelaaja1);
    }
    void multiplayer()
    {
        MultiSelectWindow mpl = new MultiSelectWindow("Multiplayer", "Callege", "Back");
        Add(mpl);
    }
    void callege(string ip, int port)
    {
             
    }
    void luovihollinen(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject vihu = new PhysicsObject(50, 50);
        vihu.Position = paikka;
        vihu.Image = tahtiKuva;
        Add(vihu);
        
    }
    void luomaailma()
    {
        InputWindow kysymysIkkuna = new InputWindow("World name:");
        kysymysIkkuna.TextEntered += ProcessInput;
        Add(kysymysIkkuna);

    }
    void ProcessInput(InputWindow ikkuna)
    {
        string vastaus = ikkuna.InputBox.Text;

    }
    List<PhysicsObject> esineet()
    {
        List<PhysicsObject> esinelista = new List<PhysicsObject>();
        PhysicsObject kivihakku = new PhysicsObject(30,30);
        PhysicsObject kivinuija = new PhysicsObject(30, 30);
        esinelista.Add(kivihakku);
        esinelista.Add(kivinuija);
        
        return esinelista;
    }
}