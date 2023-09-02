﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace RepairDesk.WpfClient.Converters
{
	public class InvertedBooleanToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool booleanValue)
			{
				return booleanValue ? Visibility.Collapsed : Visibility.Visible;
			}

			return Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Visibility visibilityValue)
			{
				return visibilityValue == Visibility.Collapsed;
			}

			return false;
		}
	}

}
