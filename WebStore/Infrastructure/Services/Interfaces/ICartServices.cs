using WebStore.ViewModels;

namespace WebStore.Infrastructure.Services.Interfaces
{
    interface ICartServices
    {
        void Add(int id);

        void Decrement(int id);

        void Remove(int id);

        void Clear();

        CartViewModel GetViewModel();
    }
}
