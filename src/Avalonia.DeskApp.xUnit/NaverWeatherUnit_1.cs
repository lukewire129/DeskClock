using Avalonia.DeskApp.Models;
using HtmlAgilityPack;
using System.Xml.Linq;
using Xunit.Abstractions;
namespace Avalonia.DeskApp.xUnit
{
    public class NaverWeatherUnit_1
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HtmlNodeCollection list;
        public NaverWeatherUnit_1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;


            HtmlWeb web = new HtmlWeb ();

            HtmlDocument htmlDoc = web.Load ("https://search.naver.com/search.naver?sm=tab_hty.top&where=nexearch&ssc=tab.nx.all&query=%EA%B2%BD%EA%B8%B0%EB%8F%84+%ED%99%94%EC%84%B1%EC%8B%9C+%EB%82%A0%EC%94%A8&oquery=%EC%A0%81%EC%9A%A9&tqi=ilAERlpzL8wsstzuv4Vssssst40-202937");

            list = htmlDoc.DocumentNode.SelectNodes ("//li[@class='_li']");
        }

        [Fact]
        public void Test1()
        {
            foreach (HtmlNode node in list)
            {
                string time = node.Descendants ("dt").FirstOrDefault ().InnerText;
                string degree_point = node.Descendants ("dd").ToList ()[1].InnerText;
                var weatherstate = node.Descendants ("dd").ToList ()[0].InnerText;


                _testOutputHelper.WriteLine ("{0} : {1} {2}", time, degree_point, weatherstate);
            }
        }

        List<Weather> weatherlist = new List<Weather> ();

        [Fact]
        public void Test2()
        {
            var dateTime = DateTime.Now.ToString ("HH");

            //현재 시간부터 23시까지 데이터만 가공
            for(int i=0; i < 22 - Convert.ToInt32 (dateTime); i++)
            {
                weatherlist.Add (new Weather ()
                {
                    Time = Convert.ToInt32(list[i].Descendants ("dt").FirstOrDefault ().InnerText),
                    Temp = list[i].Descendants ("dd").ToList ()[1].InnerText,
                    State = list[i].Descendants ("dd").ToList ()[0].InnerText
                });

            }
        }

        [Theory]
        [InlineData (7, 10, 16,19)]
        public void 우산챙기세요(int hour1, int hour2, int hour3, int hour4)
        {
            weatherlist.Where (x => x.Time >= hour1 && x.Time <= hour2 ||
                               x.Time >=hour3 && x.Time<=hour4)
                       .Any(x=>x.State.Contains("비"));
        }
    }
}