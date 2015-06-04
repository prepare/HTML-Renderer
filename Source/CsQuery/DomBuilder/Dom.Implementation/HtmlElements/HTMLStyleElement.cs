using System;
using System.Collections.Generic;

using System.Text;
using CsQuery.HtmlParser;

namespace CsQuery.Implementation
{
    /// <summary>
    /// A STYLE element
    /// </summary>

      class HTMLStyleElement : DomElement
    {
        /// <summary>
        /// Default constructor
        /// </summary>


        public HTMLStyleElement()
                : base(HtmlData.tagSTYLE)
            {

            }
        

    }
}
