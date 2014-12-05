using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;
using System.IO;


public class bbd : PhysicsGame
{
    //class inventory : Widget;
        //Inventory inventory = new Inventory();
        

    PlatformCharacter pelaaja1;

    Image pelaajanKuva = LoadImage("AAA");
    Image tahtiKuva = LoadImage("norsu2");
    Image kivihakku = LoadImage("kivihakku");
    Image puukuva = LoadImage("puunkuva");
    GameObject osoitinko;
    SoundEffect maaliAani = LoadSoundEffect("maali");
    DoubleMeter pelaaja1Elama;
    IntMeter janot;
    List<Widget> janokulu;
    Image multa = LoadImage("Multapalaeiruohoa");
    Image kivib = LoadImage("kivi");
    Image janokuplakuva = LoadImage("janokupla");
    Image osoitinkuva = LoadImage("cursor");
    string kentanNimi;


    public override void Begin()
    {
        MultiSelectWindow valikko = new MultiSelectWindow("Game Menu", "Single player", "Multiplayer", "Options", "Quit");
        valikko.AddItemHandler(0, aloita);
        valikko.AddItemHandler(1, multiplayer);
        valikko.AddItemHandler(3, Exit);
        Add(valikko);
        //LisaaNappaimet();
        janokulu = new List<Widget>();
    }

    void LataaKentta(ColorTileMap kentta)
    {
        
        //ColorTileMap kentta1 = ColorTileMap.FromLevelAsset("kentta1");
        kentta.SetTileMethod(Color.Brown, LisaaTaso);
        kentta.SetTileMethod(Color.Red, LisaaPelaaja);
        kentta.SetTileMethod(Color.Yellow, luokulta);
        kentta.SetTileMethod(Color.Black,luokivi);
        kentta.SetTileMethod(Color.Gray, luohiili);
        kentta.Optimize();
        kentta.Execute(40,40);
        osoitinko = new GameObject(40, 40);
        osoitinko.Color = Color.Red;
        osoitinko.Position = Mouse.PositionOnWorld;
        osoitinko.Image = osoitinkuva;
        Add(osoitinko);
        Mouse.ListenMovement(1.0, osoitin, null);

        
        //Level.CreateBorders();
        IsMouseVisible = true;
        Level.Background.CreateGradient(Color.SkyBlue, Color.SkyBlue);
    }

    void LisaaTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso.Image = multa;
        int noppa = RandomGen.NextInt(1,4);
        if(noppa ==1)
        {
            taso.Tag = "hed";
        }
        else
        {

    
        }
        taso.Position = paikka;
        taso.CollisionIgnoreGroup = 1;
        taso.Color = Color.Brown;
        Add(taso);
    }
    
    

    void LisaaPelaaja(Vector paikka, double leveys, double korkeus)
    {
        pelaaja1 = new PlatformCharacter(35,75);
        pelaaja1.Position = paikka;
        pelaaja1.Mass = 2.0;
        pelaaja1.Image = pelaajanKuva;
        Add(pelaaja1);
        pelaaja1Elama = new DoubleMeter(100);
        pelaaja1Elama.MaxValue = 100;
        pelaaja1Elama.LowerLimit += delegate { pelaaja1.Destroy(); };
        BarGauge pelaaja1ElamaPalkki = new BarGauge(20, Screen.Width / 3);
        pelaaja1ElamaPalkki.X = Screen.Center.X;
        pelaaja1ElamaPalkki.Y = Screen.Bottom + 30;
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

        Keyboard.Listen(Key.Left, ButtonState.Down, liikuvas, "Liikkuu vasemmalle");
        Keyboard.Listen(Key.Right, ButtonState.Down, Liikuoik, "Liikkuu vasemmalle");
        Keyboard.Listen(Key.Up, ButtonState.Down, Hyppää, "Pelaaja hyppää");

        ControllerOne.Listen(Button.Back, ButtonState.Pressed, Exit, "Poistu pelistä");

        
        Mouse.Listen(MouseButton.Left, ButtonState.Pressed, lkk, "");
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
    }

    void Liikuoik()
    {
        pelaaja1.Walk(390);
    }

    void Hyppää()
    {
        pelaaja1.Jump(390);
    }
    void liikuvas()
    {
        
        pelaaja1.Walk(-390);
    }
    void aloita()
    {
        MultiSelectWindow tasovalikko = new MultiSelectWindow("World Menu", "Create world", "Load world", "Options","Back");
        tasovalikko.AddItemHandler(0, LuoMaailma);
        tasovalikko.AddItemHandler(1, LataaMaailma);
        Add(tasovalikko);
    }
    void LataaMaailma()
    {
        //tähän filedialog ja sit nuo

        
        //lataakenttä funktiolle annetaan parametreiksi bitmapimage josta se lataa kentän
        //LisaaNappaimet();
        //Inventory inventory = new Inventory();
        //Add(inventory);
        /*
        foreach (PhysicsObject esine in esineet())
        {
            inventory.AddItem(esine, kivihakku);
            inventory.SelectItem(esine);
        }
        inventory.Y = Screen.Top - 20;

        int luku = RandomGen.NextInt(1, 200);
        luopuu(luku);
        //luopuu(new Vector kentanPiste = Level.GetRandomPosition());

        Camera.Zoom(1.5);
        //Camera.ZoomToLevel();
        Camera.Follow(pelaaja1);
         */
    }
    void multiplayer()
    {
        MultiSelectWindow mpl = new MultiSelectWindow("Multiplayer", "Callege", "Back");
        Add(mpl);
    }

    

    void LuoMaailma()
    {
        InputWindow kysymysIkkuna = new InputWindow("World name:");
        kysymysIkkuna.TextEntered += LuoUusiMaailma;
        Add(kysymysIkkuna);

    }
    void LuoUusiMaailma(InputWindow ikkuna)
    {
        kentanNimi = ikkuna.InputBox.Text + ".png";

        Gravity = new Vector(0, -900);
        LataaKentta(new ColorTileMap( generate(200, 60) ) );
        LisaaNappaimet();
        Inventory inventory = new Inventory();
        Add(inventory);
        
        foreach (PhysicsObject esine in esineet())
        {
            inventory.AddItem(esine, kivihakku);
            inventory.SelectItem(esine);
        }
        inventory.Y = Screen.Top - 20;

        int luku = RandomGen.NextInt(1, 200);
        luopuu(luku);
        //luopuu(new Vector kentanPiste = Level.GetRandomPosition());

        Camera.Zoom(1.5);
        //Camera.ZoomToLevel();
        Camera.Follow(pelaaja1);
        luoesinevalikko();
        luojano();
    }
    List<PhysicsObject> esineet()
    {
        List<PhysicsObject> esinelista = new List<PhysicsObject>();
        PhysicsObject kivihakku = new PhysicsObject(30,30);
        PhysicsObject kivilapio = new PhysicsObject(30, 30);
        esinelista.Add(kivihakku);
        esinelista.Add(kivilapio);
        
        return esinelista;
    }
    
    Image generate(int leveys, int korkeus)
    {
        Image kuva = new Image(leveys, korkeus, Color.White);
        // tallenna kuva
        int tasonkorkeus = korkeus/2;
        for (int x = 0; x < leveys; x++)
		{
            tasonkorkeus = tasonkorkeus + RandomGen.NextInt(-3, 3);
            if (tasonkorkeus < 1 * korkeus / 3)
            {
                tasonkorkeus = 1 * korkeus / 3;
            }
            if (tasonkorkeus >= korkeus)
            {
                tasonkorkeus = korkeus-1;
            }
            int multalkaa = RandomGen.NextInt(0, tasonkorkeus+1);

            for (int i = 0; i < multalkaa; i++)
            {
                kuva[(korkeus - 1) - i, x] = Color.Black;
            }
            for (int i = multalkaa; i < tasonkorkeus; i++)
            {    
                kuva[(korkeus-1)-i, x] = Color.Brown;
            }
		}
        kuva[2, 0] = Color.Red;
        // Oletetaan, että kenttä on muuttujassa: Image kentanKuva

        string tiedostonNimi =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, kentanNimi);
        
        Stream tallennusTiedosto = File.Create(tiedostonNimi);
        Stream kuvanTiedot = kuva.AsPng();
        kuvanTiedot.CopyTo(tallennusTiedosto);
        tallennusTiedosto.Close();

        return kuva;
    }
    void luokivi(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject kivi = PhysicsObject.CreateStaticObject(40, 40);
        kivi.Position = paikka;
        kivi.Image = kivib;

        kivi.Color = Color.Gray;
        kivi.CollisionIgnoreGroup = 1;
        Add(kivi);
    }
    void luovihollinen(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject vihu = new PhysicsObject(50, 50);
        vihu.Position = paikka;
        vihu.Image = tahtiKuva;
        Add(vihu);

    }
    void luopuu(double leveys)
    {
        PhysicsObject puu = new PhysicsObject(40, 120);
        puu.X = leveys;
        puu.Y = 0;
        puu.Image = puukuva;
        puu.CanRotate = false;

        //puu.IgnoresCollisionWith(pelaaja1);
        Add(puu);
    }
    void listaa()
    {
        //hakee tiedostojen nimet listaan
        //always I will find good to learn more
        //List<string> tiedo = FileManager.AsyncOperation.GetFileList(); //FileManager.AsyncOperation.GetFileList(); 
    }
    void osoitin(AnalogState ht) //ht=hiirentila
    {
        
    }
    protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
    {
        base.Update(gameTime);
        if (Mouse.PositionOnWorld != null && osoitinko != null)
        {
            Vector q = Mouse.PositionOnWorld;
            

            osoitinko.Position = q;
        }
    }
    void lkk()
    {
        luokivi(Mouse.PositionOnWorld, 0, 0);

    }
    void luojano()
    {
        HorizontalLayout asettelu = new HorizontalLayout();
        asettelu.Spacing = 10;

        Widget vedet = new Widget(asettelu);
        vedet.Color = Color.Transparent;
        vedet.X = Screen.Center.X;
        vedet.Y = Screen.Bottom + 120;
        Add(vedet);

        for (int i = 0; i < 10; i++)
        {
            Widget vesi = new Widget(30, 30, Shape.Circle);
            vesi.Color = Color.Red;
            vedet.Add(vesi);
            janokulu.Add(vesi);
            vesi.Image = janokuplakuva;
        }
        
        janot = new IntMeter(10,0,10);
        janot.LowerLimit += janooo;
        Timer janostin = new Timer();
        janostin.Interval = 39.0;
        janostin.Timeout += miinusjano;
        janostin.Start();
    }
    void janooo()
    {
        pelaaja1Elama.SetValue(pelaaja1Elama - 10);
        Timer.SingleShot(2.0, janoon);
    }
    void miinusjano()
    {
        janot.Value = janot.Value - 1;
        //int n=janokulu.Count-1;
        Widget p = janokulu[janot.Value];
        p.Destroy();

    }
    void janoon()
    {
        pelaaja1Elama.SetValue(pelaaja1Elama - 10);
        Timer.SingleShot(2.0, janooo);

    }
    void luohiili(Vector paikka,double leveys,double korkeus)
    {
        PhysicsObject hiili = PhysicsObject.CreateStaticObject(40, 40);
        hiili.Position = paikka;
        hiili.Color = Color.Black;
        hiili.CollisionIgnoreGroup = 1;
        Add(hiili);
    }
    void luokulta(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject kulta = PhysicsObject.CreateStaticObject(40, 40);
        kulta.Position = paikka;
        kulta.Color = Color.Yellow;
        kulta.CollisionIgnoreGroup = 1;
        Add(kulta);
    }
    void luovesi(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject vesipala = PhysicsObject.CreateStaticObject(40, 40);
        vesipala.Position = paikka;
        vesipala.Color = Color.Blue;
        vesipala.CollisionIgnoreGroup = 1;
        vesipala.IgnoresCollisionWith(pelaaja1);
        Add(vesipala);
    }
    void luoesinevalikko()
    {
        HorizontalLayout s = new HorizontalLayout();
        s.Spacing = 5;
        Widget v = new Widget(s);
        v.Color = Color.Brown;
        v.X = Screen.Center.X;
        v.Y = Screen.Bottom + 60;
        Add(v);
        for (int i = 0; i < 10; i++)
        {
            Widget n = new Widget(40, 40);
            n.Color = Color.LightGray;
            n.BorderColor = Color.Charcoal;
            v.Add(n);
            
        }
        Keyboard.Listen(Key.E, ButtonState.Pressed, kokoesinevalikko, "");
    }
    void kokoesinevalikko()
    {
        
        HorizontalLayout yu = new HorizontalLayout();
        yu.Spacing = 5;
        
        Widget n = new Widget(yu);
        n.X = Screen.Center.X-170;
        n.Y = Screen.Center.Y;
        n.Color = Color.Brown;
        Add(n);
        Widget yläosa = new Widget(yu);
        yläosa.X = Screen.Center.X;
        yläosa.Y = Screen.Center.Y-80;
        yläosa.Color = Color.Brown;
        Add(yläosa);
        Widget hahmokuva = new Widget(100, 200);
        hahmokuva.Color = Color.Red;
        hahmokuva.Image = pelaajanKuva;
        n.Add(hahmokuva);
        for (int i = 0; i < 10; i++)
        {
            Widget slot = new Widget(40, 40);
            slot.Color = Color.LightGray;
            slot.BorderColor = Color.Charcoal;
            yläosa.Add(slot);
            

        }
        Widget rivi2 = new Widget(yu);
        rivi2.X = Screen.Center.X;
        rivi2.Y = Screen.Center.Y - 130;
        rivi2.Color = Color.Brown;
        Add(rivi2);
        for (int i = 0; i < 10; i++)
        {
            Widget slot = new Widget(40, 40);
            slot.Color = Color.LightGray;
            slot.BorderColor = Color.Charcoal;
            rivi2.Add(slot);


        }
        Widget rivi3 = new Widget(yu);
        rivi3.X = Screen.Center.X;
        rivi3.Y = Screen.Center.Y - 180;
        rivi3.Color = Color.Brown;
        Add(rivi3);
        for (int i = 0; i < 10; i++)
        {
            Widget slot = new Widget(40, 40);
            slot.Color = Color.LightGray;
            slot.BorderColor = Color.Charcoal;
            rivi3.Add(slot);


        }
        Widget rivi4 = new Widget(yu);
        rivi4.X = Screen.Center.X;
        rivi4.Y = Screen.Center.Y - 180;
        rivi4.Color = Color.Brown;
        Add(rivi4);
        for (int i = 0; i < 10; i++)
        {
            Widget slot = new Widget(40, 40);
            slot.Color = Color.LightGray;
            slot.BorderColor = Color.Charcoal;
            rivi4.Add(slot);


        }

    }
}
