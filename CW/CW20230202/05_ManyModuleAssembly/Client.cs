using System;
using Car;

class Program
{
    static void Main()
    {
        // Здесь будет загружаться модуль auto.netmodule
        Auto obj1 = new Auto();
        obj1.AutoInfo();

        SportCar obj = new SportCar();
        obj.InfoSportCar();

	FuelCar fuelCar = new FuelCar();
	fuelCar.FuelSportCar();

        Console.ReadLine();
    }
}