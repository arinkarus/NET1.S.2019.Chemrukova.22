using BLL.Interface.Logger;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Implementation.Logger;
using BLL.Interface;
using BLL.Implementation.Transformation;
using BLL.Implementation.Entities;
using BLL.Implementation;
using BLL.Interface.Validation;
using BLL.Implementation.Validation;
using System.Xml.Linq;
using BLL.Interface.Service;
using BLL.Implementation.Service;

namespace DependencyResolver
{
    public class ConfigModule: NinjectModule
    {
        public override void Load()
        {
            string sourceFilePath = Path.Combine(Directory.GetCurrentDirectory(), ConfigurationManager.AppSettings["sourceFilePath"]);
            string destinationFilePath = Path.Combine(Directory.GetCurrentDirectory(), ConfigurationManager.AppSettings["destinationFilePath"]);
            this.Bind<ILogger>().To<NLogLogger>();
            this.Bind<ITransformer<string, URLAddress>>().To<UrlParser>();
            this.Bind<IValidator<string>>().To<UrlAddressValidator>();
            this.Bind<ITransformer<IEnumerable<string>,
                IEnumerable<URLAddress>>>().To<FromUrlLinesTransformer>();
            this.Bind<ITransformer<IEnumerable<URLAddress>, XDocument>>().To<UrlAddressesToXmlTransformer>();
            this.Bind<IDataRetriever<IEnumerable<string>>>().To<UrlDataRetriever>().WithConstructorArgument("sourceFilePath", sourceFilePath);
            this.Bind<IDataPersister<XDocument>>().To<XmlDataPersister>().WithConstructorArgument("destinationFilePath", destinationFilePath);
            this.Bind<IDocumentService>().To<XmlDocumentService>();
        }
    }
}
