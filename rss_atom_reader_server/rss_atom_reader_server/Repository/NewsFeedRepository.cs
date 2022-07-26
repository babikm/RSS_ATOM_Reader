using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using rss_atom_reader_server.DataBaseModels;
using rss_atom_reader_server.IRepository;
using rss_atom_reader_server.Models;
using Remotion.Linq.Clauses;
using System.Text.RegularExpressions;

namespace rss_atom_reader_server.Repository
{
    public class NewsFeedRepository : INewsFeedRepository
    {
        private readonly ObjectContext _context = null;
        List<NewsFeed> feed = new List<NewsFeed>();

        public NewsFeedRepository(IOptions<Settings> settings)
        {
            _context = new ObjectContext(settings);
        }

        protected string regexImg(string source)
        {
            var reg1 = new Regex("src=(?:\"|\')?(?<imgSrc>[^>]*[^/].(?:jpg|bmp|gif|png))( ?:\"|\')?");
            var match1 = reg1.Match(source);
            if (match1.Success)
            {
                Uri UrlImage = new Uri(match1.Groups["imgSrc"].Value, UriKind.Absolute);
                return UrlImage.ToString();
            }
            else
            {
                return null;
            }
        }


        private static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }


        public void MappingXml(string RssFeedUrl)
        {
            try
            {

                XDocument xDoc = new XDocument();
                xDoc = XDocument.Load(RssFeedUrl);
                var news = (from x in xDoc.Descendants("item")
                    select new
                    {
                        title = x.Element("title").Value,
                        link = x.Element("link").Value,
                        pubDate = x.Element("pubDate").Value,
                        description = HtmlToPlainText(x.Element("description").Value),
                        image = regexImg(x.Element("description").Value),
                        category = x.Element("category").Value
                    });

                if (news != null)
                {
                    foreach (var item in news)
                    {
                        var dateTime = DateTime.Parse(item.pubDate);
                        var str = dateTime.ToString("dd/MM/yyyy HH:mm");

                        NewsFeed n = new NewsFeed
                        {
                            Title = item.title,
                            Link = item.link,
                            Description = item.description,
                            PublishDate = str,
                            Image = item.image,
                            Category = item.category
                        };

                        feed.Add(n);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public async Task Add()
        {
            MappingXml("http://fakty.interia.pl/polska/feed");
            MappingXml("http://fakty.interia.pl/swiat/feed");
            MappingXml("http://fakty.interia.pl/nauka/feed");
            await _context.NewsFeeds.InsertManyAsync(this.feed);
        }

        public async Task<IEnumerable<NewsFeed>> Get()
        {
            return await _context.NewsFeeds.Find(x => true).ToListAsync();
        }

        public async Task<DeleteResult> RemoveAll()
        {
            return await _context.NewsFeeds.DeleteManyAsync(new BsonDocument());
        }
    }
}
