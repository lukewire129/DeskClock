using Avalonia.DeskApp.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avalonia.DeskApp.Services;

public interface IWeatherService
{
    List<Weather> Realod();
    bool NeedUmbrella(int hour1, int hour2, int hour3, int hour4);
}

public class TestWeatherService : IWeatherService
{
    List<Weather> weatherlist = new List<Weather> ();
    public bool NeedUmbrella(int hour1, int hour2, int hour3, int hour4)
    {
        return true;
    }

    public List<Weather> Realod()
    {
        return new List<Weather> ()
        {
            new Weather()
            {
                Time = 11,
                State = "맑음",
                Temp ="5°"
            },
            new Weather()
            {
                Time = 12,
                State = "맑음",
                Temp ="5°"
            },
            new Weather()
            {
                Time = 13,
                State = "맑음",
                Temp ="5°"
            },new Weather()
            {
                Time = 14,
                State = "맑음",
                Temp ="5°"
            },
            new Weather()
            {
                Time = 15,
                State = "맑음",
                Temp ="5°"
            },
            new Weather()
            {
                Time = 16,
                State = "맑음",
                Temp ="5°"
            },
            new Weather()
            {
                Time = 17,
                State = "맑음",
                Temp ="5°"
            }
        };
    }
}

public class WeatherService : IWeatherService
{
    List<Weather> weatherlist = new List<Weather> ();
    public bool NeedUmbrella(int hour1, int hour2, int hour3, int hour4)
    {
        return weatherlist.Where (x => x.Time >= hour1 && x.Time <= hour2 ||
                                x.Time >= hour3 && x.Time <= hour4)
                        .Any (x => x.State.Contains ("비"));
    }

    public List<Weather> Realod()
    {
        weatherlist.Clear ();
        HtmlWeb web = new HtmlWeb ();

        HtmlDocument htmlDoc = web.Load ("https://search.naver.com/search.naver?sm=tab_hty.top&where=nexearch&ssc=tab.nx.all&query=%EA%B2%BD%EA%B8%B0%EB%8F%84+%ED%99%94%EC%84%B1%EC%8B%9C+%EB%82%A0%EC%94%A8&oquery=%EC%A0%81%EC%9A%A9&tqi=ilAERlpzL8wsstzuv4Vssssst40-202937");

        HtmlNodeCollection list = htmlDoc.DocumentNode.SelectNodes ("//li[@class='_li']");
        var dateTime = DateTime.Now.ToString ("HH");
        //현재 시간부터 23시까지 데이터만 가공
        for (int i = 0; i < 22 - Convert.ToInt32 (dateTime); i++)
        {
            weatherlist.Add (new Weather ()
            {
                Time = Convert.ToInt32 (list[i].Descendants ("dt").FirstOrDefault ().InnerText),
                Temp = list[i].Descendants ("dd").ToList ()[1].InnerText,
                State = list[i].Descendants ("dd").ToList ()[0].InnerText
            });
        }

        return weatherlist;
    }
}
