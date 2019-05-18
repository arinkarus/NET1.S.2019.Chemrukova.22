using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    /// <summary>
    /// Transformer interface.
    /// </summary>
    /// <typeparam name="TSource">Type of source item.</typeparam>
    /// <typeparam name="TDest">Type of destination item.</typeparam>
    public interface ITransformer<in TSource, out TDest>
    {
        /// <summary>
        /// Transforms source to dest.
        /// </summary>
        /// <param name="source">Source item.</param>
        /// <returns>Destionation item.</returns>
        TDest Transform(TSource source);
    }
}
