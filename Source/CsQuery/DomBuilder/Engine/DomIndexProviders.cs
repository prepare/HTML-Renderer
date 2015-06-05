using System;
using System.Collections.Generic;

using System.Text;
using CsQuery.Engine;

namespace CsQuery
{
    /// <summary>
    /// The default DomIndexProvider.
    /// </summary>

    public static class DomIndexProviders
    {
        /// <summary>
        /// Static constructor.
        /// </summary>

        static DomIndexProviders()
        {
            _RangedDomIndexProvider = new RangedDomIndexProvider();
            _SimpleDomIndexProvider = new SimpleDomIndexProvider();
            _NoDomIndexProvider = new NoDomIndexProvider();
        }

        static IDomIndexProvider _RangedDomIndexProvider;
        static IDomIndexProvider _SimpleDomIndexProvider;
        static IDomIndexProvider _NoDomIndexProvider;

        /// <summary>
        /// Return a SimpleDomIndex provider
        /// </summary>
        ///
        /// <returns>
        /// The DomIndex instance
        /// </returns>

        public static IDomIndexProvider Simple
        {
            get
            {
                return _SimpleDomIndexProvider;
            }
        }

        /// <summary>
        /// Returns a RangedDomIndex provider
        /// </summary>

        public static IDomIndexProvider Ranged
        {
            get
            {
                return _RangedDomIndexProvider;
            }
        }

        /// <summary>
        /// Returns a NoDomIndex provider
        /// </summary>

        public static IDomIndexProvider None
        {
            get
            {
                return _NoDomIndexProvider;
            }
        }
        public static Engine.IDomIndex CreateDomIndex(DomIndexKind indexKind)
        {
            switch (indexKind)
            {
                case DomIndexKind.None:
                    return new Engine.DomIndexNone();
                case DomIndexKind.Ranged:
                    return new Engine.DomIndexRanged();
                case DomIndexKind.Simple:
                    return new Engine.DomIndexNone();
                default:
                    return null;
            }
        }
    }
    public enum DomIndexKind
    {
        Ranged,
        Simple,
        None
    }

    /// <summary>
    ///  DomIndexProvider returning a SimpleDomIndex
    /// </summary>

    class SimpleDomIndexProvider : IDomIndexProvider
    {
        /// <summary>
        /// Return an instance of a DomIndex class.
        /// </summary>
        ///
        /// <returns>
        /// The DomIndex instance
        /// </returns>

        public IDomIndex GetDomIndex()
        {
            return new DomIndexSimple();
        }


    }

    /// <summary>
    /// DomIndexProvider returning a RangedDomIndex
    /// </summary>

    class RangedDomIndexProvider : IDomIndexProvider
    {
        /// <summary>
        /// Return an instance of a DomIndex class.
        /// </summary>
        ///
        /// <returns>
        /// The DomIndex instance
        /// </returns>

        public IDomIndex GetDomIndex()
        {
            return new DomIndexRanged();
        }
    }

    /// <summary>
    /// DomIndexProvider returning a RangedDomIndex
    /// </summary>

    class NoDomIndexProvider : IDomIndexProvider
    {
        /// <summary>
        /// Return an instance of a DomIndex class.
        /// </summary>
        ///
        /// <returns>
        /// The DomIndex instance
        /// </returns>

        public IDomIndex GetDomIndex()
        {
            return new DomIndexNone();
        }
    }
}
