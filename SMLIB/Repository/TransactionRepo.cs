using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLIB.Context;
using SMLIB.Entity;
using SMLIB.View;

namespace SMLIB.Repository
{
    public class TransactionRepo
    {
        public static List<vwTransaction> retrieveSubTotalAndDate() {
            List<vwTransaction> transaction;
            using (SMLIB.Context.Context context=new SMLIB.Context.Context())
            {
                transaction = (from t in context.Transactions select new vwTransaction {
                                TransactionSubTotal = t.TransactionSubTotal ,
                                TransactionDate = t.TransactionDate
                                }).ToList();
            }
            return transaction;
        }
        public static void insertTransaction(Guid transactionId,Guid cashierId, double amountReceived, double amountReturned, double subTotal,  Guid ownerId) {
            using (SMLIB.Context.Context context = new SMLIB.Context.Context()) {
                var transaction = new Transaction() {
                    TransactionId = transactionId,
                    TransactionAmountReceived = amountReceived,
                    TransactionAmountReturned = amountReturned,
                    TransactionDate = DateTime.Now.ToString("d"),
                    TransactionSubTotal = subTotal,
                    Transaction_OwnerId = ownerId,
                    Transaction_Remarks = "Successful",
                    Transaction_Status = "Successful",
                    Transaction_CashierId = cashierId,
                    Transaction_Time = (DateTime.Now.TimeOfDay.ToString()),
                    TransactionDateModified = "00:00",
                    TransactionNewAmountReturned=0.00,
                    TransactionTimeModified="00:00"
                    
                    
                };
                context.Transactions.Add(transaction);
                context.SaveChanges();
            }

        }

       
            public static List<vwTransaction> retrieveTransactionToday(string dateTime) {
            List<vwTransaction> transactions;
            using (SMLIB.Context.Context context= new SMLIB.Context.Context()) {
                transactions = (from t in context.Transactions

                                where t.TransactionDate == dateTime
                                orderby t.Transaction_Time descending
                                select new vwTransaction {
                                    TransactionDate = dateTime,
                                    TransactionAmountReceived = t.TransactionAmountReceived,
                                    TransactionAmountReturned = t.TransactionAmountReturned,
                                    TransactionId = t.TransactionId,
                                    TransactionSubTotal = t.TransactionSubTotal,
                                    Transaction_OwnerId = t.Transaction_OwnerId,
                                    Transaction_Remarks = t.Transaction_Remarks,
                                    Transaction_Status = t.Transaction_Status,
                                    TransactionNewAmountReturned = t.TransactionNewAmountReturned,
                                    TransactionTime = t.Transaction_Time,
                                    TransactionDateModified = t.TransactionDateModified,
                                    TransactionTimeModified = t.TransactionTimeModified,
                                    TransactionCashier = (from u in context.UserDetails where u.UserDetailId == t.Transaction_CashierId select u.UserFirstName + " " + u.UserLastName).FirstOrDefault(),
                                    TransactionCustomer = (from udetail in context.UserDetails where udetail.UserDetailId == t.Transaction_OwnerId select udetail.UserFirstName + " " + udetail.UserLastName).FirstOrDefault()


                                }).ToList();
            }
            return transactions;
        }
        public static void updateTransaction(Guid transactionId, double newamountreturned,double subtotal,string remarks, string status) {
            using (SMLIB.Context.Context context = new SMLIB.Context.Context()) {
                var transaction = (from tran in context.Transactions
                                  where tran.TransactionId == transactionId
                                  select tran).FirstOrDefault();       
                transaction.TransactionNewAmountReturned = newamountreturned;
                transaction.TransactionSubTotal = subtotal;
                transaction.Transaction_Remarks = remarks;
                transaction.Transaction_Status = status;
                transaction.TransactionDateModified = DateTime.Now.ToString("d");
                transaction.TransactionTimeModified = DateTime.Now.ToUniversalTime().ToString();
                context.SaveChanges();
            }
            
        }
    }
}
