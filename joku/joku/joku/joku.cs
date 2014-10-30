using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;

using Jypeli.Effects;
using Jypeli.Widgets;

public class joku : Game
{
    GameObject kynä;
    GameObject kakka;
    Image kumi = LoadImage("kumi");
    Vector kymppi;
    public override void Begin()
    {
        uus();
        luokynä();
        IsMouseVisible = true;
        kymppi = new Vector(10, 10);
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

    }
    void uus()
    {
        int korkeus = 0;
        int leveys = 0;
        Image rnh = new Image(korkeus, leveys, Color.White);
        GameObject alusta = new GameObject(Screen.Width, Screen.Height);
        Add(alusta,0);
    }
    void luokynä()
    {
        kynä = new GameObject(30, 30);
        kynä.Color = Color.Black;
        
        //kynä.Shape = Shape.Circle;
        kynä.Position = Mouse.PositionOnWorld;
        Add(kynä,1);
        Mouse.Listen(MouseButton.Left, ButtonState.Down, piirrä, "");
        Keyboard.Listen(Key.A, ButtonState.Pressed, punainen, "");
        Keyboard.Listen(Key.S, ButtonState.Pressed, sininen, "");
        Keyboard.Listen(Key.D, ButtonState.Pressed, vihreä, "");
        Keyboard.Listen(Key.F, ButtonState.Pressed, keltainen, "");
        Keyboard.Listen(Key.G, ButtonState.Pressed, musta, "");
        Keyboard.Listen(Key.H, ButtonState.Pressed, luokumi, "");
        Keyboard.Listen(Key.D1, ButtonState.Pressed, x1, "");
    }
    protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
    {
        base.Update(gameTime);
        kynä.Position = Mouse.PositionOnWorld;
    }
    void piirrä()
    {
        kakka = new GameObject(30, 30);
        kakka.Position=Mouse.PositionOnWorld;
        //kakka.Shape = Shape.Circle;
        kakka.Color = kynä.Color;
        kakka.Size = kynä.Size;
        //kakka.Shape = Shape.Circle;
        Add(kakka);
    }
    void punainen()
    {
        kynä.Color = Color.Red;
        kynä.Image = null;
    }
    void sininen()
    {
        kynä.Color = Color.Blue;
        kynä.Image = null;
    }
    void keltainen()
    {
        kynä.Color = Color.Yellow;
        kynä.Image = null;
    }
    void vihreä()
    {
        kynä.Color = Color.Green;
        kynä.Image = null;
    }
    void musta()
    {
        kynä.Color = Color.Black;
        kynä.Image = null;
    }
    void luokumi()
    {
        kynä.Color = Color.White;
        kynä.Image = kumi;
    }
    void x1()
    {
        kymppi.X = kymppi.X+10;
        kymppi.Y =kymppi.Y+10;
        kynä.Size =kymppi;
    }
    void x2()
    {
    }
    void x3()
    {
    }
    void x4()
    {
    }
    void x5()
    {
    }
    void x6()
    {
    }
    void x7()
    {
    }
    void x8()
    {
    }
    void x9()
    {
    }

}
