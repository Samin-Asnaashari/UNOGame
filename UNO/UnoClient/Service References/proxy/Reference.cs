﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnoClient.proxy {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Card", Namespace="http://schemas.datacontract.org/2004/07/UNOService")]
    [System.SerializableAttribute()]
    public partial class Card : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ColorField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Color {
            get {
                return this.ColorField;
            }
            set {
                if ((object.ReferenceEquals(this.ColorField, value) != true)) {
                    this.ColorField = value;
                    this.RaisePropertyChanged("Color");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Player", Namespace="http://schemas.datacontract.org/2004/07/UNOService")]
    [System.SerializableAttribute()]
    public partial class Player : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="proxy.ILoginAndSignUp")]
    public interface ILoginAndSignUp {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/Login", ReplyAction="http://tempuri.org/ILoginAndSignUp/LoginResponse")]
        bool Login(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/Login", ReplyAction="http://tempuri.org/ILoginAndSignUp/LoginResponse")]
        System.Threading.Tasks.Task<bool> LoginAsync(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/SignUp", ReplyAction="http://tempuri.org/ILoginAndSignUp/SignUpResponse")]
        bool SignUp(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/SignUp", ReplyAction="http://tempuri.org/ILoginAndSignUp/SignUpResponse")]
        System.Threading.Tasks.Task<bool> SignUpAsync(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/CheckUserName", ReplyAction="http://tempuri.org/ILoginAndSignUp/CheckUserNameResponse")]
        string CheckUserName(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/CheckUserName", ReplyAction="http://tempuri.org/ILoginAndSignUp/CheckUserNameResponse")]
        System.Threading.Tasks.Task<string> CheckUserNameAsync(string userName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoginAndSignUpChannel : UnoClient.proxy.ILoginAndSignUp, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoginAndSignUpClient : System.ServiceModel.ClientBase<UnoClient.proxy.ILoginAndSignUp>, UnoClient.proxy.ILoginAndSignUp {
        
        public LoginAndSignUpClient() {
        }
        
        public LoginAndSignUpClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LoginAndSignUpClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoginAndSignUpClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoginAndSignUpClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Login(string userName, string password) {
            return base.Channel.Login(userName, password);
        }
        
        public System.Threading.Tasks.Task<bool> LoginAsync(string userName, string password) {
            return base.Channel.LoginAsync(userName, password);
        }
        
        public bool SignUp(string userName, string password) {
            return base.Channel.SignUp(userName, password);
        }
        
        public System.Threading.Tasks.Task<bool> SignUpAsync(string userName, string password) {
            return base.Channel.SignUpAsync(userName, password);
        }
        
        public string CheckUserName(string userName) {
            return base.Channel.CheckUserName(userName);
        }
        
        public System.Threading.Tasks.Task<string> CheckUserNameAsync(string userName) {
            return base.Channel.CheckUserNameAsync(userName);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="proxy.IGame", CallbackContract=typeof(UnoClient.proxy.IGameCallback))]
    public interface IGame {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SaveReplay", ReplyAction="http://tempuri.org/IGame/SaveReplayResponse")]
        void SaveReplay(int gameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SaveReplay", ReplyAction="http://tempuri.org/IGame/SaveReplayResponse")]
        System.Threading.Tasks.Task SaveReplayAsync(int gameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/takeCard", ReplyAction="http://tempuri.org/IGame/takeCardResponse")]
        UnoClient.proxy.Card takeCard();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/takeCard", ReplyAction="http://tempuri.org/IGame/takeCardResponse")]
        System.Threading.Tasks.Task<UnoClient.proxy.Card> takeCardAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/playCard", ReplyAction="http://tempuri.org/IGame/playCardResponse")]
        void playCard(UnoClient.proxy.Card c);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/playCard", ReplyAction="http://tempuri.org/IGame/playCardResponse")]
        System.Threading.Tasks.Task playCardAsync(UnoClient.proxy.Card c);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/LeaveGame", ReplyAction="http://tempuri.org/IGame/LeaveGameResponse")]
        void LeaveGame();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/LeaveGame", ReplyAction="http://tempuri.org/IGame/LeaveGameResponse")]
        System.Threading.Tasks.Task LeaveGameAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SendMessageGame", ReplyAction="http://tempuri.org/IGame/SendMessageGameResponse")]
        void SendMessageGame(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SendMessageGame", ReplyAction="http://tempuri.org/IGame/SendMessageGameResponse")]
        System.Threading.Tasks.Task SendMessageGameAsync(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/CardsAssigned", ReplyAction="http://tempuri.org/IGame/CardsAssignedResponse")]
        void CardsAssigned(UnoClient.proxy.Card[] cards);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/TurnChanged", ReplyAction="http://tempuri.org/IGame/TurnChangedResponse")]
        void TurnChanged(UnoClient.proxy.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SendMessageGameCallback", ReplyAction="http://tempuri.org/IGame/SendMessageGameCallbackResponse")]
        void SendMessageGameCallback(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/NotifyPlayerLeft", ReplyAction="http://tempuri.org/IGame/NotifyPlayerLeftResponse")]
        void NotifyPlayerLeft(string userName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameChannel : UnoClient.proxy.IGame, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GameClient : System.ServiceModel.DuplexClientBase<UnoClient.proxy.IGame>, UnoClient.proxy.IGame {
        
        public GameClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public GameClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public GameClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GameClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GameClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void SaveReplay(int gameID) {
            base.Channel.SaveReplay(gameID);
        }
        
        public System.Threading.Tasks.Task SaveReplayAsync(int gameID) {
            return base.Channel.SaveReplayAsync(gameID);
        }
        
        public UnoClient.proxy.Card takeCard() {
            return base.Channel.takeCard();
        }
        
        public System.Threading.Tasks.Task<UnoClient.proxy.Card> takeCardAsync() {
            return base.Channel.takeCardAsync();
        }
        
        public void playCard(UnoClient.proxy.Card c) {
            base.Channel.playCard(c);
        }
        
        public System.Threading.Tasks.Task playCardAsync(UnoClient.proxy.Card c) {
            return base.Channel.playCardAsync(c);
        }
        
        public void LeaveGame() {
            base.Channel.LeaveGame();
        }
        
        public System.Threading.Tasks.Task LeaveGameAsync() {
            return base.Channel.LeaveGameAsync();
        }
        
        public void SendMessageGame(string message) {
            base.Channel.SendMessageGame(message);
        }
        
        public System.Threading.Tasks.Task SendMessageGameAsync(string message) {
            return base.Channel.SendMessageGameAsync(message);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="proxy.ILobby")]
    public interface ILobby {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetOnlineList", ReplyAction="http://tempuri.org/ILobby/GetOnlineListResponse")]
        UnoClient.proxy.Player[] GetOnlineList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetOnlineList", ReplyAction="http://tempuri.org/ILobby/GetOnlineListResponse")]
        System.Threading.Tasks.Task<UnoClient.proxy.Player[]> GetOnlineListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/CreateParty", ReplyAction="http://tempuri.org/ILobby/CreatePartyResponse")]
        void CreateParty();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/CreateParty", ReplyAction="http://tempuri.org/ILobby/CreatePartyResponse")]
        System.Threading.Tasks.Task CreatePartyAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/LeaveParty", ReplyAction="http://tempuri.org/ILobby/LeavePartyResponse")]
        void LeaveParty();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/LeaveParty", ReplyAction="http://tempuri.org/ILobby/LeavePartyResponse")]
        System.Threading.Tasks.Task LeavePartyAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendInvites", ReplyAction="http://tempuri.org/ILobby/SendInvitesResponse")]
        void SendInvites(UnoClient.proxy.Player[] players);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendInvites", ReplyAction="http://tempuri.org/ILobby/SendInvitesResponse")]
        System.Threading.Tasks.Task SendInvitesAsync(UnoClient.proxy.Player[] players);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/AnswerInvite", ReplyAction="http://tempuri.org/ILobby/AnswerInviteResponse")]
        void AnswerInvite(bool answer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/AnswerInvite", ReplyAction="http://tempuri.org/ILobby/AnswerInviteResponse")]
        System.Threading.Tasks.Task AnswerInviteAsync(bool answer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/StartGame", ReplyAction="http://tempuri.org/ILobby/StartGameResponse")]
        void StartGame();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/StartGame", ReplyAction="http://tempuri.org/ILobby/StartGameResponse")]
        System.Threading.Tasks.Task StartGameAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendMessageLobby", ReplyAction="http://tempuri.org/ILobby/SendMessageLobbyResponse")]
        void SendMessageLobby(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendMessageLobby", ReplyAction="http://tempuri.org/ILobby/SendMessageLobbyResponse")]
        System.Threading.Tasks.Task SendMessageLobbyAsync(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyChannel : UnoClient.proxy.ILobby, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LobbyClient : System.ServiceModel.ClientBase<UnoClient.proxy.ILobby>, UnoClient.proxy.ILobby {
        
        public LobbyClient() {
        }
        
        public LobbyClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LobbyClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LobbyClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LobbyClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public UnoClient.proxy.Player[] GetOnlineList() {
            return base.Channel.GetOnlineList();
        }
        
        public System.Threading.Tasks.Task<UnoClient.proxy.Player[]> GetOnlineListAsync() {
            return base.Channel.GetOnlineListAsync();
        }
        
        public void CreateParty() {
            base.Channel.CreateParty();
        }
        
        public System.Threading.Tasks.Task CreatePartyAsync() {
            return base.Channel.CreatePartyAsync();
        }
        
        public void LeaveParty() {
            base.Channel.LeaveParty();
        }
        
        public System.Threading.Tasks.Task LeavePartyAsync() {
            return base.Channel.LeavePartyAsync();
        }
        
        public void SendInvites(UnoClient.proxy.Player[] players) {
            base.Channel.SendInvites(players);
        }
        
        public System.Threading.Tasks.Task SendInvitesAsync(UnoClient.proxy.Player[] players) {
            return base.Channel.SendInvitesAsync(players);
        }
        
        public void AnswerInvite(bool answer) {
            base.Channel.AnswerInvite(answer);
        }
        
        public System.Threading.Tasks.Task AnswerInviteAsync(bool answer) {
            return base.Channel.AnswerInviteAsync(answer);
        }
        
        public void StartGame() {
            base.Channel.StartGame();
        }
        
        public System.Threading.Tasks.Task StartGameAsync() {
            return base.Channel.StartGameAsync();
        }
        
        public void SendMessageLobby(string message) {
            base.Channel.SendMessageLobby(message);
        }
        
        public System.Threading.Tasks.Task SendMessageLobbyAsync(string message) {
            return base.Channel.SendMessageLobbyAsync(message);
        }
    }
}
