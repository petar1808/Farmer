using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;

namespace WebUI.Components.DataGrid
{
    public class DynamicDataGridModel<TData> where TData : IDynamicDataGridModel
    {
        public DynamicDataGridModel(
            IEnumerable<TData> data, 
            IEnumerable<DynamicDataGridColumnModel> columns,
            string gridName = "")
        {
            Data = data;
            Columns = columns;
            GridName = gridName;
        }

        public IEnumerable<TData> Data { get; private set; } = new List<TData>();

        public Type DataType => typeof(TData);

        public IEnumerable<DynamicDataGridColumnModel> Columns { get; set; } = new List<DynamicDataGridColumnModel>();

        public Action<int> EditAction { get; private set; } = default!;

        public Action<int> DeleteAction { get; private set; } = default!;

        public Action AddAction { get; private set; } = default!;

        public bool AllowFiltering { get; private set; } = false;

        public bool AllowPaging { get; private set; } = false;

        public bool AllowSorting { get; private set; } = false;

        public bool AllowResizable { get; private set; } = false;

        public int PageSize { get; private set; } = 10;

        public string? DefaultColumnWidth { get; private set; } = default!;

        public string GridName { get; set; }


        public void UpdateData(IEnumerable<TData> data)
        {
            this.Data = data;
        }

        public DynamicDataGridModel<TData> WithEdit(Action<int> args)
        {
            EditAction = args;
            return this;
        }

        public DynamicDataGridModel<TData> WithDelete(Action<int> args)
        {
            DeleteAction = args;
            return this;
        }

        public DynamicDataGridModel<TData> WithAdd(Action args)
        {
            AddAction = args;
            return this;
        }

        public DynamicDataGridModel<TData> WithPaging(int pageSize = 10)
        {
            this.AllowPaging = true;
            this.PageSize = pageSize;
            return this;
        }

        public DynamicDataGridModel<TData> WithFiltering()
        {
            this.AllowFiltering = true;
            return this;
        }

        public DynamicDataGridModel<TData> WithSorting()
        {
            this.AllowSorting = true;
            return this;
        }

        public DynamicDataGridModel<TData> WithResizable()
        {
            this.AllowResizable = true;
            return this;
        }

        public DynamicDataGridModel<TData> WithDefaultWidth(string defaultWidth)
        {
            this.DefaultColumnWidth = defaultWidth;
            return this;
        }
    }

    public class DynamicDataGridColumnModel
    {
        public DynamicDataGridColumnModel(
            string propertyName, 
            string displayName, 
            string stringFormat = "", 
            string? width = null,
            Func<string>? total = null)
        {
            PropertyName = propertyName;
            DisplayName = displayName;
            StringFormat = stringFormat;
            Width = width;
            Total = total;
        }

        public string PropertyName { get; set; }

        public string DisplayName { get; set; }

        public string StringFormat { get; set; }

        public string? Width { get; set; }

        public Func<string>? Total { get; set; }
    }

    public interface IDynamicDataGridModel
    {
        public int Id { get; set; }
    }

}
