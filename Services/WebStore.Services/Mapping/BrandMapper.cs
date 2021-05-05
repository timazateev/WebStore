using WebStore.Domain.Entities;
using WebStore.Domain.DTO;
using System.Collections.Generic;
using System.Linq;

namespace WebStore.Infrastructure.Mapping
{
    public static class BrandMapper
    {
        public static BrandDTO ToDTO(this Brand Brand) => Brand is null
            ? null
            : new BrandDTO
            {
                id = Brand.id,
                Name = Brand.Name,
                Order = Brand.Order,
            };


        public static Brand FromDTO(this BrandDTO Brand) => Brand is null
            ? null
            : new Brand
            {
                id = Brand.id,
                Name = Brand.Name,
                Order = Brand.Order,
            };

        public static IEnumerable<BrandDTO> ToDTO(this IEnumerable<Brand> Brand) =>
            Brand.Select(ToDTO);

        public static IEnumerable<Brand> FromDTO(this IEnumerable<BrandDTO> Brand) =>
            Brand.Select(FromDTO);
    }
}
