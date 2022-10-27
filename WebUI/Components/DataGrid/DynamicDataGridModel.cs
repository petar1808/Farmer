using Microsoft.AspNetCore.Components.Web;

namespace WebUI.Components.DataGrid
{
    public class DynamicDataGridModel<TData> where TData: IDynamicDataGridModel
    {
        public DynamicDataGridModel(IEnumerable<TData> data, IEnumerable<DynamicDataGridColumnModel> columns)
        {
            this.Data = data;
            this.Columns = columns;
        }

        public IEnumerable<TData> Data { get; private set; } = new List<TData>();

        public Type DataType => typeof(TData);

        public IEnumerable<DynamicDataGridColumnModel> Columns { get; set; } = new List<DynamicDataGridColumnModel>();

        public Action<int> EditAction { get; private set; } = default!;

        public Action<int> DeleteAction { get; private set; } = default!;

        public bool AllowFiltering { get; private set; } = false;

        public bool AllowPaging { get; private set; } = false;

        public bool AllowSorting { get; private set; } = false;

        public int PageSize { get; private set; } = 10;


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

    }

    public class DynamicDataGridColumnModel
    {
        public DynamicDataGridColumnModel(string propertyName, string displayName)
        {
            PropertyName = propertyName;
            DisplayName = displayName;
            StringFormat = "";
        }

        public DynamicDataGridColumnModel(string propertyName, string displayName, string stringFormat)
        {
            PropertyName = propertyName;
            DisplayName = displayName;
            StringFormat = stringFormat;
        }

        public string PropertyName { get; set; }

        public string DisplayName { get; set; }

        public string StringFormat { get; set; }
    }

    public interface IDynamicDataGridModel
    {
        public int Id { get; set; }
    }

}
