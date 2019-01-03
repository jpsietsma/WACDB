using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Reflection;
using System.Reflection.Emit;
using WAC_ViewModels;
using WAC_DataProviders;
using WAC_Validators;
using WAC_UserControls;
using WAC_Containers;
using System.Text.RegularExpressions;
/// <summary>
/// Summary description for ConnectorFactory
/// </summary>
namespace WAC_Connectors
{
    public class ConnectorFactory
    {
        public HttpSessionState Session { get; set; }
        public string FactoryID { get; set; }
        private readonly ConcurrentDictionary<string, WACControlConnector> connectors =
            new ConcurrentDictionary<string, WACControlConnector>();
        private Type classType = typeof(UserControlConnector);
        private Type[] constructorArgs = new Type[] { };
        private readonly ConcurrentDictionary<string, Type> classRegistry = new ConcurrentDictionary<string, Type>();
        private readonly ConcurrentDictionary<string, ConnectorConstructorDelegate> connectorClassConstructors =
            new ConcurrentDictionary<string, ConnectorConstructorDelegate>();
        private readonly ConcurrentDictionary<string, ViewModelConstructorDelegate> modelClassConstructors =
            new ConcurrentDictionary<string, ViewModelConstructorDelegate>();
        private readonly ConcurrentDictionary<string, DataProviderConstructorDelegate> providerClassConstructors =
            new ConcurrentDictionary<string, DataProviderConstructorDelegate>();
        private readonly ConcurrentDictionary<string, ValidatorConstructorDelegate> validatorClassConstructors =
            new ConcurrentDictionary<string, ValidatorConstructorDelegate>();
        delegate WACControlConnector ConnectorConstructorDelegate();
        delegate WACViewModel ViewModelConstructorDelegate();
        delegate WACDataProvider DataProviderConstructorDelegate();
        delegate WACValidator ValidatorConstructorDelegate();
        private bool classTypesRegistered = false;
        Regex reg = new Regex("(?i)(wac).+"); //take everything after the first wac
        Regex reg2 = new Regex("(?i).*(?=_)");// lose everything after the last underscore and the underscore
        private ConnectorFactory(HttpSessionState _session)
        {
            Session = _session;
        }
        public static ConnectorFactory GetConnectorFactoryForSession(HttpSessionState _session)
        {
            ConnectorFactory cf = new ConnectorFactory(_session);
            return cf;
        }
        public WACControlConnector GetConnector(string _controlID)
        {
            // this simple version will not create a connector if one isn't registered
            var a = connectors.Where(w => w.Key == _controlID).Select(s => s.Value);
            if (a.Any())
                return a.Single() as WACControlConnector;
            else
                return null;
        }

        public WACControlConnector GetConnectorForControl(Control _control)
        {
            // return registered ControlConnector if it exists otherwise, create one, register and return
            WACControlConnector ucc = null;
            if (connectors.TryGetValue(_control.ClientID, out ucc))
                return ucc;
            else
            {
                ucc = CreateUserControlConnector(UserControlConnectorName(_control));
                RegisterConnector(_control, ucc);
                return ucc;
            }
        }
        public string GetConnectedControl(WACControlConnector _connector)
        {
            var a = connectors.Where(w => w.Value.Equals(_connector)).Select(s => s.Key);
            if (a.Any())
                return a.First();
            else
                return string.Empty;
        }
        public WACValidator CreateValidator(string identifier)
        {
            if (String.IsNullOrEmpty(identifier))
                throw new ArgumentException("identifier can not be null or empty", identifier);
            if (!classRegistry.ContainsKey(identifier))
                throw new ArgumentException("No Validator has been registered with the identifier: " + identifier);
            WACValidator v = CreateValidator(classRegistry[identifier]);
            return v;
        }
        private WACValidator CreateValidator(Type type)
        {
            ValidatorConstructorDelegate del;
            if (validatorClassConstructors.TryGetValue(type.Name, out del))
                return (WACValidator)del();

            DynamicMethod dynamicMethod = new DynamicMethod("CreateInstance", type, constructorArgs, classType);
            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();

            ilGenerator.Emit(OpCodes.Newobj, type.GetConstructor(constructorArgs));
            ilGenerator.Emit(OpCodes.Ret);

            del = (ValidatorConstructorDelegate)dynamicMethod.CreateDelegate(typeof(ValidatorConstructorDelegate));
            validatorClassConstructors.TryAdd(type.Name, del);
            return (WACValidator)del();
        }
        public UserControlConnector CreateUserControlConnector(string identifier)
        {
            if (String.IsNullOrEmpty(identifier))
                throw new ArgumentException("identifier can not be null or empty", identifier);
            if (!classRegistry.ContainsKey(identifier))
                throw new ArgumentException("No UserControl Connector has been registered with the identifier: " + identifier);

            UserControlConnector ucc = CreateUserControlConnector(classRegistry[identifier]);
            return ucc;
        }
        private UserControlConnector CreateUserControlConnector(Type type)
        {
            ConnectorConstructorDelegate del;

            if (connectorClassConstructors.TryGetValue(type.Name, out del))
                return (UserControlConnector)del();

            DynamicMethod dynamicMethod = new DynamicMethod("CreateInstance", type, constructorArgs, classType);
            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();

            ilGenerator.Emit(OpCodes.Newobj, type.GetConstructor(constructorArgs));
            ilGenerator.Emit(OpCodes.Ret);

            del = (ConnectorConstructorDelegate)dynamicMethod.CreateDelegate(typeof(ConnectorConstructorDelegate));
            connectorClassConstructors.TryAdd(type.Name, del);
            return (UserControlConnector)del();
        }
        public WACViewModel CreateViewModel(string identifier)
        {
            if (String.IsNullOrEmpty(identifier))
                throw new ArgumentException("identifier can not be null or empty", identifier);
            if (!classRegistry.ContainsKey(identifier))
                throw new ArgumentException("No ViewModel has been registered with the identifier: " + identifier);

            return CreateViewModel(classRegistry[identifier]);
        }
        private WACViewModel CreateViewModel(Type type)
        {
            ViewModelConstructorDelegate del;

            if (modelClassConstructors.TryGetValue(type.Name, out del))
                return del();

            DynamicMethod dynamicMethod = new DynamicMethod("CreateInstance", type, constructorArgs, classType);
            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();

            ilGenerator.Emit(OpCodes.Newobj, type.GetConstructor(constructorArgs));
            ilGenerator.Emit(OpCodes.Ret);

            del = (ViewModelConstructorDelegate)dynamicMethod.CreateDelegate(typeof(ViewModelConstructorDelegate));
            modelClassConstructors.TryAdd(type.Name, del);
            return del();
        }
        public WACDataProvider CreateDataProvider(string identifier)
        {
            if (String.IsNullOrEmpty(identifier))
                throw new ArgumentException("identifier can not be null or empty", identifier);
            if (!classRegistry.ContainsKey(identifier))
                throw new ArgumentException("No DataProvider has been registered with the identifier: " + identifier);

            return CreateDataProvider(classRegistry[identifier]);
        }
        private WACDataProvider CreateDataProvider(Type type)
        {
            DataProviderConstructorDelegate del;

            if (providerClassConstructors.TryGetValue(type.Name, out del))
                return del();

            DynamicMethod dynamicMethod = new DynamicMethod("CreateInstance", type, constructorArgs, classType);
            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();

            ilGenerator.Emit(OpCodes.Newobj, type.GetConstructor(constructorArgs));
            ilGenerator.Emit(OpCodes.Ret);

            del = (DataProviderConstructorDelegate)dynamicMethod.CreateDelegate(typeof(DataProviderConstructorDelegate));
            providerClassConstructors.TryAdd(type.Name, del);
            return del();
        }
        public void RegisterClassTypes()
        {
            if (!classTypesRegistered)
            {
                RegisterClassTypes(typeof(WACViewModel));
                RegisterClassTypes(typeof(WACDataProvider));
                RegisterClassTypes(typeof(UserControlConnector));
                RegisterClassTypes(typeof(WACValidator));
                classTypesRegistered = true;
            }
        }
        public void RegisterClassTypes(Type classType)
        {
            var classList = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(b => b.IsSubclassOf(classType)).Select(b => b);

            foreach (Type _type in classList)
                RegisterClassType(_type);
        }
        public void RegisterClassType(Type _type)
        {
            try
            {
                classRegistry.TryAdd(_type.Name, _type);
            }
            catch (Exception ex)
            {
                WACAlert.Show(ex.Message, -1);
            }
        }
        private void RegisterConnector(Control _control, WACControlConnector _connector)
        {
            connectors.TryAdd(_control.ClientID, _connector);
        }
        public string ViewModelName(Control _control)
        {
            return ControlBaseName(_control) + "VM";
        }
        public string DataProviderName(Control _control)
        {
            return ControlBaseName(_control) + "DP";
        }
        public string UserControlConnectorName(Control _control)
        {
            return ControlBaseName(_control) + "Connector";
        }
        public string ControlBaseName(Control _control)
        {
            string s = reg2.Match(reg.Match(_control.GetType().Name).Value).Value;
            Regex reg3 = new Regex("(?i)(" + s + ")");
            return reg3.Match(_control.ClientID).Value;
        }
       
    }

   
}