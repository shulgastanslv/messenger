using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Client.Converters;

public class MessageAlignmentConverter : IMultiValueConverter
{
    public object Convert(object[]? values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is not [Guid senderId, Guid userId]) return HorizontalAlignment.Left;

        Trace.WriteLine(senderId);
        Trace.WriteLine(userId);

        return senderId == userId ? HorizontalAlignment.Right : HorizontalAlignment.Left;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}