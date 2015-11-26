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
        void CreateParty(int partyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/CreateParty", ReplyAction="http://tempuri.org/ILobby/CreatePartyResponse")]
        System.Threading.Tasks.Task CreatePartyAsync(int partyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/LeaveParty", ReplyAction="http://tempuri.org/ILobby/LeavePartyResponse")]
        void LeaveParty(int partyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/LeaveParty", ReplyAction="http://tempuri.org/ILobby/LeavePartyResponse")]
        System.Threading.Tasks.Task LeavePartyAsync(int partyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendInvites", ReplyAction="http://tempuri.org/ILobby/SendInvitesResponse")]
        void SendInvites(UnoClient.proxy.Player[] players);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendInvites", ReplyAction="http://tempuri.org/ILobby/SendInvitesResponse")]
        System.Threading.Tasks.Task SendInvitesAsync(UnoClient.proxy.Player[] players);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/AnswerInvite", ReplyAction="http://tempuri.org/ILobby/AnswerInviteResponse")]
        bool AnswerInvite(bool answer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/AnswerInvite", ReplyAction="http://tempuri.org/ILobby/AnswerInviteResponse")]
        System.Threading.Tasks.Task<bool> AnswerInviteAsync(bool answer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/StartGame", ReplyAction="http://tempuri.org/ILobby/StartGameResponse")]
        void StartGame(int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/StartGame", ReplyAction="http://tempuri.org/ILobby/StartGameResponse")]
        System.Threading.Tasks.Task StartGameAsync(int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendMessageLobby", ReplyAction="http://tempuri.org/ILobby/SendMessageLobbyResponse")]
        void SendMessageLobby(string message, int partyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/SendMessageLobby", ReplyAction="http://tempuri.org/ILobby/SendMessageLobbyResponse")]
        System.Threading.Tasks.Task SendMessageLobbyAsync(string message, int partyID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetPartyMembers", ReplyAction="http://tempuri.org/ILobby/GetPartyMembersResponse")]
        UnoClient.proxy.Player[] GetPartyMembers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILobby/GetPartyMembers", ReplyAction="http://tempuri.org/ILobby/GetPartyMembersResponse")]
        System.Threading.Tasks.Task<UnoClient.proxy.Player[]> GetPartyMembersAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILobbyCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/PlayerConnected")]
        void PlayerConnected(UnoClient.proxy.Player player);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILobby/PlayerDisConnected")]
        void PlayerDisConnected(UnoClient.proxy.Player player);
        
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
        
        public void CreateParty(int partyID) {
            base.Channel.CreateParty(partyID);
        }
        
        public System.Threading.Tasks.Task CreatePartyAsync(int partyID) {
            return base.Channel.CreatePartyAsync(partyID);
        }
        
        public void LeaveParty(int partyID) {
            base.Channel.LeaveParty(partyID);
        }
        
        public System.Threading.Tasks.Task LeavePartyAsync(int partyID) {
            return base.Channel.LeavePartyAsync(partyID);
        }
        
        public void SendInvites(UnoClient.proxy.Player[] players) {
            base.Channel.SendInvites(players);
        }
        
        public System.Threading.Tasks.Task SendInvitesAsync(UnoClient.proxy.Player[] players) {
            return base.Channel.SendInvitesAsync(players);
        }
        
        public bool AnswerInvite(bool answer) {
            return base.Channel.AnswerInvite(answer);
        }
        
        public System.Threading.Tasks.Task<bool> AnswerInviteAsync(bool answer) {
            return base.Channel.AnswerInviteAsync(answer);
        }
        
        public void StartGame(int GameID) {
            base.Channel.StartGame(GameID);
        }
        
        public System.Threading.Tasks.Task StartGameAsync(int GameID) {
            return base.Channel.StartGameAsync(GameID);
        }
        
        public void SendMessageLobby(string message, int partyID) {
            base.Channel.SendMessageLobby(message, partyID);
        }
        
        public System.Threading.Tasks.Task SendMessageLobbyAsync(string message, int partyID) {
            return base.Channel.SendMessageLobbyAsync(message, partyID);
        }
        
        public UnoClient.proxy.Player[] GetPartyMembers() {
            return base.Channel.GetPartyMembers();
        }
        
        public System.Threading.Tasks.Task<UnoClient.proxy.Player[]> GetPartyMembersAsync() {
            return base.Channel.GetPartyMembersAsync();
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/LeaveGame", ReplyAction="http://tempuri.org/IGame/LeaveGameResponse")]
        void LeaveGame(int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/LeaveGame", ReplyAction="http://tempuri.org/IGame/LeaveGameResponse")]
        System.Threading.Tasks.Task LeaveGameAsync(int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SendMessageGame", ReplyAction="http://tempuri.org/IGame/SendMessageGameResponse")]
        void SendMessageGame(string message, int GameID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGame/SendMessageGame", ReplyAction="http://tempuri.org/IGame/SendMessageGameResponse")]
        System.Threading.Tasks.Task SendMessageGameAsync(string message, int GameID);
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
        
        public void LeaveGame(int GameID) {
            base.Channel.LeaveGame(GameID);
        }
        
        public System.Threading.Tasks.Task LeaveGameAsync(int GameID) {
            return base.Channel.LeaveGameAsync(GameID);
        }
        
        public void SendMessageGame(string message, int GameID) {
            base.Channel.SendMessageGame(message, GameID);
        }
        
        public System.Threading.Tasks.Task SendMessageGameAsync(string message, int GameID) {
            return base.Channel.SendMessageGameAsync(message, GameID);
        }
    }
}
