using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Text;
using System.IO;
using System.Diagnostics;
using CsQuery.StringScanner;
using CsQuery.HtmlParser;
using CsQuery.ExtensionMethods.Internal;
namespace CsQuery.Implementation
{
    //TODO: review here, should use builder, not static class
    public static class DomE
    {
        /// <summary>
        /// Creates a new element
        /// </summary>
        ///
        /// <param name="nodeName">
        /// The NodeName for the element (upper case).
        /// </param>
        ///
        /// <returns>
        /// A new element that inherits DomElement
        /// </returns>

        public static DomElement Create(string nodeName)
        {
            return Create(HtmlData.Tokenize(nodeName));
        }

        internal static DomElement Create(ushort nodeNameId)
        {

            switch (nodeNameId)
            {
                case HtmlData.tagA:
                    return new HtmlAnchorElement();
                case HtmlData.tagFORM:
                    return new HtmlFormElement();
                case HtmlData.tagBUTTON:
                    return new HTMLButtonElement();
                case HtmlData.tagINPUT:
                    return new HTMLInputElement();
                case HtmlData.tagLABEL:
                    return new HTMLLabelElement();
                case HtmlData.tagLI:
                    return new HTMLLIElement();
                case HtmlData.tagMETER:
                    return new HTMLMeterElement();
                case HtmlData.tagOPTION:
                    return new HTMLOptionElement();
                case HtmlData.tagPROGRESS:
                    return new HTMLProgressElement();
                case HtmlData.tagSELECT:
                    return new HTMLSelectElement();
                case HtmlData.tagTEXTAREA:
                    return new HTMLTextAreaElement();
                case HtmlData.tagSTYLE:
                    return new HTMLStyleElement();
                case HtmlData.tagSCRIPT:
                    return new HTMLScriptElement();
                default:
                    return new DomElement(nodeNameId);
            }
        }


    }


}