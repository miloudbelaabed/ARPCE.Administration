using System.Globalization;
using CsvHelper.Configuration;

namespace ARPCE.Administration.Infrastructure.Files.Maps;
public class TodoItemRecordMap : ClassMap<NewsStyleUriParser>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        
    }
}
