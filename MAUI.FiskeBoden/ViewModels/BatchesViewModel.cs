using System.Collections.ObjectModel;
using Informatics.FiskeBoden.Interfaces;
using Informatics.FiskeBoden.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Informatics.FiskeBoden.ViewModels
{
    public class BatchesViewModel
    {
        private readonly IBatchService _batchService;
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;
        public ObservableCollection<Batch> Batches { get; } = new();
        public ObservableCollection<Product> Products { get; } = new();
        public ObservableCollection<Supplier> Suppliers { get; } = new();
        public Supplier? SelectedSupplier { get; set; }
        public Product? SelectedProduct { get; set; }

        public BatchesViewModel(IBatchService batchService, IProductService productService, ISupplierService supplierService)
        {
            _batchService = batchService;
            _productService = productService;
            _supplierService = supplierService;
        }

        public async Task LoadBatchesAsync()
        {
            Batches.Clear();
            var results = await _batchService.GetBatchesAsync();
            foreach (var batch in results)
            {
                Debug.WriteLine($"Fetched Batch: {batch.BatchNo}");
                Batches.Add(batch);
            }
            await LoadProductsAsync();
            await LoadSuppliersAsync();
        }

        public async Task LoadProductsAsync()
        {
            Products.Clear();
            var results = await _productService.GetProductsAsync();
            foreach (var product in results)
            {
                Debug.WriteLine($"Fetched Product: {product.ProductName}");
                Products.Add(product);
            }
        }

        public async Task LoadSuppliersAsync()
        {
            Suppliers.Clear();
            var results = await _supplierService.GetSuppliersAsync();
            foreach (var supplier in results)
            {
                Debug.WriteLine($"Fetched Supplier: {supplier.SupplierName}");
                Suppliers.Add(supplier);
            }
        }

        public async Task AddBatchAsync(Batch batch)
        {
            await _batchService.CreateBatchAsync(batch);
            Batches.Add(batch);
        }

        public async Task RemoveBatchAsync(Batch batch)
        {
            await _batchService.DeleteBatchAsync(batch.BatchId);
            Batches.Remove(batch);
        }

        public async Task UpdateBatchAsync(Batch batch)
        {
            await _batchService.UpdateBatchAsync(batch);
            var existingBatch = Batches.FirstOrDefault(b => b.BatchId == batch.BatchId);
            if (existingBatch != null)
            {
                var index = Batches.IndexOf(existingBatch);
                Batches[index] = batch;
            }
        }
    }
}