using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLIB.Context;
using SMLIB.Entity;

namespace SMLIB.Repository
{
    public class UnitOfMeasurementRepo
    {
        public static List<UnitOfMeasurement> retrieve() {
            List<UnitOfMeasurement> measurement;
            using (SMLIB.Context.Context context= new SMLIB.Context.Context())
            {
                 measurement = (from m in context.UnitOfMeasurements
                                  select m).Distinct().ToList();
            }
            return measurement;
        }
        public static Guid retrieveId(string unitOfMeasurement) {
            Guid id;
            using (SMLIB.Context.Context context= new SMLIB.Context.Context())
            {
                id = (from m in context.UnitOfMeasurements where m.UnitOfMeasurementName == unitOfMeasurement select m.UnitOfMeasurementId).FirstOrDefault();
            }
            return id;
        }
    }
}
