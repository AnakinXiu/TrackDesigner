﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TrackDesigner.Util;

namespace TrackDesigner.ViewModels;

public class MainFormViewModel : INotifyPropertyChanged
{
    private int _horizontalPieceCount;
    private int _verticalPieceCount;

    public int HorizontalPieceCount
    {
        get => _horizontalPieceCount;
        set => PropertyChanged?.RaiseIfChanged(this, ref _horizontalPieceCount, value, nameof(HorizontalPieceCount));
    }

    public int VerticalPieceCount
    {
        get => _verticalPieceCount;
        set => PropertyChanged.RaiseIfChanged(this, ref _verticalPieceCount, value, nameof(VerticalPieceCount));
    }

    public RibbonViewModel RibbonViewModel { get; set; }

    public ObservableCollection<TrackPiece> TrackPieces { get; set; } = [];

    public MainFormViewModel()
    {
        RibbonViewModel = new RibbonViewModel();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}