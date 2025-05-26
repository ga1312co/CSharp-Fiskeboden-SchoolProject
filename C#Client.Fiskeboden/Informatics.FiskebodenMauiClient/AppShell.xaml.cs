namespace Informatics.FiskebodenMauiClient;
using Informatics.FiskebodenMauiClient.Pages;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ProductCatalogPage), typeof(ProductCatalogPage));

		Routing.RegisterRoute (nameof(BatchesPage), typeof(BatchesPage));
	}
}
