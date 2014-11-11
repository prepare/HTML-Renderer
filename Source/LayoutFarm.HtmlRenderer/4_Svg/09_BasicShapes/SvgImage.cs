﻿//MS-PL, Apache2 
//2014, WinterDev

using System;
using LayoutFarm.Drawing;
using System.Collections.Generic;

using HtmlRenderer;
using HtmlRenderer.Css;
using LayoutFarm.SvgDom;

namespace LayoutFarm.SvgDom
{
    public class SvgImage : SvgVisualElement
    {
        SvgImageSpec imageSpec;

        Color strokeColor = Color.Transparent;
        Color fillColor = Color.Black;
        GraphicsPath _path;
        HtmlRenderer.Boxes.CssImageRun _imgRun;


        public SvgImage(SvgImageSpec spec, object controller)
            : base(controller)
        {
            this.imageSpec = spec;
            this._imgRun = new HtmlRenderer.Boxes.CssImageRun();
        }
        //----------------------------
        public float ActualX
        {
            get;
            set;
        }
        public float ActualY
        {
            get;
            set;
        }
        public float ActualWidth
        {
            get;
            set;
        }
        public float ActualHeight
        {
            get;
            set;
        }
        //----------------------------
        public ImageBinder ImageBinder
        {
            get { return this._imgRun.ImageBinder; }
            set { this._imgRun.ImageBinder = value; }
        }
        public override void ReEvaluateComputeValue(float containerW, float containerH, float emHeight)
        {
            var myspec = this.imageSpec;
            this.fillColor = myspec.ActualColor;
            this.strokeColor = myspec.StrokeColor;

            this.ActualX = ConvertToPx(myspec.X, containerW, emHeight);
            this.ActualY = ConvertToPx(myspec.Y, containerW, emHeight);
            this.ActualWidth = ConvertToPx(myspec.Width, containerW, emHeight);
            this.ActualHeight = ConvertToPx(myspec.Height, containerW, emHeight);
            this.ActualStrokeWidth = ConvertToPx(myspec.StrokeWidth, containerW, emHeight);
            this._path = CreateRectGraphicPath(
                    this.ActualX,
                    this.ActualY,
                    this.ActualWidth,
                    this.ActualHeight);
            if (this._imgRun.ImageBinder == null)
            {
                this._imgRun.ImageBinder = new ImageBinder(myspec.ImageSrc);
            }


        }
        public override void Paint(Painter p)
        {   
            
            IGraphics g = p.Gfx;
            if (fillColor != Color.Transparent)
            {
                using (SolidBrush sb = g.Platform.CreateSolidBrush(this.fillColor))
                {
                    g.FillPath(sb, this._path);
                }
            }
            //---------------------------------------------------------  
            if (this.ImageBinder != null)            
            {
                //---------------------------------------------------------  
                //Because we need external image resource , so ...
                //use render technique like CssBoxImage ****
                //---------------------------------------------------------  


                RectangleF r = new RectangleF(this.ActualX, this.ActualY, this.ActualWidth, this.ActualHeight);
                bool tryLoadOnce = false;

            EVAL_STATE:
                switch (this.ImageBinder.State)
                {
                    case ImageBinderState.Unload:
                        {
                            //async request image
                            if (!tryLoadOnce)
                            {

                                p.RequestImageAsync(_imgRun.ImageBinder, this._imgRun, this);
                                //retry again
                                tryLoadOnce = true;
                                goto EVAL_STATE;
                            }
                        } break;
                    case ImageBinderState.Loading:
                        {
                            //RenderUtils.DrawImageLoadingIcon(g, r);
                        } break;
                    case ImageBinderState.Loaded:
                        {

                            Image img;
                            if ((img = _imgRun.ImageBinder.Image) != null)
                            {

                                if (_imgRun.ImageRectangle == Rectangle.Empty)
                                {
                                    g.DrawImage(img, r);
                                }
                                else
                                {
                                    //
                                    g.DrawImage(img, _imgRun.ImageRectangle);
                                    //g.DrawImage(_imageWord.Image, Rectangle.Round(r), _imageWord.ImageRectangle);
                                }
                            }
                            else
                            {
                                RenderUtils.DrawImageLoadingIcon(g, r);
                                if (r.Width > 19 && r.Height > 19)
                                {
                                    g.DrawRectangle(Pens.LightGray, r.X, r.Y, r.Width, r.Height);
                                }
                            }
                        } break;
                    case ImageBinderState.NoImage:
                        {

                        } break;
                    case ImageBinderState.Error:
                        {
                            RenderUtils.DrawImageErrorIcon(g, r);
                        } break;
                }
            }
            //--------------------------------------------------------- 
            if (this.strokeColor != Color.Transparent
                && this.ActualStrokeWidth > 0)
            {
                using (SolidBrush sb = g.Platform.CreateSolidBrush(this.strokeColor))
                using (Pen pen = g.Platform.CreatePen(sb))
                {
                    pen.Width = this.ActualStrokeWidth;
                    g.DrawPath(pen, this._path);
                }
            }

        }
        static GraphicsPath CreateRectGraphicPath(float x, float y, float w, float h)
        {
            var _path = CurrentGraphicPlatform.CreateGraphicPath();
            _path.StartFigure();
            _path.AddRectangle(new RectangleF(x, y, w, h));
            _path.CloseFigure();
            return _path;
        }


    }

    public class SvgImageSpec : SvgVisualSpec
    {
        public CssLength X
        {
            get;
            set;
        }
        public CssLength Y
        {
            get;
            set;
        }
        public CssLength Width
        {
            get;
            set;
        }
        public CssLength Height
        {
            get;
            set;
        }

        public string ImageSrc
        {
            get;
            set;
        }
    }
}