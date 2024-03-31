using System;
using System.Timers;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.DeskApp.Services;
using System.Collections.ObjectModel;
using Avalonia.DeskApp.Models;

namespace Avalonia.DeskApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IWeatherService _weatherService;
    [ObservableProperty] private DateTime datetime;
    [ObservableProperty] private List<string> datas;
    [ObservableProperty] public ObservableCollection<Weather> weathers;
    
    public MainViewModel(IWeatherService weatherService)
    {
        this._weatherService = weatherService;

        var tm = new Timer();
        tm.Interval = 1000;
        tm.Elapsed += (sender, args) =>
        {
            Datetime = DateTime.Now;
        };
        tm.Start();

        this.Weathers = new ObservableCollection<Weather>(this._weatherService.Realod ());
    }
}