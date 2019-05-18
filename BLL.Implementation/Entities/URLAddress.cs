using System;
using System.Collections.Generic;

namespace BLL.Implementation.Entities
{
    /// <summary>
    /// Url address class.
    /// </summary>
    public class URLAddress
    {
        /// <summary>
        /// / Initializes a new instance of the <see cref="URLAddress" /> class.
        /// </summary>
        /// <param name="host">Given host.</param>
        /// <param name="segments">Given host.></param>
        public URLAddress(string host, IEnumerable<string> segments): this(host)
        {
            if (segments != null)
            {
                this.Segments = new List<string>(segments);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="URLAddress" /> class.
        /// </summary>
        /// <param name="host">Given host.</param>
        public URLAddress(string host)
        {
            ValidateRequiredUrlParts(host);
            this.Host = host;
        }

        /// <summary>
        /// / Initializes a new instance of the <see cref="URLAddress" /> class.
        /// </summary>
        /// <param name="host">Given host.</param>
        /// <param name="segments">Given host.></param>
        /// <param name="parameters">Given parameters.</param>
        public URLAddress(string host, IEnumerable<string> segments,
             IDictionary<string, string> parameters): this(host, segments)
        {
            if (parameters != null)
            {
                this.Parameters = new Dictionary<string, string>(parameters);
            }
        }

        /// <summary>
        /// Gets parameters.
        /// </summary>
        public Dictionary<string, string> Parameters { get; private set; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets segments.
        /// </summary>
        public List<string> Segments { get; private set; } = new List<string>();

        /// <summary>
        /// Gets host.
        /// </summary>
        public string Host { get; private set; }

        private void ValidateRequiredUrlParts(string host)
        { 
            if (host == null)
            {
                throw new ArgumentNullException($"Host cannot be null {nameof(host)}");
            }

            if (host == string.Empty)
            {
                throw new ArgumentException($"Host cannot be empty {nameof(host)}");
            }
        }
    }
}
