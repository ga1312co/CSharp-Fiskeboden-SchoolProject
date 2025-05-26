using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Informatics.FiskeBoden.Interfaces;
using Informatics.FiskeBoden.Models;
using System.Linq;

namespace Informatics.FiskeBoden.ViewModels
{
    public class SupplierViewModel : INotifyPropertyChanged
    {
        private readonly ISupplierService _supplierService;
        private readonly IBatchService _batchService;
        private Supplier? _selectedSupplier;
  
        public ObservableCollection<Supplier> Suppliers { get; } = new();

        public Supplier? SelectedSupplier
        {
            get => _selectedSupplier;
            set
            {
                _selectedSupplier = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SupplierDescription));
            }
        }

        public string SupplierDescription => SelectedSupplier != null
            ? $"Supplier: {SelectedSupplier.SupplierName}\n" +
              $"Email: {SelectedSupplier.SupplierEmail}\n" +
              $"Phone: {SelectedSupplier.SupplierPhoneNumber ?? "N/A"}\n" +
              $"Location: {SelectedSupplier.SupplierLocation ?? "Unknown"}\n" +
              $"Swish: {SelectedSupplier.SupplierSwishNumber}\n\n" +
              $"Pickup locations: {GetPickupAddresses(SelectedSupplier)}\n\n" +
              $"{SelectedSupplier.SupplierName}'s products include: {GetProductsFromBatches(SelectedSupplier)}."
            : "No supplier selected.";


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SupplierViewModel(ISupplierService supplierService,
                                 IBatchService batchService)
        {
            _supplierService = supplierService;
            _batchService = batchService;
        }

        public async Task LoadSuppliersAsync()
        {
            Console.WriteLine("🔍 LoadSuppliersAsync() körs...");

            var suppliers = await _supplierService.GetSuppliersAsync();

            Console.WriteLine($"📢 {suppliers.Count()} suppliers laddades från databasen.");

            Suppliers.Clear();
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"✅ Supplier: {supplier.SupplierName}");
                Suppliers.Add(supplier);
            }

            SelectedSupplier = Suppliers.FirstOrDefault();
            Console.WriteLine($"📌 Vald Supplier: {SelectedSupplier?.SupplierName ?? "None"}");
        }

        // ✅ Hämtar produkter från leverantörens batches
        private string GetProductsFromBatches(Supplier supplier)
        {
            if (supplier == null || supplier.Batches == null || !supplier.Batches.Any())
                return "No listed products.";

            var products = supplier.Batches
                .Select(b => b.Product?.ProductName) // Hämtar produktnamn via Batch → Product
                .Where(name => !string.IsNullOrEmpty(name)) // Tar bort null och tomma strängar
                .Distinct(); // Tar bort dubbletter

            return products.Any() ? string.Join(", ", products) : "No listed products.";
        }
    

    private string GetPickupAddresses(Supplier supplier)
        {
            if (supplier?.SupplierPickupPoints == null || !supplier.SupplierPickupPoints.Any())
                return "No pickup locations.";

            var addresses = supplier.SupplierPickupPoints
                .Where(spp => spp.PickupPoint != null) // 🔥 Se till att pickup-punkten finns
                .Select(spp => spp.PickupPoint.PickupPointAddress) // 🔥 Hämta PickupPointAddress
                .Distinct(); // Tar bort dubbletter

            return addresses.Any() ? string.Join(", ", addresses) : "No pickup locations.";
        } } }