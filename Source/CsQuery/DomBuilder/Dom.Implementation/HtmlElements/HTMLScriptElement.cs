using System;
using System.Collections.Generic;

using System.Text;
using CsQuery.HtmlParser;

namespace CsQuery.Implementation
{
    /// <summary>
    /// A SCRIPT
    /// </summary>

    class HTMLScriptElement : DomElement
    {
        /// <summary>
        /// Default constructor
        /// </summary>


        public HTMLScriptElement()
            : base(HtmlData.tagSCRIPT)
        {

        }


    }
}
