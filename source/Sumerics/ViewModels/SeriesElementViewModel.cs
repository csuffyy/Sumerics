﻿namespace Sumerics.ViewModels
{
    using Sumerics.Controls;
    using Sumerics.Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Media;
    using YAMP;

	public sealed class SeriesElementViewModel : BaseViewModel
	{
		#region Fields

        static readonly IEnumerable<PointSymbol> symbols = Enum.GetValues(typeof(PointSymbol)).Cast<PointSymbol>();

        readonly IPointSeries _series;
		readonly Int32 _index;

		String _title;
		SolidColorBrush _color;
		PointSymbol _symbol;
		Boolean _lines;

		#endregion

		#region ctor

		public SeriesElementViewModel(IPointSeries series, Int32 index)
		{
			_index = index;
			_series = series;
			Title = series.Label;
			Color = series.Color.BrushFromString();
			Symbol = series.Symbol;
			Lines = series.Lines;
		}

		#endregion

		#region Properties

		public String Title
		{
			get { return _title; }
			set 
			{
                if (String.IsNullOrEmpty(value))
                {
                    _title = String.Format("{0} #{1}", Messages.Data, _index.ToString());
                }
                else
                {
                    _title = value;
                }

				RaisePropertyChanged();
			}
		}

		public Brush Color
		{
			get { return _color; }
			set
			{
				_color = (SolidColorBrush)value;
				RaisePropertyChanged();
			}
		}

		public PointSymbol Symbol
		{
			get { return _symbol; }
			set
			{
				_symbol = value;
				RaisePropertyChanged();
			}
		}

		public Boolean Lines
		{
			get { return _lines; }
			set
			{
				_lines = value;
				RaisePropertyChanged();
			}
		}

		public IEnumerable<PointSymbol> Symbols
		{
			get { return symbols; }
		}

		#endregion

		#region Methods

		public void Save()
		{
			_series.Label = _title;
			_series.Lines = _lines;
			_series.Symbol = _symbol;
			_series.Color = _color.ToHtml();
		}

		#endregion
	}
}
