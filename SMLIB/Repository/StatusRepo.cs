using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLIB.Context;
using SMLIB.Entity;
namespace SMLIB.Repository
{
    public class StatusRepo
    {
        public static List<Status> retrieve() {
            List<Status> statuses;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                statuses = (from s in context.Statuses
                           select s).ToList();
            }
            return statuses;
        }
        public static Guid getStatusId(string status) {
            Guid id;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                id = (from s in context.Statuses
                      where s.StatusValue == status
                      select s.StatusId).FirstOrDefault();
            }
            return id;
        }
        public static void delete(Guid id) {
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                var stat = (from status in context.Statuses where status.StatusId == id select status).FirstOrDefault();
                context.Statuses.Remove(stat);
                context.SaveChanges(); 
            }
        }
        public static void update(Guid id, string name) {
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                var stat = (from s in context.Statuses where s.StatusId == id select s).FirstOrDefault();
                stat.StatusValue = name;
                context.SaveChanges();
            }
        }
        public static void insert(Guid id, string name) {
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                var stat = new Status() {
                    StatusId=id,
                    StatusValue=name
                };
                context.Statuses.Add(stat);
                context.SaveChanges();
            }
        }
        public static List<Status> retrieveByName(string name) {
            List<Status> s;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                 s = (from stat in context.Statuses where stat.StatusValue == name select stat).ToList();
            }
            return s;

        }
        public static bool checkIfStatusExists(string name) {
            bool b = false;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                b = context.Statuses.Any(x => x.StatusValue == name);
            }
            return b;
        }
    }
}
