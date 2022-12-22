using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PripravljalecPrognozLib
{
    public static class Helpers
    {
        public static PPServiceWCFClient.ArrayOfAddOfftakePoint MapOfftakePoints(List<PPOfftakePoint> otps)
        {
            var offTakePoints = new PPServiceWCFClient.ArrayOfAddOfftakePoint();
            //var offTakePoints = new List<PPServiceWCFClient.AddOfftakePoint>();
            if (otps == null)
                return offTakePoints;

            foreach (var o in otps)
            {
                offTakePoints.Add(new PPServiceWCFClient.AddOfftakePoint()
                {
                    CityGateCode = o.CityGateCode,
                    LoadType = Helpers.GetLoadType(o.LoadType),
                    MeasurementDeviceMultiplier = o.MeasurementDeviceMultiplier,
                    OfftakePointCode = o.OfftakePointCode,
                    Status = Helpers.GetOfftakePointStatus(o.Status),
                    SupplierCode = o.SupplierCode,
                    ValidFrom = o.ValidFrom,
                    YearlyOfftake = o.YearlyOfftake
                });
            }
            return offTakePoints;
        }

        public static PPServiceWCFClient.ArrayOfAddOfftakePointEis MapOfftakePointsEIS(List<PPOfftakePoint> otps)
        {
            var offTakePoints = new PPServiceWCFClient.ArrayOfAddOfftakePointEis();
            //var offTakePoints = new List<PPServiceWCFClient.AddOfftakePointEis>();
            if (otps == null)
                return offTakePoints;

            foreach (var o in otps)
            {
                var otp = new PPServiceWCFClient.AddOfftakePointEis();

                otp.CityGateCode = o.CityGateCode;
                otp.LoadType = Helpers.GetLoadType(o.LoadType);
                otp.MeasurementDeviceMultiplier = o.MeasurementDeviceMultiplier;
                otp.OfftakePointCode = o.OfftakePointCode;
                otp.Status = Helpers.GetOfftakePointStatus(o.Status);
                otp.SupplierCode = o.SupplierCode;
                otp.ValidFrom = o.ValidFrom;
                otp.YearlyOfftake = o.YearlyOfftake;
                // nova polja
                otp.IsProtectedConsumer = o.IsProtectedConsumer;
                otp.IsHouseholdConsumer = o.IsHouseholdConsumer;
                otp.OfftakeKind = o.OfftakeKind;
                otp.InterruptibleSupplyContract = o.InterruptibleSupplyContract;
                otp.AlternativeEnergySource = o.AlternativeEnergySource;
                otp.ProtectedUserConsumePart = o.ProtectedUserConsumePart;
                otp.IsActive = o.IsActive;
                otp.CurrentOfftakePointStatus = o.CurrentOfftakePointStatus;
                otp.ConsumptionGroups = o.ConsumptionGroups;                
                offTakePoints.Add(otp);                                                    
            }
            return offTakePoints;
        }

        public static PPServiceWCFClient.ArrayOfModifyOfftakePoint MapOfftakePointsModify(List<PPOfftakePoint> otps)
        {
            var offTakePoints = new PPServiceWCFClient.ArrayOfModifyOfftakePoint();
            if (otps == null)
                return offTakePoints;

            foreach (var o in otps)
            {
                offTakePoints.Add(new PPServiceWCFClient.ModifyOfftakePoint()
                {
                    CityGateCode = o.CityGateCode,
                    LoadType = Helpers.GetLoadType(o.LoadType),
                    MeasurementDeviceMultiplier = o.MeasurementDeviceMultiplier,
                    OfftakePointCode = o.OfftakePointCode,
                    Status = Helpers.GetOfftakePointStatus(o.Status),
                    //SupplierCode = o.SupplierCode,
                    ValidFrom = o.ValidFrom,
                    YearlyOfftake = o.YearlyOfftake
                });
            }
            return offTakePoints;
        }

        public static PPServiceWCFClient.ArrayOfModifyOfftakePointEis MapOfftakePointsModifyEIS(List<PPOfftakePoint> otps)
        {
            var offTakePoints = new PPServiceWCFClient.ArrayOfModifyOfftakePointEis();
            if (otps == null)
                return offTakePoints;

            foreach (var o in otps)
            {
                offTakePoints.Add(new PPServiceWCFClient.ModifyOfftakePointEis()
                {
                    CityGateCode = o.CityGateCode,
                    LoadType = Helpers.GetLoadType(o.LoadType),
                    MeasurementDeviceMultiplier = o.MeasurementDeviceMultiplier,
                    OfftakePointCode = o.OfftakePointCode,
                    Status = Helpers.GetOfftakePointStatus(o.Status),                    
                    ValidFrom = o.ValidFrom,
                    YearlyOfftake = o.YearlyOfftake,
                    IsProtectedConsumer = o.IsProtectedConsumer,
                    IsHouseholdConsumer = o.IsHouseholdConsumer,                    
                    InterruptibleSupplyContract = o.InterruptibleSupplyContract,
                    AlternativeEnergySource = o.AlternativeEnergySource,                    
                    IsActive = o.IsActive,
                    CurrentOfftakePointStatus = o.CurrentOfftakePointStatus,
                    ProtectedUserConsume = o.ProtectedUserConsumePart
                    //OfftakePurpose = o.p
                });
            }
            return offTakePoints;
        }

        public static PPServiceWCFClient.ArrayOfChangeOfftakePointSupplier MapOfftakePointsChangeSupplier(List<PPOfftakePoint> otps)
        {
            var offTakePoints = new PPServiceWCFClient.ArrayOfChangeOfftakePointSupplier();
            if (otps == null)
                return offTakePoints;

            foreach (var o in otps)
            {
                offTakePoints.Add(new PPServiceWCFClient.ChangeOfftakePointSupplier()
                {
                    CityGateCode = o.CityGateCode,
                    OfftakePointCode = o.OfftakePointCode,
                    SupplierCode = o.SupplierCode
                    ,ValidFrom = o.ValidFrom??DateTime.Now
            });
            }
            return offTakePoints;

        }
        public static PPServiceWCFClient.ArrayOfAddOfftakePointReading MapOfftakePointsReadings(List<PPOfftakePointReading> otpr)
        {
            var offTakePointsReadings = new PPServiceWCFClient.ArrayOfAddOfftakePointReading();
            if (otpr == null)
                return offTakePointsReadings;

            foreach (var o in otpr)
            {
                offTakePointsReadings.Add(new PPServiceWCFClient.AddOfftakePointReading()
                {
                    OfftakePointCode = o.OfftakePointCode,
                    CityGateCode = o.CityGateCode,
                    ReadingDate = o.ReadingDate,
                    ReadingValue = o.ReadingValue,
                    Nm3ConversionFactor = o.Nm3ConversionFactor,
                    NewNm3ConversionFactor = o.NewNm3ConversionFactor,
                    NewReadingValue = o.NewReadingValue
                });
            }
            return offTakePointsReadings;
        }

        public static PPServiceWCFClient.ArrayOfAnnullOfftakePointReading MapOfftakePointsReadingsAnnul(List<PPOfftakePointReadingAnnul> otpr)
        {
            var offTakePointsReadings = new PPServiceWCFClient.ArrayOfAnnullOfftakePointReading();
            if (otpr == null)
                return offTakePointsReadings;

            foreach (var o in otpr)
            {
                offTakePointsReadings.Add(new PPServiceWCFClient.AnnullOfftakePointReading()
                {
                    OfftakePointCode = o.OfftakePointCode,
                    CityGateCode = o.CityGateCode,
                    ReadingDate = o.ReadingDate,
                });
            }
            return offTakePointsReadings;
        }
        public static PPServiceWCFClient.ArrayOfAddOfftakePointMeasurements MapOfftakePointsMeasurements(List<PPOfftakePointMeasurments> otpr)
        {
            var offTakePointsMeasurments = new PPServiceWCFClient.ArrayOfAddOfftakePointMeasurements();
            if (otpr == null)
                return offTakePointsMeasurments;

            foreach (var o in otpr)
            {
                var otpm = new PPServiceWCFClient.AddOfftakePointMeasurements()
                {
                    CityGateCode = o.CityGateCode,
                    OfftakePointCode = o.OfftakePointCode,
                    Measurements = new PPServiceWCFClient.ArrayOfAddOfftakePointMeasurement()
                };
                                
                foreach (var m in o.Measurments)
                {
                    otpm.Measurements.Add(new PPServiceWCFClient.AddOfftakePointMeasurement()
                    {
                        ReadingTime = m.ReadingTime,
                        ReadingValue = m.ReadingValue,
                        Nm3ConversionFactor = m.Nm3ConversionFactor,
                        NewReadingValue = m.NewReadingValue,
                        NewNm3ConversionFactor = m.NewNm3ConversionFactor
                    });
                }

                offTakePointsMeasurments.Add(otpm);
            };                        
            
            return offTakePointsMeasurments;
        }

        public static PPServiceWCFClient.LoadType GetLoadType(string loadType)
        {
            switch (loadType)
            {
                case "Cooking":
                    return PPServiceWCFClient.LoadType.Cooking;
                case "HotWater":
                    return PPServiceWCFClient.LoadType.HotWater;
                case "HeatingSingleHome":
                    return PPServiceWCFClient.LoadType.HeatingSingleHome;
                case "HeatingAppartmentBlock":
                    return PPServiceWCFClient.LoadType.HeatingAppartmentBlock;
                case "Commercial":
                    return PPServiceWCFClient.LoadType.Commercial;
                case "Technical":
                    return PPServiceWCFClient.LoadType.Technical;
                case "HeatingWithoutCookingAndHotWater":
                    return PPServiceWCFClient.LoadType.HeatingWithoutCookingAndHotWater;
                default:
                    return PPServiceWCFClient.LoadType.HeatingSingleHome;
            }
        }

        public static PPServiceWCFClient.OfftakePointStatus GetOfftakePointStatus(string offtakePointStatus)
        {
            switch (offtakePointStatus)
            {
                case "NoOfftake":
                    return PPServiceWCFClient.OfftakePointStatus.NoOfftake;
                case "NotDailyMeasured":
                    return PPServiceWCFClient.OfftakePointStatus.NotDailyMeasured;
                case "DailyMeasured":
                    return PPServiceWCFClient.OfftakePointStatus.DailyMeasured;
                default:
                    return PPServiceWCFClient.OfftakePointStatus.NotDailyMeasured;
            }
        }


        public static PPServiceWCFClient.ForecastRegion GetForecastRegion(string forecastRegion)
        {
            switch (forecastRegion)
            {
                case "OsrednjaSlovenija":
                    return PPServiceWCFClient.ForecastRegion.OsrednjaSlovenija;
                case "Stajerska":
                    return PPServiceWCFClient.ForecastRegion.Stajerska;
                case "Gorenjska":
                    return PPServiceWCFClient.ForecastRegion.Gorenjska;
                case "Primorska":
                    return PPServiceWCFClient.ForecastRegion.Primorska;
                case "Koroska":
                    return PPServiceWCFClient.ForecastRegion.Koroska;
                case "Dolenjska":
                    return PPServiceWCFClient.ForecastRegion.Dolenjska;
                default:
                    return PPServiceWCFClient.ForecastRegion.Stajerska;
            }
        }

        public static string GetErrorOznaka(string error)
        {
            string oznaka = null;
            if (string.IsNullOrEmpty(error))
                return oznaka;

            try
            {
                if (error.Contains(":"))
                {
                    oznaka = error.Substring(1, error.IndexOf(":") - 2).Trim();
                }
            }
            catch (Exception ex)
            {
                // no biggie
            }

            return oznaka;
        }
    }
}
