using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    /// <summary>
    /// Given data retriever.
    /// </summary>
    /// <typeparam name="T">Type of item that will be loaded.</typeparam>
    public interface IDataRetriever<out T>
    {
        T Load();
    }
}
