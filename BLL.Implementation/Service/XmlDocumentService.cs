using BLL.Implementation.Entities;
using BLL.Interface;
using BLL.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.Implementation.Service
{
    /// <summary>
    /// Service for processing creating XML file.
    /// </summary>
    public class XmlDocumentService: IDocumentService
    {
        /// <summary>
        /// Data persister instance.
        /// </summary>
        private readonly IDataPersister<XDocument> dataPersister;

        /// <summary>
        /// Data retriever instance.
        /// </summary>
        private readonly IDataRetriever<IEnumerable<string>> dataRetriever;

        /// <summary>
        /// Transformer from string to Url addresses model.
        /// </summary>
        private readonly ITransformer<IEnumerable<string>, IEnumerable<URLAddress>> fromStringToUrlsTransformer;

        /// <summary>
        /// Transformer from Url addresses to XDocument.
        /// </summary>
        private ITransformer<IEnumerable<URLAddress>, XDocument> fromUrlToXDocumentTransformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDocumentService" /> class.
        /// </summary>
        /// <param name="dataPersister">Given persister.</param>
        /// <param name="dataRetriever">Given retriever.</param>
        /// <param name="fromStringToUrlsTransformer">Given fromStringToUrlsTransformer.</param>
        /// <param name="fromUrlToXDocumentTransformer">Given fromUrlToXDocumentTransformer.</param>
        /// <exception cref="ArgumentNullException">Thrown when one of passed parameters is null.</exception>
        public XmlDocumentService(IDataPersister<XDocument> dataPersister, IDataRetriever<IEnumerable<string>> dataRetriever,
            ITransformer<IEnumerable<string>,
            IEnumerable<URLAddress>> fromStringToUrlsTransformer,
            ITransformer<IEnumerable<URLAddress>, XDocument> fromUrlToXDocumentTransformer)
        {
            this.dataPersister = dataPersister ?? throw new ArgumentNullException("Persister can't be null");
            this.dataRetriever = dataRetriever ?? throw new ArgumentNullException("Retriever can't be null");
            this.fromStringToUrlsTransformer = fromStringToUrlsTransformer ?? throw new ArgumentNullException("Deserializer can't be null");
            this.fromUrlToXDocumentTransformer = fromUrlToXDocumentTransformer ?? throw new ArgumentNullException("Serializer can't be null");
        }

        /// <summary>
        /// Processes creating Xml document.
        /// </summary>
        public void Process()
        {
            IEnumerable<string> stringValuesFromFile = dataRetriever.Load();
            IEnumerable<URLAddress> urls = fromStringToUrlsTransformer.Transform(stringValuesFromFile);
            XDocument xDocument = fromUrlToXDocumentTransformer.Transform(urls);
            dataPersister.Save(xDocument);
        }
    }
}
