using Informatics.FiskebodenWebAPI.DTOs;
using Informatics.FiskebodenWebAPI.Models;

namespace Informatics.FiskebodenWebAPI.Extensions;

public static class DtoMappingExtensions 
{

    public static BatchDto ToDto(this Batch batch)
    {
        return new BatchDto
        {
            BatchNo = batch.BatchNo,
            BatchWeek = batch.BatchWeek,
            BatchQuantity = batch.BatchQuantity,
            ProductionDate = batch.ProductionDate,
            BatchShelfLife = batch.BatchShelfLife,
            BatchPrice = batch.BatchPrice,
            BatchOrigin = batch.BatchOrigin,
            ProductName = batch.Product?.ProductName ?? string.Empty,
            SupplierName = batch.Supplier?.SupplierName ?? string.Empty
        };
    }

    public static CategoryDto ToDto(this Category category)
    {
        return new CategoryDto
        {
            CategoryNo = category.CategoryNo,
            CategoryName = category.CategoryName,
            ProductNames = category.Products.Select(p => p.ProductName ?? string.Empty).ToList()
        };
    }

    public static PickupPointDto ToDto(this PickupPoint pickupPoint)
    {
        return new PickupPointDto
        {
            PickupPointNo = pickupPoint.PickupPointNo,
            PickupPointAddress = pickupPoint.PickupPointAddress,
            PickupPointName = pickupPoint.PickupPointName,
            SupplierPickupPoints = pickupPoint.SupplierPickupPoints
                .Select(spp => $"{spp.Supplier?.SupplierName ?? string.Empty} - {spp.PickupDay} {spp.PickupTime}")
                .ToList()
        };
    }

    public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            ProductNo = product.ProductNo,
            ProductName = product.ProductName,
            IsMeasuredInUnits = product.IsMeasuredInUnits,
            CategoryName = product.Category?.CategoryName ?? string.Empty,
            BatchNumbers = product.Batches.Select(b => b.BatchNo ?? string.Empty).ToList()
        };
    }

    public static SupplierDto ToDto(this Supplier supplier)
    {
        return new SupplierDto
        {
            SupplierNo = supplier.SupplierNo,
            SupplierName = supplier.SupplierName,
            SupplierEmail = supplier.SupplierEmail,
            SupplierPhoneNumber = supplier.SupplierPhoneNumber,
            SupplierLocation = supplier.SupplierLocation,
            SupplierSwishNumber = supplier.SupplierSwishNumber,
            BatchNumbers = supplier.Batches.Select(b => b.BatchNo ?? string.Empty).ToList(),
            PickupPointNames = supplier.SupplierPickupPoints
                .Select(spp => spp.PickupPoint?.PickupPointName ?? string.Empty).ToList()
        };
    }

    public static SupplierPickupPointDto ToDto(this SupplierPickupPoint supplierPickupPoint)
    {
        return new SupplierPickupPointDto
        {
            SupplierName = supplierPickupPoint.Supplier?.SupplierName ?? string.Empty,
            PickupPointName = supplierPickupPoint.PickupPoint?.PickupPointName ?? string.Empty,
            PickupDay = supplierPickupPoint.PickupDay,
            PickupTime = supplierPickupPoint.PickupTime
        };
    }
}
