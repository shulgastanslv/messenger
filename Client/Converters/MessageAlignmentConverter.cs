using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Client.Stores;

namespace Client.Converters;

public class MessageAlignmentConverter : IMultiValueConverter
{
    public object Convert(object[]? values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is not [Guid senderId, Guid userId]) 
            return HorizontalAlignment.Right;

        return senderId == userId ? HorizontalAlignment.Left : HorizontalAlignment.Right;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}