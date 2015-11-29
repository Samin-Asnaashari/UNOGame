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
        UnoClient.proxy.Player[] GetOnlineList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetOnlineList", ReplyAction="http://tempuri.org/ILobby/GetOnlineListResponse")]
        System.Threading.Tasks.Task<UnoClient.proxy.Player[]> GetOnlineListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/CreateParty", ReplyAction="http://tempuri.org/ILobby/CreatePartyResponse")]
        void CreateParty(string partyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/CreateParty", ReplyAction="http://tempuri.org/ILobby/CreatePartyResponse")]
        System.Threading.Tasks.Task CreatePartyAsync(string partyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/LeaveParty", ReplyAction="http://tempuri.org/ILobby/LeavePartyResponse")]
        void LeaveParty(string partyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/LeaveParty", ReplyAction="http://tempuri.org/ILobby/LeavePartyResponse")]
        System.Threading.Tasks.Task LeavePartyAsync(string partyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendInvites", ReplyAction="http://tempuri.org/ILobby/SendInvitesResponse")]
        void SendInvites(UnoClient.proxy.Player[] players);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendInvites", ReplyAction="http://tempuri.org/ILobby/SendInvitesResponse")]
        System.Threading.Tasks.Task SendInvitesAsync(UnoClient.proxy.Player[] players);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/AnswerInvite", ReplyAction="http://tempuri.org/ILobby/AnswerInviteResponse")]
        bool AnswerInvite(bool answer, string partyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/AnswerInvite", ReplyAction="http://tempuri.org/ILobby/AnswerInviteResponse")]
        System.Threading.Tasks.Task<bool> AnswerInviteAsync(bool answer, string partyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/StartGame", ReplyAction="http://tempuri.org/ILobby/StartGameResponse")]
        void StartGame(int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/StartGame", ReplyAction="http://tempuri.org/ILobby/StartGameResponse")]
        System.Threading.Tasks.Task StartGameAsync(int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendMessageParty", ReplyAction="http://tempuri.org/ILobby/SendMessagePartyResponse")]
        void SendMessageParty(string message, string partyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendMessageParty", ReplyAction="http://tempuri.org/ILobby/SendMessagePartyResponse")]
        System.Threading.Tasks.Task SendMessagePartyAsync(string message, string partyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetPartyMembers", ReplyAction="http://tempuri.org/ILobby/GetPartyMembersResponse")]
        UnoClient.proxy.Player[] GetPartyMembers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetPartyMembers", ReplyAction="http://tempuri.org/ILobby/GetPartyMembersResponse")]
        System.Threading.Tasks.Task<UnoClient.proxy.Player[]> GetPartyMembersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SubScribeToLobbyEvents", ReplyAction="http://tempuri.org/ILobby/SubScribeToLobbyEventsResponse")]
        void SubScribeToLobbyEvents(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SubScribeToLobbyEvents", ReplyAction="http://tempuri.org/ILobby/SubScribeToLobbyEventsResponse")]
        System.Threading.Tasks.Task SubScribeToLobbyEventsAsync(string username);
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SentInvite", ReplyAction="http://tempuri.org/ILobby/SentInviteResponse")]
        void SentInvite(string hostName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/PartyIsFull", ReplyAction="http://tempuri.org/ILobby/PartyIsFullResponse")]
        void PartyIsFull();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/NotifyGameStarted", ReplyAction="http://tempuri.org/ILobby/NotifyGameStartedResponse")]
        void NotifyGameStarted(UnoClient.proxy.Player[] players);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendChatMessageLobbyCallback", ReplyAction="http://tempuri.org/ILobby/SendChatMessageLobbyCallbackResponse")]
        void SendChatMessageLobbyCallback(string message);
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
        
        public UnoClient.proxy.Player[] GetOnlineList() {
            return base.Channel.GetOnlineList();
        }
        
        public System.Threading.Tasks.Task<UnoClient.proxy.Player[]> GetOnlineListAsync() {
            return base.Channel.GetOnlineListAsync();
        }
        
        public void CreateParty(string partyID) {
            base.Channel.CreateParty(partyID);
        }
        
        public System.Threading.Tasks.Task CreatePartyAsync(string partyID) {
            return base.Channel.CreatePartyAsync(partyID);
        }
        
        public void LeaveParty(string partyID) {
            base.Channel.LeaveParty(partyID);
        }
        
        public System.Threading.Tasks.Task LeavePartyAsync(string partyID) {
            return base.Channel.LeavePartyAsync(partyID);
        }
        
        public void SendInvites(UnoClient.proxy.Player[] players) {
            base.Channel.SendInvites(players);
        }
        
        public System.Threading.Tasks.Task SendInvitesAsync(UnoClient.proxy.Player[] players) {
            return base.Channel.SendInvitesAsync(players);
        }
        
        public bool AnswerInvite(bool answer, string partyID) {
            return base.Channel.AnswerInvite(answer, partyID);
        }
        
        public System.Threading.Tasks.Task<bool> AnswerInviteAsync(bool answer, string partyID) {
            return base.Channel.AnswerInviteAsync(answer, partyID);
        }
        
        public void StartGame(int GameID) {
            base.Channel.StartGame(GameID);
        }
        
        public System.Threading.Tasks.Task StartGameAsync(int GameID) {
            return base.Channel.StartGameAsync(GameID);
        }
        
        public void SendMessageParty(string message, string partyID) {
            base.Channel.SendMessageParty(message, partyID);
        }
        
        public System.Threading.Tasks.Task SendMessagePartyAsync(string message, string partyID) {
            return base.Channel.SendMessagePartyAsync(message, partyID);
        }
        
        public UnoClient.proxy.Player[] GetPartyMembers() {
            return base.Channel.GetPartyMembers();
        }
        
        public System.Threading.Tasks.Task<UnoClient.proxy.Player[]> GetPartyMembersAsync() {
            return base.Channel.GetPartyMembersAsync();
        }
        
        public void SubScribeToLobbyEvents(string username) {
            base.Channel.SubScribeToLobbyEvents(username);
        }
        
        public System.Threading.Tasks.Task SubScribeToLobbyEventsAsync(string username) {
            return base.Channel.SubScribeToLobbyEventsAsync(username);
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
        UnoClient.proxy.Card takeCard(int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/takeCard", ReplyAction="http://tempuri.org/IGame/takeCardResponse")]
        System.Threading.Tasks.Task<UnoClient.proxy.Card> takeCardAsync(int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/playCard", ReplyAction="http://tempuri.org/IGame/playCardResponse")]
        void playCard(int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/playCard", ReplyAction="http://tempuri.org/IGame/playCardResponse")]
        System.Threading.Tasks.Task playCardAsync(int GameID);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/SendMessageGame")]
        void SendMessageGame(string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/SendMessageGame")]
        System.Threading.Tasks.Task SendMessageGameAsync(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SubscribeToGameEvents", ReplyAction="http://tempuri.org/IGame/SubscribeToGameEventsResponse")]
        void SubscribeToGameEvents(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SubscribeToGameEvents", ReplyAction="http://tempuri.org/IGame/SubscribeToGameEventsResponse")]
        System.Threading.Tasks.Task SubscribeToGameEventsAsync(string username);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/CardsAssigned", ReplyAction="http://tempuri.org/IGame/CardsAssignedResponse")]
        void CardsAssigned(UnoClient.proxy.Card[] cards);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/TurnChanged", ReplyAction="http://tempuri.org/IGame/TurnChangedResponse")]
        void TurnChanged(UnoClient.proxy.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGame/SendMessageGameCallback")]
        void SendMessageGameCallback(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/NotifyOpponentsOfPlayerPunished", ReplyAction="http://tempuri.org/IGame/NotifyOpponentsOfPlayerPunishedResponse")]
        void NotifyOpponentsOfPlayerPunished(string userName);
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
        
        public UnoClient.proxy.Card takeCard(int GameID) {
            return base.Channel.takeCard(GameID);
        }
        
        public System.Threading.Tasks.Task<UnoClient.proxy.Card> takeCardAsync(int GameID) {
            return base.Channel.takeCardAsync(GameID);
        }
        
        public void playCard(int GameID) {
            base.Channel.playCard(GameID);
        }
        
        public System.Threading.Tasks.Task playCardAsync(int GameID) {
            return base.Channel.playCardAsync(GameID);
        }
        
        public void SendMessageGame(string message) {
            base.Channel.SendMessageGame(message);
        }
        
        public System.Threading.Tasks.Task SendMessageGameAsync(string message) {
            return base.Channel.SendMessageGameAsync(message);
        }
        
        public void SubscribeToGameEvents(string username) {
            base.Channel.SubscribeToGameEvents(username);
        }
        
        public System.Threading.Tasks.Task SubscribeToGameEventsAsync(string username) {
            return base.Channel.SubscribeToGameEventsAsync(username);
        }
    }
}
