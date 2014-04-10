using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class RainbowFly : PhysicsGame
{
    GameObject sateenkaari;
    PhysicsObject hahmo;
    Vector lanta;
    IntMeter pisteLaskuri;
    Image pv = LoadImage("pilvi");
    int pilviaLuotu = 0;

    
    public override void Begin()
    {
        
        hahmoluo();
        luoraha();
        luopilvi();
        LuoPistelaskuri();
        MediaPlayer.Play("savel");
        MediaPlayer.IsRepeating = true;

        Image bgImg = LoadImage("tausta");
        Level.Size = new Vector(bgImg.Width * 500, bgImg.Height);
        Level.Background.Image = LoadImage("tausta");
        Level.Background.TileToLevel();
        Level.Background.MoveTo(new Vector(-Screen.Width, 0), 100);
        
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Down, ButtonState.Down, liial,"",new Vector(0,-340));
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Down, ButtonState.Released,stopperi, "");
        Keyboard.Listen(Key.Up, ButtonState.Down, liial, "", new Vector(0,440));
        Keyboard.Listen(Key.Up, ButtonState.Released, stopperi, "");
    }
    void hahmoluo()
    {
        hahmo = new PhysicsObject(40, 65);
        hahmo.Shape = Shape.Circle;
        hahmo.Color = Color.Red;
        Add(hahmo);
        Camera.Follow(hahmo);
        Gravity = new Vector(200,0);
        hahmo.LinearDamping=0.996;
        hahmo.CanRotate = false;
        lanta = hahmo.Position;
        sateenkaari= new GameObject(1,65);
        Add(sateenkaari,-1);
        sateenkaari.Image = LoadImage("sateenkaari");
        
    }

    protected override void Update(Time time)
    {
        sateenkaari.X = (lanta.X + hahmo.X)/2;
        sateenkaari.Y = hahmo.Y;
        sateenkaari.Width = hahmo.X - lanta.X;
        base.Update(time);
    }

    void luoraha()
    {
        PhysicsObject raha=new PhysicsObject(20, 20);
        raha.Color = Color.Yellow;
        raha.Shape = Shape.Circle;
        //raha.Position = RandomGen.NextVector(100, 400);
        raha.Position = Level.GetRandomPosition();
        raha.IgnoresGravity = true;
        Add(raha);
        AddCollisionHandler(hahmo, raha, rahsaa);
        Timer.SingleShot(0.2, luoraha);
    }
    
    void liial(Vector suunta)
    {
        hahmo.Push(suunta);
    }
    void stopperi()
    {
        hahmo.Velocity = new Vector(
            hahmo.Velocity.X, 0 );
    }
    void rahsaa(PhysicsObject hahmo,PhysicsObject raha)
    {
        raha.Destroy();
        pisteLaskuri.Value += 100;
    }
    void LuoPistelaskuri()
    {
        pisteLaskuri = new IntMeter(0);

        Label pisteNaytto = new Label(100,100);
        pisteNaytto.X = Screen.Left + 100;
        pisteNaytto.Y = Screen.Top - 100;
        pisteNaytto.TextColor = Color.Black;
        pisteNaytto.Color = Color.White;
        pisteNaytto.TextScale = new Vector(2,2);

        pisteNaytto.BindTo(pisteLaskuri);
        Add(pisteNaytto);
    }
    void luopilvi()
    {
        pilviaLuotu=pilviaLuotu+1;
        PhysicsObject pilvi = new PhysicsObject(20, 20);
        pilvi.Image = (pv);
        pilvi.IgnoresGravity = true;
        pilvi.Position = Level.GetRandomPosition();
        pilvi.Size = pilvi.Size* pilviaLuotu/3.0;
        Add(pilvi);
        AddCollisionHandler(hahmo, pilvi, sähkö);
        Timer.SingleShot(0.5, luopilvi);
    }
    void sähkö(PhysicsObject hahmo, PhysicsObject pilvi)
    {
        ClearAll();
        
        MultiSelectWindow asd = new MultiSelectWindow("","uudelleen", "rageguittaa");
        asd.AddItemHandler(0, Begin);
        asd.AddItemHandler(1, ragegut);
        Add(asd);
    }
    void ragegut()
    {
        MultiSelectWindow v = new MultiSelectWindow("rageguit","Rageguit NOW!!!","takaisin");
        v.AddItemHandler(0, Exit);
        v.AddItemHandler(1, Begin);
        Add(v);

    }
}
