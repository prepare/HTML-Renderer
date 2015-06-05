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

        public static IDomElement Create(string nodeName)
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

        public static IDomCData CreateCData(string data)
        {
            return new DomCData();
        }
        public static IDomComment CreateComment(string content)
        {
            return new DomComment(content);
        }
        public static IDomText CreateTextNode(string text)
        {
            return new DomText(text);
        }
        public static IDomFragment CreateDomFragment()
        {
            return new Implementation.DomFragment();
        }


        public static IDomDocument CreateNewDoc()
        {
            return new Implementation.DomDocument();
        }
        public static IDomDocument CreateNewDoc(Engine.IDomIndex index)
        {
            return new Implementation.DomDocument(index);
        }
        public static IDomDocument Create(IEnumerable<IDomObject> elements,
          HtmlParsingMode parsingMode = HtmlParsingMode.Content,
          DocType docType = DocType.Default)
        {
            return DomDocument.Create(elements, parsingMode, docType);
        }
        /// <summary>
        /// Creates a new DomDocument (or derived) object.
        /// </summary>
        ///
        /// <param name="html">
        /// The HTML source for the document.
        /// </param>
        /// <param name="encoding">
        /// (optional) the character set encoding.
        /// </param>
        /// <param name="parsingMode">
        /// (optional) the HTML parsing mode.
        /// </param>
        /// <param name="parsingOptions">
        /// (optional) options for controlling the parsing.
        /// </param>
        /// <param name="docType">
        /// The DocType for this document.
        /// </param>
        ///
        /// <returns>
        /// A new IDomDocument object.
        /// </returns>
        public static IDomDocument Create(Stream html,
            Encoding encoding = null,
            HtmlParsingMode parsingMode = HtmlParsingMode.Content,
            HtmlParsingOptions parsingOptions = HtmlParsingOptions.Default,
            DocType docType = DocType.Default)
        {

            return ElementFactory.Create(html, encoding, parsingMode, parsingOptions, docType);
        }
        /// <summary>
        /// Creates a new DomDocument (or derived) object
        /// </summary>
        ///
        /// <param name="html">
        /// The HTML source for the document
        /// </param>
        /// <param name="parsingMode">
        /// (optional) the parsing mode.
        /// </param>
        /// <param name="parsingOptions">
        /// (optional) options for controlling the parsing.
        /// </param>
        /// <param name="docType">
        /// The DocType for this document.
        /// </param>
        ///
        /// <returns>
        /// A new IDomDocument object
        /// </returns>

        public static IDomDocument Create(string html,
            HtmlParsingMode parsingMode = HtmlParsingMode.Auto,
            HtmlParsingOptions parsingOptions = HtmlParsingOptions.Default,
            DocType docType = DocType.Default)
        {

            var encoding = Encoding.UTF8;
            using (var stream = new MemoryStream(encoding.GetBytes(html)))
            {
                return ElementFactory.Create(stream, encoding, parsingMode, parsingOptions, docType);
            }
        }
        /// <summary>
        /// Creates a new fragment in a given context.
        /// </summary>
        ///
        /// <param name="html">
        /// The elements.
        /// </param>
        /// <param name="context">
        /// (optional) the context. If omitted, will be automatically determined.
        /// </param>
        /// <param name="docType">
        /// (optional) type of the document.
        /// </param>
        ///
        /// <returns>
        /// A new fragment.
        /// </returns>

        public static IDomDocument CreateDocFragment(string html,
           string context = null,
           DocType docType = DocType.Default)
        {

            var factory = new ElementFactory();
            factory.FragmentContext = context;
            factory.HtmlParsingMode = HtmlParsingMode.Fragment;
            factory.HtmlParsingOptions = HtmlParsingOptions.AllowSelfClosingTags;
            factory.DocType = docType;

            Encoding encoding = Encoding.UTF8;
            using (var stream = new MemoryStream(encoding.GetBytes(html)))
            {
                return factory.Parse(stream, encoding);
            }
        } 
    }

    public static class DomEExt
    {

        /// <summary>
        /// Returns all child elements of a specific tag, cast to a type
        /// </summary>
        ///
        /// <typeparam name="T">
        /// Generic type parameter.
        /// </typeparam>
        /// <param name="nodeNameId">
        /// Backing field for NodeNameID property.
        /// </param>
        ///
        /// <returns>
        /// An enumerator.
        /// </returns>

        public static IEnumerable<T> ChildElementsOfTag<T>(this IDomElement elem, ushort nodeNameId)
        {
            return ChildElementsOfTag<T>(elem, nodeNameId);
        }

        private static IEnumerable<T> ChildElementsOfTag<T>(this IDomElement elem, IDomElement parent, ushort nodeNameId)
        {
            foreach (var el in elem.ChildNodes)
            {
                if (el.NodeType == NodeType.ELEMENT_NODE &&
                    el.NodeNameID == nodeNameId)
                {
                    yield return (T)el;
                }
                if (el.HasChildren)
                {
                    foreach (var child in ((DomElement)el).ChildElementsOfTag<T>(nodeNameId))
                    {
                        yield return child;
                    }
                }
            }
        }
    }
}