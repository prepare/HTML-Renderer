﻿using System;
using System.Collections.Generic;

using System.Text; 
namespace CsQuery.Engine
{
    /// <summary>
    /// Interface for a DOM index that is queryable.
    /// </summary>

    public interface IDomIndexSimple
    {
        /// <summary>
        /// Queries the index.
        /// </summary>
        ///
        /// <param name="subKey">
        /// The sub key.
        /// </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process query index in this collection.
        /// </returns>

        IEnumerable<IDomObject> QueryIndex(ushort[] subKey);

    }
}
