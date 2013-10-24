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
        

    PlatformCharacter pelaaja1;

    Image pelaajanKuva = LoadImage("norsu");
    Image tahtiKuva = LoadImage("norsu2");

    SoundEffect maaliAani = LoadSoundEffect("maali");

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
        taso.Color = Color.Black;
        Add(taso);
    }

    

    void LisaaPelaaja(Vector paikka, double leveys, double korkeus)
    {
        pelaaja1 = new PlatformCharacter(90,90);
        pelaaja1.Position = paikka;
        pelaaja1.Mass = 2.0;
        pelaaja1.Image = pelaajanKuva;
        Add(pelaaja1);
    }

    void LisaaNappaimet()
    {
        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Näytä ohjeet");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        Keyboard.Listen(Key.Left, ButtonState.Down, Liikuta, "Liikkuu vasemmalle", pelaaja1, -250.0);
        Keyboard.Listen(Key.Right, ButtonState.Down, Liikuta, "Liikkuu vasemmalle", pelaaja1, 250.0);
        Keyboard.Listen(Key.Up, ButtonState.Down, Hyppaa, "Pelaaja hyppää", pelaaja1, 350.0);

        ControllerOne.Listen(Button.Back, ButtonState.Pressed, Exit, "Poistu pelistä");

        ControllerOne.Listen(Button.DPadLeft, ButtonState.Down, Liikuta, "Pelaaja liikkuu vasemmalle", pelaaja1, 250.0);
        ControllerOne.Listen(Button.DPadRight, ButtonState.Down, Liikuta, "Pelaaja liikkuu oikealle", pelaaja1, 250.0);
        ControllerOne.Listen(Button.A, ButtonState.Pressed, Hyppaa, "Pelaaja hyppää", pelaaja1, 350.0);

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
    }

    void Liikuta(PlatformCharacter pelaaja19, double nopeus)
    {
        pelaaja19.Walk(nopeus);
    }

    void Hyppaa(PlatformCharacter pelaaja1,Double nopeus)
    {
        pelaaja1.Jump(nopeus);
    }
    void aloita()
    {
        MultiSelectWindow tasovalikko = new MultiSelectWindow("Select level", "1", "2", "3", "4", "5", "6", "7", "8", "9");
        tasovalikko.AddItemHandler(0, taso1);
        Add(tasovalikko);
         
        
    }
    void taso1()
    {
        Gravity = new Vector(0, -900);

        LuoKentta();
        LisaaNappaimet();
        Inventory inventory = new Inventory();
        Add(inventory);

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
    
}