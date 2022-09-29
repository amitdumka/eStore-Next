using AndroidX.Lifecycle;
using eStore.ViewModels.List.Accounting;

namespace eStore.Pages.Accounting;

public partial class NotesPage : ContentPage
{
    NotesViewModel viewModel;
	public NotesPage(NotesViewModel vm)
	{
		InitializeComponent();
        InitializeComponent();
        BindingContext = viewModel = vm;
        viewModel.Setup(this, RLV);
    }
}
