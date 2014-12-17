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
    [Save] public DoubleMeter pelaaja1Elama;
    [Save] public IntMeter janot;
    [Save] public List<Widget> janokulu;
    Image multa = LoadImage("Multapalaeiruohoa");
    Image kivib = LoadImage("kivi");
    Image janokuplakuva = LoadImage("janokupla");
    Image osoitinkuva = LoadImage("cursor");
    [Save] public List<GameObject> esineet2;
    [Save] public GameObject selecteditem;
    [Save] public List<Widget> esineslotit;
    [Save] public int käsiselecteditem;
    [Save] public string kentanNimi;
    bool avattu = false;
    HorizontalLayout yu;
    VerticalLayout qq;
    Widget rivi2;
    Widget rivi3;
    Widget rivi4;
    Widget n;
    Widget yläosa;
    Widget varusteet;
    [Save] public List<GameObject> Equipment;
    int selausnumero;
    Image banaanikuva = LoadImage("banaanit");
    [Save] public List<Widget> käsitaso;
    Widget kehys;
    GameObject pelaajankäsi;
    Widget rasti;
    Widget vinpain;
    VerticalLayout lll;
    Image kuvaeavesi = LoadImage("vesi1eanpeli1");
    List<PhysicsObject> Vedikartta;
    [Save] public Image maailma1;
    List<string> kaamos = new List<string>();
    string niminimi;
    List<bool> asetukset;
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
        kentta.SetTileMethod(Color.Blue, luovesi);
        kentta.Optimize();
        kentta.Execute(40,40);
        osoitinko = new GameObject(40, 40);
        osoitinko.Color = Color.Red;
        osoitinko.Position = Mouse.PositionOnWorld;
        osoitinko.Image = osoitinkuva;
        Add(osoitinko);
        

        
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
        pelaajankäsi = new GameObject(20, 20);
        pelaaja1.Add(pelaajankäsi);
        pelaajankäsi.Color = Color.Red;
        pelaajankäsi.X = pelaajankäsi.X + 14;
        pelaajankäsi.Y = pelaajankäsi.Y - 5;
        
    }

    void LisaaNappaimet()
    {
        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Näytä ohjeet");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

        Keyboard.Listen(Key.Left, ButtonState.Down, liikuvas, "Liikkuu vasemmalle");
        Keyboard.Listen(Key.Right, ButtonState.Down, Liikuoik, "Liikkuu vasemmalle");
        Keyboard.Listen(Key.Up, ButtonState.Down, Hyppää, "Pelaaja hyppää");

        ControllerOne.Listen(Button.Back, ButtonState.Pressed, Exit, "Poistu pelistä");
        Keyboard.Listen(Key.A, ButtonState.Down, liikuvas, "");
        Keyboard.Listen(Key.D, ButtonState.Down, Liikuoik, "");
        Keyboard.Listen(Key.W, ButtonState.Down, Hyppää, "");
        Mouse.Listen(MouseButton.Left, ButtonState.Pressed, lkk, "");
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
    }

    void Liikuoik()
    {
        pelaaja1.Walk(290);
        
    }

    void Hyppää()
    {
        pelaaja1.Jump(290);
    }
    void liikuvas()
    {

        pelaaja1.Walk(-290);
        

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
        int num2=montamaailmaa();

        lll = new VerticalLayout();
        Widget tarjotin = new Widget(lll);
        Add(tarjotin);
        for (int i = 0; i > num2; i++)
        {
            Label tonttu1 = new Label(nykymaailma(i));
            tonttu1.Color = Color.LightGray;
            tarjotin.Add(tonttu1);
            Mouse.ListenOn(tonttu1, MouseButton.Left, ButtonState.Pressed, delegate { pelaa(i); }, "");

        }
        //for(int i=0; i> System.IO.
        
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

        kentanNimi = ikkuna.InputBox.Text;
        Vedikartta = new List<PhysicsObject>();
        Gravity = new Vector(0, -900);
        LataaKentta(new ColorTileMap( generate(200, 60,kentanNimi) ) );
        LisaaNappaimet();
        Inventory inventory = new Inventory();
        Add(inventory);
        yu = new HorizontalLayout();
        yu.Spacing = 5;
        qq = new VerticalLayout();
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
        
        luojano();
        esineet2 = new List<GameObject>();
        esineslotit = new List<Widget>();
        käsiselecteditem = 1;
        GameObject banaanit = new GameObject(35, 35);
        banaanit.Image = banaanikuva;
        esineet2.Add(banaanit);
        GameObject puuhakku = new GameObject(35, 35);
        puuhakku.Image = kivihakku;
        esineet2.Add(puuhakku);
        käsitaso = new List<Widget>();
        luoesinevalikko();
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
    
    Image generate(int leveys, int korkeus, string kn)
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
            int multalkaa = RandomGen.NextInt(0, tasonkorkeus);

            for (int i = 0; i < multalkaa; i++)
            {
                kuva[(korkeus - 1) - i, x] = Color.Black;
            }
            for (int i = multalkaa; i < tasonkorkeus; i++)
            {    
                kuva[(korkeus-1)-i, x] = Color.Brown;
            }
		}
        kuva[39, leveys/2] = Color.Red;
        for (int i=0; i <leveys; i++)
        {
            int pulppuava = i;
            int korpulp = RandomGen.NextInt(37, 40);
            bool onko = RandomGen.NextBool();
            
            if (onko == true && kuva[korpulp,pulppuava]!=Color.White)
            {
                kuva[korpulp, pulppuava] = Color.Blue;
               
                
            }


        }
        // Oletetaan, että kenttä on muuttujassa: Image kentanKuva

        //string tiedostonNimi =
        //  Path.Combine(AppDomain.CurrentDomain.BaseDirectory, kentanNimi);
        
        //Stream tallennusTiedosto = File.Create(tiedostonNimi);
        //Stream kuvanTiedot = kuva.AsPng();
        //kuvanTiedot.CopyTo(tallennusTiedosto);
        //tallennusTiedosto.Close();
        //saveworld(tiedostonNimi);
        //System.IO.File.WriteAllText(@"C:\Users\User\Documents\Visual Studio 2010\Projects\E_A999\trunk\bbd\bbd\bbdContent/maailmalista.txt",tiedostonNimi);
        maailma1 = kuva;
        listaa(kn);
        
        saveworld(kn);
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
    void listaa(string maaillmakokonaisuudennimi)
    {
        
        if(DataStorage.Exists("maailmat.xml"))
        {
            //AddFactory<List<string>>("", täytäkaamos);
            kaamos=DataStorage.Load<List<string>>(kaamos, "maailmat.xml");
        }
        
        kaamos.Add(maaillmakokonaisuudennimi);
        //listaa maailman maailmalistaan 
        DataStorage.Save<List<string>>(kaamos,"maailmat.xml");

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
        Vedikartta.Add(vesipala);
        //vesipala.IgnoresCollisionWith(pelaaja1 as PhysicsObject);
        Add(vesipala);
        vesipala.Image = kuvaeavesi;
        
        //Vector q = vesipala.Position;
        //q.X=q.X+40;
        //if (sana == "toinen")
        //{
        //    var s = GetObjectAt(q);
        //    if (s == null)
        //    {
        //        luovesi(q, 40, 40);

        //    }
        //    q.X = q.X - 80;
        //    s = GetObjectAt(q);
        //    if (s == null)
        //    {
        //        luovesi(q, 40, 40);
        //    }
        //}
    }
    void luoesinevalikko()
    {
        
        selausnumero = 0;
        HorizontalLayout s = new HorizontalLayout();
        s.Spacing = 5;
        Widget v = new Widget(s);
        v.Color = Color.Cyan;
        v.X = Screen.Center.X;
        v.Y = Screen.Bottom + 60;
        Add(v);
        for (int i = 0; i < 9; i++)
        {
            Widget slot = new Widget(40, 40);
            slot.Color = Color.LightGray;
            slot.BorderColor = Color.Charcoal;
            v.Add(slot);
            if (selausnumero < esineet2.Count)
            {
                GameObject väliolio = esineet2[selausnumero];
                slot.Add(väliolio);
                esineslotit.Add(slot);
                väliolio.Color = Color.Blue;
                slot.Image = väliolio.Image;
                käsitaso.Add(slot);
                if (käsiselecteditem == selausnumero)
                {
                    valitseuusikäsiesine(selausnumero);
                   
                }
                selausnumero = selausnumero + 1;
                Keyboard.Listen(Key.D1, ButtonState.Pressed, delegate { valitseuusikäsiesine(1); }, "");
                Keyboard.Listen(Key.D2, ButtonState.Pressed, delegate { valitseuusikäsiesine(2); }, "");
                Keyboard.Listen(Key.D3, ButtonState.Pressed, delegate { valitseuusikäsiesine(3); }, "");
                Keyboard.Listen(Key.D4, ButtonState.Pressed, delegate { valitseuusikäsiesine(4); }, "");
                Keyboard.Listen(Key.D5, ButtonState.Pressed, delegate { valitseuusikäsiesine(5); }, "");
                Keyboard.Listen(Key.D6, ButtonState.Pressed, delegate { valitseuusikäsiesine(6); }, "");
                Keyboard.Listen(Key.D7, ButtonState.Pressed, delegate { valitseuusikäsiesine(7); }, "");
                Keyboard.Listen(Key.D8, ButtonState.Pressed, delegate { valitseuusikäsiesine(8); }, "");
                Keyboard.Listen(Key.D9, ButtonState.Pressed, delegate { valitseuusikäsiesine(9); }, "");
            }
        }
        Keyboard.Listen(Key.E, ButtonState.Pressed,kysykokoesinevalikkoa, "");
    }
    void kokoesinevalikko()
    {

        kehys = new Widget(yu);
        kehys.Color = Color.Cyan;
        kehys.X = Screen.Center.X;
        kehys.Y = Screen.Center.Y-50;
        Widget valikkokehys = new Widget(500, 400);
        valikkokehys.Color = Color.Cyan;
        
        Add(kehys);
        kehys.Add(valikkokehys);
        
        
        qq.Spacing = 5;
        varusteet = new Widget(qq);
        varusteet.X = Screen.Center.X-90;
        varusteet.Y = Screen.Center.Y;
        varusteet.Color = Color.Cyan;
        Add(varusteet);
        Widget kypärä = new Widget(40,40);
        Widget paita = new Widget(40, 40);
        Widget housut = new Widget(40, 40);
        Widget kengät = new Widget(40, 40);
        kypärä.Color = Color.LightGray;
        kypärä.BorderColor = Color.Charcoal;
        paita.Color = Color.LightGray;
        paita.BorderColor = Color.Charcoal;
        housut.Color = Color.LightGray;
        housut.BorderColor = Color.Charcoal;
        kengät.Color = Color.LightGray;
        kengät.BorderColor = Color.Charcoal;
        varusteet.Add(kypärä);
        varusteet.Add(paita);
        varusteet.Add(housut);
        varusteet.Add(kengät);

        n = new Widget(yu);
        n.X = Screen.Center.X-170;
        n.Y = Screen.Center.Y;
        n.Color = Color.Cyan;
        Add(n);
        yläosa = new Widget(yu);
        yläosa.X = Screen.Center.X;
        yläosa.Y = Screen.Center.Y-140;
        yläosa.Color = Color.LightGray;
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
        rivi2 = new Widget(yu);
        rivi2.X = Screen.Center.X;
        rivi2.Y = Screen.Center.Y - 130;
        rivi2.Color = Color.Cyan;
        Add(rivi2);
        for (int i = 0; i < 10; i++)
        {
            Widget slot = new Widget(40, 40);
            slot.Color = Color.LightGray;
            slot.BorderColor = Color.Charcoal;
            rivi2.Add(slot);


        }
        rivi3 = new Widget(yu);
        rivi3.X = Screen.Center.X;
        rivi3.Y = Screen.Center.Y - 180;
        rivi3.Color = Color.Cyan;
        Add(rivi3);
        for (int i = 0; i < 10; i++)
        {
            Widget slot = new Widget(40, 40);
            slot.Color = Color.LightGray;
            slot.BorderColor = Color.Charcoal;
            rivi3.Add(slot);


        }
        rivi4 = new Widget(yu);
        rivi4.X = Screen.Center.X;
        rivi4.Y = Screen.Center.Y - 180;
        rivi4.Color = Color.Cyan;
        Add(rivi4);
        for (int i = 0; i < 10; i++)
        {
            Widget slot = new Widget(40, 40);
            slot.Color = Color.LightGray;
            slot.BorderColor = Color.Charcoal;
            rivi4.Add(slot);


        }
        vinpain = new Widget(yu);
        vinpain.X = Screen.Center.X + 210;
        vinpain.Y = Screen.Center.Y + 120;
        Add(vinpain);
        rasti = new Widget(40, 40);
        rasti.X = Screen.Center.X + 210;
        rasti.Y = Screen.Center.Y + 120;
        rasti.Color = Color.Red;
        vinpain.Add(rasti);
        Mouse.ListenOn(rasti, MouseButton.Left,ButtonState.Pressed, kysykokoesinevalikkoa,"");
    }
    void kysykokoesinevalikkoa()
    {
        if (avattu == true)
        {
            kehys.Destroy();

            vinpain.Destroy();

            varusteet.Destroy();

            
            n.Destroy();
            
            yläosa.Destroy();
            
            rivi2.Destroy();
            
            rivi3.Destroy();
            
            rivi4.Destroy();
            avattu=false;
            return;
        }
        avattu = true;
        kokoesinevalikko();
    }
    void valitseuusikäsiesine(int numero)
    {


        numero = numero - 1;
        if (numero < käsitaso.Count)
        {

            käsitaso[numero].BorderColor = Color.Red;
            käsitaso[numero].Color = Color.Green;
            pelaajankäsi.Image = käsitaso[numero].Image;
            
        }
            
        
    }
    void saveworld(string nimi)
    {
        string mj = nimi + "w1" + ".xml";
        DataStorage.Save<Image>(maailma1, mj);
        string mj2 = nimi + "stg" + ".xml";
        DataStorage.Save<List<bool>>(asetukset, mj2);
        //bookmark
        //
        //
        //
        //
        Timer.SingleShot(39.0, delegate { saveworld(nimi); });
    }
    void equip(Image varusteenkuva, GameObject varusteitse)
    {
        
        GameObject varusteee = varusteitse;
        varusteee.Image = varusteenkuva;
        pelaaja1.Add(varusteee);

    }
    int montamaailmaa()
    {
        kaamos=DataStorage.Load<List<string>>(kaamos, "maailmat.xml");
        int num1 = kaamos.Count;
        return num1;
    }
    string nykymaailma(int f)
    {
        string qff = kaamos[f].Remove(kaamos[f].IndexOf("."));
        return qff;
    }
    void pelaa(int num3)
    {
        LoadGame(kaamos[num3]);
        ColorTileMap maailma1ilmentymä = new ColorTileMap(maailma1);
        LataaKentta(maailma1ilmentymä);
        //Vedikartta = new List<PhysicsObject>();
        Gravity = new Vector(0, -900);
        //LataaKentta(new ColorTileMap(generate(200, 60, kentanNimi)));
        LisaaNappaimet();
        //Inventory inventory = new Inventory();
        //Add(inventory);
        yu = new HorizontalLayout();
        yu.Spacing = 5;
        qq = new VerticalLayout();
        //foreach (PhysicsObject esine in esineet())
        //{
        //    inventory.AddItem(esine, kivihakku);
        //    inventory.SelectItem(esine);
        //}
        //inventory.Y = Screen.Top - 20;

        int luku = RandomGen.NextInt(1, 200);
        luopuu(luku);
        ////luopuu(new Vector kentanPiste = Level.GetRandomPosition());

        Camera.Zoom(1.5);
        ////Camera.ZoomToLevel();
        Camera.Follow(pelaaja1);

        luojano();
        esineet2 = new List<GameObject>();
        esineslotit = new List<Widget>();
        käsiselecteditem = 1;
        GameObject banaanit = new GameObject(35, 35);
        banaanit.Image = banaanikuva;
        esineet2.Add(banaanit);
        GameObject puuhakku = new GameObject(35, 35);
        puuhakku.Image = kivihakku;
        esineet2.Add(puuhakku);
        käsitaso = new List<Widget>();
        luoesinevalikko();
    }
    List<string> täytäkaamos()
    {
        List<string> maa_ilma = kaamos;
        return maa_ilma;
    }
    int muutaluvuksi(List<string> tekstil)
    {
        List<string> dd = new List<string>();
        for (char c = 'A'; c <= 'Z'; ++c)
        {
            dd.Add(c.ToString());
        }
        for (int i = 0; i < tekstil.Count; i++)
        {
            string tekstipä = tekstil[i];
            
        }
        

        int vaihdettu = 0;
        for (int i =0; i<teksti.Length;i++)
        {

        }
        return vaihdettu;
    }
}
