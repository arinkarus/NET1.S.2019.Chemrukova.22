using BLL.Implementation.Entities;
using BLL.Interface;
using System;
using System.Collections.Generic;

namespace BLL.Implementation.Transformation
{
    /// <summary>
    /// Parses string and returns Url address.
    /// </summary>
    public class UrlParser : ITransformer<string, URLAddress>
    {
        /// <summary>
        /// Parses given string.
        /// </summary>
        /// <param name="source">Given string.</param>
        /// <returns>Url address instance.</returns>
        public URLAddress Transform(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException($"{nameof(source)} can't be null!");
            }

            var uri = new Uri(source, UriKind.Absolute);
            var host = this.GetHost(uri);
            var segments = this.GetSegments(uri);
            var parameters = this.GetParameters(uri);
            var urlAddress = new URLAddress(host, segments, parameters);
            return urlAddress;
        }

        private string GetHost(Uri uri)
        {
            return uri.Host;
        }

        private IEnumerable<string> GetSegments(Uri uri)
        {
            var segments = uri.Segments;
            foreach (var segment in segments)
            {
                if (segment != "/")
                {
                    if (segment[segment.Length - 1] == '/')
                    {
                        yield return segment.Substring(0, segment.Length - 1);
                    }
                    else
                    {
                        yield return segment;
                    }
                }
            }
        }

        private IDictionary<string, string> GetParameters(Uri uri)
        {
            var dictionary = new Dictionary<string, string>();
            var query = uri.Query;
            var parameters = query.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var parameter in parameters)
            {
                var parts = parameter.Split('=');
                var key = parts[0][0] == '?' ? parts[0].Substring(1) : parts[0];
                dictionary.Add(key, parts[1]);
            }

            return dictionary;
        }
    }
}
