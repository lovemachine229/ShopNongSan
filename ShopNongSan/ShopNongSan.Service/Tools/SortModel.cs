using ShopNongSan.Data.Collection;

namespace ShopNongSan.Service.Tools
{
    public class SortModel
    {
        private string UpIcon = "fa fa-arrow-up";
        private string DownIcon = "fa fa-arrow-down";

        public string SortedProperty { get; set; }
        public SortOrder SortedOrder { get; set; }

        private List<SortableColumn> sortableColumns = new List<SortableColumn>();

        public void AddColumn(string colName, bool isDefaultColumn = false)
        {
            SortableColumn tmp = this.sortableColumns.Where(c => c.ColumnName.ToLower() == colName.ToLower()).SingleOrDefault();
            if(tmp == null)
                sortableColumns.Add(new SortableColumn() { ColumnName = colName});

            if(isDefaultColumn == true || sortableColumns.Count == 1)
            {
                SortedProperty = colName;
                SortedOrder = SortOrder.Ascending;
            }
        }

        public SortableColumn GetColumn(string colName)
        {
            SortableColumn tmp = this.sortableColumns.Where(c => c.ColumnName.ToLower() == colName.ToLower()).SingleOrDefault();
            if (tmp == null)
                sortableColumns.Add(new SortableColumn() { ColumnName = colName });
            return tmp;
        }

        public void ApplySort(string sortExpression)
        {
            //this.GetColumn("name").SortIcon = "";
            //this.GetColumn("name").SortExpression = "name";

            //this.GetColumn("time").SortIcon = "";
            //this.GetColumn("time").SortExpression = "time";

            if (sortExpression == "")
                sortExpression = this.SortedProperty;

            sortExpression = sortExpression.ToLower();

            foreach(SortableColumn sortableColumn in this.sortableColumns)
            {
                sortableColumn.SortIcon = "";
                sortableColumn.SortExpression = sortableColumn.ColumnName;

                if(sortExpression == sortableColumn.ColumnName.ToLower())
                {
                    this.SortedOrder = SortOrder.Ascending;
                    this.SortedProperty = sortableColumn.ColumnName;

                    sortableColumn.SortIcon = DownIcon;
                    sortableColumn.SortExpression = sortableColumn.ColumnName + "desc";
                }

                if (sortExpression == sortableColumn.ColumnName.ToLower() + "desc")
                {
                    this.SortedOrder = SortOrder.Descending;
                    this.SortedProperty = sortableColumn.ColumnName;

                    sortableColumn.SortIcon = UpIcon;
                    sortableColumn.SortExpression = sortableColumn.ColumnName;
                }
            }
        }
    }

    public class SortableColumn
    {
        public string ColumnName { get; set; }
        public string SortExpression { get; set; }
        public string SortIcon { get; set; }
    }
}
