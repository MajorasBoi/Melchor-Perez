using System;
using System.Threading.Tasks;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using InfluxDB.Client.Writes;
using Models;

namespace Mappers
{
    public class DomainEntityConverter: IDomainObjectMapper
    {
        public object ConvertToEntity(FluxRecord fluxRecord, Type type)
        {
            if (type != typeof(Sensor))
            {
                throw new NotSupportedException($"This converter doesn't supports: {type}");
            }

            var customEntity = new Sensor
            {
                Type = EType.Temperature,
                Brand = EBrand.Siemens,
                Value = (float)Convert.ToDouble(fluxRecord.GetValueByKey("value")),
                Model = Convert.ToString(fluxRecord.GetValueByKey("model")),
                ID = Convert.ToInt32(fluxRecord.GetValueByKey("id")),
                TimeStamp = fluxRecord.GetTime().GetValueOrDefault().ToDateTimeUtc(),
            };
           
            if (fluxRecord.GetMeasurement() == "Temperature")
                customEntity.Type = EType.Temperature;
            else
                customEntity.Type = EType.Humidity;
            if (fluxRecord.GetValueByKey("brand").ToString() == "Siemens")
                customEntity.Brand = EBrand.Siemens;
            else
                customEntity.Brand = EBrand.EndressHauser;
            return customEntity;
        }

        public T ConvertToEntity<T>(FluxRecord fluxRecord)
        {
            return (T)ConvertToEntity(fluxRecord, typeof(T));
        }

        public PointData ConvertToPointData<T>(T entity, WritePrecision precision)
        {
            PointData point = null;
            
            if (!(entity is Sensor sensor))
            {
                throw new NotSupportedException($"This converter doesn't supports: {entity}");
            }
            if(sensor.Type == EType.Temperature)
            {
                point = PointData
                .Measurement("Temperature")
                .Tag("model", sensor.Model)
                .Tag("brand", sensor.Brand.ToString())
                .Tag("id", sensor.ID.ToString())
                .Field("value", sensor.Value)
                .Timestamp(sensor.TimeStamp, precision);

                return point;
            }
            else
            {
                point = PointData
                .Measurement("Humidity")
                .Tag("model", sensor.Model)
                .Tag("brand", sensor.Brand.ToString())
                .Tag("id", sensor.ID.ToString())
                .Field("value", sensor.Value)
                .Timestamp(sensor.TimeStamp, precision);

                return point;
            }
        }
   
    }
}