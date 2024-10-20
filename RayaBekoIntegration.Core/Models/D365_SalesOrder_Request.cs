using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.Models
{
    public class D365_SalesOrder_Request
    {
        public List<SalesOrder> SalesOrder { get; set; }

    }
    public class Address
    {
        public string RecID { set; get; }
        public string BuildingNumber { get; set; }
        public string AddressLine_1 { get; set; }
        public string AddressLine_2 { get; set; }
        public string City { get; set; }
        public string AccountNumber { get; set; }
        public string District { get; set; }
        public string StreetNumber { get; set; }
    }
    public class ClsCustomer
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Tax_Regist_Number { get; set; }
        public string Customer_Type { get; set; }
        public string M_CustID { get; set; }
        public string NationalID { get; set; }
        public string Birthday { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class ShippingAddress
    {
        public string BuildingNumber { get; set; }
        public string AddressLine_1 { get; set; }
        public string AddressLine_2 { get; set; }
        public string City { get; set; }
        public string region { get; set; }
        public string District { get; set; }
        public string StreetNumber { get; set; }
    }

    public class OrderItem
    {
        public string StoreID { get; set; }
        public string ReferenceId { get; set; }
        public string ItemCode { get; set; }
        public int QtySold { get; set; }
        public decimal UnitPrice { get; set; }
        public int HasInsurance { set; get; }
        public decimal InsuranceAmount { set; get; }

        public string Store_RECID { set; get; }
        public string ItemCode_RECID { set; get; }
        public string Category_Name { set; get; }
        public decimal? Item_POD { set; get; }

    }

    public class Totals
    {
        public double base_total { get; set; }
        public double discount { get; set; }
        public string discount_description { get; set; }
        public double payment_fees { get; set; }
        public double grand_total { get; set; }
        public string discount_coupon { set; get; }
        public string shipping_fees { set; get; }
        public string COD_Delivery_Fee { set; get; }
        public string additional_payment_fee { set; get; }

        public double total_paid { get; set; }
        public double total_due { get; set; }
    }

    public class SalesOrder
    {
        public string M_OrderNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public string DueDate { get; set; }
        public string M_ReferenceId { get; set; }
        public string StoreID { get; set; }
        public long WorkerNumber { get; set; }
        public string M_Recive_Type { get; set; }
        public string Comment { get; set; }
        public string PaymentMethod { get; set; }
        public List<ClsCustomer> Customer { get; set; }
        public List<ShippingAddress> shipping_address { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Totals Totals { get; set; }
        public string Source { set; get; }
        public List<Payment> Payments { get; set; }
        public string PaymentTransactionId { set; get; }
        public string IsElite { set; get; }
        public string OrderSubmitSource { set; get; }
    }
    public class Payment
    {
        public string name { get; set; }
        public string value { get; set; }
    }
    public class Customer
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Tax_Regist_Number { get; set; }
        public string Customer_Type { get; set; }
        public string M_CustID { get; set; }
        public string NationalID { get; set; }
        public string Birthday { get; set; }
    }
}
