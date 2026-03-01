using System.Collections.ObjectModel;
using System.Linq;

namespace To_Do_List;

public partial class MainPage : ContentPage
{
    ObservableCollection<ToDoClass> todoItems;
    int idCounter = 1;
    ToDoClass selectedItem;

    public MainPage()
    {
        InitializeComponent();

        todoItems = new ObservableCollection<ToDoClass>();
        todoCollectionView.ItemsSource = todoItems;
    }

    // ADD
    private void AddToDoItem(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(titleEntry.Text))
            return;

        todoItems.Add(new ToDoClass
        {
            id = idCounter++,
            title = titleEntry.Text,
            detail = detailsEditor.Text
        });

        ClearFields();
    }

    // DELETE
    private void DeleteToDoItem(object sender, EventArgs e)
    {
        var button = sender as Button;
        int id = (int)button.CommandParameter;

        var item = todoItems.FirstOrDefault(x => x.id == id);
        if (item != null)
            todoItems.Remove(item);
    }

    // SELECT (for editing)
    private void OnItemSelected(object sender, SelectionChangedEventArgs e)
    {
        selectedItem = e.CurrentSelection.FirstOrDefault() as ToDoClass;

        if (selectedItem == null)
            return;

        titleEntry.Text = selectedItem.title;
        detailsEditor.Text = selectedItem.detail;

        addBtn.IsVisible = false;
        editBtn.IsVisible = true;
        cancelBtn.IsVisible = true;
    }

    // UPDATE
    private void EditToDoItem(object sender, EventArgs e)
    {
        if (selectedItem == null)
            return;

        selectedItem.title = titleEntry.Text;
        selectedItem.detail = detailsEditor.Text;

        CancelEdit(null, null);
    }

    // CANCEL
    private void CancelEdit(object sender, EventArgs e)
    {
        selectedItem = null;
        ClearFields();

        addBtn.IsVisible = true;
        editBtn.IsVisible = false;
        cancelBtn.IsVisible = false;

        todoCollectionView.SelectedItem = null;
    }

    private void ClearFields()
    {
        titleEntry.Text = "";
        detailsEditor.Text = "";
    }
}