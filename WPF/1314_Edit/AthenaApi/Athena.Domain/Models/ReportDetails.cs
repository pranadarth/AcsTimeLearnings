using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Domain.Models
{
    public class ReportDetails
    {
        public List<Component> Components { get; set; }
    }
    public class Component
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public List<string> Header { get; set; }
        public List<TableRow> Rows { get; set; }
        public List<string> Labels { get; set; }
        public List<Dataset> Datasets { get; set; }
        public bool IsVertical { get; set; }
    }

    public class TableRow
    {
        public object TableData { get; set; }
    }

    public class TableDataDefinition
    {
        public object Value { get; set; }
        public object DrillDown { get; set; }
        public Formula Formula { get; set; }

    }

    public class Formula
    {
        public string Name { get; set; }
        public object Columns { get; set; }
    }

    public class Dataset
    {
        public string Label { get; set; }
        public List<int> Data { get; set; }
        public string BorderColor { get; set; }
        public string BackgroundColor { get; set; }
    }

}
