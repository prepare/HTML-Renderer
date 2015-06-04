﻿using System;
using System.Collections.Generic;

using System.Text;
using CsQuery.HtmlParser;
using CsQuery.Utility;

namespace CsQuery.Implementation
{
    /// <summary>
    /// An HTML progress element.
    /// </summary>

    class HTMLMeterElement : DomElement, IHTMLMeterElement
    {
        /// <summary>
        /// Default constructor.
        /// </summary>

        public HTMLMeterElement()
            : base(HtmlData.tagMETER)
        {
        }

        /// <summary>
        /// The value of the meter
        /// </summary>

        public new int Value
        {
            get
            {
                return Support3.IntOrZero(GetAttribute(HtmlData.ValueAttrId));
            }
            set
            {
                SetAttribute(HtmlData.ValueAttrId, value.ToString());
            }
        }

        /// <summary>
        /// The maximum value.
        /// </summary>

        public double Max
        {
            get
            {
                return Support3.DoubleOrZero(GetAttribute("max"));
            }
            set
            {
                SetAttribute("max", value.ToString());
            }
        }

        /// <summary>
        /// The minimum value.
        /// </summary>

        public double Min
        {
            get
            {
                return Support3.DoubleOrZero(GetAttribute("min"));
            }
            set
            {
                SetAttribute("min", value.ToString());
            }
        }
        /// <summary>
        /// The low value.
        /// </summary>

        public double Low
        {
            get
            {
                return Support3.DoubleOrZero(GetAttribute("low"));
            }
            set
            {
                SetAttribute("low", value.ToString());
            }
        }

        /// <summary>
        /// The high value.
        /// </summary>

        public double High
        {
            get
            {
                return Support3.DoubleOrZero(GetAttribute("high"));
            }
            set
            {
                SetAttribute("high", value.ToString());
            }
        }

        /// <summary>
        /// The optimum value.
        /// </summary>

        public double Optimum
        {
            get
            {
                return Support3.DoubleOrZero(GetAttribute("optimum"));
            }
            set
            {
                SetAttribute("optimum", value.ToString());
            }
        }


        /// <summary>
        /// A NodeList of all LABEL elements within this Progress element
        /// </summary>

        public INodeList<IDomElement> Labels
        {
            get
            {
                return new NodeList<IDomElement>(DomEExt.ChildElementsOfTag<IDomElement>(this, HtmlData.tagLABEL));
            }
        }



    }
}
