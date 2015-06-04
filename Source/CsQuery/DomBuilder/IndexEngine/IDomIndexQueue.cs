using System;
using System.Collections.Generic; 
using System.Text; 

namespace CsQuery.Engine
{
    /// <summary>
    /// Interface for a DOM index that contains a Queue feature.
    /// </summary>
    public interface IDomIndexQueue
    {
        /// <summary>
        /// When true, changes are queued until the next read operation
        /// </summary>

        bool QueueChanges { get; set; }

    }
}
