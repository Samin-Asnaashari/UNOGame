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
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StatusCode", Namespace="http://schemas.datacontract.org/2004/07/UNOService")]
    public enum StatusCode : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        LOGIN_INCORRECT = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        LOGIN_ALREADY = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        REGISTER_USERNAME_TAKEN = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        REGISTER_PASSWORD_TOO_SHORT = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UNKOWN_ERROR = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SUCCESS = 5,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Player", Namespace="http://schemas.datacontract.org/2004/07/UNOService")]
    [System.SerializableAttribute()]
    public partial class Player : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UnoClient.proxy.PlayerState StateField;
        
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
        public UnoClient.proxy.PlayerState State {
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
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InAfterGame = 2,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Card", Namespace="http://schemas.datacontract.org/2004/07/UNOService")]
    [System.SerializableAttribute()]
    public partial class Card : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UnoClient.proxy.CardColor ColorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UnoClient.proxy.CardType TypeField;
        
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
        public UnoClient.proxy.CardColor Color {
            get {
                return this.ColorField;
            }
            set {
                if ((this.ColorField.Equals(value) != true)) {
                    this.ColorField = value;
                    this.RaisePropertyChanged("Color");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Number {
            get {
                return this.NumberField;
            }
            set {
                if ((this.NumberField.Equals(value) != true)) {
                    this.NumberField = value;
                    this.RaisePropertyChanged("Number");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public UnoClient.proxy.CardType Type {
            get {
                return this.TypeField;
            }
            set {
                if ((this.TypeField.Equals(value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="CardColor", Namespace="http://schemas.datacontract.org/2004/07/UNOService.Game")]
    public enum CardColor : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Red = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Green = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Blue = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Yellow = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CardType", Namespace="http://schemas.datacontract.org/2004/07/UNOService.Game")]
    public enum CardType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        normal = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        reverse = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        skip = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        draw2 = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        wild = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        draw4Wild = 5,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="proxy.ILoginAndSignUp")]
    public interface ILoginAndSignUp {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/Login", ReplyAction="http://tempuri.org/ILoginAndSignUp/LoginResponse")]
        UnoClient.proxy.StatusCode Login(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/Login", ReplyAction="http://tempuri.org/ILoginAndSignUp/LoginResponse")]
        System.Threading.Tasks.Task<UnoClient.proxy.StatusCode> LoginAsync(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/SignUp", ReplyAction="http://tempuri.org/ILoginAndSignUp/SignUpResponse")]
        UnoClient.proxy.StatusCode SignUp(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/SignUp", ReplyAction="http://tempuri.org/ILoginAndSignUp/SignUpResponse")]
        System.Threading.Tasks.Task<UnoClient.proxy.StatusCode> SignUpAsync(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/CheckUserName", ReplyAction="http://tempuri.org/ILoginAndSignUp/CheckUserNameResponse")]
        bool CheckUserName(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginAndSignUp/CheckUserName", ReplyAction="http://tempuri.org/ILoginAndSignUp/CheckUserNameResponse")]
        System.Threading.Tasks.Task<bool> CheckUserNameAsync(string userName);
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
        
        public UnoClient.proxy.StatusCode Login(string userName, string password) {
            return base.Channel.Login(userName, password);
        }
        
        public System.Threading.Tasks.Task<UnoClient.proxy.StatusCode> LoginAsync(string userName, string password) {
            return base.Channel.LoginAsync(userName, password);
        }
        
        public UnoClient.proxy.StatusCode SignUp(string userName, string password) {
            return base.Channel.SignUp(userName, password);
        }
        
        public System.Threading.Tasks.Task<UnoClient.proxy.StatusCode> SignUpAsync(string userName, string password) {
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="proxy.ILobby", CallbackContract=typeof(UnoClient.proxy.ILobbyCallback))]
    public interface ILobby {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetOnlineList", ReplyAction="http://tempuri.org/ILobby/GetOnlineListResponse")]
        System.Collections.Generic.List<UnoClient.proxy.Player> GetOnlineList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetOnlineList", ReplyAction="http://tempuri.org/ILobby/GetOnlineListResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<UnoClient.proxy.Player>> GetOnlineListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/CreateParty", ReplyAction="http://tempuri.org/ILobby/CreatePartyResponse")]
        void CreateParty();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/CreateParty", ReplyAction="http://tempuri.org/ILobby/CreatePartyResponse")]
        System.Threading.Tasks.Task CreatePartyAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/LeaveParty", ReplyAction="http://tempuri.org/ILobby/LeavePartyResponse")]
        void LeaveParty();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/LeaveParty", ReplyAction="http://tempuri.org/ILobby/LeavePartyResponse")]
        System.Threading.Tasks.Task LeavePartyAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendInvites", ReplyAction="http://tempuri.org/ILobby/SendInvitesResponse")]
        void SendInvites(System.Collections.Generic.List<string> playerNames);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendInvites", ReplyAction="http://tempuri.org/ILobby/SendInvitesResponse")]
        System.Threading.Tasks.Task SendInvitesAsync(System.Collections.Generic.List<string> playerNames);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/AnswerInvite", ReplyAction="http://tempuri.org/ILobby/AnswerInviteResponse")]
        bool AnswerInvite(string inviteSender);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/AnswerInvite", ReplyAction="http://tempuri.org/ILobby/AnswerInviteResponse")]
        System.Threading.Tasks.Task<bool> AnswerInviteAsync(string inviteSender);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/StartGame", ReplyAction="http://tempuri.org/ILobby/StartGameResponse")]
        void StartGame();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/StartGame", ReplyAction="http://tempuri.org/ILobby/StartGameResponse")]
        System.Threading.Tasks.Task StartGameAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendMessageParty", ReplyAction="http://tempuri.org/ILobby/SendMessagePartyResponse")]
        void SendMessageParty(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendMessageParty", ReplyAction="http://tempuri.org/ILobby/SendMessagePartyResponse")]
        System.Threading.Tasks.Task SendMessagePartyAsync(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetPartyMembers", ReplyAction="http://tempuri.org/ILobby/GetPartyMembersResponse")]
        System.Collections.Generic.List<UnoClient.proxy.Player> GetPartyMembers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetPartyMembers", ReplyAction="http://tempuri.org/ILobby/GetPartyMembersResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<UnoClient.proxy.Player>> GetPartyMembersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SubscribeToLobbyEvents", ReplyAction="http://tempuri.org/ILobby/SubscribeToLobbyEventsResponse")]
        void SubscribeToLobbyEvents(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SubscribeToLobbyEvents", ReplyAction="http://tempuri.org/ILobby/SubscribeToLobbyEventsResponse")]
        System.Threading.Tasks.Task SubscribeToLobbyEventsAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/StartTheReplay", ReplyAction="http://tempuri.org/ILobby/StartTheReplayResponse")]
        void StartTheReplay(int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/StartTheReplay", ReplyAction="http://tempuri.org/ILobby/StartTheReplayResponse")]
        System.Threading.Tasks.Task StartTheReplayAsync(int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetSavedGmes", ReplyAction="http://tempuri.org/ILobby/GetSavedGmesResponse")]
        System.Collections.Generic.List<int> GetSavedGmes(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetSavedGmes", ReplyAction="http://tempuri.org/ILobby/GetSavedGmesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<int>> GetSavedGmesAsync(string username);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/PlayerConnected")]
        void PlayerConnected(UnoClient.proxy.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/PlayerDisconnected")]
        void PlayerDisconnected(UnoClient.proxy.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/PlayerLeftParty")]
        void PlayerLeftParty(UnoClient.proxy.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/PlayerAddedToParty")]
        void PlayerAddedToParty(string playerName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/ReceiveInvite")]
        void ReceiveInvite(string hostName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/PartyIsFull")]
        void PartyIsFull();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/ChangePlayerState")]
        void ChangePlayerState(UnoClient.proxy.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/SendChatMessageLobbyCallback")]
        void SendChatMessageLobbyCallback(string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/NotifyGameStarted")]
        void NotifyGameStarted();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/NotifyRePlayGameStarted")]
        void NotifyRePlayGameStarted();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyChannel : UnoClient.proxy.ILobby, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LobbyClient : System.ServiceModel.DuplexClientBase<UnoClient.proxy.ILobby>, UnoClient.proxy.ILobby {
        
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
        
        public System.Collections.Generic.List<UnoClient.proxy.Player> GetOnlineList() {
            return base.Channel.GetOnlineList();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<UnoClient.proxy.Player>> GetOnlineListAsync() {
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
        
        public void SendInvites(System.Collections.Generic.List<string> playerNames) {
            base.Channel.SendInvites(playerNames);
        }
        
        public System.Threading.Tasks.Task SendInvitesAsync(System.Collections.Generic.List<string> playerNames) {
            return base.Channel.SendInvitesAsync(playerNames);
        }
        
        public bool AnswerInvite(string inviteSender) {
            return base.Channel.AnswerInvite(inviteSender);
        }
        
        public System.Threading.Tasks.Task<bool> AnswerInviteAsync(string inviteSender) {
            return base.Channel.AnswerInviteAsync(inviteSender);
        }
        
        public void StartGame() {
            base.Channel.StartGame();
        }
        
        public System.Threading.Tasks.Task StartGameAsync() {
            return base.Channel.StartGameAsync();
        }
        
        public void SendMessageParty(string message) {
            base.Channel.SendMessageParty(message);
        }
        
        public System.Threading.Tasks.Task SendMessagePartyAsync(string message) {
            return base.Channel.SendMessagePartyAsync(message);
        }
        
        public System.Collections.Generic.List<UnoClient.proxy.Player> GetPartyMembers() {
            return base.Channel.GetPartyMembers();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<UnoClient.proxy.Player>> GetPartyMembersAsync() {
            return base.Channel.GetPartyMembersAsync();
        }
        
        public void SubscribeToLobbyEvents(string username, string password) {
            base.Channel.SubscribeToLobbyEvents(username, password);
        }
        
        public System.Threading.Tasks.Task SubscribeToLobbyEventsAsync(string username, string password) {
            return base.Channel.SubscribeToLobbyEventsAsync(username, password);
        }
        
        public void StartTheReplay(int GameID) {
            base.Channel.StartTheReplay(GameID);
        }
        
        public System.Threading.Tasks.Task StartTheReplayAsync(int GameID) {
            return base.Channel.StartTheReplayAsync(GameID);
        }
        
        public System.Collections.Generic.List<int> GetSavedGmes(string username) {
            return base.Channel.GetSavedGmes(username);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<int>> GetSavedGmesAsync(string username) {
            return base.Channel.GetSavedGmesAsync(username);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="proxy.IGame", CallbackContract=typeof(UnoClient.proxy.IGameCallback))]
    public interface IGame {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SaveReplay", ReplyAction="http://tempuri.org/IGame/SaveReplayResponse")]
        void SaveReplay();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SaveReplay", ReplyAction="http://tempuri.org/IGame/SaveReplayResponse")]
        System.Threading.Tasks.Task SaveReplayAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/TakeCards")]
        void TakeCards();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/TakeCards")]
        System.Threading.Tasks.Task TakeCardsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/TryPlayCard", ReplyAction="http://tempuri.org/IGame/TryPlayCardResponse")]
        bool TryPlayCard(UnoClient.proxy.Card card);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/TryPlayCard", ReplyAction="http://tempuri.org/IGame/TryPlayCardResponse")]
        System.Threading.Tasks.Task<bool> TryPlayCardAsync(UnoClient.proxy.Card card);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/SendMessageGame")]
        void SendMessageGame(string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/SendMessageGame")]
        System.Threading.Tasks.Task SendMessageGameAsync(string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/SubscribeToGameEvents")]
        void SubscribeToGameEvents(string UserName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/SubscribeToGameEvents")]
        System.Threading.Tasks.Task SubscribeToGameEventsAsync(string UserName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/EndGame")]
        void EndGame();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/EndGame")]
        System.Threading.Tasks.Task EndGameAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/ChooseNotToPlayCard")]
        void ChooseNotToPlayCard();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/ChooseNotToPlayCard")]
        System.Threading.Tasks.Task ChooseNotToPlayCardAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/AssignCards")]
        void AssignCards(System.Collections.Generic.List<UnoClient.proxy.Card> cards);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/InitializeGame")]
        void InitializeGame(System.Collections.Generic.List<string> playersUserNames);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/NotifyPlayersNumberOfCardsTaken")]
        void NotifyPlayersNumberOfCardsTaken(int nrOfCardsTaken, string playerWhoTookCardsUserName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/CardPlayed")]
        void CardPlayed(UnoClient.proxy.Card c, string playerWhoPlayed);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/SendMessageGameCallback")]
        void SendMessageGameCallback(string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/EndOfTheGame")]
        void EndOfTheGame(string winner);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/SetActivePlayer")]
        void SetActivePlayer();
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
        
        public void SaveReplay() {
            base.Channel.SaveReplay();
        }
        
        public System.Threading.Tasks.Task SaveReplayAsync() {
            return base.Channel.SaveReplayAsync();
        }
        
        public void TakeCards() {
            base.Channel.TakeCards();
        }
        
        public System.Threading.Tasks.Task TakeCardsAsync() {
            return base.Channel.TakeCardsAsync();
        }
        
        public bool TryPlayCard(UnoClient.proxy.Card card) {
            return base.Channel.TryPlayCard(card);
        }
        
        public System.Threading.Tasks.Task<bool> TryPlayCardAsync(UnoClient.proxy.Card card) {
            return base.Channel.TryPlayCardAsync(card);
        }
        
        public void SendMessageGame(string message) {
            base.Channel.SendMessageGame(message);
        }
        
        public System.Threading.Tasks.Task SendMessageGameAsync(string message) {
            return base.Channel.SendMessageGameAsync(message);
        }
        
        public void SubscribeToGameEvents(string UserName) {
            base.Channel.SubscribeToGameEvents(UserName);
        }
        
        public System.Threading.Tasks.Task SubscribeToGameEventsAsync(string UserName) {
            return base.Channel.SubscribeToGameEventsAsync(UserName);
        }
        
        public void EndGame() {
            base.Channel.EndGame();
        }
        
        public System.Threading.Tasks.Task EndGameAsync() {
            return base.Channel.EndGameAsync();
        }
        
        public void ChooseNotToPlayCard() {
            base.Channel.ChooseNotToPlayCard();
        }
        
        public System.Threading.Tasks.Task ChooseNotToPlayCardAsync() {
            return base.Channel.ChooseNotToPlayCardAsync();
        }
    }
}
