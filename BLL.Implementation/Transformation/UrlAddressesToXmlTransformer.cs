using BLL.Implementation.Entities;
using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BLL.Implementation.Transformation
{
    /// <summary>
    /// Transforms Url addresses to XDocument.
    /// </summary>
    public class UrlAddressesToXmlTransformer : ITransformer<IEnumerable<URLAddress>, XDocument>
    {
        /// <summary>
        /// Transforms Url addresses to XDocument.
        /// </summary>
        /// <param name="items">Given items.</param>
        /// <exception cref="ArgumentNullException">Thrown when items parameter is null.</exception>
        public XDocument Transform(IEnumerable<URLAddress> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException($"Url addresses can't be null {nameof(items)}");
            }

            XDocument xmlDocument = new XDocument(
              new XDeclaration("1.0", "utf-8", "no"),
                   new XElement("urlAddresses",
                       items.Select(item => new XElement("urlAddress",
                           new XElement("host", new XAttribute("name", item.Host)),
                                new XElement("uri", item.Segments.Select(i =>
                                    new XElement("segment", i))),
                                new XElement("parameters", item.Parameters.Select(p => new XElement("parameter",
                                        new XAttribute("key", p.Key),
                                        new XAttribute("value", p.Value)))
                       )))
                ));

            xmlDocument.Elements("urlAddresses").Elements("urlAddress").Where(x => x.Element("parameters").IsEmpty).
                AsParallel().ForAll(x => x.Element("parameters").Remove());
            xmlDocument.Save("dest.xml");
            return xmlDocument;
        }
    }
}
