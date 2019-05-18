using BLL.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.Implementation
{
    /// <summary>
    /// Saves XDocument to specified file. 
    /// </summary>
    public class XmlDataPersister: IDataPersister<XDocument>
    {
        /// <summary>
        /// Path of destination file.
        /// </summary>
        private readonly string destinationFilePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDataPersister"/> class.
        /// </summary>
        /// <param name="destinationFilePath">Path of source file.</param>
        /// <exception cref="ArgumentNullException">Thrown when source file path is null.</exception>
        /// <exception cref="ArgumentException">Thrown when file path is empty.</exception>
        public XmlDataPersister(string destinationFilePath)
        {
            this.destinationFilePath = destinationFilePath;
            if (destinationFilePath == null)
            {
                throw new ArgumentNullException($"Source file path cannot be null {nameof(destinationFilePath)}");
            }

            if (destinationFilePath == string.Empty)
            {
                throw new ArgumentException($"Source file path is empty! {nameof(destinationFilePath)}");
            }

            this.destinationFilePath = destinationFilePath;
        }

        /// <summary>
        /// Saves XDocument to file.
        /// </summary>
        /// <param name="item">Given item.</param>
        public void Save(XDocument item)
        {
            item.Save(this.destinationFilePath);
        }
    }
}
