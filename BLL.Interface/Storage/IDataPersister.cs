using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    /// <summary>
    /// Data persiter interface.
    /// </summary>
    /// <typeparam name="T">Type of value that will be saved.</typeparam>
    public interface IDataPersister<in T>
    {
        /// <summary>
        /// Saves item.
        /// </summary>
        /// <param name="item">Given item.</param>
        void Save(T item);
    }
}
