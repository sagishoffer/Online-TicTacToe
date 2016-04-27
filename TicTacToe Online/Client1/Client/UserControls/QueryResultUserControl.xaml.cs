using Client.TicTacToeService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Client.UserControls;
using System.Collections.ObjectModel;

namespace Client
{
    /// <summary>
    /// Interaction logic for QueryResultUserControl.xaml
    /// </summary>
    public partial class QueryResultUserControl : UserControl
    {
        private List<int> rowsIndexChanged;
        private string cellText;
        private string colHeader;
        private string selectedValue;
        private int rowIndex;

        // Constructor. inits the table
        public QueryResultUserControl(object[] tableData, string[] headers, string[] types, bool[] readOnly, bool[] allowNull)
        {
            InitializeComponent();
            rowsIndexChanged = new List<int>();
            initTable(tableData, headers, types, readOnly, allowNull);
        }

        // Method inits the table with all data columns
        private void initTable(object[] tableData, string[] headers, string[] types, bool[] readOnly, bool[] allowNull)
        {
            for (int i = 0; i < headers.Count(); i++)
            {
                DataGridColumn column;

                if (types[i].Equals("image"))
                    column = initImageColumn(headers[i]);
                else if (types[i].Equals("comboBox"))
                    column = initComboBoxColumn(headers[i]);
                else if (types[i].Equals("date") || types[i].Equals("datetime"))
                    column = initDateColumn(headers[i], readOnly[i], types[i]);
                else
                    column = initTextColumn(headers[i], readOnly[i], allowNull[i], types[i]);

                datagrid.Columns.Add(column);
            }

            datagrid.ItemsSource = tableData.ToList();
        }

        // Method inits image column
        private DataGridTemplateColumn initImageColumn(string header)
        {
            DataGridTemplateColumn imgColumn = new DataGridTemplateColumn();
            imgColumn.Width = 200;
            imgColumn.Header = Regex.Replace(header, "([a-z])([A-Z])", "$1 $2");

            FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.Name = "image";
            imageFactory.SetBinding(Image.SourceProperty, new Binding(header));

            DataTemplate dataTemplate = new DataTemplate();
            dataTemplate.VisualTree = imageFactory;
            imgColumn.CellTemplate = dataTemplate;

            Style cellStyle = new Style(typeof(DataGridCell));
            cellStyle.Setters.Add(new EventSetter(DataGridCell.MouseDoubleClickEvent, new MouseButtonEventHandler(contentPresenter_DoubleClickEvent)));
            imgColumn.CellStyle = cellStyle;

            return imgColumn;
        }

        // Method inits comboBox column
        private DataGridComboBoxColumn initComboBoxColumn(string header)
        {
            DataGridComboBoxColumn cboColumn = new DataGridComboBoxColumn();
            cboColumn.Header = Regex.Replace(header, "([a-z])([A-Z])", "$1 $2");
            Binding colBinding = new Binding(header);

            ObservableCollection<string> items = new ObservableCollection<string>();
            items.Add("Begginer");
            items.Add("Normal");
            items.Add("Professional");

            cboColumn.SelectedItemBinding = colBinding;
            cboColumn.ItemsSource = items;

            return cboColumn;
        }

        // Method inits date column
        private DataGridTemplateColumn initDateColumn(string header, bool readOnly, string type)
        {
            DataGridTemplateColumn dateColumn = new DataGridTemplateColumn();
            dateColumn.Header = Regex.Replace(header, "([a-z])([A-Z])", "$1 $2");
            dateColumn.IsReadOnly = readOnly;
            
            Binding dateBinding = new Binding(header);
            dateBinding.Mode = BindingMode.TwoWay;
            dateBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            if (type.Equals("date"))
                dateBinding.StringFormat = "dd/MM/yyyy";

            // Editing
            FrameworkElementFactory dateFactoryEdit = new FrameworkElementFactory(typeof(DatePicker));
            dateFactoryEdit.Name = "datepicker";
            dateFactoryEdit.SetBinding(DatePicker.SelectedDateProperty, dateBinding);
            DataTemplate cellEditingTemplate = new DataTemplate();
            cellEditingTemplate.VisualTree = dateFactoryEdit;
            dateColumn.CellEditingTemplate = cellEditingTemplate;

            // View
            FrameworkElementFactory dateFactoryView = new FrameworkElementFactory(typeof(TextBlock));
            dateFactoryView.SetValue(TextBlock.TextProperty, dateBinding);
            DataTemplate cellTemplate = new DataTemplate();
            cellTemplate.VisualTree = dateFactoryView;
            dateColumn.CellTemplate = cellTemplate;

            return dateColumn;
        }

        // Method inits text column
        private DataGridTextColumn initTextColumn(string header, bool readOnly, bool allowNull, string type)
        {
            DataGridTextColumn textColumn = new DataGridTextColumn();
            textColumn.Header = Regex.Replace(header, "([a-z])([A-Z])", "$1 $2");
            textColumn.IsReadOnly = readOnly;

            Binding colBinding = new Binding(header);
            colBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            // Set validation by the column's type
            if (type.Equals("char"))
                colBinding.ValidationRules.Add(new CharCellDataInfoValidationRule(allowNull) { ValidationStep = ValidationStep.RawProposedValue });
            else if (type.Equals("int"))
                colBinding.ValidationRules.Add(new IntCellDataInfoValidationRule(allowNull) { ValidationStep = ValidationStep.RawProposedValue });
            else if (type.Equals("charint"))
                colBinding.ValidationRules.Add(new CharIntCellDataInfoValidationRule(allowNull) { ValidationStep = ValidationStep.RawProposedValue });
            else if (type.Equals("words"))
                colBinding.ValidationRules.Add(new StringCellDataInfoValidationRule(allowNull) { ValidationStep = ValidationStep.RawProposedValue });

            textColumn.Binding = colBinding;

            return textColumn;
        }

        // Method save the old value of the cell
        private void datagrid_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            // In case of image/datePicker
            if (e.EditingElement is ContentPresenter)
            {
                var cp = e.EditingElement as ContentPresenter;
                DataTemplate editingtemplate = cp.ContentTemplate;
                Image img = editingtemplate.FindName("image", cp) as Image;
                if (img != null)
                    this.cellText = img.Source == null ? "" : img.Source.ToString();
                else
                {
                    DatePicker dp = editingtemplate.FindName("datepicker", cp) as DatePicker;
                    if (dp != null)
                        this.cellText = dp.Text;
                }
            }

            // In case of text
            else if (e.EditingElement is TextBox)
            {
                var tb = e.EditingElement as TextBox;
                this.cellText = tb.Text;
            }

            // In case of comboBox
            else if (e.EditingElement is ComboBox)
            {
                var box = e.EditingElement as ComboBox;
                this.cellText = box.Text;
            }

            // In case of datePicker
            else if (e.EditingElement is DatePicker)
            {
                var dp = e.EditingElement as DatePicker;
                this.cellText = dp.Text;
            }
        }

        // Method compares the old value with the new value and adds the cell's row to list (if they are not equals)
        private void datagrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int rowIndex = e.Row.GetIndex();

            if (cellText != null)
            {
                if (e.EditingElement is ContentPresenter)
                {
                    string newValue = "";
                    var contentpresenter = e.EditingElement as ContentPresenter;
                    DataTemplate editingtemplate = contentpresenter.ContentTemplate;
                    Image img = editingtemplate.FindName("image", contentpresenter) as Image;
                    if (img != null)
                        newValue = img.Source == null ? "" : img.Source.ToString();
                    else
                    {
                        DatePicker dp = editingtemplate.FindName("datepicker", contentpresenter) as DatePicker;
                        if (dp != null)
                            newValue = dp.Text;
                    }

                    if (!rowsIndexChanged.Contains(rowIndex) && !cellText.Equals(newValue))
                        rowsIndexChanged.Add(rowIndex);
                }
                else if (e.EditingElement is TextBox)
                {
                    var t = e.EditingElement as TextBox;
                    if (!cellText.Equals(t.Text) && !rowsIndexChanged.Contains(rowIndex))
                        rowsIndexChanged.Add(rowIndex);
                }
                else if (e.EditingElement is ComboBox)
                {
                    var box = e.EditingElement as ComboBox;
                    if (!cellText.Equals(box.Text) && !rowsIndexChanged.Contains(rowIndex))
                        rowsIndexChanged.Add(rowIndex);
                }
            }
        }

        // Method saves the row index, column header and the value of the selected cell
        private void datagrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (datagrid.SelectionUnit == DataGridSelectionUnit.Cell)
            {
                foreach (var item in e.AddedCells)
                {
                    var col = item.Column as DataGridColumn;

                    colHeader = col.Header.ToString().Replace(" ", string.Empty);
                    rowIndex = datagrid.Items.IndexOf(datagrid.SelectedCells[0].Item);

                    var fc = col.GetCellContent(item.Item);

                    if (fc is TextBlock)
                    {
                        selectedValue = (fc as TextBlock).Text;
                    }
                    else if (fc is ComboBox)
                    {
                        selectedValue = (fc as ComboBox).Text;
                    }
                    else if (fc is DatePicker)
                    {
                        selectedValue = (fc as DatePicker).Text;
                    }
                }
            }
        }

        // Method returns all the rows that changed
        public List<T> getRowsChanged<T>()
        {
            List<T> rows = new List<T>();

            foreach (var row in rowsIndexChanged)
            {
                rows.Add((T)datagrid.Items.GetItemAt(row));
            }

            return rows;
        }

        // Method returns the selected row
        public int getSelectedRow()
        {
            return datagrid.SelectedIndex;
        }

        // Method sets the row index, column header and the value of the selected cell
        public void getSelectedCell(out int row, out string col, out string value)
        {
            row = rowIndex;
            col = colHeader;
            value = selectedValue;
        }

        // Method sets the selection unit cell (cell / FullRow)
        public void setSelectionUnitCell(bool selectionCell)
        {
            if (selectionCell)
                datagrid.SelectionUnit = DataGridSelectionUnit.Cell;
            else
                datagrid.SelectionUnit = DataGridSelectionUnit.FullRow;
        }

        // Method opens file chooser for selecting image
        private void contentPresenter_DoubleClickEvent(object sender, MouseButtonEventArgs e)
        {
            DataGridCell cell = sender as DataGridCell;
            if (cell != null)
            {
                string[] fileType = { ".jpeg", ".png", ".jpg", ".gif" };
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

                System.Windows.Forms.DialogResult result = openFileDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK || result == System.Windows.Forms.DialogResult.Yes)
                {
                    string path = openFileDialog.FileName;
                    string type = Path.GetExtension(path);
                    if (fileType.Contains(type))
                    {
                        var uri = new System.Uri(path);
                        var converted = uri.AbsoluteUri;
                        this.cellText = converted;

                        ((ChampsObject)((ContentPresenter)cell.Content).Content).Image = converted;
                    }
                }
            }
        }

    }
}
