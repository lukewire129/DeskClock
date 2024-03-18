using System;
using System.Timers;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.DeskApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private DateTime datetime;
    [ObservableProperty] private List<string> datas;
    
    public MainViewModel()
    {
    

        var tm = new Timer();
        tm.Interval = 1000;
        tm.Elapsed += (sender, args) =>
        {
            Datetime = DateTime.Now;
        };
        tm.Start();
    }
}