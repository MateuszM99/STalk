import React, { useState,useEffect } from 'react'
import './style.scss'
import {getUsersRequest,getUsersContactsRequest,getUsersFriendsRequestsRequest,deleteUserFromContactsRequest,acceptAddToContactsRequest,declineAddToContactsRequest,addToContactsRequest} from '../../services/api/ContactsRequests'
import AddFriend from './AddFriend'
import FriendRequest from './FriendRequest'
import Friend from './Friend'
import axios from 'axios'

function FriendsWindow() {

    const [foundUsers,setFoundUsers] = useState(null);
    const [userFindMessage,setUserFindMessage] = useState("");
    const [userSearchString,setUserSearchString] = useState(null);
    const [friends,setFriends] = useState(null);
    const [friendsRequests,setFriendsRequests] = useState(null);

    const getUserSearchString = (e) => {
        setUserSearchString(e.target.value);
    }

    const findUsers = async() => {

        if(userSearchString != null && userSearchString != ""){
            try{
                let response = await getUsersRequest(userSearchString);
                console.log(response.data.users);
                setFoundUsers(response.data.users); 
                setUserFindMessage(response.data.message);
            } catch(err) {
                setUserFindMessage("Invalid input")
            }
        }
    }

    const deleteFromContacts = async(username) => {
        try{
            let response = await deleteUserFromContactsRequest(username);
            
        } catch(err) {
            setUserFindMessage("Invalid input")
        }
        getData();
    }

    const acceptFriendsRequest = async (requestId) => {
        try{
            console.log('Request id: ' + requestId);
            let response = await acceptAddToContactsRequest(requestId); 
        } catch(err) {
            
        }
        getData();
    }

    const declineFriendsRequest = async (requestId) => {
        try{
            let response = await declineAddToContactsRequest(requestId); 
        } catch(err) {
            
        }
        getData();
    }

    const sendFriendsRequest = async (username,status) => {
        console.log(username);
        if(status == 0){
            try{
                let response = await addToContactsRequest(username); 
            } catch(err) {
                
            }
        }
        findUsers();
    }

    async function getData(){
        try{
            let response = await getUsersContactsRequest();
            setFriends(response.data.users); 
            //setUserFindMessage(response.data.message);
            console.log(response.data.users);
        } catch(err) {
            //setUserFindMessage("Invalid input")
        }

        try{
            let response = await getUsersFriendsRequestsRequest();
            console.log(response.data.addToContactRequests);
            setFriendsRequests(response.data.addToContactRequests); 
            //setUserFindMessage(response.data.message);
        } catch(err) {
           // setUserFindMessage("Invalid input")
        }
    }

    useEffect(() => {
        if(friends == null || friendsRequests == null)
        getData();

      },[friendsRequests,friends]);

    return (
        <div className="col-md-10">
            <div>
                <div className="friends__search__friend border-bottom">
                    <h6>Add a friend</h6>
                    <div className="friends__search__friend__input__wrapper">
                    <input name="searchString" onChange={getUserSearchString}></input>
                    <button className="friends__action__button" onClick={findUsers}>Search</button>
                    </div>
                </div>
                <div className="friends__add__friend border-bottom" style={foundUsers != null ? {display:'flex'} : {display: 'none'}}>
                    <p>{userFindMessage}</p>
                    {foundUsers?.map((user => 
                        <AddFriend username={user.username} status={user.userStatus} image={user.profileImage} onSend={sendFriendsRequest}/>
                        ))}
                </div>
                <div className="friends__friend__requests border-bottom">
                    <h6>Friend Requests</h6>
                        {friendsRequests?.map((request =>
                            <FriendRequest key={request.id} id={request.id} username={request.userFrom.username} image={request.userFrom.profileImage} onAccept={acceptFriendsRequest} onDecline={declineAddToContactsRequest}/>
                            ))}
                </div>
                <div className="friends__friends__list border-bottom">
                <h6>Friends</h6>
                    {friends?.map((user => 
                        <Friend  username={user.username} image={user.profileImage} onDelete={deleteFromContacts} />
                        ))}
                </div>
            </div>
        </div>
    )
}

export default FriendsWindow
