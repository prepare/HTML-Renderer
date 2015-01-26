﻿// 2015,2014 ,Apache2, WinterDev
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PixelFarm.Drawing;
using LayoutFarm.UI;
using LayoutFarm.ContentManagers;

namespace LayoutFarm
{
    [DemoNote("1.6 ScrollView")]
    class Demo_ScrollView : DemoBase
    {
        ImageContentManager imageContentMan = new ImageContentManager();
        protected override void OnStartDemo(SampleViewport viewport)
        {
            imageContentMan.ImageLoadingRequest += (s, e) =>
            {
                e.SetResultImage(LoadBitmap(e.ImagSource));
                e.ImageBinder.State = ImageBinderState.Loaded;
            };
            AddScrollView1(viewport, 0, 0);
            AddScrollView2(viewport, 250, 0);
        }
        void LazyImageLoad(ImageBinder binder)
        {
            //load here as need
            imageContentMan.AddRequestImage(binder);

        }
        void AddScrollView1(SampleViewport viewport, int x, int y)
        {
            var panel = new LayoutFarm.CustomWidgets.Panel(200, 175);
            panel.SetLocation(x + 30, y + 30);
            panel.BackColor = Color.LightGray;
            viewport.AddContent(panel);
            //-------------------------  
            {
                //vertical scrollbar
                var vscbar = new LayoutFarm.CustomWidgets.ScrollBar(15, 200);
                vscbar.SetLocation(x + 10, y + 10);
                vscbar.MinValue = 0;
                vscbar.MaxValue = 170;
                vscbar.SmallChange = 20;
                viewport.AddContent(vscbar);
                //add relation between viewpanel and scroll bar 
                var scRelation = new LayoutFarm.CustomWidgets.ScrollingRelation(vscbar, panel);
            }
            //-------------------------  
            {
                //horizontal scrollbar
                var hscbar = new LayoutFarm.CustomWidgets.ScrollBar(200, 15);
                hscbar.ScrollBarType = CustomWidgets.ScrollBarType.Horizontal;
                hscbar.SetLocation(x + 30, y + 10);
                hscbar.MinValue = 0;
                hscbar.MaxValue = 170;
                hscbar.SmallChange = 20;
                viewport.AddContent(hscbar);
                //add relation between viewpanel and scroll bar 
                var scRelation = new LayoutFarm.CustomWidgets.ScrollingRelation(hscbar, panel);
            }

            //add content to panel
            for (int i = 0; i < 10; ++i)
            {
                var box1 = new LayoutFarm.CustomWidgets.EaseBox(30, 30);
                box1.BackColor = Color.OrangeRed;
                box1.SetLocation(i * 20, i * 40);

                panel.AddChildBox(box1);
            }
            //--------------------------   

            panel.SetViewport(0, 0);
        }
        void AddScrollView2(SampleViewport viewport, int x, int y)
        {
            var panel = new LayoutFarm.CustomWidgets.Panel(400, 300);
            panel.SetLocation(x + 30, y + 30);
            panel.BackColor = Color.LightGray;
            panel.PanelLayoutKind = CustomWidgets.PanelLayoutKind.VerticalStack;
            viewport.AddContent(panel);

            //-------------------------  
            //load images...
            int lastY = 0;

            for (int i = 0; i < 5; ++i)
            {
                var imgbox = new LayoutFarm.CustomWidgets.ImageBox(36, 400);
                ClientImageBinder binder = new ClientImageBinder("../../images/0" + (i + 1) + ".jpg");
                binder.SetOwner(imgbox);
                binder.SetLazyFunc(LazyImageLoad);

                //if use lazy img load func
                //imageContentMan.AddRequestImage(new ImageContentRequest(binder, imgbox, imgbox));

                imgbox.ImageBinder = binder;
                imgbox.BackColor = Color.OrangeRed;
                imgbox.SetLocation(0, lastY);

                imgbox.MouseUp += (s, e) =>
                {
                    if (e.Button == UIMouseButtons.Right)
                    {
                        //test remove this imgbox on right mouse click
                        panel.RemoveChildBox(imgbox);
                    }

                };


                lastY += imgbox.Height + 5;
                panel.AddChildBox(imgbox);

            }
            //--------------------------
            //panel may need more 
            panel.SetViewport(0, 0);

            //-------------------------  
            {
                //vertical scrollbar
                var vscbar = new LayoutFarm.CustomWidgets.ScrollBar(15, 200);
                vscbar.SetLocation(x + 10, y + 10);
                vscbar.MinValue = 0;
                vscbar.MaxValue = lastY;
                vscbar.SmallChange = 20;
                viewport.AddContent(vscbar);
                //add relation between viewpanel and scroll bar 
                var scRelation = new LayoutFarm.CustomWidgets.ScrollingRelation(vscbar, panel);
            }
            //-------------------------  
            {
                //horizontal scrollbar
                var hscbar = new LayoutFarm.CustomWidgets.ScrollBar(300, 15);
                hscbar.ScrollBarType = CustomWidgets.ScrollBarType.Horizontal;
                hscbar.SetLocation(x + 30, y + 10);
                hscbar.MinValue = 0;
                hscbar.MaxValue = 170;
                hscbar.SmallChange = 20;
                viewport.AddContent(hscbar);
                //add relation between viewpanel and scroll bar 
                var scRelation = new LayoutFarm.CustomWidgets.ScrollingRelation(hscbar, panel);
            }
        }
        static Bitmap LoadBitmap(string filename)
        {
            System.Drawing.Bitmap gdiBmp = new System.Drawing.Bitmap(filename);
            Bitmap bmp = new Bitmap(gdiBmp.Width, gdiBmp.Height, gdiBmp);
            return bmp;
        }
    }
}