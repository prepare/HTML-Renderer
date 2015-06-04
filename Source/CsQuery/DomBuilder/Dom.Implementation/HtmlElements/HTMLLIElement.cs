﻿using System;
using System.Collections.Generic;

using System.Text;
using CsQuery.HtmlParser;
using CsQuery.Utility;

namespace CsQuery.Implementation
{
    /// <summary>
    /// An HTML LI element.
    /// </summary>

    class HTMLLIElement : DomElement, IHTMLLIElement
    {
        /// <summary>
        /// Default constructor.
        /// </summary>

        public HTMLLIElement()
            : base(HtmlData.tagLI)
        {
        }

        /// <summary>
        /// The Value property of this LI element, or zero if it is not set.
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




    }
}
