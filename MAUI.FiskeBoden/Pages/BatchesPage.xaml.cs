using Informatics.FiskeBoden.ViewModels;
using Informatics.FiskeBoden.Interfaces;
using Informatics.FiskeBoden.Models;
using System.Diagnostics;

namespace Informatics.FiskeBoden.Pages
{
    public partial class BatchesPage : ContentPage
    {
        private readonly BatchesViewModel? _viewModel;
        private Batch? _selectedBatch;

        public BatchesPage(IBatchService batchService, IProductService productService, ISupplierService supplierService)
        {
            InitializeComponent();
            if (batchService == null || productService == null || supplierService == null)
            {
                throw new ArgumentNullException("Services cannot be null");
            }
            _viewModel = new BatchesViewModel(batchService, productService, supplierService);
            BindingContext = _viewModel;
        }

        public BatchesPage()
        {
            InitializeComponent();
            var app = Application.Current as App;
            if (app?.Services == null)
            {
                throw new Exception("Application services are missing.");
            }

            var batchService = app.Services.GetRequiredService<IBatchService>();
            var productService = app.Services.GetRequiredService<IProductService>();
            var supplierService = app.Services.GetRequiredService<ISupplierService>();

            _viewModel = new BatchesViewModel(batchService, productService, supplierService);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel != null)
            {
                await _viewModel.LoadBatchesAsync();
                UpdateTableView();
            }
        }

        private async void OnAddBatchClicked(object sender, EventArgs e)
        {
            if (_viewModel != null)
            {
                await Navigation.PushModalAsync(new AddBatchPage(_viewModel));
            }
            else
            {
                await DisplayAlert("Error", "ViewModel is not available.", "OK");
            }
        }

        private async void OnEditBatchClicked(object sender, EventArgs e)
        {
            if (_viewModel != null && _selectedBatch != null)
            {
                await Navigation.PushModalAsync(new AddBatchPage(_viewModel, _selectedBatch));
            }
            else
            {
                await DisplayAlert("Error", "Please select a batch to edit.", "OK");
            }
        }

        private async void OnRemoveBatchClicked(object sender, EventArgs e)
        {
            if (_viewModel != null && _selectedBatch != null)
            {
                bool confirm = await DisplayAlert("Confirm Deletion",
                    $"Are you sure you want to remove {_selectedBatch.BatchNo}?", "Yes", "No");
                if (confirm)
                {
                    await _viewModel.RemoveBatchAsync(_selectedBatch);
                    UpdateTableView();
                }
            }
            else
            {
                await DisplayAlert("Error", "Please select a batch to remove.", "OK");
            }
        }

        private void UpdateTableView()
        {
            if (_viewModel == null) return;

            var root = new TableRoot();
            var section = new TableSection("Batches");

            // Add header row
            var headerGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    // New columns
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
                BackgroundColor = Colors.Gray
            };

            var headerLabels = new[]
            {
                new Label { Text = "Batch No", FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center },
                new Label { Text = "Supplier", FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center },
                new Label { Text = "Product", FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center },
                new Label { Text = "Week", FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center },
                new Label { Text = "Quantity", FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center },
                // New headers
                new Label { Text = "Production Date", FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center },
                new Label { Text = "Shelf Life", FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center },
                new Label { Text = "Price", FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center },
                new Label { Text = "Origin", FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }
            };

            for (int i = 0; i < headerLabels.Length; i++)
            {
                headerGrid.Children.Add(headerLabels[i]);
                Grid.SetColumn(headerLabels[i], i);
            }

            section.Add(new ViewCell { View = headerGrid });

            foreach (var batch in _viewModel.Batches)
            {
                Debug.WriteLine($"Adding Batch to TableView: {batch.BatchNo}");

                var grid = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        // New columns
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                    }
                };

                var batchNoLabel = new Label { Text = batch.BatchNo, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                var supplierNameLabel = new Label { Text = batch.Supplier.SupplierName, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                var productNameLabel = new Label { Text = batch.Product.ProductName, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                var batchWeekLabel = new Label { Text = batch.BatchWeek?.ToString() ?? "", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                
                var batchQuantityText = batch.BatchQuantity?.ToString() ?? "";
                if (!batch.Product.IsMeasuredInUnits)
                {
                    batchQuantityText += " KG";
                }
                var batchQuantityLabel = new Label { Text = batchQuantityText, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                
                // New labels
                var productionDateLabel = new Label { Text = batch.ProductionDate.ToString("yyyy-MM-dd"), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                var batchShelfLifeLabel = new Label { Text = batch.BatchShelfLife.ToString(), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                var batchPriceLabel = new Label { Text = batch.BatchPrice.ToString("C"), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                var batchOriginLabel = new Label { Text = batch.BatchOrigin, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };

                grid.Children.Add(batchNoLabel);
                Grid.SetColumn(batchNoLabel, 0);

                grid.Children.Add(supplierNameLabel);
                Grid.SetColumn(supplierNameLabel, 1);

                grid.Children.Add(productNameLabel);
                Grid.SetColumn(productNameLabel, 2);

                grid.Children.Add(batchWeekLabel);
                Grid.SetColumn(batchWeekLabel, 3);

                grid.Children.Add(batchQuantityLabel);
                Grid.SetColumn(batchQuantityLabel, 4);
                
                // Add new columns
                grid.Children.Add(productionDateLabel);
                Grid.SetColumn(productionDateLabel, 5);
                
                grid.Children.Add(batchShelfLifeLabel);
                Grid.SetColumn(batchShelfLifeLabel, 6);
                
                grid.Children.Add(batchPriceLabel);
                Grid.SetColumn(batchPriceLabel, 7);
                
                grid.Children.Add(batchOriginLabel);
                Grid.SetColumn(batchOriginLabel, 8);

                var viewCell = new ViewCell { View = grid };
                viewCell.Tapped += (s, e) =>
                {
                    _selectedBatch = batch;
                    UpdateTableView(); // Refresh to show selection
                };

                if (_selectedBatch == batch)
                {
                    viewCell.View.BackgroundColor = Colors.LightGray; // Highlight selected row
                }

                section.Add(viewCell);
            }

            root.Add(section);
            BatchesTableView.Root = root;
        }
    }
}