Vantiv-Developer-Portal and Vantiv-Payment-Web-Services
=======================================================
*** IMPORTANT You will need to create a project at https://apideveloper.vantiv.com/ in order to get access to the sandbox and test your code

Integration Guidelines available here: https://apideveloper.vantiv.com/docs/payment-web-services/implementation-guidelines<br>
Online developers guide: https://apideveloper.vantiv.com/documentation<br>

The C# implementation of Vantiv-Developer-Portal sample code demonstrating how to integrate with both Vantiv-Developer-Portal as well as Vantiv-Payment-Web-Services
- Contains a REST sample* for integrating to Vantiv-Developer-Portal v6.2.8
- Contains a SOAP sample* for integrating to Payment-Web-Services v6.2.8

* Please note that if you are unsure of which solution which will match your solution needs you should contact a vantiv solution consultant first before starting any development efforts. Not doing so may lead to lost coding time. 

####PWS Getting Started
There are a few things necessary to start sending transactions which the sample application demonstrates.
1. Create a local proxy or class used to send transactions
2. Implement a Custom binding
3. Open the channel to process transactions
4. Create a PWS message and send the transaction. 

#####Create a local proxy class from the wsdl/xsd provided by your solution contact


#####Custom Binding
In your config file you'll need a custom binding
//Ref: App.config
```
  <system.serviceModel>
    <bindings>
      <customBinding>
        <binding name="PaymentPortTypeSoap11">
          <security authenticationMode="UserNameOverTransport" includeTimestamp="false">
            <secureConversationBootstrap />
          </security>
          <textMessageEncoding messageVersion="Soap11" />
          <httpsTransport />
        </binding>
      </customBinding>
    </bindings>
    <client>
      <!--PaymentWebServices-->
      <endpoint address="https://ws-cert.vantiv.com/merchant/payments-cert/v5" binding="customBinding" bindingConfiguration="PaymentPortTypeSoap11" contract="PaymentPortType" name="PaymentPortTypeSoap11_PaymentPortType" />
    </client>
  </system.serviceModel>
```

In your code that will use the proxy class
//Ref: Send_Transactions.cs (Variable declarations)
```
PaymentPortTypeClient PWSClient = new PaymentPortTypeClient();

private string _PWSEndpointAddress = "https://ws-cert.vantiv.com/merchant/payments-cert/v6"; 
private string _UserName = ""; //Provided by your solution consultant
private string _Password = "";//Provided by your solution consultant

//The following are used to switch the URI for posting data
private static object svcInfoChannelLock = new object();
```

Initialize the proxy class In the initialize of your code
```
//Setup Endpoint addresses 
//Ref: Send_Transactions.cs (Send_Transactions())
lock (svcInfoChannelLock)
{
    PWSClient.Endpoint.Address = new EndpointAddress(_PWSEndpointAddress);
    PWSClient.ClientCredentials.UserName.UserName = _UserName;
    PWSClient.ClientCredentials.UserName.Password = _Password;
    PWSClient.Open();
}                     
```
