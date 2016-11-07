using WayneCarCorps.Models;

namespace WayneCarCorps.Data.Common
{
    public interface IModelsFactory
    {
        Car CreateCar();

        Dealer CreateDealer();

        Model CreateModel();

        Manufacturer CreateManufacturer();

        Color CreateColor();

        CarType CreateCarType();

        Country CreateCountry();

        Sale CreateSale();
    }
}
