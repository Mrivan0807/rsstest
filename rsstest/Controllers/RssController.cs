using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace YourNamespace.Controllers
{
    public class RssController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string rssFeedUrl = "https://tw.stock.yahoo.com/rss?category=tw-market";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(rssFeedUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // 读取 RSS 内容
                        string rssContent = await response.Content.ReadAsStringAsync();

                        // 将 RSS 内容解析为 SyndicationFeed 对象
                        using (var stringReader = new StringReader(rssContent))
                        {
                            using (var xmlReader = XmlReader.Create(stringReader))
                            {
                                var feed = SyndicationFeed.Load(xmlReader);

                                // 将 SyndicationFeed 对象转换为 JSON 字符串
                                var options = new JsonSerializerOptions
                                {
                                    WriteIndented = true // 若要排版輸出的 JSON，可設置為 true
                                };
                                string jsonData = JsonSerializer.Serialize(feed, options);

                                // 返回 JSON 格式的 RSS 資料
                                return Content(jsonData, "application/json");
                            }
                        }
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode); // 如果請求失敗，返回相應的狀態碼
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message); // 如果發生異常，返回500錯誤並提供錯誤消息
                }
            }
        }
    }
}
