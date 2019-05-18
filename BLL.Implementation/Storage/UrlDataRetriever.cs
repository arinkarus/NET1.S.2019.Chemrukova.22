using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;

namespace BLL.Implementation
{
    /// <summary>
    /// Uri data retriever.
    /// </summary>
    public class UrlDataRetriever: IDataRetriever<IEnumerable<string>>
    {
        /// <summary>
        /// Source file path.
        /// </summary>
        private readonly string sourceFilePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlDataRetriever"/> class.
        /// </summary>
        /// <param name="sourceFilePath">Path of source file.</param>
        /// <exception cref="ArgumentNullException">Thrown when source file path is null.</exception>
        /// <exception cref="ArgumentException">Thrown when file path is empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown when file not found.</exception>
        public UrlDataRetriever(string sourceFilePath)
        {
            if (sourceFilePath == null)
            {
                throw new ArgumentNullException($"Source file path cannot be null {nameof(sourceFilePath)}");
            }

            if (sourceFilePath == string.Empty)
            {
                throw new ArgumentException($"Source file path is empty! {nameof(sourceFilePath)}");
            }

            if (!File.Exists(sourceFilePath))
            {
                throw new FileNotFoundException($"File {nameof(sourceFilePath)} doesn't exist!");
            }

            this.sourceFilePath = sourceFilePath;
        }

        /// <summary>
        /// Returns values read by line from source file.
        /// </summary>
        /// <returns>Uri lines.</returns>
        public IEnumerable<string> Load()
        {
            using (var fileStream = File.OpenText(this.sourceFilePath))
            {
                string line;
                while ((line = fileStream.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
