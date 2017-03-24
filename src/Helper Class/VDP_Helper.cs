/*
Copyright (c) 2014 Vantiv, Inc. - All Rights Reserved.

Sample Code is for reference only and is solely intended to be used for educational purposes and is provided “AS IS” and “AS AVAILABLE” and without 
warranty. It is the responsibility of the developer to  develop and write its own code before successfully certifying their solution.  

This sample may not, in whole or in part, be copied, photocopied, reproduced, translated, or reduced to any electronic medium or machine-readable 
form without prior consent, in writing, from Vantiv, Inc.

Use, duplication or disclosure by the U.S. Government is subject to restrictions set forth in an executed license agreement and in subparagraph (c)(1) 
of the Commercial Computer Software-Restricted Rights Clause at FAR 52.227-19; subparagraph (c)(1)(ii) of the Rights in Technical Data and Computer 
Software clause at DFARS 252.227-7013, subparagraph (d) of the Commercial Computer Software--Licensing clause at NASA FAR supplement 16-52.227-86; 
or their equivalent.

Information in this sample code is subject to change without notice and does not represent a commitment on the part of Vantiv, Inc.  In addition to 
the foregoing, the Sample Code is subject to the terms and conditions set forth in the Vantiv Terms and Conditions of Use (http://www.apideveloper.vantiv.com) 
and the Vantiv Privacy Notice (http://www.vantiv.com/Privacy-Notice).  
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using CertSuiteTool_ODP;
using System.Net;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace CertSuiteTool.Helper_Class
{
    class VDP_Helper
    {
        #region Variable Declarations
        private static Send_Transactions target;

        #region Vantiv Developer Portal (VDP) endpoints
        //Credit
        private static string urlCredit_Sale;
        private static string urlCredit_Authorization;
        private static string urlCredit_AuthorizationCompletion;
        private static string urlCredit_Adjustment;
        private static string urlCredit_Return;
        private static string urlCredit_Reversal;
        private static string urlCredit_Credit;
        private static string urlCredit_Void;
        private static string urlCredit_BatchClose;
        private static string urlCredit_BatchBalance;
        
        //Debit
        private static string urlDebit_Sale;
        private static string urlDebit_Reversal;
        private static string urlDebit_Return;
        private static string urlDebit_PinlessSale;
        private static string urlDebit_PinlessReturn;
        //Gift
        private static string urlGift_Activation;
        private static string urlGift_BalanceInquiry;
        private static string urlGift_BatchBalance;
        private static string urlGift_Reload;
        private static string urlGift_Unload;
        private static string urlGift_Close;
        private static string urlGift_Authorization;
        private static string urlGift_AuthorizationCompletion;
        private static string urlGift_Sale;
        private static string urlGift_Reversal;
        private static string urlGift_Return;

        //Reports
        private static string urlTransactionQuery;
        //Services
        private static string urlPaymentAccountCreate;
        #endregion  Vantiv Developer Portal (VDP) endpoints

        #endregion Variable Declarations

        public VDP_Helper(Send_Transactions _myForm)
        {
            target = _myForm;//Setup access to the form fields
            //urlDomainVDP = target.txtVDPBaseEndpointURL.Text;
            //Credit
            urlCredit_Sale = "/v1/credit/sale?sp=1";
            urlCredit_Authorization = "/v1/credit/authorization?sp=1";
            urlCredit_AuthorizationCompletion = "/v1/credit/authorizationcompletion?sp=1";
            urlCredit_Adjustment = "/v1/credit/adjustment?sp=1";
            urlCredit_Return = "/v1/credit/return?sp=1";
            urlCredit_Reversal = "/v1/credit/reversal?sp=1";
            urlCredit_Credit = "/v1/credit/credit?sp=1";
            urlCredit_Void = "/v1/credit/void?sp=1";
            
            //Settlement
            urlCredit_BatchClose = "/v1/credit/batchclose?sp=1";
            urlCredit_BatchBalance = "/v1/credit/batchbalance?sp=1";

            //Debit
            urlDebit_Sale = "/v1/debit/sale?sp=1";
            urlDebit_Reversal = "/v1/debit/reversal?sp=1";
            urlDebit_Return = "/v1/debit/return?sp=1";
            urlDebit_PinlessSale = "/v1/debit/pinlesssale?sp=1";
            urlDebit_PinlessReturn = "/v1/debit/pinlessreturn?sp=1";

            //Gift
            urlGift_Activation = "/v1/gift/activation?sp=1";
            urlGift_BalanceInquiry = "/v1/gift/balanceinquiry?sp=1";
            urlGift_BatchBalance = "/v1/gift/batchbalance?sp=1";
            urlGift_Reload = "/v1/gift/reload?sp=1";
            urlGift_Unload = "/v1/gift/unload?sp=1";
            urlGift_Close = "/v1/gift/close?sp=1";
            urlGift_Authorization = "/v1/gift/authorization?sp=1";
            urlGift_AuthorizationCompletion = "/v1/gift/authorizationcompletion?sp=1";
            urlGift_Sale = "/v1/gift/sale?sp=1";
            urlGift_Reversal = "/v1/gift/reversal?sp=1";
            urlGift_Return = "/v1/gift/return?sp=1";

            //Reports
            urlTransactionQuery = "/v1/reports/transactionquery?sp=1";
            //Services
            urlPaymentAccountCreate = "/v1/services/paymentaccountcreate?sp=1";
        }
        
        #region REST Objects

        private static void merchantType(ref OmnichannelDeveloperPortal vdp)
        {
            /*
             * As mentioned above, there are 3 sections to a request. This section describes the Merchant details section, its optional 
             * and mandatory values, and selectable values. Merchant Detail has 2 sections, the merchant detail and terminal detail. Terminal 
             * detail has 3 options: Software, Mobile, and Terminal. The differences and details will be discussed later in this section. 
             * See example 3 for a sample merchant detail.  The merchant detail information will also be delivered as part of the merchant 
             * boarding process.  The merchant or developer will receive a VAR sheet, which will contain all the necessary merchant credentials 
             * (chain, merchant ID, terminal ID, etc) necessary to configure the merchant details section of the header.
             */

            //Merchant Detail Section
            if (target.TxtCashierNumber.Text.Length > 0)
                vdp.merchant.CashierNumber = target.TxtCashierNumber.Text;
            vdp.merchant.ChainCode = target.TxtChainCode.Text; //NOTE: *DB* I believe documentation is incorrect (ChainNumber) : 5 character alphanumeric value to represent the company’s chain where the transaction was entered. If provided on the VAR sheet the value provided should be used	Conditional, use if provided
            //vdp.ClerkNumber = Convert.ToInt32(TxtClerkNumber.Text);//3 digit value to represent the clerk entering the transaction	Mandatory
            //if (TxtClerkNumber.Text.Length > 0)
            //    vdp.ClerkNumberSpecified = true;
            vdp.merchant.DivisionNumber = target.TxtDivisionNumber.Text; //3 character alphanumeric value to represent the company’s division where the transaction was entered. The default value is “001”	Optional,use if Division level reporting is required 
            vdp.merchant.LaneNumber = target.TxtLaneNumber.Text; //3 character alpha numeric value to represent which lane that the transaction was entered.  Used for multi threading of transactions.	Conditional, should be 2 digit number, 0-99.  Used for multi threading transactions in host capture environment
            vdp.merchant.MerchantID = target.TxtMID.Text;//"4445000865113"; //Identifying ID set up during the boarding process. It can be up to 36 digits.  Also known as MID or merchant account.	Mandatory
            vdp.merchant.MerchantName = target.TxtMerchantName.Text; //15 character string value used to send the name of the bill payment acquiring merchant in order for customer to see the merchant name in debit statement.	Conditional-used for PIN-less Debit transactions
            vdp.merchant.NetworkRouting = target.TxtNetworkRouting.Text; //2 character value used to send the transaction to the proper credit network	Mandatory
            vdp.merchant.StoreNumber = target.TxtStoreNumber.Text; //8 character alphanumeric value to represent the company’s store number where the transaction was entered. The default value is “00000001”	Optional

            //Terminal Detail Section (3 options)
           /* NOTE: 5/5/2015 - The concept below of Software, Mobile or Terminal will be removed in the next major release of PWS. Please also know that
            * setting one or the other does not have any processing differences. Because of that reason we will hardcode the sample to always use the "Software"
            * option. We've left the code commented out below just for reference. It will be removed with the next major release of PWS.
           */
            //if (target.CboTerminalDetail.Text == "Terminal")//Options "Terminal", "Software", "Mobile"
            //{
            //    vdp.terminal.DeviceType = CertSuiteTool_VDP.DeviceType.Terminal;
            //    terminalValues(ref vdp);
            //}
            //else if (target.CboTerminalDetail.Text == "Software")
            //{
            vdp.terminal.DeviceType = CertSuiteTool_ODP.DeviceType.Software;
            terminalValues(ref vdp);
            //}
            //else if (target.CboTerminalDetail.Text == "Mobile")
            //{
            //    vdp.terminal.DeviceType = CertSuiteTool_VDP.DeviceType.Mobile;
            //    terminalValues(ref vdp);
            //}
        }

        private static void terminalValues(ref OmnichannelDeveloperPortal vdp)
        {
            vdp.terminal.IPv4Address = "192.0.2.235"; //The IPv4Address of the calling transaction. The format is 4 sets of up to 3 digits separated by “.”. An example would be “127.0.0.0”. Either IPv4Address or IPv6Address may be entered.	Optional
            //vdp.terminal.setIPv6Address("192.0.2.235"); //The unique 128-bit networking Internet Protocol version 6 address (IP Address) of the terminal recording the transaction detail. Either the IPv4Address or IPv6Address may be provided. Pattern validator: ([A-Fa-f0-9]{1,4}:){7}[A-Fa-f0-9]{1,4}
            vdp.terminal.TerminalID = target.TxtTID.Text; //3 digit value that identifies the terminal used. The terminal ID will be provided on the VAR sheet.  Also known as the TID	Mandatory
            vdp.terminal.TerminalEnvironmentCode = (CertSuiteTool_ODP.TerminalClassificationType)target.CboTerminalEnvironmentalCode.SelectedItem;
            //if (target.CboCardInputCode.Text.Length > 1)
            //    vdp.terminal.CardInputCode = (CertSuiteTool_ODP.CardInputCode)target.CboCardInputCode.SelectedItem;//SOON TO BE DEPRICATED by cardReader 4.22.2015
            //else
            //    vdp.terminal.CardInputCode = null;
            vdp.terminal.CardReader = (CertSuiteTool_ODP.CardReaderType)target.CboCardReaderType.SelectedItem;//ADDED 4.22.2015
            vdp.terminal.PinEntry = (CertSuiteTool_ODP.PinEntryType)target.CboPinEntry.SelectedItem; //An optional value to identify the type of pin entry used by this terminal. Optional – Defaults must be changed to reflect actual entry method and point of entry environment. Mandatory for PIN Debit Transactions, should always be set to “supported” 
            vdp.terminal.BalanceInquiry = (bool)target.CboBalanceInquiry.SelectedItem; //Boolean value (true, false) to determine if balance inquiry fields will be returned. The default value is false.	Optional – Must be set to true for card present transactions so that prepaid cards can be supported.  We recommend leaving as false for card not present transactions.
            vdp.terminal.HostAdjustment = (bool)target.CboHostAdjustment.SelectedItem; //Boolean value (true, false) to determine if adjust transaction will be allowed for a batch. The default value is false.	Optional – if need to adjust a transaction amount (ex. Add tip, add level 2 data) set to true.  Flag setting should be consistent across all transaction types for a given application. Note – Setting HostAdjustment to true will disable batch auto close functionality.  The host will not auto close a batch if HostAdjust is set to true.
            vdp.terminal.EntryMode = (CertSuiteTool_ODP.EntryModeType)target.CboEntryMode.SelectedItem; //Required value to identify the type of terminal entry used in the transaction. Your choices are: Mandatory – Value must reflect entry method and point of entry environment.  If not updated, may cause interchange qualification impact.
         }

        private static void InstrumentType(ref OmnichannelDeveloperPortal vdp)
        {
            //if requested set the address information
            if (target.ChkSetAddressInformation.Checked)
                addressType(ref vdp);
            else
                vdp.address = null;

            vdp.card = new Card();
            if (!target.ChkUseToken.Checked)//When using a token a token should not be requested.
                vdp.transaction.TokenRequested = target.ChkTokenRequested.Checked;
            if (target.TxtCardSecurityCode.Text.Length > 0)
                vdp.card.CVV = target.TxtCardSecurityCode.Text;
           
            if (target.CboCreditType.Text == "CardKeyed")
            {
                if(target.TxtPrimaryAccountNumber.Text.Length > 0)
                    vdp.card.CardNumber = target.TxtPrimaryAccountNumber.Text;
                DateTime dt = Convert.ToDateTime(target.TxtExpirationDate.Text);
                vdp.card.ExpirationMonth = dt.Month.ToString("D2");
                vdp.card.ExpirationYear = dt.Year.ToString();
            }
            else if (target.CboCreditType.Text == "CardSwiped")
            {
                if (target.ChkEncryptedData.Checked)
                {
                    MessageBox.Show("Encryption is currently not supported through Vantiv Developer Portal (VDP)");
                }
                else
                {
                    if ((ItemChoiceType)target.CboTrackChoice.SelectedItem == ItemChoiceType.Track1)
                    {
                        vdp.card.Track1Data = target.TxtTrackData.Text;
                    }
                    else if ((ItemChoiceType)target.CboTrackChoice.SelectedItem == ItemChoiceType.Track2)
                    {
                        vdp.card.Track2Data = target.TxtTrackData.Text;
                    }
                }
            }
            if (target.ChkUseToken.Checked)
            {
                if( target.TxtTokenId.Text.Length > 1)
                    vdp.card.TokenId = target.TxtTokenId.Text;
                if (target.TxtTokenValue.Text.Length > 1)
                    vdp.card.TokenValue = target.TxtTokenValue.Text;
                vdp.card.CardNumber = null; //Per the schema if Token is set, PAN should not be sent.
                vdp.card.CVV = null; //In the case of a tokenized transaction CV data would not be available as CV codes cannot be stored in a database in any form per PCI.
            }
            //Check to see if this is a PINDebit transaction
            if (target.CboPaymentInstrument.Text == "Debit")
            {
                vdp.card.PINBlockEncryptedFormat = (CertSuiteTool_ODP.EncryptionType)target.CboPINDataEncryptionType.SelectedItem;
                vdp.card.PINBlock = target.TxtTxtPINDataPINBlock.Text;
                vdp.card.KeySerialNumber = target.TxtPINDataKeySerialNum.Text;
            }
        }

        private static void addressType(ref OmnichannelDeveloperPortal vdp)
        {
            if (target.ChkSetAddressInformation.Checked)
            {
                vdp.address = new Address();
                vdp.address.BillingAddress1 = target.TxtAddressLine.Text;
                vdp.address.BillingCity = target.TxtCity.Text;
                vdp.address.CountryCode = (CertSuiteTool_ODP.ISO3166CountryCodeType)target.CboCountryCode.SelectedItem;
                vdp.address.BillingZipcode = target.TxtPostalCode.Text;
                vdp.address.BillingState = target.CboStateType.Text;
            }
            else
                vdp.address = null;
        }

        #endregion REST Objects

        #region Transaction Processing

        //Credit transaction processing
        public static ResponseDetails saleRequest(ref OmnichannelDeveloperPortal vdp)
        {

            Random r = new Random();
            merchantType(ref vdp);
            vdp.transaction.MarketCode = (MarketCode)target.CboTransactionType.SelectedItem;
            vdp.transaction.ClerkNumber = target.TxtClerkNumber.Text;
            //vdp.TransactionType = (TransactionTypeType)CboTransactionType.SelectedItem;//Mandatory
            vdp.transaction.PaymentType = (CertSuiteTool_ODP.PaymentType)target.CboPaymentType.SelectedItem;//Mandatory
            vdp.transaction.DraftLocatorId = "D" + r.Next(1, 99999999).ToString(); //11 character value.  This field can be used to pass whatever discretionary data the merchant wants to pass.  Examples include employee ID number, invoice numbers, any internal value they use to track transactions.	Optional – only passes thru to reporting on Visa and MasterCard transactions.
            vdp.transaction.ReferenceNumber = "R" + r.Next(1, 99999).ToString(); //6 digit value which uniquely identifies the transaction.	Optional
            vdp.transaction.TransactionAmount = target.TxtTransactionAmount.Text;
            vdp.transaction.TransactionTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"); //The time of this transaction. Use yyyy-MM- ddThh:mm:ss-SS:SS – Should be in merchants local time zone. Mandatory – should be in merchant’s local time zone.
            if (!target.ChkUseToken.Checked)//When using a token a token should not be requested.
                vdp.transaction.TokenRequested = target.ChkTokenRequested.Checked;//Boolean value (true, false) to determine if token is returned for the card. The default value is false.	Optional
            if (target.CboPartialApprovalCode.Text.Length > 0)
                vdp.transaction.PartialApprovalCode = (CertSuiteTool_ODP.PartialIndicatorType)target.CboPartialApprovalCode.SelectedItem;
            //vdp.BillPaymentPayee = billPaymentPayeeType(); //For PIN-less Debit : Bill payment payee details contain values about the payee including payee name, phone and account number payee uses to identify the payer.
            int rInt = r.Next(100000, 999999); //for ints
            vdp.transaction.SystemTraceID = rInt.ToString(); //A conditional ID used to track each transaction. This must be an integer. Required for Raft and Tandem, optional for Litle? Required for Litle on CancelRequest.
            vdp.transaction.TransactionID = rInt.ToString(); //This 6-character numeric string is used to uniquely identify a transaction within a 24-hour period. The Transaction Id is also referred to as a STAN (System Trace Audit Number). In the case of Debit Refund scenarios, the OriginalSequenceNumber should match the TransactionID of the original/cancel transactions. Required.
            rInt = r.Next(1, 99999999);

            InstrumentType(ref vdp); //Set base values
            string url = "";//set the proper endpoint based on instrument type

            //set additional instrument values based on payment instrument
            if (target.CboPaymentInstrument.Text == "Credit")
            {
                url = urlCredit_Sale;//In this case Credit Sale

                //CardType is only used in the case of Credit. Debit and Gift do not use CardType.
                vdp.card.CardType = (CertSuiteTool_ODP.CreditCardNetworkType)target.CboCardType.SelectedItem;

                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Debit")
            {
                url = urlDebit_Sale;//In this case Debit Sale
                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Gift")
            {
                url = urlGift_Sale;//In this case Gift Sale
            }

            //Set tax if passed for Level2 card data
            if (target.ChkSetPurchaseCardLevel2.Checked && target.CboPaymentInstrument.Text != "Gift")
            {
                vdp.transaction.Taxable = target.ChkTaxable.Checked;//It specifies if the transaction amount is taxable or not.
                vdp.transaction.SalesTaxAmount = target.TxtSalesTaxAmount.Text;//It represents the amount that is added as tax to the total amount in the transaction.
                vdp.transaction.TaxExempt = target.ChkTaxExempt.Checked; //It specifies if the transaction amount is exempt from tax or not.
            }

            //Check to see if a Tip amount is set. 
            if (target.TxtTransactionTipAmount.Text.Length > 0)
                vdp.transaction.TipAmount = target.TxtTransactionTipAmount.Text;

            string jsonRequest = "";
            string jsonResponse = CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + url, Header_licenseid(), "POST", convertToJson(vdp), ref jsonRequest, ref vdp);
            XmlDocument xmlResponse = convertToXML(jsonResponse);
            VDP_TransactionSummary ts = new VDP_TransactionSummary(vdp, xmlResponse, jsonRequest, jsonResponse);
            return ProcessResponse(ts);

            //return ProcessResponse(convertToXML(CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + urlSale, "POST", convertToJson(vdp))), vdp);
        }

        public static ResponseDetails authorizationRequest(ref OmnichannelDeveloperPortal vdp)
        {
            Random r = new Random();
            vdp.transaction.DraftLocatorId = "D" + r.Next(1, 99999999).ToString(); //11 character value.  This field can be used to pass whatever discretionary data the merchant wants to pass.  Examples include employee ID number, invoice numbers, any internal value they use to track transactions.	Optional – only passes thru to reporting on Visa and MasterCard transactions.
            merchantType(ref vdp);
            vdp.transaction.MarketCode = (MarketCode)target.CboTransactionType.SelectedItem;
            vdp.transaction.ClerkNumber = target.TxtClerkNumber.Text;
            //vdp.transaction.NetworkResponseCode = "";
            vdp.transaction.PaymentType = (CertSuiteTool_ODP.PaymentType)target.CboPaymentType.SelectedItem;//Mandatory
            vdp.transaction.ReferenceNumber = "R" + r.Next(1, 99999).ToString(); //6 digit value which uniquely identifies the transaction.	Optional
            //vdp.reportgroup = ""; //An optional (required for Litle) attribute used by the merchant to map each transaction to a reporting category.  This can be no longer than 25 characters. 
            if(!target.ChkUseToken.Checked)//When using a token a token should not be requested.
                vdp.transaction.TokenRequested = target.ChkTokenRequested.Checked;//Boolean value (true, false) to determine if token is returned for the card. The default value is false.	Optional
            vdp.transaction.TransactionAmount = target.TxtTransactionAmount.Text;
            vdp.transaction.TransactionTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"); //The time of this transaction. Use yyyy-MM- ddThh:mm:ss-SS:SS – Should be in merchants local time zone. Mandatory – should be in merchant’s local time zone.
            if (target.CboPartialApprovalCode.Text.Length > 0)
                vdp.transaction.PartialApprovalCode = (CertSuiteTool_ODP.PartialIndicatorType)target.CboPartialApprovalCode.SelectedItem;
            //vdp.TransactionType = (TransactionTypeType)CboTransactionType.SelectedItem;//Mandatory
            int rInt = r.Next(100000, 999999); //for ints
            vdp.transaction.SystemTraceID = rInt.ToString(); //A conditional ID used to track each transaction. This must be an integer. Required for Raft and Tandem, optional for Litle? Required for Litle on CancelRequest.
            vdp.transaction.TransactionID = rInt.ToString(); //This 6-character numeric string is used to uniquely identify a transaction within a 24-hour period. The Transaction Id is also referred to as a STAN (System Trace Audit Number). In the case of Debit Refund scenarios, the OriginalSequenceNumber should match the TransactionID of the original/cancel transactions. Required.
            rInt = r.Next(1, 99999999);

            InstrumentType(ref vdp); //Set base values
            string url = "";//set the proper endpoint based on instrument type

            //set additional instrument values based on payment instrument
            if (target.CboPaymentInstrument.Text == "Credit")
            {
                url = urlCredit_Authorization;//In this case Credit Sale

                //CardType is only used in the case of Credit. Debit and Gift do not use CardType.
                vdp.card.CardType = (CertSuiteTool_ODP.CreditCardNetworkType)target.CboCardType.SelectedItem;

                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Debit")
            {
                MessageBox.Show("Debit transactions do not use the Authorize transaction type. Please use Sale instead.");
                return null;
            }
            else if (target.CboPaymentInstrument.Text == "Gift")
            {
                url = urlGift_Authorization;//In this case Credit Sale
            }

            //Set tax if passed for Level2 card data
            if (target.ChkSetPurchaseCardLevel2.Checked && target.CboPaymentInstrument.Text != "Gift")
            {
                vdp.transaction.Taxable = target.ChkTaxable.Checked;//It specifies if the transaction amount is taxable or not.
                vdp.transaction.SalesTaxAmount = target.TxtSalesTaxAmount.Text;//It represents the amount that is added as tax to the total amount in the transaction.
                vdp.transaction.TaxExempt = target.ChkTaxExempt.Checked; //It specifies if the transaction amount is exempt from tax or not.
            }

            string jsonRequest = "";
            string jsonResponse = CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + url, Header_licenseid(), "POST", convertToJson(vdp), ref jsonRequest, ref vdp);
            XmlDocument xmlResponse = convertToXML(jsonResponse);
            VDP_TransactionSummary ts = new VDP_TransactionSummary(vdp, xmlResponse, jsonRequest, jsonResponse);
            return ProcessResponse(ts);

        }

        public static ResponseDetails authorizationCompletionRequest(ResponseDetails _rd, ref OmnichannelDeveloperPortal vdp)
        {
            Random ran = new Random();
            merchantType(ref vdp);
            //vdp.TransactionType = (TransactionTypeType)CboTransactionType.SelectedItem;//Mandatory
            vdp.transaction.MarketCode = (MarketCode)target.CboTransactionType.SelectedItem;
            vdp.transaction.ClerkNumber = target.TxtClerkNumber.Text;
            vdp.transaction.PaymentType = (CertSuiteTool_ODP.PaymentType)target.CboPaymentType.SelectedItem;//Mandatory
            vdp.transaction.DraftLocatorId = "D" + ran.Next(1, 99999999).ToString(); //11 character value.  This field can be used to pass whatever discretionary data the merchant wants to pass.  Examples include employee ID number, invoice numbers, any internal value they use to track transactions.	Optional – only passes thru to reporting on Visa and MasterCard transactions.
            vdp.transaction.ReferenceNumber = "R" + ran.Next(1, 99999).ToString(); //6 digit value which uniquely identifies the transaction.	Optional
            vdp.transaction.CaptureAmount = target.TxtTransactionAmount.Text;
            vdp.transaction.TransactionTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"); //The time of this transaction. Use yyyy-MM- ddThh:mm:ss-SS:SS – Should be in merchants local time zone. Mandatory – should be in merchant’s local time zone.
            if (!target.ChkUseToken.Checked)//When using a token a token should not be requested.
                vdp.transaction.TokenRequested = target.ChkTokenRequested.Checked;//Boolean value (true, false) to determine if token is returned for the card. The default value is false.	Optional
            //vdp.BillPaymentPayee = billPaymentPayeeType(); //For PIN-less Debit : Bill payment payee details contain values about the payee including payee name, phone and account number payee uses to identify the payer.
            int rInt = ran.Next(100000, 999999); //for ints
            vdp.transaction.SystemTraceID = rInt.ToString();
            vdp.transaction.TransactionID = rInt.ToString(); //This 6-character numeric string is used to uniquely identify a transaction within a 24-hour period. The Transaction Id is also referred to as a STAN (System Trace Audit Number). In the case of Debit Refund scenarios, the OriginalSequenceNumber should match the TransactionID of the original/cancel transactions. Required.
            rInt = ran.Next(1, 99999999);

            InstrumentType(ref vdp); //Set base values
            string url = "";//set the proper endpoint based on instrument type

            //set additional instrument values based on payment instrument
            if (target.CboPaymentInstrument.Text == "Credit")
            {
                url = urlCredit_AuthorizationCompletion;//In this case Credit Sale

                //CardType is only used in the case of Credit. Debit and Gift do not use CardType.
                vdp.card.CardType = (CertSuiteTool_ODP.CreditCardNetworkType)target.CboCardType.SelectedItem;

                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Debit")
            {
                MessageBox.Show("Debit transactions do not use the Authorize transaction type. Please use Sale instead.");
                return null;
            }
            else if (target.CboPaymentInstrument.Text == "Gift")
            {
                url = urlGift_AuthorizationCompletion;//In this case Credit Sale
            }

            //Set tax if passed for Level2 card data
            if (target.ChkSetPurchaseCardLevel2.Checked && target.CboPaymentInstrument.Text != "Gift")
            {
                vdp.transaction.Taxable = target.ChkTaxable.Checked;//It specifies if the transaction amount is taxable or not.
                vdp.transaction.SalesTaxAmount = target.TxtSalesTaxAmount.Text;//It represents the amount that is added as tax to the total amount in the transaction.
                vdp.transaction.TaxExempt = target.ChkTaxExempt.Checked; //It specifies if the transaction amount is exempt from tax or not.
            }

            //Set the Capture specific values
            if (target.TxtTransactionTipAmount.Text.Length > 0)
                vdp.transaction.TipAmount = target.TxtTransactionTipAmount.Text;
            vdp.transaction.AuthorizationCode = _rd.AuthorizationCode;
            vdp.transaction.OriginalAuthorizedAmount = _rd.Amount.Value.ToString();
            vdp.transaction.CaptureAmount = target.TxtTransactionAmount.Text;
            vdp.transaction.OriginalReferenceNumber = SELECT(_rd.VDP_TxnSummary.XMLResponse, "//ReferenceNumber");

            string jsonRequest = "";
            string jsonResponse = CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + url, Header_licenseid(), "POST", convertToJson(vdp), ref jsonRequest, ref vdp);
            XmlDocument xmlResponse = convertToXML(jsonResponse);
            VDP_TransactionSummary ts = new VDP_TransactionSummary(vdp, xmlResponse, jsonRequest, jsonResponse);
            return ProcessResponse(ts);

            //return ProcessResponse(convertToXML(CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + urlAuthorizationCompletion, "POST", convertToJson(vdp))), vdp);
        }

        public static ResponseDetails adjustRequest(ResponseDetails _rd, ref OmnichannelDeveloperPortal vdp)
        {
            Random ran = new Random();
            vdp.transaction.MarketCode = (MarketCode)target.CboTransactionType.SelectedItem;
            vdp.transaction.ClerkNumber = target.TxtClerkNumber.Text;
            vdp.transaction.AdjustedTotalAmount = target.TxtTransactionAmount.Text; //The new amount, which is the original amount and the adjustment
            //vdp.AuthorizationCode = "";
            //vdp.BillPaymentPayee = new BillPaymentPayeeType();
            //vdp.ConvenienceFee = new AmountType();

            InstrumentType(ref vdp); //Set base values
            //set additional instrument values based on payment instrument
            if (target.CboPaymentInstrument.Text == "Credit")
            {
                //CardType is only used in the case of Credit. Debit and Gift do not use CardType.
                vdp.card.CardType = (CertSuiteTool_ODP.CreditCardNetworkType)target.CboCardType.SelectedItem;

                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }

            vdp.transaction.DraftLocatorId = "D" + ran.Next(1, 99999999).ToString(); //11 character value.  This field can be used to pass whatever discretionary data the merchant wants to pass.  Examples include employee ID number, invoice numbers, any internal value they use to track transactions.	Optional – only passes thru to reporting on Visa and MasterCard transactions.
            merchantType(ref vdp);
            //vdp.NetworkResponseCode = "";
            vdp.transaction.PaymentType = (CertSuiteTool_ODP.PaymentType)target.CboPaymentType.SelectedItem;//Mandatory
            //vdp.PurchaseOrder = "";
            vdp.transaction.ReferenceNumber = "R" + ran.Next(1, 99999).ToString(); //6 digit value which uniquely identifies the transaction.	Optional
            //vdp.reportgroup = "";
            int rInt = ran.Next(100000, 999999); //for ints
            vdp.transaction.SystemTraceID = rInt.ToString();
            vdp.transaction.TransactionID = rInt.ToString(); //This 6-character numeric string is used to uniquely identify a transaction within a 24-hour period. The Transaction Id is also referred to as a STAN (System Trace Audit Number). In the case of Debit Refund scenarios, the OriginalSequenceNumber should match the TransactionID of the original/cancel transactions. Required.
            rInt = ran.Next(1, 99999999);
            //vdp.Tax = new TaxAmountType();
            //vdp.TipAmount = new AmountType();
            if (!target.ChkUseToken.Checked)//When using a token a token should not be requested.
                vdp.transaction.TokenRequested = target.ChkTokenRequested.Checked;//Boolean value (true, false) to determine if token is returned for the card. The default value is false.	Optional
            vdp.transaction.TransactionTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"); //The time of this transaction. Use yyyy-MM- ddThh:mm:ss-SS:SS – Should be in merchants local time zone. Mandatory – should be in merchant’s local time zone.
            //vdp.TransactionType = (TransactionTypeType)CboTransactionType.SelectedItem;//Mandatory       

            vdp.transaction.AuthorizationCode = _rd.AuthorizationCode;
            vdp.transaction.OriginalAuthorizedAmount = _rd.Amount.Value.ToString();
            vdp.transaction.OriginalReferenceNumber = SELECT(_rd.VDP_TxnSummary.XMLResponse, "//ReferenceNumber");

            string jsonRequest = "";
            string jsonResponse = CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + urlCredit_Adjustment, Header_licenseid(), "POST", convertToJson(vdp), ref jsonRequest, ref vdp);
            XmlDocument xmlResponse = convertToXML(jsonResponse);
            VDP_TransactionSummary ts = new VDP_TransactionSummary(vdp, xmlResponse, jsonRequest, jsonResponse);
            return ProcessResponse(ts);

            //return ProcessResponse(convertToXML(CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + urlAdjustment, "POST", convertToJson(vdp))), vdp);
        }

        public static ResponseDetails returnRequest(ref OmnichannelDeveloperPortal vdp)
        {
            Random r = new Random();
            merchantType(ref vdp);
            vdp.transaction.MarketCode = (MarketCode)target.CboTransactionType.SelectedItem;
            vdp.transaction.ClerkNumber = target.TxtClerkNumber.Text;
            //vdp.Merchant.Terminal = null;
            //vdp.TransactionType = (TransactionTypeType)CboTransactionType.SelectedItem;//Mandatory
            vdp.transaction.PaymentType = (CertSuiteTool_ODP.PaymentType)target.CboPaymentType.SelectedItem;//Mandatory
            vdp.transaction.DraftLocatorId = "D" + r.Next(1, 99999999).ToString(); //11 character value.  This field can be used to pass whatever discretionary data the merchant wants to pass.  Examples include employee ID number, invoice numbers, any internal value they use to track transactions.	Optional – only passes thru to reporting on Visa and MasterCard transactions.
            vdp.transaction.ReferenceNumber = "R" + r.Next(1, 99999).ToString(); //6 digit value which uniquely identifies the transaction.	Optional
            vdp.transaction.TransactionTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"); //The time of this transaction. Use yyyy-MM- ddThh:mm:ss-SS:SS – Should be in merchants local time zone. Mandatory – should be in merchant’s local time zone.
            if (!target.ChkUseToken.Checked)//When using a token a token should not be requested.
                vdp.transaction.TokenRequested = target.ChkTokenRequested.Checked;//Boolean value (true, false) to determine if token is returned for the card. The default value is false.	Optional
            vdp.transaction.TransactionAmount = target.TxtTransactionAmount.Text;
            //a.BillPaymentPayee = billPaymentPayeeType(); //For PIN-less Debit : Bill payment payee details contain values about the payee including payee name, phone and account number payee uses to identify the payer.
            int rInt = r.Next(100000, 999999); //for ints
            vdp.transaction.SystemTraceID = rInt.ToString(); //A conditional ID used to track each transaction. This must be an integer. Required for Raft and Tandem, optional for Litle? Required for Litle on CancelRequest.
            vdp.transaction.TransactionID = rInt.ToString(); //This 6-character numeric string is used to uniquely identify a transaction within a 24-hour period. The Transaction Id is also referred to as a STAN (System Trace Audit Number). In the case of Debit Refund scenarios, the OriginalSequenceNumber should match the TransactionID of the original/cancel transactions. Required.
            rInt = r.Next(1, 99999999);

            InstrumentType(ref vdp); //Set base values
            string url = "";//set the proper endpoint based on instrument type

            //set additional instrument values based on payment instrument
            if (target.CboPaymentInstrument.Text == "Credit")
            {
                url = urlCredit_Return;//In this case Credit Sale

                //CardType is only used in the case of Credit. Debit and Gift do not use CardType.
                vdp.card.CardType = (CertSuiteTool_ODP.CreditCardNetworkType)target.CboCardType.SelectedItem;

                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Debit")
            {
                url = urlDebit_Return;//In this case Credit Sale

                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Gift")
            {
                url = urlGift_Return;//In this case Credit Sale
            }

            string jsonRequest = "";
            string jsonResponse = CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + url, Header_licenseid(), "POST", convertToJson(vdp), ref jsonRequest, ref vdp);
            XmlDocument xmlResponse = convertToXML(jsonResponse);
            VDP_TransactionSummary ts = new VDP_TransactionSummary(vdp, xmlResponse, jsonRequest, jsonResponse);
            return ProcessResponse(ts);

            //return ProcessResponse(convertToXML(CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + urlReturn, "POST", convertToJson(vdp))), vdp);
        }

        public static ResponseDetails reversalRequest(ResponseDetails _rd, ref OmnichannelDeveloperPortal vdp)
        {
            Random r = new Random();
            //vdp.BillPaymentPayee = billPaymentPayeeType(); //For PIN-less Debit : Bill payment payee details contain values about the payee including payee name, phone and account number payee uses to identify the payer.
            //vdp.BillPaymentPayee.PayeeAccountNumber = "";
            //vdp.BillPaymentPayee.PayeeName = "";
            //vdp.BillPaymentPayee.PayeePhoneNumber = "";
            vdp.transaction.DraftLocatorId = "D" + r.Next(1, 99999999).ToString(); //11 character value.  This field can be used to pass whatever discretionary data the merchant wants to pass.  Examples include employee ID number, invoice numbers, any internal value they use to track transactions.	Optional – only passes thru to reporting on Visa and MasterCard transactions.
            merchantType(ref vdp);
            vdp.transaction.MarketCode = (MarketCode)target.CboTransactionType.SelectedItem;
            vdp.transaction.ClerkNumber = target.TxtClerkNumber.Text;
            //vdp.Merchant.Terminal = null;
            vdp.transaction.PaymentType = (CertSuiteTool_ODP.PaymentType)target.CboPaymentType.SelectedItem;//Mandatory
            vdp.transaction.ReferenceNumber = "R" + r.Next(1, 99999).ToString(); //6 digit value which uniquely identifies the transaction.	Optional
            vdp.transaction.TransactionTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"); //The time of this transaction. Use yyyy-MM- ddThh:mm:ss-SS:SS – Should be in merchants local time zone. Mandatory – should be in merchant’s local time zone.
            //vdp.TransactionType = ((TransactionRequestType)(_rd.TxnRequest)).TransactionType;//Mandatory
            if (!target.ChkUseToken.Checked)//When using a token a token should not be requested.
                vdp.transaction.TokenRequested = target.ChkTokenRequested.Checked;//Boolean value (true, false) to determine if token is returned for the card. The default value is false.	Optional
            int rInt = r.Next(100000, 999999); //for ints
            vdp.transaction.SystemTraceID = rInt.ToString(); //A conditional ID used to track each transaction. This must be an integer. Required for Raft and Tandem, optional for Litle? Required for Litle on CancelRequest.
            vdp.transaction.TransactionID = rInt.ToString(); //This 6-character numeric string is used to uniquely identify a transaction within a 24-hour period. The Transaction Id is also referred to as a STAN (System Trace Audit Number). In the case of Debit Refund scenarios, the OriginalSequenceNumber should match the TransactionID of the original/cancel transactions. Required.
            rInt = r.Next(1, 99999999);

            InstrumentType(ref vdp); //Set base values
            string url = "";//set the proper endpoint based on instrument type

            //set additional instrument values based on payment instrument
            if (target.CboPaymentInstrument.Text == "Credit")
            {
                url = urlCredit_Reversal;//In this case Credit Sale

                //CardType is only used in the case of Credit. Debit and Gift do not use CardType.
                vdp.card.CardType = (CertSuiteTool_ODP.CreditCardNetworkType)target.CboCardType.SelectedItem;

                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Debit")
            {
                url = urlDebit_Reversal;//In this case Credit Sale

                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Gift")
            {
                url = urlGift_Reversal;//In this case Credit Sale
            }
            if (target.ChkCancelReplacementAmount.Checked)
            {
                //Remaining amount of the transaction after partial cancel. If the cancel amount is less than original amount, use the replacement amount
                vdp.transaction.ReplacementAmount = target.TxtCancelReplacementAmount.Text;
            }

            //Check to see if this is a system or merchant cancel
            if (target.CboReversalReason.SelectedItem != null && (ReversalReasonType)target.CboReversalReason.SelectedItem == ReversalReasonType.TIME_OUT)
            {//System
                vdp.transaction.CancelType = (CertSuiteTool_ODP.CancelTransactionType)target.CancelTransactionTypeFromRequest(_rd.TxnRequestType);
                vdp.transaction.OriginalAuthorizedAmount = _rd.VDP_TxnSummary.VDPRequest.transaction.TransactionAmount;
                vdp.transaction.OriginalTransactionTimestamp = _rd.VDP_TxnSummary.VDPRequest.transaction.TransactionTimestamp;
                vdp.transaction.OriginalSystemTraceId = _rd.VDP_TxnSummary.VDPRequest.transaction.SystemTraceID;
                vdp.transaction.SystemTraceID = _rd.VDP_TxnSummary.VDPRequest.transaction.SystemTraceID;
                vdp.transaction.OriginalSequenceNumber = _rd.VDP_TxnSummary.VDPRequest.transaction.TransactionID;
                //vdp.OriginalReferenceNumber = ((TransactionResponseType)(_rd.Response)).ReferenceNumber;
                //vdp.OriginalAuthCode = _rd.AuthorizationCode;
                //vdp.NetworkResponseCode = ((TransactionResponseType)(_rd.Response)).NetworkResponseCode;
                vdp.transaction.ReversalReason = (CertSuiteTool_ODP.ReversalReasonType)target.CboReversalReason.SelectedItem;
            }
            else
            {//Merchant
                vdp.transaction.CancelType = (CertSuiteTool_ODP.CancelTransactionType)target.CancelTransactionTypeFromRequest(_rd.TxnRequestType);
                vdp.transaction.OriginalAuthorizedAmount = _rd.Amount.Value.ToString();
                vdp.transaction.OriginalTransactionTimestamp = _rd.VDP_TxnSummary.VDPRequest.transaction.TransactionTimestamp;
                vdp.transaction.OriginalSystemTraceId = SELECT(_rd.VDP_TxnSummary.XMLResponse, "//@system-trace-id");
                vdp.transaction.OriginalReferenceNumber = SELECT(_rd.VDP_TxnSummary.XMLResponse, "//ReferenceNumber");
                vdp.transaction.OriginalSequenceNumber = _rd.VDP_TxnSummary.VDPRequest.transaction.TransactionID;
                // NEED TO ADD can.OriginalSequenceNumber = p.Merchant.Software.SequenceNumber;
                vdp.transaction.OriginalAuthCode = _rd.AuthorizationCode;
                vdp.transaction.NetworkResponseCode = SELECT(_rd.VDP_TxnSummary.XMLResponse, "//NetworkResponseCode");
                if(target.CboReversalReason.SelectedItem != null)
                    vdp.transaction.ReversalReason = (CertSuiteTool_ODP.ReversalReasonType)target.CboReversalReason.SelectedItem;
            }

            string jsonRequest = "";
            string jsonResponse = CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + url, Header_licenseid(), "POST", convertToJson(vdp), ref jsonRequest, ref vdp);
            XmlDocument xmlResponse = convertToXML(jsonResponse);
            VDP_TransactionSummary ts = new VDP_TransactionSummary(vdp, xmlResponse, jsonRequest, jsonResponse);
            return ProcessResponse(ts);

            //return ProcessResponse(convertToXML(CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + urlReversal, "POST", convertToJson(vdp))), vdp);

        }

        public static ResponseDetails voidRequest(ResponseDetails _rd, ref OmnichannelDeveloperPortal vdp, bool _txnTimeOut)
        {
            Random r = new Random();
            vdp.transaction.DraftLocatorId = "D" + r.Next(1, 99999999).ToString(); //11 character value.  This field can be used to pass whatever discretionary data the merchant wants to pass.  Examples include employee ID number, invoice numbers, any internal value they use to track transactions.	Optional – only passes thru to reporting on Visa and MasterCard transactions.
            merchantType(ref vdp);
            vdp.transaction.MarketCode = (MarketCode)target.CboTransactionType.SelectedItem;
            vdp.transaction.ClerkNumber = target.TxtClerkNumber.Text;
            //vdp.Merchant.Terminal = null;
            vdp.transaction.PaymentType = (CertSuiteTool_ODP.PaymentType)target.CboPaymentType.SelectedItem;//Mandatory
            vdp.transaction.ReferenceNumber = "R" + r.Next(1, 99999).ToString(); //6 digit value which uniquely identifies the transaction.	Optional
            vdp.transaction.TransactionTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"); //The time of this transaction. Use yyyy-MM- ddThh:mm:ss-SS:SS – Should be in merchants local time zone. Mandatory – should be in merchant’s local time zone.
            //vdp.TransactionType = ((TransactionRequestType)(_rd.TxnRequest)).TransactionType;//Mandatory
            if (!target.ChkUseToken.Checked)//When using a token a token should not be requested.
                vdp.transaction.TokenRequested = target.ChkTokenRequested.Checked;//Boolean value (true, false) to determine if token is returned for the card. The default value is false.	Optional
            int rInt = r.Next(100000, 999999); //for ints
            vdp.transaction.SystemTraceID = rInt.ToString(); //A conditional ID used to track each transaction. This must be an integer. Required for Raft and Tandem, optional for Litle? Required for Litle on CancelRequest.
            vdp.transaction.TransactionID = rInt.ToString(); //This 6-character numeric string is used to uniquely identify a transaction within a 24-hour period. The Transaction Id is also referred to as a STAN (System Trace Audit Number). In the case of Debit Refund scenarios, the OriginalSequenceNumber should match the TransactionID of the original/cancel transactions. Required.
            rInt = r.Next(1, 99999999);

            InstrumentType(ref vdp); //Set base values
            //set additional instrument values based on payment instrument
            if (target.CboPaymentInstrument.Text == "Credit")
            {
                //CardType is only used in the case of Credit. Debit and Gift do not use CardType.
                vdp.card.CardType = (CertSuiteTool_ODP.CreditCardNetworkType)target.CboCardType.SelectedItem;

                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Debit")
            {
                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Gift")
            {

            }
            if (target.ChkCancelReplacementAmount.Checked)
            {
                //Remaining amount of the transaction after partial cancel. If the cancel amount is less than original amount, use the replacement amount
                vdp.transaction.ReplacementAmount = target.TxtCancelReplacementAmount.Text;
            }

            //Merchant initiated Cancel or Void
            if (!_txnTimeOut)
            {
                vdp.transaction.CancelType = (CertSuiteTool_ODP.CancelTransactionType)target.CancelTransactionTypeFromRequest(_rd.TxnRequestType);
                vdp.transaction.OriginalAuthorizedAmount = _rd.Amount.Value.ToString();
                vdp.transaction.OriginalTransactionTimestamp = _rd.VDP_TxnSummary.VDPRequest.transaction.TransactionTimestamp;
                //Note: In the case of a timeout when sending a void the void systemTraceId, OriginalSystemTraceId need to be the same value as the one set in the transaction that timed out. Original Card data also needs to be included.
                vdp.transaction.OriginalSystemTraceId = SELECT(_rd.VDP_TxnSummary.XMLResponse, "//@system-trace-id");
                vdp.transaction.OriginalReferenceNumber = SELECT(_rd.VDP_TxnSummary.XMLResponse, "//ReferenceNumber");
                vdp.transaction.OriginalSequenceNumber = _rd.VDP_TxnSummary.VDPRequest.transaction.TransactionID;
                // NEED TO ADD can.OriginalSequenceNumber = p.Merchant.Software.SequenceNumber;
                vdp.transaction.OriginalAuthCode = _rd.AuthorizationCode;
                vdp.transaction.NetworkResponseCode = SELECT(_rd.VDP_TxnSummary.XMLResponse, "//NetworkResponseCode");
                vdp.transaction.ReversalReason = CertSuiteTool_ODP.ReversalReasonType.CUSTOMER_CANCELED_TRANSACTION;

            }
            //System initiated Cancel or Void
            else 
            {
                //In the case of a timeout, the SystemTraceId of the original txn should be set in the systemTraceId as well as OriginalSystemTraceId
                //The ReferenceNumber should not be set as it was not received in the response.
                vdp.transaction.CancelType = (CertSuiteTool_ODP.CancelTransactionType)target.CancelTransactionTypeFromRequest(_rd.TxnRequestType);
                vdp.transaction.OriginalAuthorizedAmount = vdp.transaction.TransactionAmount;
                vdp.transaction.OriginalTransactionTimestamp = vdp.transaction.TransactionTimestamp;
                //Note: In the case of a timeout when sending a void the void systemTraceId, OriginalSystemTraceId need to be the same value as the one set in the transaction that timed out. Original Card data also needs to be included.
                vdp.transaction.OriginalSystemTraceId = vdp.transaction.SystemTraceID;
                vdp.transaction.SystemTraceID = vdp.transaction.SystemTraceID;
                vdp.transaction.OriginalSequenceNumber = vdp.transaction.TransactionID;
                vdp.transaction.ReversalReason = CertSuiteTool_ODP.ReversalReasonType.TIME_OUT;

            }

            string jsonRequest = "";
            string jsonResponse = CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + urlCredit_Void, Header_licenseid(), "POST", convertToJson(vdp), ref jsonRequest, ref vdp);
            XmlDocument xmlResponse = convertToXML(jsonResponse);
            VDP_TransactionSummary ts = new VDP_TransactionSummary(vdp, xmlResponse, jsonRequest, jsonResponse);
            return ProcessResponse(ts);            
        }
        
        public static ResponseDetails batchClose(ref OmnichannelDeveloperPortal _vdp)
        {
            merchantType(ref _vdp);
            //only pass the merchant and terminal object
            OmnichannelDeveloperPortal vdp = _vdp;
            vdp.address = null;
            vdp.card = null;
            Random r = new Random();
            vdp.transaction = new CertSuiteTool_ODP.Transaction();
            int rInt = r.Next(100000, 999999); //for ints
            vdp.transaction.ClerkNumber = target.TxtClerkNumber.Text;
            vdp.transaction.SystemTraceID = rInt.ToString(); //A conditional ID used to track each transaction. This must be an integer. Required for Raft and Tandem, optional for Litle? Required for Litle on CancelRequest.
            vdp.transaction.TransactionID = rInt.ToString(); //This 6-character numeric string is used to uniquely identify a transaction within a 24-hour period. The Transaction Id is also referred to as a STAN (System Trace Audit Number). In the case of Debit Refund scenarios, the OriginalSequenceNumber should match the TransactionID of the original/cancel transactions. Required.
            vdp.transaction.TransactionTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"); //The time of this transaction. Use yyyy-MM- ddThh:mm:ss-SS:SS – Should be in merchants local time zone. Mandatory – should be in merchant’s local time zone.

            string jsonRequest = "";
            string jsonResponse = CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + urlCredit_BatchClose, Header_licenseid(), "POST", convertToJson(vdp), ref jsonRequest, ref vdp);
            XmlDocument xmlResponse = convertToXML(jsonResponse);
            VDP_TransactionSummary ts = new VDP_TransactionSummary(vdp, xmlResponse, jsonRequest, jsonResponse);
            return ProcessResponse(ts);
        }

        public static ResponseDetails batchBalance(ref OmnichannelDeveloperPortal _vdp)
        {
            merchantType(ref _vdp);
            //only pass the merchant and terminal object
            OmnichannelDeveloperPortal vdp = _vdp;
            vdp.address = null;
            vdp.card = null;
            Random r = new Random();
            vdp.transaction = new CertSuiteTool_ODP.Transaction();
            int rInt = r.Next(100000, 999999); //for ints
//vdp.transaction.ClerkNumber = target.TxtClerkNumber.Text;
            vdp.transaction.SystemTraceID = rInt.ToString(); //A conditional ID used to track each transaction. This must be an integer. Required for Raft and Tandem, optional for Litle? Required for Litle on CancelRequest.
            vdp.transaction.TransactionID = rInt.ToString(); //This 6-character numeric string is used to uniquely identify a transaction within a 24-hour period. The Transaction Id is also referred to as a STAN (System Trace Audit Number). In the case of Debit Refund scenarios, the OriginalSequenceNumber should match the TransactionID of the original/cancel transactions. Required.
            vdp.transaction.TransactionTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"); //The time of this transaction. Use yyyy-MM- ddThh:mm:ss-SS:SS – Should be in merchants local time zone. Mandatory – should be in merchant’s local time zone.

            string jsonRequest = "";
            string jsonResponse = CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + urlCredit_BatchBalance, Header_licenseid(), "POST", convertToJson(vdp), ref jsonRequest, ref vdp);
            XmlDocument xmlResponse = convertToXML(jsonResponse);
            VDP_TransactionSummary ts = new VDP_TransactionSummary(vdp, xmlResponse, jsonRequest, jsonResponse);
            return ProcessResponse(ts);
        }

        //Additional Gift transaction processing
        public static ResponseDetails activateRequest(ref OmnichannelDeveloperPortal vdp)
        {
            Random r = new Random();
            vdp.transaction.DraftLocatorId = "D" + r.Next(1, 99999999).ToString(); //11 character value.  This field can be used to pass whatever discretionary data the merchant wants to pass.  Examples include employee ID number, invoice numbers, any internal value they use to track transactions.	Optional – only passes thru to reporting on Visa and MasterCard transactions.
            merchantType(ref vdp);
            vdp.transaction.MarketCode = (MarketCode)target.CboTransactionType.SelectedItem;
            vdp.transaction.ClerkNumber = target.TxtClerkNumber.Text;
            //vdp.transaction.NetworkResponseCode = "";
            vdp.transaction.PaymentType = (CertSuiteTool_ODP.PaymentType)target.CboPaymentType.SelectedItem;//Mandatory
            //vdp.transaction.ReferenceNumber = "R" + r.Next(1, 99999).ToString(); //6 digit value which uniquely identifies the transaction.	Optional
            //vdp.reportgroup = ""; //An optional (required for Litle) attribute used by the merchant to map each transaction to a reporting category.  This can be no longer than 25 characters. 
            if (!target.ChkUseToken.Checked)//When using a token a token should not be requested.
                vdp.transaction.TokenRequested = target.ChkTokenRequested.Checked;//Boolean value (true, false) to determine if token is returned for the card. The default value is false.	Optional
            if(target.TxtTransactionAmount.Text.Length > 0)
                vdp.transaction.TransactionAmount = target.TxtTransactionAmount.Text;
            vdp.transaction.TransactionTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"); //The time of this transaction. Use yyyy-MM- ddThh:mm:ss-SS:SS – Should be in merchants local time zone. Mandatory – should be in merchant’s local time zone.
           //vdp.TransactionType = (TransactionTypeType)CboTransactionType.SelectedItem;//Mandatory
            int rInt = r.Next(100000, 999999); //for ints
            vdp.transaction.SystemTraceID = rInt.ToString(); //A conditional ID used to track each transaction. This must be an integer. Required for Raft and Tandem, optional for Litle? Required for Litle on CancelRequest.
            vdp.transaction.TransactionID = rInt.ToString(); //This 6-character numeric string is used to uniquely identify a transaction within a 24-hour period. The Transaction Id is also referred to as a STAN (System Trace Audit Number). In the case of Debit Refund scenarios, the OriginalSequenceNumber should match the TransactionID of the original/cancel transactions. Required.
            rInt = r.Next(1, 99999999);

            InstrumentType(ref vdp); //Set base values
            //set additional instrument values based on payment instrument
            if (target.CboPaymentInstrument.Text == "Credit")
            {
                //CardType is only used in the case of Credit. Debit and Gift do not use CardType.
                vdp.card.CardType = (CertSuiteTool_ODP.CreditCardNetworkType)target.CboCardType.SelectedItem;

                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Debit")
            {
                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Gift")
            {

            }

            string jsonRequest = "";
            string jsonResponse = CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + urlGift_Activation, Header_licenseid(), "POST", convertToJson(vdp), ref jsonRequest, ref vdp);
            XmlDocument xmlResponse = convertToXML(jsonResponse);
            VDP_TransactionSummary ts = new VDP_TransactionSummary(vdp, xmlResponse, jsonRequest, jsonResponse);
            return ProcessResponse(ts);

        }

        public static ResponseDetails balanceInquiryRequest(ref OmnichannelDeveloperPortal vdp)
        {
            Random r = new Random();
            vdp.transaction.DraftLocatorId = "D" + r.Next(1, 99999999).ToString(); //11 character value.  This field can be used to pass whatever discretionary data the merchant wants to pass.  Examples include employee ID number, invoice numbers, any internal value they use to track transactions.	Optional – only passes thru to reporting on Visa and MasterCard transactions.
            merchantType(ref vdp);
            vdp.transaction.MarketCode = (MarketCode)target.CboTransactionType.SelectedItem;
            vdp.transaction.ClerkNumber = target.TxtClerkNumber.Text;
            //vdp.transaction.NetworkResponseCode = "";
            vdp.transaction.PaymentType = (CertSuiteTool_ODP.PaymentType)target.CboPaymentType.SelectedItem;//Mandatory
            //vdp.transaction.ReferenceNumber = "R" + r.Next(1, 99999).ToString(); //6 digit value which uniquely identifies the transaction.	Optional
            //vdp.reportgroup = ""; //An optional (required for Litle) attribute used by the merchant to map each transaction to a reporting category.  This can be no longer than 25 characters. 
            if (!target.ChkUseToken.Checked)//When using a token a token should not be requested.
                vdp.transaction.TokenRequested = target.ChkTokenRequested.Checked;//Boolean value (true, false) to determine if token is returned for the card. The default value is false.	Optional
            vdp.transaction.TransactionTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"); //The time of this transaction. Use yyyy-MM- ddThh:mm:ss-SS:SS – Should be in merchants local time zone. Mandatory – should be in merchant’s local time zone.
            //vdp.TransactionType = (TransactionTypeType)CboTransactionType.SelectedItem;//Mandatory
            int rInt = r.Next(100000, 999999); //for ints
            vdp.transaction.SystemTraceID = rInt.ToString(); //A conditional ID used to track each transaction. This must be an integer. Required for Raft and Tandem, optional for Litle? Required for Litle on CancelRequest.
            vdp.transaction.TransactionID = rInt.ToString(); //This 6-character numeric string is used to uniquely identify a transaction within a 24-hour period. The Transaction Id is also referred to as a STAN (System Trace Audit Number). In the case of Debit Refund scenarios, the OriginalSequenceNumber should match the TransactionID of the original/cancel transactions. Required.
            rInt = r.Next(1, 99999999);

            InstrumentType(ref vdp); //Set base values
            //set additional instrument values based on payment instrument
            if (target.CboPaymentInstrument.Text == "Credit")
            {
                //CardType is only used in the case of Credit. Debit and Gift do not use CardType.
                vdp.card.CardType = (CertSuiteTool_ODP.CreditCardNetworkType)target.CboCardType.SelectedItem;

                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Debit")
            {
                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Gift")
            {

            }

            string jsonRequest = "";
            string jsonResponse = CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + urlGift_BalanceInquiry, Header_licenseid(), "POST", convertToJson(vdp), ref jsonRequest, ref vdp);
            XmlDocument xmlResponse = convertToXML(jsonResponse);
            VDP_TransactionSummary ts = new VDP_TransactionSummary(vdp, xmlResponse, jsonRequest, jsonResponse);
            return ProcessResponse(ts);

        }

        public static ResponseDetails reloadRequest(ref OmnichannelDeveloperPortal vdp)
        {
            Random r = new Random();
            vdp.transaction.DraftLocatorId = "D" + r.Next(1, 99999999).ToString(); //11 character value.  This field can be used to pass whatever discretionary data the merchant wants to pass.  Examples include employee ID number, invoice numbers, any internal value they use to track transactions.	Optional – only passes thru to reporting on Visa and MasterCard transactions.
            merchantType(ref vdp);
            vdp.transaction.MarketCode = (MarketCode)target.CboTransactionType.SelectedItem;
            vdp.transaction.ClerkNumber = target.TxtClerkNumber.Text;
            //vdp.transaction.NetworkResponseCode = "";
            vdp.transaction.PaymentType = (CertSuiteTool_ODP.PaymentType)target.CboPaymentType.SelectedItem;//Mandatory
            //vdp.transaction.ReferenceNumber = "R" + r.Next(1, 99999).ToString(); //6 digit value which uniquely identifies the transaction.	Optional
            //vdp.reportgroup = ""; //An optional (required for Litle) attribute used by the merchant to map each transaction to a reporting category.  This can be no longer than 25 characters. 
            if (!target.ChkUseToken.Checked)//When using a token a token should not be requested.
                vdp.transaction.TokenRequested = target.ChkTokenRequested.Checked;//Boolean value (true, false) to determine if token is returned for the card. The default value is false.	Optional
            vdp.transaction.TransactionAmount = target.TxtTransactionAmount.Text;
            vdp.transaction.TransactionTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"); //The time of this transaction. Use yyyy-MM- ddThh:mm:ss-SS:SS – Should be in merchants local time zone. Mandatory – should be in merchant’s local time zone.
            //vdp.TransactionType = (TransactionTypeType)CboTransactionType.SelectedItem;//Mandatory
            int rInt = r.Next(100000, 999999); //for ints
            vdp.transaction.SystemTraceID = rInt.ToString(); //A conditional ID used to track each transaction. This must be an integer. Required for Raft and Tandem, optional for Litle? Required for Litle on CancelRequest.
            vdp.transaction.TransactionID = rInt.ToString(); //This 6-character numeric string is used to uniquely identify a transaction within a 24-hour period. The Transaction Id is also referred to as a STAN (System Trace Audit Number). In the case of Debit Refund scenarios, the OriginalSequenceNumber should match the TransactionID of the original/cancel transactions. Required.
            rInt = r.Next(1, 99999999);

            InstrumentType(ref vdp); //Set base values
            //set additional instrument values based on payment instrument
            if (target.CboPaymentInstrument.Text == "Credit")
            {
                //CardType is only used in the case of Credit. Debit and Gift do not use CardType.
                vdp.card.CardType = (CertSuiteTool_ODP.CreditCardNetworkType)target.CboCardType.SelectedItem;

                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Debit")
            {
                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Gift")
            {

            }

            string jsonRequest = "";
            string jsonResponse = CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + urlGift_Reload, Header_licenseid(), "POST", convertToJson(vdp), ref jsonRequest, ref vdp);
            XmlDocument xmlResponse = convertToXML(jsonResponse);
            VDP_TransactionSummary ts = new VDP_TransactionSummary(vdp, xmlResponse, jsonRequest, jsonResponse);
            return ProcessResponse(ts);

        }

        public static ResponseDetails unloadRequest(ref OmnichannelDeveloperPortal vdp)
        {
            Random r = new Random();
            vdp.transaction.DraftLocatorId = "D" + r.Next(1, 99999999).ToString(); //11 character value.  This field can be used to pass whatever discretionary data the merchant wants to pass.  Examples include employee ID number, invoice numbers, any internal value they use to track transactions.	Optional – only passes thru to reporting on Visa and MasterCard transactions.
            merchantType(ref vdp);
            vdp.transaction.MarketCode = (MarketCode)target.CboTransactionType.SelectedItem;
            vdp.transaction.ClerkNumber = target.TxtClerkNumber.Text;
            //vdp.transaction.NetworkResponseCode = "";
            vdp.transaction.PaymentType = (CertSuiteTool_ODP.PaymentType)target.CboPaymentType.SelectedItem;//Mandatory
            //vdp.transaction.ReferenceNumber = "R" + r.Next(1, 99999).ToString(); //6 digit value which uniquely identifies the transaction.	Optional
            //vdp.reportgroup = ""; //An optional (required for Litle) attribute used by the merchant to map each transaction to a reporting category.  This can be no longer than 25 characters. 
            if (!target.ChkUseToken.Checked)//When using a token a token should not be requested.
                vdp.transaction.TokenRequested = target.ChkTokenRequested.Checked;//Boolean value (true, false) to determine if token is returned for the card. The default value is false.	Optional
            vdp.transaction.TransactionAmount = target.TxtTransactionAmount.Text;
            vdp.transaction.TransactionTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"); //The time of this transaction. Use yyyy-MM- ddThh:mm:ss-SS:SS – Should be in merchants local time zone. Mandatory – should be in merchant’s local time zone.
            //vdp.TransactionType = (TransactionTypeType)CboTransactionType.SelectedItem;//Mandatory
            int rInt = r.Next(100000, 999999); //for ints
            vdp.transaction.SystemTraceID = rInt.ToString(); //A conditional ID used to track each transaction. This must be an integer. Required for Raft and Tandem, optional for Litle? Required for Litle on CancelRequest.
            vdp.transaction.TransactionID = rInt.ToString(); //This 6-character numeric string is used to uniquely identify a transaction within a 24-hour period. The Transaction Id is also referred to as a STAN (System Trace Audit Number). In the case of Debit Refund scenarios, the OriginalSequenceNumber should match the TransactionID of the original/cancel transactions. Required.
            rInt = r.Next(1, 99999999);

            InstrumentType(ref vdp); //Set base values
            //set additional instrument values based on payment instrument
            if (target.CboPaymentInstrument.Text == "Credit")
            {
                //CardType is only used in the case of Credit. Debit and Gift do not use CardType.
                vdp.card.CardType = (CertSuiteTool_ODP.CreditCardNetworkType)target.CboCardType.SelectedItem;

                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Debit")
            {
                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Gift")
            {

            }

            string jsonRequest = "";
            string jsonResponse = CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + urlGift_Unload, Header_licenseid(), "POST", convertToJson(vdp), ref jsonRequest, ref vdp);
            XmlDocument xmlResponse = convertToXML(jsonResponse);
            VDP_TransactionSummary ts = new VDP_TransactionSummary(vdp, xmlResponse, jsonRequest, jsonResponse);
            return ProcessResponse(ts);

        }

        public static ResponseDetails closeRequest(ref OmnichannelDeveloperPortal vdp)
        {
            Random r = new Random();
            vdp.transaction.DraftLocatorId = "D" + r.Next(1, 99999999).ToString(); //11 character value.  This field can be used to pass whatever discretionary data the merchant wants to pass.  Examples include employee ID number, invoice numbers, any internal value they use to track transactions.	Optional – only passes thru to reporting on Visa and MasterCard transactions.
            merchantType(ref vdp);
            vdp.transaction.MarketCode = (MarketCode)target.CboTransactionType.SelectedItem;
            vdp.transaction.ClerkNumber = target.TxtClerkNumber.Text;
            //vdp.transaction.NetworkResponseCode = "";
            vdp.transaction.PaymentType = (CertSuiteTool_ODP.PaymentType)target.CboPaymentType.SelectedItem;//Mandatory
            //vdp.transaction.ReferenceNumber = "R" + r.Next(1, 99999).ToString(); //6 digit value which uniquely identifies the transaction.	Optional
            //vdp.reportgroup = ""; //An optional (required for Litle) attribute used by the merchant to map each transaction to a reporting category.  This can be no longer than 25 characters. 
            if (!target.ChkUseToken.Checked)//When using a token a token should not be requested.
                vdp.transaction.TokenRequested = target.ChkTokenRequested.Checked;//Boolean value (true, false) to determine if token is returned for the card. The default value is false.	Optional
            vdp.transaction.TransactionTimestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"); //The time of this transaction. Use yyyy-MM- ddThh:mm:ss-SS:SS – Should be in merchants local time zone. Mandatory – should be in merchant’s local time zone.
            //vdp.TransactionType = (TransactionTypeType)CboTransactionType.SelectedItem;//Mandatory
            int rInt = r.Next(100000, 999999); //for ints
            vdp.transaction.SystemTraceID = rInt.ToString(); //A conditional ID used to track each transaction. This must be an integer. Required for Raft and Tandem, optional for Litle? Required for Litle on CancelRequest.
            vdp.transaction.TransactionID = rInt.ToString(); //This 6-character numeric string is used to uniquely identify a transaction within a 24-hour period. The Transaction Id is also referred to as a STAN (System Trace Audit Number). In the case of Debit Refund scenarios, the OriginalSequenceNumber should match the TransactionID of the original/cancel transactions. Required.
            rInt = r.Next(1, 99999999);

            InstrumentType(ref vdp); //Set base values
            //set additional instrument values based on payment instrument
            if (target.CboPaymentInstrument.Text == "Credit")
            {
                //CardType is only used in the case of Credit. Debit and Gift do not use CardType.
                vdp.card.CardType = (CertSuiteTool_ODP.CreditCardNetworkType)target.CboCardType.SelectedItem;

                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Debit")
            {
                if (target.CboCreditType.Text == "CardKeyed")
                {
                    if (target.TxtCardholderName.Text.Length > 0)
                        vdp.card.CardholderName = target.TxtCardholderName.Text;
                }
            }
            else if (target.CboPaymentInstrument.Text == "Gift")
            {

            }

            string jsonRequest = "";
            string jsonResponse = CreateRequest_Json(target.txtVDPBaseEndpointURL.Text + urlGift_Close, Header_licenseid(), "POST", convertToJson(vdp), ref jsonRequest, ref vdp);
            XmlDocument xmlResponse = convertToXML(jsonResponse);
            VDP_TransactionSummary ts = new VDP_TransactionSummary(vdp, xmlResponse, jsonRequest, jsonResponse);
            return ProcessResponse(ts);
        }

        #endregion Transaction Processing

        public static ResponseDetails ProcessResponse(VDP_TransactionSummary _ts)
        {
            string AuthorizationCode = "";

            try
            {
                try
                {//Obtain the AuthorizationCode
                    AuthorizationCode = SELECT(_ts.XMLResponse, "//AuthorizationCode");
                }
                catch{}

                //Add to CheckListBox
                AmountType a = new AmountType();
                a.currency = (ISO4217CurrencyCodeType)target.CboCurrencyCodeType.SelectedItem;
                a.currencySpecified = true;
                a.Value = Convert.ToDecimal(target.TxtTransactionAmount.Text);
                
                ResponseDetails rd = new ResponseDetails(target.CboPWSorVDP.Text, a, AuthorizationCode, target.CboPaymentInstrument.Text,(_ts.XMLResponse).DocumentElement.LocalName.Replace("Response", ""), _ts, null);
                target.ChkLstTransactionsProcessed.Items.Add(rd);

                return rd;
            }
            catch
            {
                return null;
            }
        }

        public static string SELECT(XmlDocument doc, string _validation)
        {
            string result = "";
            XPathNavigator nav;
            //List of Namespaces to load into the manager
            XmlNamespaceManager nsm = new XmlNamespaceManager(doc.NameTable);
            foreach (XmlNode n in doc.DocumentElement.Attributes)
            {
                if (n.LocalName.Length > 0 && n.LocalName != "xmlns") //Make sure a namespace prefix is included. Do no use the default xmlns
                    nsm.AddNamespace(n.LocalName, n.Value);
            }
            // Create a navigator to query with XPath.
            nav = doc.CreateNavigator();

            XPathNodeIterator NodeIter;
            // Select the node and place the results in an iterator.
            NodeIter = nav.Select(_validation, nsm);

            while (NodeIter.MoveNext())
            {
                result += NodeIter.Current.Value;
            }
            return result;
        }

        public static string convertToJson(OmnichannelDeveloperPortal _vdp)
        {
            //http://stackoverflow.com/questions/7799769/parsing-an-enumeration-in-json-net/

            Newtonsoft.Json.JsonSerializerSettings jss = new Newtonsoft.Json.JsonSerializerSettings();
            jss.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            jss.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            string json = "{";

            if (_vdp.merchant != null)
            {
                json += "\"merchant\":";
                json += Newtonsoft.Json.JsonConvert.SerializeObject(_vdp.merchant, jss) + ",";
            }
            if (_vdp.terminal != null)
            {
                json += "\"terminal\":";
                json += Newtonsoft.Json.JsonConvert.SerializeObject(_vdp.terminal, jss) + ",";
            }
            if (_vdp.transaction != null)
            {
                json += "\"transaction\":";
                json += Newtonsoft.Json.JsonConvert.SerializeObject(_vdp.transaction, jss) + ",";
            }
            if (_vdp.address != null)
            {
                json += "\"address\":";
                json += Newtonsoft.Json.JsonConvert.SerializeObject(_vdp.address, jss) + ",";
            }
            if (_vdp.card != null)
            {
                json += "\"card\":";
                json += Newtonsoft.Json.JsonConvert.SerializeObject(_vdp.card, jss) + ",";
            }

            json = json.Substring(0, json.Length - 1);//Remove the last comma
            json += "}";

            return json;
        }

        public static string CreateRequest_Json(string _url, List<string> _header, string _method, string _body, ref string _jsonRequest)
        {
            //Overload for requests that only pass a Json object and not the VantivDeveloperPortal object.
            OmnichannelDeveloperPortal vdp = new OmnichannelDeveloperPortal();
            return CreateRequest_Json(_url, _header, _method, _body, ref _jsonRequest, ref vdp);
        }
        public static string CreateRequest_Json(string _url, List<string> _header, string _method, string _body, ref string _jsonRequest, ref OmnichannelDeveloperPortal _vdp)
        {
            try
            {
                //Note: _vdp is passed in the case of a timeout scenario. 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
                request.Method = _method; //Valid values are "GET", "POST", "PUT" and "DELETE"
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50215)";
                if (_body.Length > 0)
                    request.ContentType = "application/json";
                request.Timeout = 1000 * 60;
                //Authorization is made up of UserName and Password in the format [UserName]:[Password]. In this case the identity token is the only value set and is the [UserName]
                //String _Authorization = "Apikey: " + _apiKey;

                if (_header != null && _header.Count > 0)//Only used in case an additional header value(s) needs to be added
                {
                    foreach (string s in _header)
                        request.Headers.Add(s);
                }

                if (_body.Length > 0)
                {
                    StreamWriter writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(_body);
                    writer.Close();
                }

                string ContentLength = "";
                if (request.ContentLength > 0)
                    ContentLength = "\r\nContent-Length: " + request.ContentLength;
                else if (request.ContentLength != null && request.ContentLength == -1)
                    request.ContentLength = 0;

                string _HeaderInformation = _method + " " + request.Address.AbsolutePath + request.Address.Query +
                            "\r\n" + request.Headers.ToString().Trim() + ContentLength + "\r\n\r\n";

                 _jsonRequest = _HeaderInformation + _body;

                HttpWebResponse response = null;

                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (System.TimeoutException te)
                {
                    //Timeout has occured. Check to see if a System Reversal should be initiated. Please note that this can be a endless loop
                    // until internet connect is re-established.

                    DialogResult Result;
                    Result = MessageBox.Show("A timeout has occured. Attempt to reverse the transaction?", "Reverse Transaction?", MessageBoxButtons.YesNo);
                    if (Result == DialogResult.Yes)
                    {
                        ResponseDetails rd = new ResponseDetails(null, null, null, null, null, null, null);
                        ResponseDetails rd2 = voidRequest(rd, ref _vdp, true);
                        return rd2.VDP_TxnSummary.JsonResponse;
                    }
                    else
                        return te.Message;

                }
                catch (Exception e)
                {
                    if (_method != "DELETE")
                        throw (e);
                }
                using (Stream data = response.GetResponseStream())
                {
                    try
                    {
                        string text = new StreamReader(data).ReadToEnd();
                        return text;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        data.Close();
                    }
                }
            }
            catch (System.TimeoutException te)
            {
                //Timeout has occured. Check to see if a System Reversal should be initiated. Please note that this can be a endless loop
                // until internet connect is re-established.

                DialogResult Result;
                Result = MessageBox.Show("A timeout has occured. Attempt to reverse the transaction?", "Reverse Transaction?", MessageBoxButtons.YesNo);
                if (Result == DialogResult.Yes)
                {
                    ResponseDetails rd = new ResponseDetails(null, null, null, null, null, null, null);
                    ResponseDetails rd2 = voidRequest(rd, ref _vdp, true);
                    return rd2.VDP_TxnSummary.JsonResponse;
                }
                else
                    return te.Message;

            }
            catch (System.Net.WebException ex2)
            {
                //Lets get the webException returned in the response
                using (Stream data2 = ex2.Response.GetResponseStream())
                {
                    try
                    {
                        string text = new StreamReader(data2).ReadToEnd();
                        //MessageBox.Show(text);
 
                        MessageBox.Show(text);
                        return text;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {
                        data2.Close();
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static XmlDocument convertToXML(string _response)
        {
            //http://stackoverflow.com/questions/7799769/parsing-an-enumeration-in-json-net/

            //Object response = new Object();
            //Newtonsoft.Json.JsonSerializerSettings jss = new Newtonsoft.Json.JsonSerializerSettings();
            //jss.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //jss.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            //TransactionResponseType TTT = Newtonsoft.Json.JsonConvert.DeserializeObject<TransactionResponseType>(_response);
            XmlDocument response = new XmlDocument();
            response = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(_response);

            //response = Newtonsoft.Json.JsonConvert.DeserializeObject<Object>(_response);//,jss
            return response;
        }

        public static List<string> Header_licenseid()
        {
            List<string> header = new List<string>();
            header.Add("licenseid:" + target.TxtLicenseKeyAPIKey.Text);

            return header;
        }

    }

    #region Extra Classes
    public class VDP_TransactionSummary
    {
        /*The following class is used by both PWS and VDP as a way to demonstrate the data that may be saved in the database.
         * The developer should be familiar with data needed to perform follow-on transaction and ensure at a minimum they have that 
         * data in their database. They may also wish to record other meta-data that meets their application needs. 
         */
        /* *** PCI Considerations ***
        * The developer also  needs to follow PCI data standards in terms of the data they save in their database. For example PCI 
        * does not permit Track data nor CV data to be saved in any format in a database. It's the software companys responsiblity to 
        * build a solution that follows PCI standards for more information please reference https://www.pcisecuritystandards.org/ 
        * or an assesor for guidance.
        */
        public OmnichannelDeveloperPortal VDPRequest;
        public XmlDocument XMLResponse;
        public string JsonRequest;
        public string JsonResponse;

        public VDP_TransactionSummary(OmnichannelDeveloperPortal vDPRequest, XmlDocument xMLResponse, string jsonRequest, string jsonResponse)
        {
            VDPRequest = vDPRequest;
            XMLResponse = xMLResponse;
            JsonRequest = jsonRequest;
            JsonResponse = jsonResponse;
        }
    }
    #endregion Extra Classes
}
