using BLL.Implementation.Entities;
using BLL.Interface;
using BLL.Interface.Logger;
using BLL.Interface.Validation;
using System;
using System.Collections.Generic;

namespace BLL.Implementation
{
    /// <summary>
    /// Transforms lines to url addresses. 
    /// </summary>
    public class FromUrlLinesTransformer : ITransformer<IEnumerable<string>,
        IEnumerable<URLAddress>>
    {
        /// <summary>
        /// Logger instance.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Validator instance.
        /// </summary>
        private readonly IValidator<string> validator;

        /// <summary>
        /// Parser instance.
        /// </summary>
        private readonly ITransformer<string, URLAddress> parser;

        /// <summary>
        /// Initializes a new instance of the <see cref="FromUrlLinesTransformer" /> class.
        /// </summary>
        /// <param name="validator">Given validator.</param>
        /// <param name="parser">Given parser.</param>
        /// <param name="logger">Given logger.</param>
        public FromUrlLinesTransformer(IValidator<string> validator, ITransformer<string, URLAddress> parser, ILogger logger)
        {
            this.parser = parser ?? throw new ArgumentNullException($"Parses is null {nameof(parser)}");
            this.validator = validator ?? throw new ArgumentNullException($"Validator is null {nameof(validator)}");
            this.logger = logger ?? throw new ArgumentNullException($"Logger is null {nameof(logger)}");
            this.parser = parser;
            this.validator = validator;
            this.logger = logger;
        }

        /// <summary>
        /// Transforms strings to url addresses.
        /// </summary>
        /// <param name="source">Given string values.</param>
        /// <returns>Url addresses.</returns>
        /// <exception cref="ArgumentNullException">Thrown when source is null.</exception>
        public IEnumerable<URLAddress> Transform(IEnumerable<string> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"Souce is null: {nameof(source)}");
            }

            IEnumerable<URLAddress> GetUrlAddresses()
            {
                foreach (var urlString in source)
                {
                    if (validator.IsValid(urlString))
                    {
                        yield return parser.Transform(urlString);
                    }
                    else
                    {
                        this.logger.Error($"Can' parse {urlString}");
                    }
                }
            }

            return GetUrlAddresses();
        }
    }
}
