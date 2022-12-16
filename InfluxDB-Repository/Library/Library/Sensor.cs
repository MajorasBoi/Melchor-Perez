using System;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using InfluxDB.Client.Writes;

namespace Models
{
    public enum EType
    {
       Temperature,
       Humidity,
    }

    public enum EBrand
    {
        Siemens,
        EndressHauser,
    }

    public class Sensor
    {
        #region Properties

        public EType Type { get; set; }
        public EBrand Brand { get; set; }
        public double Value { get; set; }
        public string Model { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ID { get; set; }

        #endregion
        #region Constructors

        public Sensor(EType type, EBrand brand, double value, string model, DateTime time, int id)
        {
            Type = type;
            Brand = brand;
            Value = value;
            Model = model;
            ID = id;
            TimeStamp = time;
        }

        public Sensor(Sensor sensor)
        {
            Type = sensor.Type;
            Brand = sensor.Brand;
            Value = sensor.Value;
            Model = sensor.Model;
            ID = sensor.ID;
            TimeStamp = sensor.TimeStamp;
        }

        public Sensor()
        {
            Type = EType.Temperature;
            Brand = EBrand.Siemens;
            Value = 0;
            Model = "Unknown";
            ID = 0;
            TimeStamp = DateTime.UtcNow;
        }

        #endregion
    }
}