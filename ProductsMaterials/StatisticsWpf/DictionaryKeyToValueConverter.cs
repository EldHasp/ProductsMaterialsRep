using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace StatisticsWpf
{
    /// <summary>Конвертер преобразующий ключ в значение по привязанному словарю</summary>
    public class DictionaryKeyToValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (values.Length != 2)
                return null;

            IDictionary dictionary = values[1] as IDictionary;

            if (dictionary == null)
                return null;

            if (dictionary?.Contains(values[0]) == true)
                return dictionary[values[0]];

            return null;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
