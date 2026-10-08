using System;

namespace CORNCommon.Classes
{
    /// <summary>
    /// Enums
    /// <author>Rizwan Ansari</author>
    /// <date>2007-09-05</date>
    /// </summary>
    public class Enums
    {
        public enum TransactionType
        {
            Debit = 76,
            Credit = 75
        }

        public enum PrintPaper
        {
            A5 = 5,
            A4 = 4
        }

        public enum PaymentMode
        {
            Cash = 66,
            Cheque = 67,
            Credit = 68,
            Both = 69
        }

        public enum PartyType
        {
            Supplier = 64,
            Customer = 65,
            FixedAssets = 62,
            Expense = 61,
            Bank = 60,
            Cash = 63,
            SalesForce = 100,
            SaleType = 106,
            Purchase = 123
        }

        public enum ClaimType
        {
            PromotionalClaim = 51,
            SalesReturnClaim = 52,
            DamageClaim = 108,
            TADAClaims = 53,
            IncentivesClaim = 54,
            OtherClaim = 55
        }

        public enum ClaimStatus
        {
            Pending = 56,
            Approved = 57,
            Cancelled = 58,
            Rejected = 59
        }

        public enum ClaimTo
        {
            HeadOffice = 0,
            Factory = 1,
            Distributor = 2
        }

        public enum PurchaseType
        {
            Pickslip = 94,
            Invoice = 95,
            Requisition = 101,
            ReturnPickSlip = 109
        }

        public enum DisputeStatus
        {
            Pending = 0,
            Approved = 1,
            Rejected = 2
        }

        public enum SalesForceType
        {
            Delieryman = 98,
            OrderBooker = 97,
            SalesPerson = 99
        }

        public enum CheckEntries
        {
            Unhandled = 0,
            Bounced = 1,
            Cleared = 2,
            Deposit = 3
        }

        public enum AppSetting
        {
            DefaultPage = 1,
            AppPath = 2,
            Orientation = 3,
            ComputerInfo = 4,
            PaperFormat = 5,
            Deployed = 6,
            DistributorID = 7,
            IsSync = 8,
            SMSSendingType = 9,
            SMSComPort = 10,
            SMSbaudrate = 11,
            SMStimeout = 12,
            SMSUserID = 13,
            SMSPassword = 14,
            SMSMask = 15,
            MarginLeft = 16,
            MarginRight = 17,
            MarginTop = 18,
            MarginBottom = 19,
            SMTPServer = 20,
            EMAILUserName = 21,
            EMAILPassword = 22,
            EMIALPort = 23,
            IsEnabledSSL = 24,
            IsUsedDefaultCredentials = 25,
            PrinterName = 26,
            PrinterAddress = 27,
            NoOfCopies = 28,
            IsDirectPrinting = 29,
            PrinterPort = 30,
            IsTrail = 31,
            ActivationDate = 32,
            StartDate = 33,
            EndDate = 34,
            DeactivationDate = 35,
            IsEncreptedCredentials = 36,
            ShowClosingStockStatus = 37,
            NegativeAllowed = 38,
            ExpiryAllowed = 39,
            RawMaterialDeductionOnProduction = 40,
            PackageMaterialDeductionOnSale = 41,
            IsDeployed = 42,
            IsFinanceIntegrate = 43
        }

        public enum AppSettingMaster
        {
            ConfigSetting = 1,
            Sync = 2,
            SMS = 3,
            PageSettings = 4,
            EmailSetting = 5,
            PrinterSettings = 6,
            WebSettings = 7,
            InventorySetting = 8,
            FinanceSetting = 9
        }
        public enum COAMapping
        {
            
            Inventoryatstore         = 3,
            AccountPayable           = 4,
            PurchaseDiscount         = 5,
            StockInTransit           = 6,
            StockDamage              = 7,
            StockinTrade             = 8,
            ShortExcessstock         = 9,
            CreditCardSaleReceivable = 10,
            CashSale                 = 11,
            CreditSales              = 12,
            SalesTax                 = 13,
            DiscountonSale           = 14,
            Consumption              = 15,
            CashInHand               = 17,
            PettyExpense = 18
        }

    }
}
