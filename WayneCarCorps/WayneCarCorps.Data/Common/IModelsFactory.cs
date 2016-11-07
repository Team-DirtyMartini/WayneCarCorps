using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
