﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnoClient.service {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StatusCode", Namespace="http://schemas.datacontract.org/2004/07/UNOService")]
    [System.SerializableAttribute()]
    public partial struct StatusCode : System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Code {
            get {
                return this.CodeField;
            }
            set {
                if ((this.CodeField.Equals(value) != true)) {
                    this.CodeField = value;
                    this.RaisePropertyChanged("Code");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        void RaisePropertyChanged(string propertyName) {
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
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UnoClient.service.PlayerState StateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
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
        public UnoClient.service.PlayerState State {
            get {
                return this.StateField;
            }
            set {
                if ((this.StateField.Equals(value) != true)) {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PlayerState", Namespace="http://schemas.datacontract.org/2004/07/UNOService.Game")]
    public enum PlayerState : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InLobby = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InGame = 1,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Party", Namespace="http://schemas.datacontract.org/2004/07/UNOService")]
    [System.SerializableAttribute()]
    public partial class Party : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UnoClient.service.Player HostField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PartyIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UnoClient.service.Player[] PlayersField;
        
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
        public UnoClient.service.Player Host {
            get {
                return this.HostField;
            }
            set {
                if ((object.ReferenceEquals(this.HostField, value) != true)) {
                    this.HostField = value;
                    this.RaisePropertyChanged("Host");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PartyID {
            get {
                return this.PartyIDField;
            }
            set {
                if ((this.PartyIDField.Equals(value) != true)) {
                    this.PartyIDField = value;
                    this.RaisePropertyChanged("PartyID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public UnoClient.service.Player[] Players {
            get {
                return this.PlayersField;
            }
            set {
                if ((object.ReferenceEquals(this.PlayersField, value) != true)) {
                    this.PlayersField = value;
                    this.RaisePropertyChanged("Players");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Card", Namespace="http://schemas.datacontract.org/2004/07/UNOService")]
    [System.SerializableAttribute()]
    public partial class Card : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="service.ILoginAndSignUp")]
    public interface ILoginAndSignUp {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/Login", ReplyAction="http://tempuri.org/ILoginAndSignUp/LoginResponse")]
        UnoClient.service.StatusCode Login(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/Login", ReplyAction="http://tempuri.org/ILoginAndSignUp/LoginResponse")]
        System.Threading.Tasks.Task<UnoClient.service.StatusCode> LoginAsync(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/SignUp", ReplyAction="http://tempuri.org/ILoginAndSignUp/SignUpResponse")]
        UnoClient.service.StatusCode SignUp(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/SignUp", ReplyAction="http://tempuri.org/ILoginAndSignUp/SignUpResponse")]
        System.Threading.Tasks.Task<UnoClient.service.StatusCode> SignUpAsync(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/CheckUserName", ReplyAction="http://tempuri.org/ILoginAndSignUp/CheckUserNameResponse")]
        bool CheckUserName(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/CheckUserName", ReplyAction="http://tempuri.org/ILoginAndSignUp/CheckUserNameResponse")]
        System.Threading.Tasks.Task<bool> CheckUserNameAsync(string userName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoginAndSignUpChannel : UnoClient.service.ILoginAndSignUp, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoginAndSignUpClient : System.ServiceModel.ClientBase<UnoClient.service.ILoginAndSignUp>, UnoClient.service.ILoginAndSignUp {
        
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
        
        public UnoClient.service.StatusCode Login(string userName, string password) {
            return base.Channel.Login(userName, password);
        }
        
        public System.Threading.Tasks.Task<UnoClient.service.StatusCode> LoginAsync(string userName, string password) {
            return base.Channel.LoginAsync(userName, password);
        }
        
        public UnoClient.service.StatusCode SignUp(string userName, string password) {
            return base.Channel.SignUp(userName, password);
        }
        
        public System.Threading.Tasks.Task<UnoClient.service.StatusCode> SignUpAsync(string userName, string password) {
            return base.Channel.SignUpAsync(userName, password);
        }
        
        public bool CheckUserName(string userName) {
            return base.Channel.CheckUserName(userName);
        }
        
        public System.Threading.Tasks.Task<bool> CheckUserNameAsync(string userName) {
            return base.Channel.CheckUserNameAsync(userName);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="service.ILobby", CallbackContract=typeof(UnoClient.service.ILobbyCallback))]
    public interface ILobby {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetOnlineList", ReplyAction="http://tempuri.org/ILobby/GetOnlineListResponse")]
        UnoClient.service.Player[] GetOnlineList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetOnlineList", ReplyAction="http://tempuri.org/ILobby/GetOnlineListResponse")]
        System.Threading.Tasks.Task<UnoClient.service.Player[]> GetOnlineListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/getPlayerFromLobbyContext", ReplyAction="http://tempuri.org/ILobby/getPlayerFromLobbyContextResponse")]
        UnoClient.service.Player getPlayerFromLobbyContext();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/getPlayerFromLobbyContext", ReplyAction="http://tempuri.org/ILobby/getPlayerFromLobbyContextResponse")]
        System.Threading.Tasks.Task<UnoClient.service.Player> getPlayerFromLobbyContextAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/CreateParty", ReplyAction="http://tempuri.org/ILobby/CreatePartyResponse")]
        UnoClient.service.Party CreateParty();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/CreateParty", ReplyAction="http://tempuri.org/ILobby/CreatePartyResponse")]
        System.Threading.Tasks.Task<UnoClient.service.Party> CreatePartyAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/LeaveParty", ReplyAction="http://tempuri.org/ILobby/LeavePartyResponse")]
        void LeaveParty(string host);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/LeaveParty", ReplyAction="http://tempuri.org/ILobby/LeavePartyResponse")]
        System.Threading.Tasks.Task LeavePartyAsync(string host);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendInvites", ReplyAction="http://tempuri.org/ILobby/SendInvitesResponse")]
        void SendInvites(UnoClient.service.Party p, string[] playerNames);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendInvites", ReplyAction="http://tempuri.org/ILobby/SendInvitesResponse")]
        System.Threading.Tasks.Task SendInvitesAsync(UnoClient.service.Party p, string[] playerNames);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/AnswerInvite", ReplyAction="http://tempuri.org/ILobby/AnswerInviteResponse")]
        bool AnswerInvite(UnoClient.service.Party p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/AnswerInvite", ReplyAction="http://tempuri.org/ILobby/AnswerInviteResponse")]
        System.Threading.Tasks.Task<bool> AnswerInviteAsync(UnoClient.service.Party p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/StartGame", ReplyAction="http://tempuri.org/ILobby/StartGameResponse")]
        void StartGame(string host);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/StartGame", ReplyAction="http://tempuri.org/ILobby/StartGameResponse")]
        System.Threading.Tasks.Task StartGameAsync(string host);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendMessageParty", ReplyAction="http://tempuri.org/ILobby/SendMessagePartyResponse")]
        void SendMessageParty(string message, string host);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendMessageParty", ReplyAction="http://tempuri.org/ILobby/SendMessagePartyResponse")]
        System.Threading.Tasks.Task SendMessagePartyAsync(string message, string host);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/getPlayerFromName", ReplyAction="http://tempuri.org/ILobby/getPlayerFromNameResponse")]
        UnoClient.service.Player getPlayerFromName(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/getPlayerFromName", ReplyAction="http://tempuri.org/ILobby/getPlayerFromNameResponse")]
        System.Threading.Tasks.Task<UnoClient.service.Player> getPlayerFromNameAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SubscribeToLobbyEvents", ReplyAction="http://tempuri.org/ILobby/SubscribeToLobbyEventsResponse")]
        void SubscribeToLobbyEvents(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SubscribeToLobbyEvents", ReplyAction="http://tempuri.org/ILobby/SubscribeToLobbyEventsResponse")]
        System.Threading.Tasks.Task SubscribeToLobbyEventsAsync(string username, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/PlayerConnected")]
        void PlayerConnected(UnoClient.service.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/PlayerDisconnected")]
        void PlayerDisconnected(UnoClient.service.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/PlayerLeftParty")]
        void PlayerLeftParty(UnoClient.service.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/PlayerAddedToParty")]
        void PlayerAddedToParty(string playerName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/ReceiveInvite")]
        void ReceiveInvite(UnoClient.service.Party p);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/PartyIsFull")]
        void PartyIsFull();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/ChangePlayerState", ReplyAction="http://tempuri.org/ILobby/ChangePlayerStateResponse")]
        void ChangePlayerState(UnoClient.service.Player player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendChatMessageLobbyCallback", ReplyAction="http://tempuri.org/ILobby/SendChatMessageLobbyCallbackResponse")]
        void SendChatMessageLobbyCallback(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/NotifyGameStarted", ReplyAction="http://tempuri.org/ILobby/NotifyGameStartedResponse")]
        void NotifyGameStarted(string PartyID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyChannel : UnoClient.service.ILobby, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LobbyClient : System.ServiceModel.DuplexClientBase<UnoClient.service.ILobby>, UnoClient.service.ILobby {
        
        public LobbyClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public LobbyClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public LobbyClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LobbyClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LobbyClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public UnoClient.service.Player[] GetOnlineList() {
            return base.Channel.GetOnlineList();
        }
        
        public System.Threading.Tasks.Task<UnoClient.service.Player[]> GetOnlineListAsync() {
            return base.Channel.GetOnlineListAsync();
        }
        
        public UnoClient.service.Player getPlayerFromLobbyContext() {
            return base.Channel.getPlayerFromLobbyContext();
        }
        
        public System.Threading.Tasks.Task<UnoClient.service.Player> getPlayerFromLobbyContextAsync() {
            return base.Channel.getPlayerFromLobbyContextAsync();
        }
        
        public UnoClient.service.Party CreateParty() {
            return base.Channel.CreateParty();
        }
        
        public System.Threading.Tasks.Task<UnoClient.service.Party> CreatePartyAsync() {
            return base.Channel.CreatePartyAsync();
        }
        
        public void LeaveParty(string host) {
            base.Channel.LeaveParty(host);
        }
        
        public System.Threading.Tasks.Task LeavePartyAsync(string host) {
            return base.Channel.LeavePartyAsync(host);
        }
        
        public void SendInvites(UnoClient.service.Party p, string[] playerNames) {
            base.Channel.SendInvites(p, playerNames);
        }
        
        public System.Threading.Tasks.Task SendInvitesAsync(UnoClient.service.Party p, string[] playerNames) {
            return base.Channel.SendInvitesAsync(p, playerNames);
        }
        
        public bool AnswerInvite(UnoClient.service.Party p) {
            return base.Channel.AnswerInvite(p);
        }
        
        public System.Threading.Tasks.Task<bool> AnswerInviteAsync(UnoClient.service.Party p) {
            return base.Channel.AnswerInviteAsync(p);
        }
        
        public void StartGame(string host) {
            base.Channel.StartGame(host);
        }
        
        public System.Threading.Tasks.Task StartGameAsync(string host) {
            return base.Channel.StartGameAsync(host);
        }
        
        public void SendMessageParty(string message, string host) {
            base.Channel.SendMessageParty(message, host);
        }
        
        public System.Threading.Tasks.Task SendMessagePartyAsync(string message, string host) {
            return base.Channel.SendMessagePartyAsync(message, host);
        }
        
        public UnoClient.service.Player getPlayerFromName(string username) {
            return base.Channel.getPlayerFromName(username);
        }
        
        public System.Threading.Tasks.Task<UnoClient.service.Player> getPlayerFromNameAsync(string username) {
            return base.Channel.getPlayerFromNameAsync(username);
        }
        
        public void SubscribeToLobbyEvents(string username, string password) {
            base.Channel.SubscribeToLobbyEvents(username, password);
        }
        
        public System.Threading.Tasks.Task SubscribeToLobbyEventsAsync(string username, string password) {
            return base.Channel.SubscribeToLobbyEventsAsync(username, password);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="service.IGame", CallbackContract=typeof(UnoClient.service.IGameCallback))]
    public interface IGame {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SaveReplay", ReplyAction="http://tempuri.org/IGame/SaveReplayResponse")]
        void SaveReplay(int gameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SaveReplay", ReplyAction="http://tempuri.org/IGame/SaveReplayResponse")]
        System.Threading.Tasks.Task SaveReplayAsync(int gameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/takeCard", ReplyAction="http://tempuri.org/IGame/takeCardResponse")]
        UnoClient.service.Card takeCard(int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/takeCard", ReplyAction="http://tempuri.org/IGame/takeCardResponse")]
        System.Threading.Tasks.Task<UnoClient.service.Card> takeCardAsync(int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/playCard", ReplyAction="http://tempuri.org/IGame/playCardResponse")]
        void playCard(int GameID, UnoClient.service.Card card);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/playCard", ReplyAction="http://tempuri.org/IGame/playCardResponse")]
        System.Threading.Tasks.Task playCardAsync(int GameID, UnoClient.service.Card card);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/SendMessageGame")]
        void SendMessageGame(string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/SendMessageGame")]
        System.Threading.Tasks.Task SendMessageGameAsync(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SubscribeToGameEvents", ReplyAction="http://tempuri.org/IGame/SubscribeToGameEventsResponse")]
        void SubscribeToGameEvents(string UserName, int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SubscribeToGameEvents", ReplyAction="http://tempuri.org/IGame/SubscribeToGameEventsResponse")]
        System.Threading.Tasks.Task SubscribeToGameEventsAsync(string UserName, int GameID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/CardsAssigned", ReplyAction="http://tempuri.org/IGame/CardsAssignedResponse")]
        void CardsAssigned(UnoClient.service.Card[] cards);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/TurnChanged", ReplyAction="http://tempuri.org/IGame/TurnChangedResponse")]
        void TurnChanged(UnoClient.service.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/SendMessageGameCallback")]
        void SendMessageGameCallback(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/NotifyOpponentsOfPlayerPunished", ReplyAction="http://tempuri.org/IGame/NotifyOpponentsOfPlayerPunishedResponse")]
        void NotifyOpponentsOfPlayerPunished(string userName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameChannel : UnoClient.service.IGame, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GameClient : System.ServiceModel.DuplexClientBase<UnoClient.service.IGame>, UnoClient.service.IGame {
        
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
        
        public UnoClient.service.Card takeCard(int GameID) {
            return base.Channel.takeCard(GameID);
        }
        
        public System.Threading.Tasks.Task<UnoClient.service.Card> takeCardAsync(int GameID) {
            return base.Channel.takeCardAsync(GameID);
        }
        
        public void playCard(int GameID, UnoClient.service.Card card) {
            base.Channel.playCard(GameID, card);
        }
        
        public System.Threading.Tasks.Task playCardAsync(int GameID, UnoClient.service.Card card) {
            return base.Channel.playCardAsync(GameID, card);
        }
        
        public void SendMessageGame(string message) {
            base.Channel.SendMessageGame(message);
        }
        
        public System.Threading.Tasks.Task SendMessageGameAsync(string message) {
            return base.Channel.SendMessageGameAsync(message);
        }
        
        public void SubscribeToGameEvents(string UserName, int GameID) {
            base.Channel.SubscribeToGameEvents(UserName, GameID);
        }
        
        public System.Threading.Tasks.Task SubscribeToGameEventsAsync(string UserName, int GameID) {
            return base.Channel.SubscribeToGameEventsAsync(UserName, GameID);
        }
    }
}
